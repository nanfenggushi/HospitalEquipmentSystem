using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SupplierEntity = HospitalEquipment.Model.Supplier;

namespace HospitalEquipmentSystem.UI.management
{
    public partial class Supplier : UIForm
    {
        private SupplierManager manager = new SupplierManager();
        private int? currentSupplierId = null;
        private int currentPage = 1;
        private int pageSize = 6;
        private int totalCount = 0;
        private int loadVersion = 0;
        public Supplier()
        {
            InitializeComponent();
            // 分页按钮事件
            this.btnPrevPage.Click += (s, e) => { if (currentPage > 1) { currentPage--; _ = LoadData(); } };
            this.btnNextPage.Click += (s, e) => { int totalPages = (int)Math.Ceiling((double)totalCount / pageSize); if (currentPage < totalPages) { currentPage++; _ = LoadData(); } };
        }
        private async void Supplier_Load(object sender, EventArgs e)
        {
            // 绑定按钮事件（设计器未绑定，在此统一绑定）
            this.uiButton1.Click += uiButton1_Click;        // 新增
            this.uiButton2.Click += uiButton2_Click;        // 取消
            this.uiButton3.Click += uiButton3_Click;        // 删除
            this.uiButton4.Click += uiButton4_Click;        // 保存
            this.uiButton5.Click += uiButton5_Click;        // 刷新

            // 状态列显示中文（一次性绑定，避免重复添加）
            uiDataGridView1.CellFormatting += (s, ev) =>
            {
                if (ev.ColumnIndex >= 0 && uiDataGridView1.Columns[ev.ColumnIndex].Name == "Column7" && ev.Value != null)
                {
                    if (ev.Value is bool b)
                    {
                        ev.Value = b ? "启用" : "禁用";
                        ev.FormattingApplied = true;
                    }
                }
            };

            await LoadData();
            EnableEdit(false);
            ClearFields();
            uiDataGridView1.ClearSelection();

        }



