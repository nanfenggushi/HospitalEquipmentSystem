﻿using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using HospitalEquipment.Util;

namespace HospitalEquipmentSystem.UI
{
    public partial class MainTainManagement : UIForm
    {
        private MaintenanceBLL _bll;

        private MaintenanceBLL BLL => _bll ?? (_bll = new MaintenanceBLL());

        public MainTainManagement()
        {
            InitializeComponent();

            if (DesignMode) return;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;

            WireEvents();
            LoadData();
        }

        // ==================== 数据加载 ====================

        private void LoadData()
        {
            //默认下拉选项
            string urgency = GetFilterValue(cmbUrgency, "全部紧急度");
            string dept = GetFilterValue(cmbDept, "全部科室");
            string keyword = txtSearch.Text.Trim();

            var list = BLL.GetOrders(urgency, dept, keyword, null);
            dgvOrders.DataSource = list;

            RefreshKpiCards();
            RefreshAlerts();
            RefreshWorkloads();
        }

        // ==================== KPI 卡片 ====================

        private void RefreshKpiCards()
        {
            var kpi = BLL.GetKpiData();
            lblKpi1Val.Text = kpi["Pending"].ToString();    // 待分配
            lblKpi2Val.Text = kpi["InProgress"].ToString(); // 处理中
            lblKpi3Val.Text = kpi["Urgent"].ToString();     // 特急
            lblKpi4Val.Text = kpi["Completed"].ToString();  // 已完成
            lblKpi5Val.Text = kpi["Assigned"].ToString();   // 已指派
            lblKpi6Val.Text = $"{kpi["TotalDownHours"]}h";  // 总停机
        }

        // ==================== 实时告警 ====================

        private void RefreshAlerts()
        {
            pnlAlertsList.Controls.Clear();
            var dt = BLL.GetAlerts();
            if (dt == null || dt.Rows.Count == 0)
            {
                AddAlertItem("暂无告警", "", Color.FromArgb(159, 179, 200));
                return;
            }
            foreach (DataRow row in dt.Rows)
            {
                var urgency = row["Urgency"].ToString();
                var color = urgency == "Urgent" ? Color.FromArgb(239, 68, 68) : Color.FromArgb(245, 176, 66);
                var title = $"{row["RepairNo"]} {row["EquipmentName"]}";
                var desc = $"{row["FaultDesc"]} · 已停机{row["DowntimeHours"]}h";
                AddAlertItem(title, desc, color);
            }
        }

        private void AddAlertItem(string title, string desc, Color accentColor)
        {
            var pnl = new UIPanel
            {
                Width = pnlAlertsList.ClientSize.Width - 20,
                Height = 56,
                Margin = new Padding(10, 4, 10, 4),
                FillColor = Color.FromArgb(19, 35, 58),
                RectColor = accentColor,
                Radius = 6
            };

            var lblTitle = new UILabel
            {
                Text = title,
                Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(230, 238, 247),
                Location = new Point(14, 6),
                AutoSize = true
            };

            var lblDesc = new UILabel
            {
                Text = desc,
                Font = new Font("Microsoft YaHei", 8F),
                ForeColor = Color.FromArgb(159, 179, 200),
                Location = new Point(14, 30),
                AutoSize = true
            };

            pnl.Controls.Add(lblTitle);
            pnl.Controls.Add(lblDesc);
            pnlAlertsList.Controls.Add(pnl);
        }

        // ==================== 维修员负载 ====================

        private void RefreshWorkloads()
        {
            pnlWorkloadList.Controls.Clear();
            var dt = BLL.GetWorkloads();
            if (dt == null || dt.Rows.Count == 0)
            {
                AddWorkloadItem("暂无负载数据", 0);
                return;
            }
            foreach (DataRow row in dt.Rows)
            {
                var name = row["RepairerName"].ToString();
                var count = Convert.ToInt32(row["TaskCount"]);
                AddWorkloadItem(name, count);
            }
        }

