using HospitalEquipment.BLL;
using HospitalEquipment.BLL.management;
using HospitalEquipment.Model;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 科室收入对比：按期间录入/查询/修改/删除各科室收入
    /// </summary>
    public partial class DeptRevenueForm : UIForm
    {
        private readonly DeptRevenueManager manager = new DeptRevenueManager();
        private readonly EquipmentManager equipmentManager = new EquipmentManager();
        private int editingRevenueId = 0;

        public DeptRevenueForm()
        {
            InitializeComponent();
            this.Load += DeptRevenueForm_Load;
            this.btnQuery.Click += BtnQuery_Click;
            this.btnAdd.Click += BtnAdd_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnDelete.Click += BtnDelete_Click;
            this.btnCancel.Click += BtnCancel_Click;
            this.uiDataGridView1.SelectionChanged += UiDataGridView_SelectionChanged;
        }

        private async void DeptRevenueForm_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigureGrid();
                await LoadPeriods();
                await LoadDepartments();
                await RefreshGrid(CurrentPeriod());
            }
            catch (Exception ex)
            {
                UIMessageBox.Show("初始化失败：" + ex.Message, "错误", UIStyle.Red);
            }
        }

        /// <summary>
        /// 配置表格列绑定与只读属性
        /// </summary>
        private void ConfigureGrid()
        {
            uiDataGridView1.AutoGenerateColumns = false;
            uiDataGridView1.AllowUserToAddRows = false;
            uiDataGridView1.AllowUserToDeleteRows = false;
            uiDataGridView1.AllowUserToResizeRows = false;
            uiDataGridView1.ReadOnly = true;
            uiDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            uiDataGridView1.Columns["colPeriod"].DataPropertyName = "Period";
            uiDataGridView1.Columns["colDept"].DataPropertyName = "DeptName";
            uiDataGridView1.Columns["colAmount"].DataPropertyName = "Amount";
            uiDataGridView1.Columns["colAmount"].DefaultCellStyle.Format = "N2";
            uiDataGridView1.Columns["colAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            uiDataGridView1.Columns["colRemark"].DataPropertyName = "Remark";
            uiDataGridView1.Columns["colCreatedAt"].DataPropertyName = "CreatedAt";
            uiDataGridView1.Columns["colCreatedAt"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
        }

        /// <summary>
        /// 加载期间下拉框（倒序，最新期间在首位）
        /// </summary>
        private async Task LoadPeriods()
        {
            var periods = await manager.GetPeriodsAsync();
            uiComboBoxPeriod.Items.Clear();
            if (periods != null && periods.Count > 0)
            {
                uiComboBoxPeriod.Items.AddRange(periods.ToArray());
                uiComboBoxPeriod.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 加载科室下拉框
        /// </summary>
        private async Task LoadDepartments()
        {
            var departments = await equipmentManager.GetDepartments();
            uiComboBoxDept.DataSource = null;
            uiComboBoxDept.DisplayMember = "Value";
            uiComboBoxDept.ValueMember = "Key";
            uiComboBoxDept.DataSource = departments ?? new List<KeyValuePair<int, string>>();
            uiComboBoxDept.SelectedIndex = -1;
        }

        /// <summary>
        /// 当前选中的期间；未选择返回 null（表示默认最新期间）
        /// </summary>
        private string CurrentPeriod()
        {
            return uiComboBoxPeriod.SelectedItem as string;
        }

        /// <summary>
        /// 刷新表格数据
        /// </summary>
        private async Task RefreshGrid(string period)
        {
            var rows = await manager.GetByPeriodAsync(period) ?? new List<DeptRevenue>();
            uiDataGridView1.DataSource = null;
            uiDataGridView1.DataSource = rows;
            lblSummary.Text = string.Format("共 {0} 个科室，合计 ¥{1:N2}", rows.Count, rows.Sum(r => r.Amount));
        }

        private async void BtnQuery_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentPeriod()))
            {
                UIMessageBox.Show("请先选择查询期间", "提示", UIStyle.Orange);
                return;
            }
            await RefreshGrid(CurrentPeriod());
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            editingRevenueId = 0;
            ResetEditor();
            uiDataGridView1.ClearSelection();
            uiTextBoxPeriod.Focus();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            editingRevenueId = 0;
            ResetEditor();
            uiDataGridView1.ClearSelection();
        }

        /// <summary>
        /// 清空编辑区（期间默认取当前下拉选中的期间）
        /// </summary>
        private void ResetEditor()
        {
            uiTextBoxPeriod.Text = CurrentPeriod() ?? "";
            uiComboBoxDept.SelectedIndex = -1;
            uiTextBoxAmount.Text = "";
            uiTextBoxRemark.Text = "";
        }

        private void UiDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (uiDataGridView1.CurrentRow == null || uiDataGridView1.CurrentRow.DataBoundItem == null)
                return;
            if (!(uiDataGridView1.CurrentRow.DataBoundItem is DeptRevenue row))
                return;

            editingRevenueId = row.RevenueId;
            uiTextBoxPeriod.Text = row.Period ?? "";
            try
            {
                uiComboBoxDept.SelectedValue = row.DeptId;
            }
            catch
            {
                uiComboBoxDept.SelectedIndex = -1;
            }
            uiTextBoxAmount.Text = row.Amount.ToString("0.00");
            uiTextBoxRemark.Text = row.Remark ?? "";
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string period = uiTextBoxPeriod.Text.Trim();
                if (string.IsNullOrEmpty(period) || !System.Text.RegularExpressions.Regex.IsMatch(period, @"^\d{4}-\d{2}$"))
                {
                    UIMessageBox.Show("期间格式不正确，应为 yyyy-MM，例如 2026-07", "提示", UIStyle.Orange);
                    return;
                }

                int deptId = 0;
                if (uiComboBoxDept.SelectedValue is int val)
                    deptId = val;
                if (deptId <= 0)
                {
                    UIMessageBox.Show("请选择科室", "提示", UIStyle.Orange);
                    return;
                }

                if (!decimal.TryParse(uiTextBoxAmount.Text.Trim(), NumberStyles.Any, CultureInfo.CurrentCulture, out decimal amount))
                {
                    UIMessageBox.Show("金额格式不正确，请输入数字", "提示", UIStyle.Orange);
                    return;
                }

                var model = new DeptRevenue
                {
                    RevenueId = editingRevenueId,
                    Period = period,
                    DeptId = deptId,
                    Amount = amount,
                    Remark = uiTextBoxRemark.Text.Trim()
                };

                string error = await manager.AddOrUpdateAsync(model);
                if (error != null)
                {
                    UIMessageBox.Show(error, "提示", UIStyle.Orange);
                    return;
                }

                UIMessageBox.Show(editingRevenueId > 0 ? "更新成功" : "保存成功", "成功", UIStyle.Green);
                editingRevenueId = 0;
                await RefreshGrid(CurrentPeriod());
                ResetEditor();
                uiDataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                UIMessageBox.Show("保存失败：" + ex.Message, "错误", UIStyle.Red);
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!(uiDataGridView1.CurrentRow?.DataBoundItem is DeptRevenue row))
            {
                UIMessageBox.Show("请先选中要删除的记录", "提示", UIStyle.Orange);
                return;
            }

            if (!UIMessageBox.ShowAsk(string.Format("确定要删除 {0}（{1}）的收入记录吗？", row.DeptName, row.Period), true))
                return;

            string error = await manager.DeleteAsync(row.RevenueId);
            if (error != null)
            {
                UIMessageBox.Show(error, "提示", UIStyle.Orange);
                return;
            }

            UIMessageBox.Show("删除成功", "成功", UIStyle.Green);
            editingRevenueId = 0;
            await RefreshGrid(CurrentPeriod());
            ResetEditor();
            uiDataGridView1.ClearSelection();
        }
    }
}
