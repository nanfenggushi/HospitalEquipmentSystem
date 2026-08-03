using Sunny.UI;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HospitalEquipment.BLL;
using HospitalEquipment.Util;

namespace HospitalEquipmentSystem.UI
{
    public partial class OrderEditForm : UIForm
    {
        private enum FormMode { Add, Edit }
        private FormMode _mode;
        private int _recordId;
        private readonly MaintenanceBLL _bll = new MaintenanceBLL();

        // 设备/科室 ID 映射（索引 → ID）
        private int[] _equipIds;
        private int[] _deptIds;

        /// <summary>
        /// 新增模式
        /// </summary>
        public OrderEditForm()
        {
            _mode = FormMode.Add;
            _recordId = 0;
            InitializeComponent();

            // 初始化标题和下拉项
            this.Text = "新建工单";
            lblTitle.Text = "新建维修工单";
            cmbFaultType.Items.AddRange(new object[] { "电气故障", "机械故障", "软件故障", "耗材更换", "其他" });
            cmbFaultType.SelectedIndex = 0;
            cmbUrgency.Items.AddRange(new object[] { "低", "普通", "紧急" });
            cmbUrgency.SelectedIndex = 1;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
            btnDelete.Visible = false;

            if (DesignMode) return;

            PopulateDropdowns();
            txtRepairNo.Text = _bll.GenerateRepairNo();
        }

        /// <summary>
        /// 编辑模式
        /// </summary>
        public OrderEditForm(int recordId) : this()
        {
            if (DesignMode) return;

            _mode = FormMode.Edit;
            _recordId = recordId;
            this.Text = "编辑工单";
            lblTitle.Text = "编辑工单信息";
            LoadOrderData();

            btnDelete.Visible = true;
            btnDelete.Click += (s, e) =>
            {
                var result = System.Windows.Forms.MessageBox.Show(
                    "确定要删除该工单吗？此操作不可恢复。", "确认删除",
                    System.Windows.Forms.MessageBoxButtons.OKCancel,
                    System.Windows.Forms.MessageBoxIcon.Warning);
                if (result != System.Windows.Forms.DialogResult.OK)
                    return;

                if (_bll.DeleteOrder(_recordId))
                {
                    UIMessageBox.Show("工单已删除。");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    UIMessageBox.Show("删除失败，请检查数据库连接。");
                }
            };
        }

        /// <summary>
        /// 填充设备和科室下拉列表
        /// </summary>
        private void PopulateDropdowns()
        {
            var equipDt = _bll.GetEquipmentList();
            cmbEquipment.Items.Clear();
            _equipIds = new int[equipDt.Rows.Count];
            for (int i = 0; i < equipDt.Rows.Count; i++)
            {
                _equipIds[i] = Convert.ToInt32(equipDt.Rows[i]["EquipmentId"]);
                cmbEquipment.Items.Add(equipDt.Rows[i]["EquipmentName"].ToString());
            }

            var deptDt = _bll.GetDeptList();
            cmbDept.Items.Clear();
            _deptIds = new int[deptDt.Rows.Count];
            for (int i = 0; i < deptDt.Rows.Count; i++)
            {
                _deptIds[i] = Convert.ToInt32(deptDt.Rows[i]["DeptId"]);
                cmbDept.Items.Add(deptDt.Rows[i]["DeptName"].ToString());
            }
        }

        /// <summary>
        /// 编辑模式：加载已有工单数据
        /// </summary>
        private void LoadOrderData()
        {
            var dt = _bll.GetOrderById(_recordId);
            if (dt == null || dt.Rows.Count == 0) return;

            var row = dt.Rows[0];
            txtRepairNo.Text = row["RepairNo"].ToString();
            txtFaultDesc.Text = row["FaultDesc"].ToString();

            var equipId = Convert.ToInt32(row["EquipmentId"]);
            for (int i = 0; i < _equipIds.Length; i++)
            {
                if (_equipIds[i] == equipId) { cmbEquipment.SelectedIndex = i; break; }
            }

            var deptId = Convert.ToInt32(row["ReportDeptId"]);
            for (int i = 0; i < _deptIds.Length; i++)
            {
                if (_deptIds[i] == deptId) { cmbDept.SelectedIndex = i; break; }
            }

            var faultType = row["FaultType"].ToString();
            for (int i = 0; i < cmbFaultType.Items.Count; i++)
            {
                if (cmbFaultType.Items[i].ToString() == faultType)
                {
                    cmbFaultType.SelectedIndex = i;
                    break;
                }
            }

            var urgencyCn = MaintenanceHelper.UrgencyToCn(row["Urgency"].ToString());
            for (int i = 0; i < cmbUrgency.Items.Count; i++)
            {
                if (cmbUrgency.Items[i].ToString() == urgencyCn)
                {
                    cmbUrgency.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmbEquipment.SelectedIndex < 0)
            {
                UIMessageBox.Show("请选择设备");
                return;
            }
            if (cmbDept.SelectedIndex < 0)
            {
                UIMessageBox.Show("请选择科室");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFaultDesc.Text))
            {
                UIMessageBox.Show("请输入故障描述");
                return;
            }

            int equipId = _equipIds[cmbEquipment.SelectedIndex];
            int deptId = _deptIds[cmbDept.SelectedIndex];
            string faultType = cmbFaultType.Text;
            string faultDesc = txtFaultDesc.Text.Trim();
            string urgency = MaintenanceHelper.UrgencyToEn(cmbUrgency.Text);

            bool ok;
            if (_mode == FormMode.Add)
                ok = _bll.CreateOrder(equipId, deptId, 1, faultType, faultDesc, urgency);
            else
                ok = _bll.UpdateOrder(_recordId, equipId, deptId, faultType, faultDesc, urgency);

            if (ok)
            {
                UIMessageBox.Show(_mode == FormMode.Add ? "工单创建成功！" : "工单修改成功！");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                UIMessageBox.Show("操作失败，请检查数据库连接。");
            }
        }
    }
}
