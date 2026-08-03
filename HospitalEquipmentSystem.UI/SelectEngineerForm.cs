using HospitalEquipment.BLL;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    public partial class SelectEngineerForm : UIForm
    {
        private List<int> _engineerIds;

        public int SelectedEngineerId { get; private set; }

        public SelectEngineerForm()
        {
            InitializeComponent();
        }

        public SelectEngineerForm(MaintenanceBLL bll) : this()
        {
            if (DesignMode) return;
            LoadEngineers(bll);
        }

        private void LoadEngineers(MaintenanceBLL bll)
        {
            var dt = bll.GetEngineerList();
            _engineerIds = new List<int>();
            cmbEngineer.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                _engineerIds.Add(Convert.ToInt32(row["UserId"]));
                cmbEngineer.Items.Add(row["RealName"].ToString());
            }

            if (cmbEngineer.Items.Count > 0)
                cmbEngineer.SelectedIndex = 0;

            btnOk.Click += (s, e) =>
            {
                int idx = cmbEngineer.SelectedIndex;
                if (idx < 0 || idx >= _engineerIds.Count)
                {
                    UIMessageBox.Show("请选择一位工程师");
                    return;
                }
                SelectedEngineerId = _engineerIds[idx];
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
        }
    }
}
