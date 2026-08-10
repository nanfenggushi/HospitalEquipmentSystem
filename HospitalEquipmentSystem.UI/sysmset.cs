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



namespace HospitalEquipmentSystem.UI
{
    public partial class sysmset : UIForm
    {
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
        
         

        

        private void Form1_Load(object sender, EventArgs e)
        {
            // 用户管理表格
            dgvUser.Rows.Add("系统管理员", "admin", "管理员", "设备科");
            dgvUser.Rows.Add("张医生", "doctor_zhang", "医护", "心内科");
            dgvUser.Rows.Add("李维修", "repair_li", "维修员", "设备科");

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
            

             
         
    

    

