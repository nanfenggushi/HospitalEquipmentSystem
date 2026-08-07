using Sunny.UI;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalEquipment.Util
{
    /// <summary>
    /// 深色科技主题工具：将子窗体统一为维修管理模块的深色风格
    /// 背景色: 11,22,34 | 面板色: 19,35,58 | 边框色: 30,58,138 | 强调色: 27,111,214
    /// </summary>
    public static class ThemeHelper
    {
        // ==================== 主题色板 ====================
        public static readonly Color BgDark     = Color.FromArgb(11, 22, 34);
        public static readonly Color BgPanel    = Color.FromArgb(19, 35, 58);
        public static readonly Color BgHeader   = Color.FromArgb(15, 29, 46);
        public static readonly Color BorderBlue = Color.FromArgb(30, 58, 138);
        public static readonly Color AccentBlue = Color.FromArgb(27, 111, 214);
        public static readonly Color AccentHover= Color.FromArgb(46, 139, 255);
        public static readonly Color TextMain   = Color.FromArgb(230, 238, 247);
        public static readonly Color TextSub    = Color.FromArgb(159, 179, 200);
        public static readonly Color TextMuted  = Color.FromArgb(107, 130, 156);

        /// <summary>
        /// 对窗体应用深色主题（在 InitializeComponent 之后调用）
        /// </summary>
        public static void ApplyDarkTheme(Form form)
        {
            form.BackColor = BgDark;
            ApplyToControl(form);
        }

        /// <summary>
        /// 递归遍历控件树，按类型应用主题样式
        /// </summary>
        private static void ApplyToControl(Control ctrl)
        {
            // 注：派生类型必须排在基类型之前，否则会被基类型 case 提前拦截
            switch (ctrl)
            {
                // --- 日期/下拉/文本框（继承链: UIDatePicker→UIComboBox→UITextBox）---
                case UIDatePicker dp:
                    ApplyDatePickerTheme(dp);
                    break;

                case UIComboBox cmb:
                    ApplyComboBoxTheme(cmb);
                    break;

                case UITextBox txt:
                    ApplyTextBoxTheme(txt);
                    break;

                // --- 按钮 ---
                case UISymbolButton btn:
                    ApplySymbolButtonTheme(btn);
                    break;

                case UIButton uiBtn:
                    ApplyButtonTheme(uiBtn);
                    break;

                // --- 标签 ---
                case UISymbolLabel symLbl:
                    ApplySymbolLabelTheme(symLbl);
                    break;

                case UILabel lbl:
                    ApplyLabelTheme(lbl);
                    break;

                // --- 标题面板（继承自 UIPanel）---
                case UITitlePanel titlePnl:
                    ApplyTitlePanelTheme(titlePnl);
                    break;

                // --- DataGridView ---
                case UIDataGridView dgv:
                    ApplyDgvTheme(dgv);
                    break;

                // --- 列表/分页 ---
                case UIListBox listBox:
                    ApplyListBoxTheme(listBox);
                    break;

                case UIPagination pag:
                    pag.ButtonFillSelectedColor = AccentBlue;
                    break;

                // --- 通用面板（放最后，作为兜底）---
                case UIPanel pnl:
                    ApplyPanelTheme(pnl);
                    break;
            }

            foreach (Control child in ctrl.Controls)
            {
                ApplyToControl(child);
            }
        }

        // ==================== 各控件主题方法 ====================

        private static void ApplyPanelTheme(UIPanel pnl)
        {
            // 只替换白色/透明填充色为深色面板色
            if (IsLightColor(pnl.FillColor))
                pnl.FillColor = BgPanel;
            if (pnl.FillColor2 != Color.Transparent && pnl.FillColor2 != pnl.FillColor)
                pnl.FillColor2 = pnl.FillColor;
            if (IsLightColor(pnl.RectColor))
                pnl.RectColor = BorderBlue;
        }

        private static void ApplyTitlePanelTheme(UITitlePanel pnl)
        {
            if (IsLightColor(pnl.FillColor))
                pnl.FillColor = BgPanel;
            pnl.TitleColor = BgHeader;
            pnl.TitleForeColor = TextMain;
            if (IsLightColor(pnl.RectColor))
                pnl.RectColor = BorderBlue;
        }

        private static void ApplyLabelTheme(UILabel lbl)
        {
            if (lbl.ForeColor == Color.FromArgb(48, 48, 48) || lbl.ForeColor == Color.Black
                || lbl.ForeColor == Color.Gray)
                lbl.ForeColor = TextMain;
            lbl.BackColor = Color.Transparent;
        }

        private static void ApplySymbolLabelTheme(UISymbolLabel lbl)
        {
            if (lbl.ForeColor == Color.FromArgb(48, 48, 48) || lbl.ForeColor == Color.Black)
                lbl.ForeColor = TextMain;
        }

        private static void ApplySymbolButtonTheme(UISymbolButton btn)
        {
            if (IsLightColor(btn.FillColor) || btn.FillColor == Color.FromArgb(64, 128, 204))
            {
                btn.FillColor = AccentBlue;
                btn.FillColor2 = AccentBlue;
                btn.FillHoverColor = AccentHover;
                btn.FillPressColor = AccentBlue;
                btn.ForeColor = Color.White;
                btn.RectColor = AccentBlue;
            }
        }

        private static void ApplyButtonTheme(UIButton btn)
        {
            if (IsLightColor(btn.FillColor))
            {
                btn.FillColor = AccentBlue;
                btn.FillHoverColor = AccentHover;
                btn.ForeColor = Color.White;
                btn.RectColor = AccentBlue;
            }
        }

        private static void ApplyTextBoxTheme(UITextBox txt)
        {
            if (IsLightColor(txt.FillColor))
                txt.FillColor = BgHeader;
            if (txt.ForeColor == Color.FromArgb(48, 48, 48))
                txt.ForeColor = TextMain;
        }

        private static void ApplyComboBoxTheme(UIComboBox cmb)
        {
            if (IsLightColor(cmb.FillColor))
                cmb.FillColor = BgHeader;
            if (cmb.ForeColor == Color.FromArgb(48, 48, 48) || cmb.ForeColor == Color.Gray)
                cmb.ForeColor = TextMain;
            cmb.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cmb.ItemSelectForeColor = TextMain;
        }

        private static void ApplyDatePickerTheme(UIDatePicker dp)
        {
            if (IsLightColor(dp.FillColor))
                dp.FillColor = BgHeader;
            if (dp.ForeColor == Color.FromArgb(48, 48, 48))
                dp.ForeColor = TextMain;
        }

        private static void ApplyListBoxTheme(UIListBox listBox)
        {
            if (IsLightColor(listBox.FillColor))
                listBox.FillColor = BgPanel;
            listBox.ForeColor = TextMain;
        }

        /// <summary>
        /// 应用 DataGridView 深色主题样式
        /// </summary>
        public static void ApplyDgvTheme(UIDataGridView dgv)
        {
            dgv.BackgroundColor = BgPanel;
            dgv.GridColor = BorderBlue;
            dgv.EnableHeadersVisualStyles = false;

            var headerStyle = new DataGridViewCellStyle
            {
                BackColor = BgHeader,
                ForeColor = TextSub,
                Font = new Font("微软雅黑", 9F),
                SelectionBackColor = AccentBlue,
                SelectionForeColor = Color.White,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                WrapMode = DataGridViewTriState.True
            };
            dgv.ColumnHeadersDefaultCellStyle = headerStyle;

            var rowStyle = new DataGridViewCellStyle
            {
                BackColor = BgPanel,
                ForeColor = TextMain,
                Font = new Font("微软雅黑", 9F),
                SelectionBackColor = AccentBlue,
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.True
            };
            dgv.RowsDefaultCellStyle = rowStyle;

            var altStyle = new DataGridViewCellStyle
            {
                BackColor = BgHeader,
                ForeColor = TextMain,
                SelectionBackColor = AccentBlue,
                SelectionForeColor = Color.White
            };
            dgv.AlternatingRowsDefaultCellStyle = altStyle;

            var rowHeaderStyle = new DataGridViewCellStyle
            {
                BackColor = BgPanel,
                ForeColor = TextMain,
                Font = new Font("微软雅黑", 9F),
                SelectionBackColor = AccentBlue,
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.True
            };
            dgv.RowHeadersDefaultCellStyle = rowHeaderStyle;

            dgv.StripeOddColor = BgPanel;
            dgv.StripeEvenColor = BgHeader;
            dgv.ForeColor = TextMain;
        }

        /// <summary>
        /// 判断颜色是否为浅色（白色或接近白色）
        /// </summary>
        private static bool IsLightColor(Color c)
        {
            return c == Color.White
                || c == Color.Transparent
                || c.Name == "0"
                || (c.R > 200 && c.G > 200 && c.B > 200);
        }
    }
}
