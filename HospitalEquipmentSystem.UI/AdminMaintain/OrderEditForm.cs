using Sunny.UI;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        // 拍照上传后的照片本地路径
        private string _photoPath;
        private static readonly string UPLOAD_DIR = Path.Combine(Application.StartupPath, "Uploads", "Photos");

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
            ThemeHelper.ApplyDarkTheme(this);

            // 初始化标题和下拉项
            this.Text = "新建维修工单";
            cmbFaultType.Items.AddRange(new object[] { "电气故障", "机械故障", "软件故障" });
            cmbFaultType.SelectedIndex = 0; // 默认选"待确认"
            cmbUrgency.Items.AddRange(new object[] { "低", "普通", "紧急" });
            cmbUrgency.SelectedIndex = 1;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
            btnUploadPhoto.Click += BtnUploadPhoto_Click;
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
            this.Text = "编辑维修工单";
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

            // 编辑模式：加载已有照片
            var photoPath = row["PhotoPath"]?.ToString();
            if (!string.IsNullOrEmpty(photoPath) && File.Exists(photoPath))
            {
                _photoPath = photoPath;
                picPreview.Image = Image.FromFile(photoPath);
            }
        }

        /// <summary>
        /// 拍照上传：选择图片并复制到 Uploads/Photos 目录
        /// </summary>
        private void BtnUploadPhoto_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "选择故障照片";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                // 确保目录存在
                if (!Directory.Exists(UPLOAD_DIR))
                    Directory.CreateDirectory(UPLOAD_DIR);

                // 生成文件名：{时间戳}.jpg
                string fileName = $"{DateTime.Now:yyyyMMddHHmmss}.jpg";
                string destPath = Path.Combine(UPLOAD_DIR, fileName);

                // 复制文件
                File.Copy(ofd.FileName, destPath, true);
                _photoPath = destPath;

                // 显示预览
                picPreview.Image = Image.FromFile(destPath);
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
            // "待确认"不是具体故障类型，保存为空字符串，留待维修员确认
            if (faultType == "待确认")
                faultType = "";
            string faultDesc = txtFaultDesc.Text.Trim();
            string urgency = MaintenanceHelper.UrgencyToEn(cmbUrgency.Text);

            bool ok;
            if (_mode == FormMode.Add)
            {
                // 新增模式：先创建工单拿到 RecordId，再写照片路径
                int newId = _bll.SubmitRepair(equipId, deptId, 1, faultType, faultDesc, urgency);
                ok = newId > 0;
                if (ok) _recordId = newId;
            }
            else
            {
                ok = _bll.UpdateOrder(_recordId, equipId, deptId, faultType, faultDesc, urgency);
            }

            if (ok)
            {
                // 保存成功后写入照片路径
                if (!string.IsNullOrEmpty(_photoPath) && _recordId > 0)
                    _bll.UpdatePhotoPath(_recordId, _photoPath);

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
