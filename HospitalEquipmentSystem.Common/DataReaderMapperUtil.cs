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
            panel.Controls.Clear();

            T form = new T();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.Manual;

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
