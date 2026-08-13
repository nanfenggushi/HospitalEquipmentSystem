using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI.management
{
    public partial class MaterialForm : UIForm
    {
        private MaterialManager _manager;
        private MaterialManager Manager => _manager ?? (_manager = new MaterialManager());

        private int currentPage = 1;
        private int pageSize = 6;
        private int totalCount = 0;
        private int loadVersion = 0;

        public MaterialForm()
        {
            InitializeComponent();
            this.btnPrevPage.Click += (s, e) => { if (currentPage > 1) { currentPage--; _ = LoadData(); } };
            this.btnNextPage.Click += (s, e) => { int totalPages = (int)Math.Ceiling((double)totalCount / pageSize); if (currentPage < totalPages) { currentPage++; _ = LoadData(); } };
        }

        private async void MaterialForm_Load(object sender, EventArgs e)
        {
            await LoadCategories();
            await LoadData();
        }

        #region 数据加载

        private async Task LoadCategories()
        {
            try
            {
                var dt = HospitalEquipmentSystem.Common.DbHelper.GetDataTable(
                    "SELECT CategoryId, CategoryName FROM EquipmentCategories ORDER BY CategoryName");
                DataRow dr = dt.NewRow();
                dr["CategoryId"] = 0;
                dr["CategoryName"] = "（全部）";
                dt.Rows.InsertAt(dr, 0);
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryId";
                cmbCategory.DataSource = dt;
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"加载分类失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }

        private async Task LoadData()
        {
            try
            {
                int version = ++loadVersion;
                dgvMaterials.AutoGenerateColumns = false;

                if (dgvMaterials.Columns.Contains("colName"))
                    dgvMaterials.Columns["colName"].DataPropertyName = "MaterialName";
                if (dgvMaterials.Columns.Contains("colFaultType"))
                    dgvMaterials.Columns["colFaultType"].DataPropertyName = "FaultType";
                if (dgvMaterials.Columns.Contains("colPrice"))
                    dgvMaterials.Columns["colPrice"].DataPropertyName = "UnitPrice";
                if (dgvMaterials.Columns.Contains("colQty"))
                    dgvMaterials.Columns["colQty"].DataPropertyName = "DefaultQuantity";
                if (dgvMaterials.Columns.Contains("colUnit"))
                    dgvMaterials.Columns["colUnit"].DataPropertyName = "Unit";
                if (dgvMaterials.Columns.Contains("colActive"))
                    dgvMaterials.Columns["colActive"].DataPropertyName = "IsActive";
                if (dgvMaterials.Columns.Contains("colId"))
                {
                    dgvMaterials.Columns["colId"].DataPropertyName = "MaterialId";
                    dgvMaterials.Columns["colId"].Visible = false;
                }

                string keyword = txtSearchName.Text.Trim();
                int selectedCategoryId = cmbCategory.SelectedValue != null ? Convert.ToInt32(cmbCategory.SelectedValue) : 0;

                var result = await Manager.GetPagedMaterials(currentPage, pageSize);
                if (version != loadVersion) return;

                var list = result.list;
                if (!string.IsNullOrWhiteSpace(keyword))
                    list = list.Where(m => m.MaterialName != null && m.MaterialName.Contains(keyword)).ToList();
                if (selectedCategoryId > 0)
                    list = list.Where(m => m.CategoryId == selectedCategoryId).ToList();

                totalCount = list.Count;
                var paged = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                dgvMaterials.DataSource = null;
                dgvMaterials.DataSource = paged;

                UpdatePager();
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"加载物料失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }

        private void UpdatePager()
        {
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            if (totalPages < 1) totalPages = 1;
            lblPageInfo.Text = $"第 {currentPage}/{totalPages} 页 · 每页 {pageSize} 条 · 共 {totalCount} 条";
            btnPrevPage.Enabled = currentPage > 1;
            btnNextPage.Enabled = currentPage < totalPages;
        }

        #endregion

        #region 按钮事件

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            await LoadData();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new MaterialEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await Manager.SaveMaterial(editForm.Material);
                        UIMessageBox.Show("新增成功！", "提示", UIStyle.Green);
                        currentPage = 1;
                        await LoadData();
                    }
                    catch (Exception ex)
                    {
                        UIMessageBox.Show($"保存失败：{ex.Message}", "错误", UIStyle.Red);
                    }
                }
            }
        }

        #endregion

        #region 表格事件

        private void dgvMaterials_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvMaterials.Rows[e.RowIndex];
            if (row.Cells["colId"].Value == null) return;
            int materialId = Convert.ToInt32(row.Cells["colId"].Value);

            if (dgvMaterials.Columns[e.ColumnIndex].Name == "colEdit")
            {
                EditMaterial(materialId);
            }
            else if (dgvMaterials.Columns[e.ColumnIndex].Name == "colToggle")
            {
                ToggleMaterialActive(materialId, row);
            }
            else if (dgvMaterials.Columns[e.ColumnIndex].Name == "colDelete")
            {
                DeleteMaterial(materialId);
            }
        }

        private async void EditMaterial(int materialId)
        {
            try
            {
                var material = await Manager.GetById(materialId);
                if (material == null)
                {
                    UIMessageBox.Show("物料不存在！", "错误", UIStyle.Red);
                    return;
                }

                using (var editForm = new MaterialEditForm(material))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        await Manager.SaveMaterial(editForm.Material);
                        UIMessageBox.Show("保存成功！", "提示", UIStyle.Green);
                        await LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"编辑失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }

        private async void ToggleMaterialActive(int materialId, DataGridViewRow row)
        {
            try
            {
                bool currentActive = Convert.ToBoolean(row.Cells["colActive"].Value);
                var material = await Manager.GetById(materialId);
                if (material == null) return;

                material.IsActive = !currentActive;
                await Manager.SaveMaterial(material);
                UIMessageBox.Show(material.IsActive ? "已启用！" : "已禁用！", "提示", UIStyle.Green);
                await LoadData();
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"操作失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }

        private async void DeleteMaterial(int materialId)
        {
            try
            {
                if (!UIMessageBox.ShowAsk("确认彻底删除该物料吗？\n删除后不可恢复。")) return;

                await Manager.DeleteMaterialPermanent(materialId);
                UIMessageBox.Show("删除成功！", "提示", UIStyle.Green);
                await LoadData();
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"删除失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
