using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 支持自定义滚动条颜色的 FlowLayoutPanel
    /// </summary>
    public class ColoredFlowLayoutPanel : FlowLayoutPanel
    {
        public Color ScrollBarBackColor { get; set; } = Color.DimGray;
        public Color ScrollBarColor { get; set; } = Color.FromArgb(80, 160, 255);

        [DllImport("uxtheme.dll")]
        private static extern int SetWindowTheme(IntPtr hwnd, string appname, string idlist);

        [DllImport("user32.dll")]
        private static extern bool GetScrollInfo(IntPtr hwnd, int nBar, ref SCROLLINFO lpsi);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("user32.dll")]
        private static extern bool RedrawWindow(IntPtr hwnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

        private const int WM_NCPAINT = 0x85;
        private const int SB_VERT = 1;
        private const int SIF_RANGE = 0x1;
        private const int SIF_PAGE = 0x2;
        private const int SIF_POS = 0x4;
        private const uint RDW_INVALIDATE = 0x0001;
        private const uint RDW_FRAME = 0x0400;
        private const uint RDW_UPDATENOW = 0x0100;

        [StructLayout(LayoutKind.Sequential)]
        private struct SCROLLINFO
        {
            public int cbSize;
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            // 禁用视觉样式，使滚动条使用经典样式（便于自绘）
            SetWindowTheme(this.Handle, "", "");
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_NCPAINT)
            {
                DrawCustomScrollBar();
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_INVALIDATE | RDW_UPDATENOW);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_INVALIDATE | RDW_UPDATENOW);
        }

        private void DrawCustomScrollBar()
        {
            SCROLLINFO si = new SCROLLINFO();
            si.cbSize = Marshal.SizeOf(typeof(SCROLLINFO));
            si.fMask = SIF_RANGE | SIF_PAGE | SIF_POS;
            if (!GetScrollInfo(this.Handle, SB_VERT, ref si))
                return;

            // 没有垂直滚动条时跳过
            if (si.nMax <= 0 || si.nPage >= (si.nMax - si.nMin + 1))
                return;

            int scrollBarWidth = SystemInformation.VerticalScrollBarWidth;
            IntPtr hdc = GetWindowDC(this.Handle);
            if (hdc == IntPtr.Zero) return;

            try
            {
                using (Graphics g = Graphics.FromHdc(hdc))
                {
                    int height = this.Height;
                    int x = this.Width - scrollBarWidth;

                    // 画滚动条背景
                    using (Brush bgBrush = new SolidBrush(ScrollBarBackColor))
                    {
                        g.FillRectangle(bgBrush, x, 0, scrollBarWidth, height);
                    }

                    // 计算滑块位置和大小
                    int range = si.nMax - si.nMin + 1;
                    int visible = si.nPage;
                    int thumbHeight = Math.Max(30, (int)((float)visible / range * height));
                    int maxThumbY = height - thumbHeight;
                    int thumbY = 0;
                    if (range > visible)
                        thumbY = (int)((float)si.nPos / (range - visible) * maxThumbY);

                    // 画滑块
                    using (Brush thumbBrush = new SolidBrush(ScrollBarColor))
                    {
                        g.FillRectangle(thumbBrush, x + 2, thumbY, scrollBarWidth - 4, thumbHeight);
                    }
                }
            }
            finally
            {
                ReleaseDC(this.Handle, hdc);
            }
        }
    }
}
