using System;                          // 引用基础命名空间，这里主要是 System 类型相关（当前文件未直接用，保留引用）
using System.ComponentModel;           // 引用组件模型，组件相关类型在这里
using System.Drawing;                  // 引用绘图命名空间，Color、Size 在这里
using System.Windows.Forms;            // 引用 WinForms 控件，Cursor、Control 在这里
using Sunny.UI;                        // 引用 SunnyUI 控件库，UIUserControl 在这里
using HospitalEquipment.Model;         // 引用实体层，MaintenanceRecord 在这里

namespace HospitalEquipmentSystem.UI    // 声明当前代码所属的命名空间（界面层）
{
    /// <summary>
    /// 报警卡片控件：报警流里每一条数据对应一张卡片
    /// 卡片上显示：级别、内容（设备名+故障）、时间
    /// </summary>
    public partial class AlarmCard : UIUserControl    // 继承 SunnyUI 的 UIUserControl
    {
        /// <summary>
        /// 卡片点击事件：携带这张卡片对应的维修记录
        /// </summary>
        public event Action<MaintenanceRecord> Clicked;

        /// <summary>
        /// 这张卡片对应的维修记录（用于跳转明细列表）
        /// </summary>
        public MaintenanceRecord AlarmRecord { get; private set; }

        /// <summary>
        /// 构造函数：创建卡片时自动执行
        /// </summary>
        public AlarmCard()
        {
            // 初始化设计器里定义好的控件
            InitializeComponent();

            // 设置卡片背景颜色（深灰蓝色）
            this.FillColor = Color.FromArgb(44, 51, 66);

            // 设置卡片圆角大小
            this.Radius = 10;

            // 设置卡片默认尺寸
            this.Size = new Size(400, 84);

            // 整张卡片都做成可点击，并给鼠标手型提示
            this.Cursor = Cursors.Hand;
            lblLevel.Cursor = Cursors.Hand;
            lblInfo.Cursor = Cursors.Hand;
            lblTime.Cursor = Cursors.Hand;

            // 卡片本身和内部标签都响应点击，点击任意位置都能跳转
            WireClick(this);
            WireClick(lblLevel);
            WireClick(lblInfo);
            WireClick(lblTime);
        }

        /// <summary>
        /// 把报警信息填入卡片上的三个标签
        /// </summary>
        /// <param name="level">报警级别（特急/紧急/普通）</param>
        /// <param name="info">报警内容（设备名+故障描述）</param>
        /// <param name="time">报警时间</param>
        /// <param name="record">对应的维修记录，用于点击卡片后跳转明细</param>
        public void SetAlarmInfo(string level, string info, string time, MaintenanceRecord record = null)
        {
            // 把级别填入左上的级别标签
            lblLevel.Text = level;

            // 把内容填入中间的内容标签
            lblInfo.Text = info;

            // 把时间填入右下的时间标签
            lblTime.Text = time;

            // 保存对应的维修记录
            AlarmRecord = record;
        }

        /// <summary>
        /// 把控件点击统一转发为卡片的 Clicked 事件
        /// </summary>
        private void WireClick(Control c)
        {
            c.Click += (s, e) =>
            {
                if (AlarmRecord != null)
                {
                    Clicked?.Invoke(AlarmRecord);
                }
            };
        }
    }
}

