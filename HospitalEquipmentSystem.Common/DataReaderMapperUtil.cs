using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalEquipment.Util
{
    /// <summary>
    /// Panel 承载工具：在 Panel 中显示窗体或占位提示页（SwitchPages 使用）
    /// </summary>
    public static class DataReaderMapper
    {
        /// <summary>
        /// 在指定 Panel 中显示 T 类型的窗体（泛型版）
        /// 自动清除 Panel 原有控件，将窗体嵌入 Panel 中显示
        /// </summary>
        /// <typeparam name="T">窗体类型，必须继承 Form 且有无参构造函数</typeparam>
        /// <param name="panel">要承载窗体的 Panel 容器</param>
        /// <param name="autoScale">是否自动缩放适配 Panel 大小</param>
        public static void ShowFormInPanel<T>(Panel panel, bool autoScale = false) where T : Form, new()
        {
            ShowFormInPanel(panel, new T(), autoScale);
        }

        /// <summary>
        /// 在指定 Panel 中显示已经创建好的窗体实例（用于需要传初始参数的页面）
        /// </summary>
        /// <typeparam name="T">窗体类型，必须继承 Form</typeparam>
        /// <param name="panel">要承载窗体的 Panel 容器</param>
        /// <param name="form">要显示的窗体实例</param>
        /// <param name="autoScale">是否自动缩放适配 Panel 大小</param>
        public static void ShowFormInPanel<T>(Panel panel, T form, bool autoScale = false) where T : Form
        {
            if (form == null) return;

            // 先创建新页面并排好版，最后一次性显示，避免边排版边绘制造成的闪烁
            panel.SuspendLayout();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.Manual;
            // 先不显示，等新页面完全排版好后再一次性显示
            form.Visible = false;

            if (autoScale)
            {
                float scaleX = (float)panel.ClientSize.Width / form.Width;
                float scaleY = (float)panel.ClientSize.Height / form.Height;
                float scale = Math.Min(scaleX, scaleY);
                if (scale > 1.0f) scale = 1.0f;
                form.Scale(new SizeF(scale, scale));
                form.Left = (panel.ClientSize.Width - form.Width) / 2;
                form.Top = (panel.ClientSize.Height - form.Height) / 2;
            }
            else
            {
                form.Dock = DockStyle.Fill;
            }

            panel.Controls.Add(form);
            form.BringToFront();

            // 移除旧页面（不主动 Dispose，避免其后台定时器/异步任务在销毁后抛异常导致程序闪退）
            var oldControls = new System.Collections.Generic.List<Control>();
            foreach (Control c in panel.Controls)
            {
                if (c != form) oldControls.Add(c);
            }
            foreach (Control c in oldControls)
            {
                panel.Controls.Remove(c);
            }

            panel.ResumeLayout(false);
            panel.PerformLayout();

            // 布局完成后一次性显示新页面，减少闪烁
            form.Show();
        }

        /// <summary>
        /// 在 Panel 中显示占位提示页（用于功能开发中的页面）
        /// </summary>
        /// <param name="panel">要承载的 Panel 容器</param>
        /// <param name="message">提示文字</param>
        /// <param name="backColor">背景色</param>
        public static void ShowPlaceholder(Panel panel, string message, Color? backColor = null)
        {
            panel.Controls.Clear();
            panel.AutoScroll = false;

            Label lbl = new Label();
            lbl.Text = message;
            lbl.Font = new Font("微软雅黑", 18F);
            lbl.ForeColor = Color.Gray;
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;

            if (backColor.HasValue)
            {
                panel.BackColor = backColor.Value;
            }

            panel.Controls.Add(lbl);
        }
    }
}
