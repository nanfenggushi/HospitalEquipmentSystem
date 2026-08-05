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
        private SupplierManager  manager = new SupplierManager();
        private int? currentSupplierId = null;
        public Supplier()
        {
            InitializeComponent();
        }
        private void Supplier_Load(object sender, EventArgs e)
        {
            LoadData();
            EnableEdit(false);
            ClearFields();
            uiDataGridView1.ClearSelection();

        }

        

        #region 数据加载
        private void LoadData()
        {
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
            var list=manager.GetAll();
            uiDataGridView1.DataSource = null;
            uiDataGridView1.DataSource = list;

            //状态列显示中文
            uiDataGridView1.CellFormatting+=(s,ev)=>
            {
                if (ev.ColumnIndex >= 0 && uiDataGridView1.Columns[ev.ColumnIndex].Name== "Column7"&&ev.Value!=null)
                {
                    ev.Value = (bool)ev.Value ? "启用" : "禁用";
                    ev.FormattingApplied = true;
                }
            };
            uiDataGridView1.ClearSelection();
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
            var supplier1 = new SupplierEntity();
            currentSupplierId = supplier1.SupplierId;
            uiTextBox2.Text = supplier1.SupplierName;
            uiTextBox6.Text = supplier1.SupplierCode ?? "";
            uiTextBox3.Text = supplier1.ContactPerson ?? "";
            uiTextBox7.Text = supplier1.Phone ?? "";
            uiTextBox4.Text = supplier1.Email ?? "";
            uiTextBox8.Text = supplier1.Address ?? "";
            uiTextBox5.Text = supplier1.Website ?? "";
            uiTextBox9.Text = supplier1.Remark ?? "";
            uiCheckBox1.Checked = supplier1.IsActive;
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
        private void uiDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = uiDataGridView1.Rows[e.RowIndex];
            if (row.Cells["Column8"].Value == null) return;

            int id = Convert.ToInt32(row.Cells["Column8"].Value);
            var supplier = manager.GetById(id);
            if (supplier != null)
                LoadToEdit(supplier);
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
        private void uiButton4_Click(object sender, EventArgs e)
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
                    result = manager.Insert(supplier);
                else
                    result = manager.Update(supplier);

                if (result)
                {
                    UIMessageBox.Show("保存成功！", "提示", UIStyle.Green);
                    LoadData();
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
        private void uiButton3_Click(object sender, EventArgs e)
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
                    if (manager.Delete(currentSupplierId.Value))
                    {
                        UIMessageBox.Show("删除成功！", "提示", UIStyle.Green);
                        LoadData();
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
        private void uiButton5_Click(object sender, EventArgs e)
        {
            LoadData();
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
        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            string keyword = uiTextBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                LoadData();
                return;
            }

            var result = manager.Search(keyword);
            uiDataGridView1.DataSource = null;
            uiDataGridView1.DataSource = result;

            // 重置列头（因为 DataSource 重新赋值后列头设置可能丢失）
            if (uiDataGridView1.Columns.Contains("Column8"))
                uiDataGridView1.Columns["Column8"].Visible = false;
            if (uiDataGridView1.Columns.Contains("Column1"))
                uiDataGridView1.Columns["Column1"].HeaderText = "供应商名称";
            if (uiDataGridView1.Columns.Contains("Column7"))
                uiDataGridView1.Columns["Column7"].HeaderText = "状态";
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
