using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HospitalEquipment.BLL;
using HospitalEquipment.Util;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 医生端提交报修弹窗：选择设备（本科室 / 借用）+ 填写故障信息
    /// </summary>
    public partial class RepairSubmitForm : UIForm
    {
        private MaintenanceBLL _bll;

        /// <summary>选中的设备ID</summary>
        public int SelectedEquipmentId { get; private set; }

        /// <summary>设备所属科室ID</summary>
        public int SelectedDeptId { get; private set; }

        /// <summary>故障类型（文本）</summary>
        public string FaultType => cmbFaultType.Text;

        /// <summary>故障描述</summary>
        public string FaultDesc => txtFaultDesc.Text.Trim();

        /// <summary>紧急度（英文值）</summary>
        public string Urgency { get; private set; } = "Normal";

        // 设备映射：索引 → ID/DeptId
        private int[] _equipIds;
        private int[] _deptIds;

        // 紧急程度映射：索引 → 英文值
        private string[] _urgencyEnValues;

        private MaintenanceBLL BLL => _bll ?? (_bll = new MaintenanceBLL());

        public RepairSubmitForm()
        {
            InitializeComponent();
            ThemeHelper.ApplyDarkTheme(this);
            if (DesignMode) return;

            txtFaultDesc.Watermark = "详细描述设备故障情况...";

            rdbDept.CheckedChanged += (s, e) => { if (rdbDept.Checked) LoadEquipmentByDept(); };
            rdkBorrowed.CheckedChanged += (s, e) => { if (rdkBorrowed.Checked) LoadBorrowedEquipment(); };

            LoadFaultTypes();
            LoadUrgencies();
            LoadEquipmentByDept();
        }

        // ==================== 下拉框数据加载 ====================

        /// <summary>
        /// 加载故障类型（List<string> → Items.AddRange，纯字符串）
        /// </summary>
        private void LoadFaultTypes()
        {
            List<string> types = BLL.GetFaultTypeOptionsList();
            cmbFaultType.Items.Clear();
            cmbFaultType.Items.AddRange(types.ToArray());
            // 强制清除可能残留的 DataRowView 项
            RemoveDataRowViewItems(cmbFaultType);
            if (cmbFaultType.Items.Count > 0) cmbFaultType.SelectedIndex = 0;
        }

        /// <summary>
        /// 加载紧急程度（Dictionary → 中文显示 + 数组存英文值）
        /// </summary>
        private void LoadUrgencies()
        {
            var dict = BLL.GetUrgencyOptionsDict();
            cmbUrgency.Items.Clear();
            var enValues = new List<string>();
            foreach (var kvp in dict)
            {
                cmbUrgency.Items.Add(kvp.Value);
                enValues.Add(kvp.Key);
            }
            _urgencyEnValues = enValues.ToArray();
            RemoveDataRowViewItems(cmbUrgency);

            // 默认选中"普通"
            int normalIdx = Array.IndexOf(_urgencyEnValues, "Normal");
            cmbUrgency.SelectedIndex = normalIdx >= 0 ? normalIdx : 0;

            Urgency = _urgencyEnValues[cmbUrgency.SelectedIndex];
            cmbUrgency.SelectedIndexChanged += (s, e) =>
            {
                if (cmbUrgency.SelectedIndex >= 0 && cmbUrgency.SelectedIndex < _urgencyEnValues.Length)
                    Urgency = _urgencyEnValues[cmbUrgency.SelectedIndex];
            };
        }

        /// <summary>
        /// 加载本科室设备
        /// </summary>
        private void LoadEquipmentByDept()
        {
            DataTable dt = BLL.GetEquipmentByDept(LoginUser.DeptId);
            FillEquipmentComboBox(dt, "本科室暂无设备");
        }

        /// <summary>
        /// 加载借用设备
        /// </summary>
        private void LoadBorrowedEquipment()
        {
            DataTable dt = BLL.GetBorrowedEquipmentByApplicant(LoginUser.UserId);
            FillEquipmentComboBox(dt, "暂无借用中的设备");
        }

        /// <summary>
        /// 填充设备下拉框（手动 Items.Add + 数组存 ID/DeptId）
        /// </summary>
        private void FillEquipmentComboBox(DataTable dt, string emptyText)
        {
            cmbEquipment.Items.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                _equipIds = new int[dt.Rows.Count];
                _deptIds = new int[dt.Rows.Count];
                var names = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _equipIds[i] = Convert.ToInt32(dt.Rows[i]["EquipmentId"]);
                    _deptIds[i] = Convert.ToInt32(dt.Rows[i]["DeptId"]);
                    names.Add(dt.Rows[i]["EquipmentName"].ToString());
                }
                cmbEquipment.Items.AddRange(names.ToArray());
                RemoveDataRowViewItems(cmbEquipment);
                cmbEquipment.SelectedIndex = 0;
            }
            else
            {
                _equipIds = new int[] { 0 };
                _deptIds = new int[] { 0 };
                cmbEquipment.Items.Add(emptyText);
                cmbEquipment.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 强制删除 ComboBox 中所有 DataRowView 类型的项
        /// （SunnyUI UIComboBox 在某些情况下会残留 DataRowView 项，显示为 "System.Data.DataRowView"）
        /// </summary>
        private void RemoveDataRowViewItems(UIComboBox combo)
        {
            // 倒序遍历删除，避免索引错位
            for (int i = combo.Items.Count - 1; i >= 0; i--)
            {
                var item = combo.Items[i];
                if (item is DataRowView || item == null ||
                    (item != null && item.ToString().Contains("System.Data")))
                {
                    combo.Items.RemoveAt(i);
                }
            }
        }

        // ==================== 提交/取消 ====================

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbEquipment.SelectedIndex < 0 || _equipIds[cmbEquipment.SelectedIndex] == 0)
            {
                UIMessageBox.ShowWarning("请选择设备");
                return;
            }

            SelectedEquipmentId = _equipIds[cmbEquipment.SelectedIndex];
            SelectedDeptId = _deptIds[cmbEquipment.SelectedIndex];

            if (string.IsNullOrWhiteSpace(FaultDesc))
            {
                UIMessageBox.ShowWarning("请填写故障描述");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