        #region 数据加载
        private async Task LoadData()
        {
            try
            {
                int version = ++loadVersion;
                // 1. 关闭自动生成列（重要！）
                uiDataGridView1.AutoGenerateColumns = false;

                // 2. 设置设计器中各列的 DataPropertyName（数据绑定字段名）
                if (uiDataGridView1.Columns.Contains("Column1"))
                    uiDataGridView1.Columns["Column1"].DataPropertyName = "SupplierName";
                if (uiDataGridView1.Columns.Contains("Column2"))
                    uiDataGridView1.Columns["Column2"].DataPropertyName = "SupplierCode";
                if (uiDataGridView1.Columns.Contains("Column3"))
                    uiDataGridView1.Columns["Column3"].DataPropertyName = "ContactPerson";
                if (uiDataGridView1.Columns.Contains("Column4"))
                    uiDataGridView1.Columns["Column4"].DataPropertyName = "Phone";
                if (uiDataGridView1.Columns.Contains("Column5"))
                    uiDataGridView1.Columns["Column5"].DataPropertyName = "Email";
                if (uiDataGridView1.Columns.Contains("Column6"))
                    uiDataGridView1.Columns["Column6"].DataPropertyName = "Address";
                if (uiDataGridView1.Columns.Contains("Column7"))
                    uiDataGridView1.Columns["Column7"].DataPropertyName = "IsActive";
                if (uiDataGridView1.Columns.Contains("Column8"))
                {
                    uiDataGridView1.Columns["Column8"].DataPropertyName = "SupplierId";
                    uiDataGridView1.Columns["Column8"].Visible = false; // 隐藏ID列
                }
                string keyword = uiTextBox1.Text.Trim();
                var result = await manager.GetPaged(currentPage, pageSize, keyword);
                if (version != loadVersion) return;

                totalCount = result.total;
                uiDataGridView1.DataSource = null;
                uiDataGridView1.DataSource = result.list;

                // 更新分页信息
                UpdatePager();
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"加载供应商失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }

        /// <summary>
        /// 更新分页控件状态
        /// </summary>
        private void UpdatePager()
        {
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            if (totalPages < 1) totalPages = 1;
            lblPageInfo.Text = $"第 {currentPage}/{totalPages} 页 · 每页 {pageSize} 条 · 共 {totalCount} 条";
            btnPrevPage.Enabled = currentPage > 1;
            btnNextPage.Enabled = currentPage < totalPages;
        }
        #endregion
        #region 编辑区控制
        /// <summary>
        /// 启用或禁用编辑区所有控件
        /// </summary>
        /// <param name="enable">true=启用编辑，false=禁用编辑</param>
        /// <remarks>
        /// 控制包括：所有文本框、复选框、保存/取消/删除按钮
        /// 通常用于：选中行时启用编辑，取消或保存后禁用编辑
        /// </remarks>

        private void EnableEdit(bool enable)
        {
            uiTextBox2.Enabled = enable;
            uiTextBox6.Enabled = enable;
            uiTextBox3.Enabled = enable;
            uiTextBox7.Enabled = enable;
            uiTextBox4.Enabled = enable;
            uiTextBox8.Enabled = enable;
            uiTextBox5.Enabled = enable;
            uiTextBox9.Enabled = enable;
            uiCheckBox1.Enabled = enable;
            uiButton4.Enabled = enable;
            uiButton2.Enabled = enable;
            uiButton3.Enabled = enable;
        }
        /// <summary>
        /// 清空编辑区所有文本框，并将复选框重置为"启用"状态
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        /// <remarks>
        /// 同时将 currentSupplierId 设为 null，表示当前没有选中任何供应商
        ///通常在：取消编辑、刷新列表、删除成功后调用
        ///</remarks>
        private void ClearFields()
        {
            uiTextBox2.Clear();
            uiTextBox6.Clear();
            uiTextBox3.Clear();
            uiTextBox7.Clear();
            uiTextBox4.Clear();
            uiTextBox8.Clear();
            uiTextBox5.Clear();
            uiTextBox9.Clear();
            uiCheckBox1.Checked = true;
            currentSupplierId = null;
        }
        /// <summary>
        /// 将供应商数据加载到编辑区控件中
        /// </summary>
        /// <param name="supplier">要加载的供应商实体对象</param>
        /// <remarks>
        /// 加载完成后会自动调用 EnableEdit(true) 启用编辑区
        /// 对于可能为 null 的字符串字段，使用 ?? "" 防止显示"null"
        /// </remarks>

        private void LoadToEdit(SupplierEntity supplier)
        {
            currentSupplierId = supplier.SupplierId;
            uiTextBox2.Text = supplier.SupplierName;
            uiTextBox6.Text = supplier.SupplierCode ?? "";
            uiTextBox3.Text = supplier.ContactPerson ?? "";
            uiTextBox7.Text = supplier.Phone ?? "";
            uiTextBox4.Text = supplier.Email ?? "";
            uiTextBox8.Text = supplier.Address ?? "";
            uiTextBox5.Text = supplier.Website ?? "";
            uiTextBox9.Text = supplier.Remark ?? "";
            uiCheckBox1.Checked = supplier.IsActive;
            EnableEdit(true);
        }
        #endregion
        #region 表格事件

        /// <summary>
        /// 表格行单击事件：点击行时将供应商数据加载到编辑区
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">包含点击行索引的单元格事件参数</param>
        /// <remarks>
        /// 通过 Column8（隐藏的供应商ID列）获取选中行的ID
        /// 然后调用 LoadToEdit 将数据显示在右侧编辑区
        /// </remarks>
        private async void uiDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                var row = uiDataGridView1.Rows[e.RowIndex];
                if (row.Cells["Column8"].Value == null) return;

                int id = Convert.ToInt32(row.Cells["Column8"].Value);
                var supplier = await manager.GetById(id);
                if (supplier != null)
                    LoadToEdit(supplier);
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"加载供应商信息失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }
        #endregion
        #region 按钮事件
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton1_Click(object sender, EventArgs e)
        {
            ClearFields();
            EnableEdit(true);
            uiTextBox2.Focus();
            currentSupplierId = 0;
            uiDataGridView1.ClearSelection();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiButton4_Click(object sender, EventArgs e)
        {
            if (!currentSupplierId.HasValue)
            {
                UIMessageBox.Show("请先在表格中选择一个供应商或点击'新增'！", "提示", UIStyle.Green);
                return;
            }

            try
            {
                var supplier = new SupplierEntity
                {
                    SupplierId = currentSupplierId.Value,
                    SupplierName = uiTextBox2.Text.Trim(),
                    SupplierCode = uiTextBox6.Text.Trim(),
                    ContactPerson = uiTextBox3.Text.Trim(),
                    Phone = uiTextBox7.Text.Trim(),
                    Email = uiTextBox4.Text.Trim(),
                    Address = uiTextBox8.Text.Trim(),
                    Website = uiTextBox5.Text.Trim(),
                    Remark = uiTextBox9.Text.Trim(),
                    IsActive = uiCheckBox1.Checked
                };

                bool result;
                if (currentSupplierId == 0)
                    result = await manager.Insert(supplier);
                else
                    result = await manager.Update(supplier);

                if (result)
                {
                    UIMessageBox.Show("保存成功！", "提示", UIStyle.Green);
                    currentPage = 1;
                    await LoadData();
                    EnableEdit(false);
                    ClearFields();
                    uiDataGridView1.ClearSelection();
                }
                else
                {
                    UIMessageBox.Show("保存失败，请查看控制台日志。", "错误", UIStyle.Red);
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"保存失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton2_Click(object sender, EventArgs e)
        {
            ClearFields();
            EnableEdit(false);
            uiDataGridView1.ClearSelection();
            currentSupplierId = null;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiButton3_Click(object sender, EventArgs e)
        {
            if (!currentSupplierId.HasValue || currentSupplierId == 0)
            {
                UIMessageBox.Show("请先在表格中选择一个供应商！", "提示", UIStyle.Green);
                return;
            }

            string name = uiTextBox2.Text.Trim();
            if (UIMessageBox.Show($"确定要删除供应商“{name}”吗？", "确认删除", UIStyle.Red))
            {
                try
                {
                    if (await manager.Delete(currentSupplierId.Value))
                    {
                        UIMessageBox.Show("删除成功！", "提示", UIStyle.Green);
                        currentPage = 1;
                        await LoadData();
                        ClearFields();
                        EnableEdit(false);
                        uiDataGridView1.ClearSelection();
                        currentSupplierId = null;
                    }
                }
                catch (Exception ex)
                {
                    UIMessageBox.Show($"删除失败：{ex.Message}", "错误", UIStyle.Red);
                }
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiButton5_Click(object sender, EventArgs e)
        {
            uiTextBox1.Clear();
            currentPage = 1;
            await LoadData();
            ClearFields();
            EnableEdit(false);
            uiDataGridView1.ClearSelection();
            currentSupplierId = null;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            await LoadData();
        }
        #endregion
        private void uiTitlePanel1_Click(object sender, EventArgs e)
        {

        }

        private void uiLabel6_Click(object sender, EventArgs e)
        {

        }






        private void Supplier_LocationChanged(object sender, EventArgs e)
        {

        }

    }
}