        private void AddWorkloadItem(string name, int taskCount)
        {
            int maxBars = 10;
            var pnl = new UIPanel
            {
                Width = pnlWorkloadList.ClientSize.Width - 20,
                Height = 48,
                Margin = new Padding(10, 4, 10, 4),
                FillColor = Color.FromArgb(19, 35, 58),
                Radius = 6
            };

            var lblName = new UILabel
            {
                Text = name,
                Font = new Font("Microsoft YaHei", 10F),
                ForeColor = Color.FromArgb(230, 238, 247),
                Location = new Point(12, 6),
                AutoSize = true
            };

            var lblCount = new UILabel
            {
                Text = $"{taskCount} 单",
                Font = new Font("Microsoft YaHei", 9F),
                ForeColor = taskCount >= 5 ? Color.FromArgb(239, 68, 68) : Color.FromArgb(159, 179, 200),
                Location = new Point(pnl.Width - 80, 8),
                Size = new Size(65, 20),
                TextAlign = ContentAlignment.MiddleRight
            };

            // 进度条
            var barBack = new UIPanel
            {
                Width = pnl.Width - 100,
                Height = 10,
                Radius = 5,
                FillColor = Color.FromArgb(11, 22, 34),
                RectColor = Color.Transparent,
                Location = new Point(12, 30)
            };
            int barW = Math.Min((int)(barBack.Width * taskCount / (float)maxBars), barBack.Width);
            var barFill = new UIPanel
            {
                Width = barW > 0 ? barW : 4,
                Height = 10,
                Radius = 5,
                FillColor = taskCount >= 5 ? Color.FromArgb(239, 68, 68) : Color.FromArgb(46, 139, 255),
                RectColor = Color.Transparent,
                Location = new Point(0, 0),
                Dock = DockStyle.Left
            };
            barBack.Controls.Add(barFill);

            pnl.Controls.Add(lblName);
            pnl.Controls.Add(lblCount);
            pnl.Controls.Add(barBack);
            pnlWorkloadList.Controls.Add(pnl);
        }

        // ==================== 筛选 & 事件 ====================

        private string GetFilterValue(UIComboBox cmb, string allText)
        {
            string val = cmb.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(val) || val == allText)
                return null;
            return MaintenanceHelper.UrgencyToEn(val);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void WireEvents()
        {
            btnQuery.Click += BtnQuery_Click;
            btnReset.Click += BtnReset_Click;
            btnNewOrder.Click += BtnNewOrder_Click;
            dgvOrders.CellContentClick += DgvOrders_CellContentClick;
        }

        private void BtnNewOrder_Click(object sender, EventArgs e)
        {
            using (var form = new OrderEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void DgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvOrders.Columns["Column12"].Index)
                return;

            var row = dgvOrders.Rows[e.RowIndex];
            if (row.DataBoundItem is MaintenanceRecordDto dto)
            {
                ShowOperationMenu(dto);
            }
        }

        private void ShowOperationMenu(MaintenanceRecordDto dto)
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add($"工单号: {dto.RepairNo}").Enabled = false;
            menu.Items.Add(new ToolStripSeparator());

            // 通用操作：修改
            menu.Items.Add("修改工单信息", null, (s, ev) =>
            {
                using (var form = new OrderEditForm(dto.RecordId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                        LoadData();
                }
            });
            menu.Items.Add(new ToolStripSeparator());

            // 阶段相关操作
            switch (dto.ProgressStageText)
            {
                case "待分配":
                    menu.Items.Add("指派维修人", null, (s, ev) =>
                    {
                        using (var frm = new SelectEngineerForm(BLL))
                        {
                            if (frm.ShowDialog() == DialogResult.OK && frm.SelectedEngineerId > 0)
                            {
                                if (BLL.AssignRepairer(dto.RecordId, frm.SelectedEngineerId))
                                {
                                    UIMessageBox.Show($"工单 {dto.RepairNo} 已成功指派维修人");
                                    LoadData();
                                }
                                else
                                    UIMessageBox.Show("指派失败，请重试");
                            }
                        }
                    });
                    break;
                case "已指派":
                    menu.Items.Add("开始维修", null, (s, ev) =>
                    {
                        if (BLL.StartRepair(dto.RecordId))
                        {
                            UIMessageBox.Show($"工单 {dto.RepairNo} 已开始维修");
                            LoadData();
                        }
                        else
                            UIMessageBox.Show("操作失败，请重试");
                    });
                    break;
                case "处理中":
                    menu.Items.Add("完成维修", null, (s, ev) =>
                    {
                        if (BLL.CompleteRepair(dto.RecordId))
                        {
                            UIMessageBox.Show($"工单 {dto.RepairNo} 已完成维修");
                            LoadData();
                        }
                        else
                            UIMessageBox.Show("操作失败，请重试");
                    });
                    break;
                case "已完成":
                    menu.Items.Add("查看详情", null, (s, ev) =>
                        UIMessageBox.Show($"工单号: {dto.RepairNo}\n设备: {dto.EquipmentName}\n维修人: {dto.RepairerName}\n停机时长: {dto.DowntimeHours}h"));
                    break;
            }

            menu.Show(dgvOrders, dgvOrders.PointToClient(Cursor.Position));
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cmbUrgency.SelectedIndex = 0;
            cmbDept.SelectedIndex = 0;
            cmbSource.SelectedIndex = 0;
            cmbDateRange.SelectedIndex = 0;

            LoadData();
        }
    }
}
