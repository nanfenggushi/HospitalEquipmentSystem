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

namespace HospitalEquipmentSystem.UI.management
{
    public partial class WarehousingManagement : UIForm
    {
        private InboundRecordManager manager = new InboundRecordManager();
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalcount = 0;
        private int? currentEditId = null;
        private int loadVersion = 0;
        private bool isBindingPagination = false;

        public WarehousingManagement()
        {
            InitializeComponent();
        }

        private void uiLabel1_Click(object sender, EventArgs e)
        {

        }


        private async void WarehousingManagement_Load(object sender, EventArgs e)
        {
            // 审核状态显示中文（一次性绑定，避免重复添加）
            uiDataGridView1.CellFormatting += (s, ev) =>
            {
                if (ev.ColumnIndex >= 0 && uiDataGridView1.Columns[ev.ColumnIndex].Name == "Column6" && ev.Value != null)
                {
                    string status = ev.Value.ToString();
                    ev.Value = status switch
                    {
                        "Pending" => "待审核",
                        "Approved" => "已审核",
                        "Rejected" => "已驳回",
                        _ => status
                    };
                    ev.FormattingApplied = true;
                }
            };

            // 初始化下拉框
            try
            {
                await LoadComboBoxes();

                // 默认生成单号
                await GenerateNewInboundNo();

                // 设置默认日期
                uiDatePicker1.Value = DateTime.Today;

                // 加载数据
                currentPage = 1;
                await LoadData();

                // 初始禁用编辑区
                EnableEdit(false);
                ClearFields();
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"初始化失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }
        #region 初始化
        /// <summary>
        /// 加载所有下拉框数据
        /// </summary>
        private async Task LoadComboBoxes()
        {
            // 设备下拉
            var equipmentList = await manager.GetEquipmentList();
            uiComboBox1.DataSource = null;
            uiComboBox1.DisplayMember = "Value";
            uiComboBox1.ValueMember = "Key";
            uiComboBox1.DataSource = equipmentList;
            uiComboBox1.SelectedIndex = -1;

            // 供应商下拉
            var supplierList = await manager.GetSupplierList();
            uiComboBox2.DataSource = null;
            uiComboBox2.DataSource = supplierList;
            uiComboBox2.SelectedIndex = -1;

            // 操作人下拉
            var operatorList = await manager.GetOperatorList();
            uiComboBox3.DataSource = null;
            uiComboBox3.DisplayMember = "Value";
            uiComboBox3.ValueMember = "Key";
            uiComboBox3.DataSource = operatorList;
            uiComboBox3.SelectedIndex = -1;
        }
        /// <summary>
        /// 生成新的入库单号
        /// </summary>
        private async Task GenerateNewInboundNo()
        {
            uiTextBox2.Text = await manager.GenerateInboundNo();
        }
        #endregion
        #region 数据加载
        /// <summary>
        /// 加载入库记录列表
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async Task LoadData()
        {
            try
            {
                int version = ++loadVersion;
                string keyword = uiTextBox1.Text.Trim();
                var result = await manager.GetPaged(currentPage, pageSize, keyword);
                if (version != loadVersion) return;
                totalcount = result.total;

                int displayedRow = uiDataGridView1.FirstDisplayedScrollingRowIndex;
                int displayedColumn = uiDataGridView1.FirstDisplayedScrollingColumnIndex;
                int currentRow = uiDataGridView1.CurrentCell != null ? uiDataGridView1.CurrentCell.RowIndex : -1;
                int currentColumn = uiDataGridView1.CurrentCell != null ? uiDataGridView1.CurrentCell.ColumnIndex : -1;

                //配置表格列
                ConfigureDataGridView();

                // 绑定数据
                uiDataGridView1.DataSource = null;
                uiDataGridView1.DataSource = result.list;

                // 更新分页信息
                isBindingPagination = true;
                try
                {
                    uiPagination1.TotalCount = totalcount;
                    uiPagination1.PageSize = pageSize;
                    uiPagination1.ActivePage = currentPage;
                }
                finally
                {
                    isBindingPagination = false;
                }

                try
                {
                    if (result.list.Count > 0)
                    {
                        if (currentRow >= 0 && currentRow < uiDataGridView1.Rows.Count)
                        {
                            int restoreColumn = currentColumn >= 0 && currentColumn < uiDataGridView1.Columns.Count
                                ? currentColumn
                                : 0;
                            uiDataGridView1.CurrentCell = uiDataGridView1.Rows[currentRow].Cells[restoreColumn];
                            uiDataGridView1.Rows[currentRow].Selected = true;
                        }
                        if (displayedRow >= 0 && displayedRow < uiDataGridView1.Rows.Count)
                            uiDataGridView1.FirstDisplayedScrollingRowIndex = displayedRow;
                        if (displayedColumn >= 0 && displayedColumn < uiDataGridView1.Columns.Count)
                            uiDataGridView1.FirstDisplayedScrollingColumnIndex = displayedColumn;
                    }
                }
                catch
                {
                    // 恢复失败时不阻断数据加载，仅保留默认位置
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"加载数据失败: {ex.Message}");
            }
        }
        /// <summary>
        /// 配置表格列
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void ConfigureDataGridView()
        {
            uiDataGridView1.AutoGenerateColumns = false;

            if (uiDataGridView1.Columns.Contains("Column1"))
            {
                uiDataGridView1.Columns["Column1"].DataPropertyName = "InboundNo";
                uiDataGridView1.Columns["Column1"].HeaderText = "入库单号";
            }
            if (uiDataGridView1.Columns.Contains("Column2"))
            {
                uiDataGridView1.Columns["Column2"].DataPropertyName = "EquipmentName";
                uiDataGridView1.Columns["Column2"].HeaderText = "设备名称";
            }
            if (uiDataGridView1.Columns.Contains("Column3"))
            {
                uiDataGridView1.Columns["Column3"].DataPropertyName = "Supplier";
                uiDataGridView1.Columns["Column3"].HeaderText = "供应商";
            }
            if (uiDataGridView1.Columns.Contains("Column4"))
            {
                uiDataGridView1.Columns["Column4"].DataPropertyName = "Quantity";
                uiDataGridView1.Columns["Column4"].HeaderText = "数量";
            }
            if (uiDataGridView1.Columns.Contains("Column5"))
            {
                uiDataGridView1.Columns["Column5"].DataPropertyName = "InboundDate";
                uiDataGridView1.Columns["Column5"].HeaderText = "入库日期";
                uiDataGridView1.Columns["Column5"].DefaultCellStyle.Format = "yyyy-MM-dd";
            }
            if (uiDataGridView1.Columns.Contains("Column6"))
            {
                uiDataGridView1.Columns["Column6"].DataPropertyName = "AuditStatus";
                uiDataGridView1.Columns["Column6"].HeaderText = "审核状态";
            }
        }

        #endregion
        #region 编辑区操作

        /// <summary>
        /// 启用/禁用编辑区
        /// </summary>
        /// <param name="v"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void EnableEdit(bool enable)
        {
            uiTextBox2.Enabled = enable;       // 入库单号（自动生成，可编辑但建议只读）
            uiDatePicker1.Enabled = enable;    // 入库日期
            uiComboBox1.Enabled = enable;      // 设备名称
            uiTextBox3.Enabled = enable;       // 设备编号（只读）
            uiTextBox4.Enabled = enable;       // 分类（只读）
            uiComboBox2.Enabled = enable;      // 供应商
            uiTextBox5.Enabled = enable;       // 采购价格
            uiTextBox6.Enabled = enable;       // 数量
            uiComboBox3.Enabled = enable;      // 操作人
            uiTextBox7.Enabled = enable;       // 备注
            uiButton5.Enabled = enable;        // 保存
            uiButton3.Enabled = enable;        // 取消
            uiButton6.Enabled = enable;        // 删除
        }
        /// <summary>
        /// 清空编辑区字段
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void ClearFields()
        {
            uiTextBox2.Clear();
            uiDatePicker1.Value = DateTime.Today;
            uiComboBox1.SelectedIndex = -1;
            uiTextBox3.Clear();
            uiTextBox4.Clear();
            uiComboBox2.SelectedIndex = -1;
            uiTextBox5.Clear();
            uiTextBox6.Clear();
            uiComboBox3.SelectedIndex = -1;
            uiTextBox7.Clear();
            currentEditId = null;
        }
        /// <summary>
        /// 加载数据到编辑区
        /// </summary>
        /// <param name="record"></param>
        private void LoadToEdit(InboundRecord record)
        {
            currentEditId = record.InboundId;
            uiTextBox2.Text = record.InboundNo;
            uiDatePicker1.Value = record.InboundDate;
            uiComboBox1.SelectedValue = record.EquipmentId;
            uiTextBox3.Text = record.EquipmentNo ?? "";
            uiTextBox4.Text = record.EquipmentName ?? "";
            uiComboBox2.SelectedItem = record.Supplier;
            uiTextBox5.Text = record.PurchasePrice?.ToString() ?? "";
            uiTextBox6.Text = record.Quantity.ToString();
            uiComboBox3.SelectedValue = record.OperatorId;
            uiTextBox7.Text = record.Remarks ?? "";

            // 如果是已审核的记录，禁用编辑区
            if (record.AuditStatus == "Pending")
            {
                EnableEdit(false);
                UIMessageBox.Show($"该记录已「{record.AuditStatusText}」，不可编辑！", "提示", UIStyle.Green);
            }
            else
            {
                EnableEdit(true);
            }
        }
        #endregion
        #region 设备选择联动
        /// <summary>
        /// 设备选择变化时，自动带出设备编号和分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (uiComboBox1.SelectedValue is int equipmentId && equipmentId > 0)
                {
                    uiTextBox3.Text = await manager.GetEquipmentNoById(equipmentId);
                    uiTextBox4.Text = await manager.GetCategoryNameByEquipmentId(equipmentId);
                }
                else
                {
                    uiTextBox3.Clear();
                    uiTextBox4.Clear();
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"加载设备信息失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }
        #endregion
        #region 表格事件
        /// <summary>
        /// 点击表格行加载到编辑区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                var row = uiDataGridView1.Rows[e.RowIndex];

                // 获取 InboundId（隐藏列，需要先设置 DataPropertyName）
                // 方案：通过其他列获取，或添加隐藏列
                // 这里通过 InboundNo 查询
                if (row.Cells["Column1"].Value == null) return;

                string inboundNo = row.Cells["Column1"].Value.ToString();
                var record = await manager.GetByNo(inboundNo);
                if (record != null)
                    LoadToEdit(record);
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"加载入库记录失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }
        #endregion
        #region 分页事件
        private async void uiPagination1_PageChanged(object sender, object pagingSource, int pageIndex, int count)
        {
            if (isBindingPagination) return;

            currentPage = pageIndex;
            await LoadData();
        }
        #endregion
        #region 按钮事件
        /// <summary>
        /// 新增按钮：清空编辑区并启用编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiButton2_Click(object sender, EventArgs e)
        {
            ClearFields();
            await GenerateNewInboundNo();
            uiDatePicker1.Value = DateTime.Today;
            EnableEdit(true);
            uiComboBox1.Focus();

        }
        /// <summary>
        /// 保存按钮：保存当前编辑区数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiButton5_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证必填
                if (uiComboBox1.SelectedValue == null || !(uiComboBox1.SelectedValue is int equipmentId) || equipmentId <= 0)
                {
                    UIMessageBox.Show("请选择设备！", "提示", UIStyle.Green);
                    uiComboBox1.Focus();
                    return;
                }

                if (uiComboBox2.SelectedItem == null || string.IsNullOrWhiteSpace(uiComboBox2.SelectedItem.ToString()))
                {
                    UIMessageBox.Show("请选择供应商！", "提示", UIStyle.Green);
                    uiComboBox2.Focus();
                    return;
                }

                if (!decimal.TryParse(uiTextBox5.Text.Trim(), out decimal price))
                {
                    UIMessageBox.Show("请输入有效的采购价格！", "提示", UIStyle.Green);
                    uiTextBox5.Focus();
                    return;
                }

                if (!int.TryParse(uiTextBox6.Text.Trim(), out int quantity) || quantity <= 0)
                {
                    UIMessageBox.Show("请输入有效的数量（大于0）！", "提示", UIStyle.Green);
                    uiTextBox6.Focus();
                    return;
                }

                var record = new InboundRecord
                {
                    InboundId = currentEditId ?? 0,
                    EquipmentId = equipmentId,
                    InboundNo = uiTextBox2.Text.Trim(),
                    Supplier = uiComboBox2.SelectedItem.ToString(),
                    PurchasePrice = price,
                    Quantity = quantity,
                    InboundDate = uiDatePicker1.Value.Date,
                    OperatorId = uiComboBox3.SelectedValue as int?,
                    Remarks = uiTextBox7.Text.Trim(),
                    AuditStatus = "Pending"
                };

                bool result;
                if (currentEditId == 0)
                    result = await manager.Insert(record);
                else
                    result = await manager.Update(record);

                if (result)
                {
                    UIMessageBox.Show("保存成功！", "提示", UIStyle.Green);
                    await LoadData();
                    ClearFields();
                    EnableEdit(false);
                    await GenerateNewInboundNo();
                }
                else
                {
                    UIMessageBox.Show("保存失败，请检查控制台日志。", "错误", UIStyle.Red);
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"保存失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }
        /// <summary>
        /// 取消按钮：清空编辑区并禁用编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiButton3_Click(object sender, EventArgs e)
        {
            ClearFields();
            EnableEdit(false);
            await GenerateNewInboundNo();
            uiDataGridView1.ClearSelection();
            currentEditId = null;

        }
        /// <summary>
        /// 删除按钮：删除当前编辑区数据（需确认）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiButton6_Click(object sender, EventArgs e)
        {
            if (!currentEditId.HasValue || currentEditId == 0)
            {
                UIMessageBox.Show("请先在表格中选择一条记录！", "提示", UIStyle.Green);
                return;
            }

            string inboundNo = uiTextBox2.Text.Trim();
            if (UIMessageBox.Show($"确定要删除入库单“{inboundNo}”吗？\n（仅待审核状态可删除）", "确认删除", UIStyle.Red))
            {
                try
                {
                    if (await manager.Delete(currentEditId.Value))
                    {
                        UIMessageBox.Show("删除成功！", "提示", UIStyle.Green);
                        await LoadData();
                        ClearFields();
                        EnableEdit(false);
                        uiDataGridView1.ClearSelection();
                        await GenerateNewInboundNo();
                        currentEditId = null;
                    }
                }
                catch (Exception ex)
                {
                    UIMessageBox.Show($"删除失败：{ex.Message}", "错误", UIStyle.Red);
                }
            }
        }
        /// <summary>
        /// 导出按钮：导出当前列表数据到 Excel（可选）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton7_Click(object sender, EventArgs e)
        {
            UIMessageBox.Show("导出功能开发中...", "提示", UIStyle.Green);
        }
        /// <summary>
        /// 查询按钮：根据当前筛选条件搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiButton4_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            await LoadData();
        }
        /// <summary>
        /// 刷新按钮：重新加载当前页数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void uiButton1_Click(object sender, EventArgs e)
        {
            uiTextBox1.Clear();
            currentPage = 1;
            await LoadData();
        }
        #endregion
    }
}
