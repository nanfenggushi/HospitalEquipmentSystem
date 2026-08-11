using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalEquipment.BLL;



namespace HospitalEquipmentSystem.UI
{
    public partial class sysmset : UIForm
    {
        // ===== 分页相关字段 =====
        private int currentPage = 1;
        private int pageSize = 5;
        private int totalCount = 0;
        private bool isBindingPagination = false;
        private readonly UserBLL userBLL = new UserBLL();

        public sysmset()
        {
            InitializeComponent();
            
            }
         
            private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
           {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
            }
        
         

        

        private async void Form1_Load(object sender, EventArgs e)
        {
            // 选中标签文字白色
             

             
            foreach (Control c in tabPageRole.Controls)
            {
                
                if (c is Label lbl)
                {
                    lbl
        .ForeColor = Color.White;
                }
            }
            // 用户管理表格 - 从数据库加载数据
            await LoadUsers();

            //角色权限文字
            lblRoleText.Text =
@"管理员        全部权限：设备管理 / 维修 / 借用 / 监控 / 统计 / 系统设置
医护          查看设备、申请借用、申报故障
维修员        接收工单、维修处理、填写结果";

            //操作日志
            dgvLog.Rows.Add("admin", "登录系统", "2026‑07‑30 08:00", "10.0.0.12");
            dgvLog.Rows.Add("doctor_zhang", "查看设备 EQ‑1003", "2026‑07‑30 09:12", "10.0.0.33");
            dgvLog.Rows.Add("repair_li", "维修工单 RP‑502 完成", "2026‑07‑29 16:40", "10.0.0.21");
            //系统参数
            LoadSystemParams();

            DataGridView dgv = dgvUser;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 48, 60);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251); dgv.CellMouseEnter += (s, ev) =>
            {
                if (ev.RowIndex >= 0 && !dgv.Rows[ev.RowIndex].Selected)
                {
                    dgv.Rows[ev.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(238, 242, 255);
                }
            };

            dgv.CellMouseLeave += (s, ev) =>
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.Selected)
                    {
                        row.DefaultCellStyle.BackColor = row.Index % 2 == 1
                            ? Color.FromArgb(249, 250, 251)
                            : Color.White;
                    }
                }
            };
            

        }

        #region 用户管理 - 数据库分页加载

        /// <summary>
        /// 从数据库分页加载用户数据到 dgvUser，并同步更新 uiPagination1
        /// </summary>
        private async Task LoadUsers()
        {
            try
            {
                // 调用 BLL 分页查询
                var result = await Task.Run(() => userBLL.GetPagedUsers(currentPage, pageSize));
                DataTable dt = result.users;
                totalCount = result.total;

                // 清空旧数据
                dgvUser.Rows.Clear();

                // 填充新数据
                foreach (DataRow row in dt.Rows)
                {
                    string realName = row["RealName"]?.ToString() ?? "";
                    string username = row["Username"]?.ToString() ?? "";
                    string role = RoleToCn(row["Role"]?.ToString() ?? "");
                    string deptName = row["DeptName"]?.ToString() ?? "";

                    dgvUser.Rows.Add(realName, username, role, deptName);
                }

                // 同步分页控件
                isBindingPagination = true;
                try
                {
                    uiPagination1.TotalCount = totalCount;
                    uiPagination1.PageSize = pageSize;
                    uiPagination1.ActivePage = currentPage;
                }
                finally
                {
                    isBindingPagination = false;
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"加载用户数据失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }

        /// <summary>
        /// 分页控件翻页事件
        /// </summary>
        private async void uiPagination1_PageChanged(object sender, object pagingSource, int pageIndex, int count)
        {
            if (isBindingPagination) return;

            currentPage = pageIndex;
            await LoadUsers();
        }

        /// <summary>
        /// 角色英文转中文（与 CellPainting 彩色标签匹配）
        /// </summary>
        private string RoleToCn(string role)
        {
            switch (role?.ToLower())
            {
                case "admin": return "管理员";
                case "doctor": return "医护";
                case "repair": return "维修员";
                case "technician": return "技术员";
                default: return role;
            }
        }

        #endregion

        #region 系统参数

        private void LoadSystemParams()
        {
            // 系统参数数据（后续可改为从数据库 SystemParams 表读取）
            var parameters = new (string param, string group, string value)[]
            {
                ("系统名称", "基础", "智慧医院设备管理平台"),
                ("报警阈值(%)", "监控", "90"),
                ("自动保养提醒", "保养", "开启"),
                ("数据保留(天)", "数据", "365"),
                ("允许移动端", "安全", "开启")
            };

            uiDataGridView1.Rows.Clear();
            foreach (var item in parameters)
            {
                uiDataGridView1.Rows.Add(item.param, item.group, item.value);
            }

            // 表格基础样式
            uiDataGridView1.BackgroundColor = Color.White;
            uiDataGridView1.BorderStyle = BorderStyle.None;
            uiDataGridView1.GridColor = Color.FromArgb(230, 232, 236);
            uiDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            uiDataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(50, 60, 80);
            uiDataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10.2F, FontStyle.Regular);
            uiDataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            uiDataGridView1.ColumnHeadersHeight = 40;
            uiDataGridView1.DefaultCellStyle.BackColor = Color.White;
            uiDataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            uiDataGridView1.DefaultCellStyle.Font = new Font("微软雅黑", 10.2F, FontStyle.Regular);
            uiDataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            uiDataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            uiDataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            uiDataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            uiDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void uiDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 为"分组"列绘制圆角标签
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                e.PaintBackground(e.ClipBounds, true);
                string groupText = e.Value?.ToString();
                if (string.IsNullOrEmpty(groupText))
                {
                    e.Handled = true;
                    return;
                }

                Color bgColor;
                Color foreColor;
                switch (groupText)
                {
                    case "基础":
                        bgColor = Color.FromArgb(225, 245, 230);
                        foreColor = Color.FromArgb(40, 180, 80);
                        break;
                    case "监控":
                        bgColor = Color.FromArgb(255, 235, 235);
                        foreColor = Color.FromArgb(230, 70, 70);
                        break;
                    case "保养":
                        bgColor = Color.FromArgb(255, 244, 224);
                        foreColor = Color.FromArgb(230, 150, 40);
                        break;
                    case "数据":
                        bgColor = Color.FromArgb(228, 237, 255);
                        foreColor = Color.FromArgb(70, 130, 230);
                        break;
                    case "安全":
                        bgColor = Color.FromArgb(240, 240, 240);
                        foreColor = Color.FromArgb(100, 100, 100);
                        break;
                    default:
                        bgColor = Color.LightGray;
                        foreColor = Color.Gray;
                        break;
                }

                float textWidth = e.Graphics.MeasureString(groupText, uiDataGridView1.Font).Width;
                int labelWidth = (int)textWidth + 32;
                labelWidth = Math.Max(70, labelWidth);

                int cellInnerWidth = e.CellBounds.Width;
                int leftOffset = (cellInnerWidth - labelWidth) / 2;

                Rectangle tagRect = new Rectangle(
                    e.CellBounds.X + leftOffset,
                    e.CellBounds.Y + 8,
                    labelWidth,
                    e.CellBounds.Height - 16
                );

                using (SolidBrush bgBrush = new SolidBrush(bgColor))
                using (GraphicsPath path = GetRoundedRect(tagRect, 10))
                using (StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                using (SolidBrush textBrush = new SolidBrush(foreColor))
                {
                    e.Graphics.FillPath(bgBrush, path);
                    e.Graphics.DrawString(groupText, uiDataGridView1.Font, textBrush, tagRect, sf);
                }
                e.Handled = true;
            }
        }

        #endregion

        private void btnLogout_Click(object sender, EventArgs e)
        {
             
        }

        
        

         

        private void dgvUser_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                e.PaintBackground(e.ClipBounds, true);
                string roleText = e.Value?.ToString();
                if (string.IsNullOrEmpty(roleText))
                {
                    e.Handled = true;
                    return;
                }

                Color bgColor;
                Color dotColor;
                switch (roleText)
                {
                    case "管理员":
                        bgColor = Color.FromArgb(225, 245, 230);
                        dotColor = Color.FromArgb(40, 180, 80);
                        break;
                    case "医护":
                        bgColor = Color.FromArgb(228, 237, 255);
                        dotColor = Color.FromArgb(70, 130, 230);
                        break;
                    case "维修员":
                        bgColor = Color.FromArgb(255, 244, 224);
                        dotColor = Color.FromArgb(230, 150, 40);
                        break;
                    case "技术员":
                        bgColor = Color.FromArgb(240, 235, 255);
                        dotColor = Color.FromArgb(150, 100, 220);
                        break;
                    default:
                        bgColor = Color.LightGray;
                        dotColor = Color.Gray;
                        break;
                }

                //测量文字，计算标签尺寸
                float textWidth = e.Graphics.MeasureString(roleText, dgvUser.Font).Width;
                int labelWidth = (int)textWidth + 32;
                labelWidth = Math.Max(70, labelWidth);

                //单元格内部水平居中
                int cellInnerWidth = e.CellBounds.Width;
                int leftOffset = (cellInnerWidth - labelWidth) / 2;

                Rectangle tagRect = new Rectangle(
                    e.CellBounds.X + leftOffset,
                    e.CellBounds.Y + 1,
                    labelWidth,
                    e.CellBounds.Height - 2
                );

                using (SolidBrush bgBrush = new SolidBrush(bgColor))
                using (GraphicsPath path = GetRoundedRect(tagRect, 12))
                using (SolidBrush dotBrush = new SolidBrush(dotColor))
                using (StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    e.Graphics.FillPath(bgBrush, path);
                    //圆点垂直居中
                    e.Graphics.FillEllipse(dotBrush,
                        tagRect.X + 8,
                        tagRect.Y + (tagRect.Height - 8) / 2,
                        8, 8);

                    Rectangle textRect = new Rectangle(
                        tagRect.X + 20,
                        tagRect.Y,
                        tagRect.Width - 22,
                        tagRect.Height
                    );
                    using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(40, 40, 40)))
                    {
                        e.Graphics.DrawString(roleText, dgvUser.Font, textBrush, textRect, sf);
                    }
                }
                e.Handled = true;
            }




        }

        private void uiDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
        }
    }
}
            

             
         
    

    

