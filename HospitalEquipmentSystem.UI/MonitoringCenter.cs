using Sunny.UI;                              // 引用 SunnyUI 控件库，界面用的 UIForm/UIProcessBar 等都在这里
using System;                                 // 引用基础命名空间，DateTime、Action 等在这里
using System.Drawing;                          // 引用绘图命名空间，Size 在这里
using System.Collections.Generic;             // 引用泛型集合，Queue<T>、Dictionary 等在这里
using System.Threading.Tasks;                 // 引用后台任务，Task.Run 在这里，用于异步查询数据库
using System.Windows.Forms;                   // 引用 WinForms，Timer、Application 等在这里
using HospitalEquipment.BLL;                  // 引用业务逻辑层，MonitorCenterBLL 在这里
using HospitalEquipment.Model;                // 引用实体层，MaintenanceRecord 在这里

namespace HospitalEquipmentSystem.UI           // 声明当前代码所属的命名空间（界面层）
{
    /// <summary>
    /// 设备监控中心主窗体
    /// 性能说明：数据库查询全部放在后台线程(Task.Run)，UI线程只做赋值，
    /// 时钟使用 System.Threading.Timer 后台触发，避免UI阻塞导致跳秒。
    /// </summary>
    public partial class MonitoringCenter : UIForm        // 主窗体类，继承 SunnyUI 的 UIForm
    {
        // ====== 业务逻辑层实例 ======
        private readonly MonitorCenterBLL _bll = new MonitorCenterBLL();    // 保存 BLL 层实例，UI 只通过它拿数据

        // 已加载的最大报警记录ID，用于增量查询（只查比它新的记录）
        private int _lastRecordId = 0;                                       // 记录上次加载到的最大报警ID

        // 最近一次报警显示/触发的时刻，用于"5秒无报警自动隐藏"判断
        private DateTime _lastAlarmTime = DateTime.Now;                      // 保存最近一次报警显示的时间

        // 报警栈：10秒定时器把新报警压入栈，2.5秒定时器弹出最新一条显示（后进先出）
        private Stack<MaintenanceRecord> _alarmStack = new Stack<MaintenanceRecord>();   // 报警显示队列


        // 后台刷新锁：防止上一次查询没结束，下一次又开始，造成UI卡顿叠加
        private volatile bool _refreshing = false;                           // 仪表盘刷新锁（后台查询时置 true）
        private volatile bool _checkingAlarm = false;

        private System.Threading.Timer clockTimer;
        /// <summary>
        /// 构造函数：创建主窗体时调用一次
        /// </summary>
        public MonitoringCenter()
        {
            // 初始化设计器里拖出来的所有控件
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件：窗体首次显示时执行一次
        /// 负责：启动动画、时钟、三个刷新定时器
        /// </summary>
        /// <param name="sender">触发事件的控件（这里就是窗体本身）</param>
        /// <param name="e">事件参数（这里不需要用到）</param>
        private void MonitoringCenter_Load(object sender, EventArgs e)
        {
            // 1. 启动黄色跳动实心圆动画
            StartPulseAnimation();

            // 2. 先给所有统计控件设置默认显示值（0% / 0），避免查询返回前界面空白
            SetDefaultStats();
            //先检测一下报警数据
            CheckNewAlarmsAsync();
            // 3. 右上角实时时钟：后台线程定时器，UI 再忙也准点触发
            clockTimer = new System.Threading.Timer(_ =>
            {
                // 跨线程更新标签，BeginInvoke 是异步非阻塞的
                uiLabel3.BeginInvoke(new Action(() =>
                    uiLabel3.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }, null, 0, 1000);

            // 4. 报警流区域初始高度（足以显示一张卡片），等数据出现后再向下变大，直到 uiPanel9 上方停止
       

            // 4.1 启用自动滚动：内容超出可视区域（封顶后数据继续增加）才出现滚动条
            uiFlowLayoutPanel1.AutoScroll = true;

            // 5. uiPanel9 报警浮窗初始隐藏（等有报警才显示）
            uiPanel9.Visible = false;

            // 6. 实时报警数初始显示 0（等 10 秒报警流检测到数据后再更新）
            uiLabel10.Text = "0";

            // 用 try-catch 包住初始化逻辑，数据库连不上时不让程序崩溃
            try
            {
                // 启动时先后台刷新一次所有仪表盘数据
                RefreshAllAsync();

                // ==== 定时器A：每 2 秒后台刷新仪表盘统计 ====
                Timer twoSecTimer = new Timer();
                twoSecTimer.Interval = 2000;
                twoSecTimer.Tick += (s, ev) => RefreshAllAsync();
                twoSecTimer.Start();

                // ==== 定时器B：每 2.5 秒从栈取一条报警显示到 uiPanel9 和报警流 ====
                Timer alarmShowTimer = new Timer();
                alarmShowTimer.Interval = 2500;
                alarmShowTimer.Tick += (s, ev) => ShowNextAlarm();
                alarmShowTimer.Start();

                // ==== 定时器C：每 10 秒后台增量检测新报警 ====
                Timer tenSecTimer = new Timer();
                tenSecTimer.Interval = 10000;
                tenSecTimer.Tick += (s, ev) => CheckNewAlarmsAsync();
                tenSecTimer.Start();
            }
            catch (Exception ex)
            {
                // 在报警流里加一条提示卡片，方便看到失败原因
                AddAlarmCard("系统", "初始化失败: " + ex.Message, DateTime.Now.ToString("HH:mm:ss"));
            }
        }

        // ==================== 启动默认值 ====================

        /// <summary>
        /// 给所有统计控件设置默认显示值，保证窗体一打开就有内容
        /// 真实数据由后台异步查询完成后覆盖
        /// </summary>
        private void SetDefaultStats()
        {
            // 面板1：设备在线率默认 0%
            uiLabel5.Text = "0%";

            // 面板1：在线率进度条默认 0
            uiProcessBar1.Value = 0;

            // 面板2：系统负载默认 0%
            uiLabel6.Text = "0%";

            // 面板2：负载进度条默认 0
            uiProcessBar2.Value = 0;

            // 面板3：供电稳定度默认 0%
            uiLabel8.Text = "0%";

            // 面板3：供电进度条默认 0
            uiProcessBar3.Value = 0;

            // 面板5：设备总数默认 0
            uiLabel13.Text = "0";

            // 面板6：故障/维修中默认 0
            uiLabel15.Text = "0";

            // 面板7：待处理工单默认 0
            uiLabel17.Text = "0";

            // 面板8：借用中默认 0
            uiLabel19.Text = "0";

            // 分类分布：超声设备默认 0
            uiLabel22.Text = "0";

            // 分类分布：超声进度条默认 0
            uiProcessBar4.Value = 0;

            // 分类分布：监护设备默认 0
            uiLabel23.Text = "0";

            // 分类分布：监护进度条默认 0
            uiProcessBar5.Value = 0;

            // 分类分布：影像设备默认 0
            uiLabel25.Text = "0";

            // 分类分布：影像进度条默认 0
            uiProcessBar6.Value = 0;

            // 分类分布：生命支持设备默认 0
            uiLabel27.Text = "0";

            // 分类分布：生命支持进度条默认 0
            uiProcessBar7.Value = 0;

            // 分类分布：检验设备默认 0
            uiLabel29.Text = "0";

            // 分类分布：检验进度条默认 0
            uiProcessBar8.Value = 0;
        }


        // ==================== 报警流动态高度 ====================

        /// <summary>
        /// 规则：卡片越多面板越长，但封顶在 uiPanel9 上方，不再变大
        /// </summary>
        private void UpdateFlowPanelHeight()
        {
            // 没有卡片就不延伸
            if (uiFlowLayoutPanel1.FlowLayoutPanel.Controls.Count == 0)
            {
                return;
            }

            // 取最新一张卡片的实际高度作为延伸量，保证延伸大小和卡片控件大小一致
            int cardHeight = uiFlowLayoutPanel1.FlowLayoutPanel.Controls[0].Height;

            // 新面板高度 = 当前高度 + 一条数据的高度（每次只延伸一条数据）
           
            // 封顶：uiPanel9 顶部 - 本面板顶部 - 5 像素间距
            int maxPanelHeight = uiPanel9.Top - uiTitlePanel1.Top - 20;
            if (uiTitlePanel1.Height < maxPanelHeight)
            {
                uiTitlePanel1.Height += cardHeight;
            }
             

        }
        // ==================== 黄色跳动实心圆 ====================

        /// <summary>
        /// 在 pictureBox1 上绘制黄色实心圆，并让它每 1.5 秒放大缩小一次（心跳效果）
        /// </summary>
        private void StartPulseAnimation()
        {
            // 清掉 pictureBox 原有的图片，改用自己的绘制
            pictureBox1.Image = null;

            // 把背景设为透明，让黄色圆直接浮在窗体背景上
            pictureBox1.BackColor = System.Drawing.Color.Transparent;

            // 通过反射开启 PictureBox 的双缓冲，避免绘制时闪烁
            pictureBox1.GetType().InvokeMember("DoubleBuffered",              // 查找 DoubleBuffered 属性
                System.Reflection.BindingFlags.SetProperty |                  // 表示要设置属性值
                System.Reflection.BindingFlags.Instance |                     // 表示这是实例属性
                System.Reflection.BindingFlags.NonPublic,                     // 该属性是 protected 的，需要非公开标志
                null, pictureBox1,                                            // 目标对象是 pictureBox1
                new object[] { true });                                       // 把属性值设为 true

            // 创建黄色实心画刷，之后绘制圆都用它
            System.Drawing.SolidBrush _yb = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);

            // 当前圆的半径，初始 8 像素（正常大小）
            float _r = 8f;

            // 当前跳动阶段：0=静止，1=放大中，2=缩小中
            int _phase = 0;

            // 动画帧定时器：每 30 毫秒刷新一次画面（约 33 帧/秒）
            Timer anim = new Timer();                        // 创建动画定时器
            anim.Interval = 30;                              // 设置间隔 30 毫秒
            anim.Tick += (s, ev) =>                          // 注册每次触发时的处理逻辑
            {
                // 如果处于放大阶段
                if (_phase == 1)
                {
                    // 半径每次增加 0.4 像素
                    _r += 0.4f;
                    // 当半径达到最大 12 像素时
                    if (_r >= 12f)
                    {
                        // 固定在最大半径
                        _r = 12f;
                        // 切换到缩小阶段
                        _phase = 2;
                    }
                }
                // 否则如果处于缩小阶段
                else if (_phase == 2)
                {
                    // 半径每次减小 0.15 像素（缩小得慢一点，更柔和）
                    _r -= 0.15f;
                    // 当半径回到最小 8 像素时
                    if (_r <= 8f)
                    {
                        // 固定在最小半径
                        _r = 8f;
                        // 回到静止阶段
                        _phase = 0;
                    }
                }
                // 请求控件重绘，触发 Paint 事件
                pictureBox1.Invalidate();
            };
            anim.Start();                                    // 启动动画定时器

            // 跳动触发定时器：每 1.5 秒触发一次跳动
            Timer trigger = new Timer();                     // 创建触发定时器
            trigger.Interval = 1500;                         // 设置间隔 1500 毫秒
            trigger.Tick += (s, ev) =>                       // 注册每次触发时的处理逻辑
            {
                // 只有处于静止阶段才触发新的跳动
                if (_phase == 0)
                {
                    // 切换到放大阶段
                    _phase = 1;
                }
            };
            trigger.Start();                                 // 启动触发定时器

            // 绘制事件：每次重绘时执行
            pictureBox1.Paint += (s, ev) =>                  // 注册绘制逻辑
            {
                // 开启抗锯齿，让圆形边缘平滑
                ev.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // 计算圆的中心 X 坐标（控件宽度的一半）
                int cx = pictureBox1.Width / 2;

                // 计算圆的中心 Y 坐标（控件高度的一半）
                int cy = pictureBox1.Height / 2;

                // 用黄色画刷画实心圆：左上角(cx-r, cy-r)，直径 2r
                ev.Graphics.FillEllipse(_yb, cx - _r, cy - _r, _r * 2, _r * 2);
            };
        }

        // ==================== 报警流（10秒，后台查询） ====================

        /// <summary>
        /// 增量检测新报警：数据库查询放后台线程，避免阻塞UI
        /// 查到新数据后回到UI线程更新报警流 + 放入队列
        /// </summary>
        private void CheckNewAlarmsAsync()
        {
            // 上次还没查完就跳过，避免任务堆积
            if (_checkingAlarm)
            {
                // 直接返回，不再发起新的查询
                return;
            }

            // 标记正在查询，防止重复并发查询
            _checkingAlarm = true;

            // 放到后台线程执行数据库查询
            Task.Run(() =>
            {
                // 用 try-finally 保证锁一定被释放
                try
                {
                    // 在后台线程查询比 _lastRecordId 新的报警记录
                    var list = _bll.GetNewAlarms(_lastRecordId);

                    // 如果有新的报警
                    if (list.Count > 0)
                    {
                        // 回到 UI 线程更新控件
                        this.BeginInvoke(new Action(() =>
                        {
                            // 把实时报警数设置为本次检测到的报警条数
                            SetLabel(uiLabel10, list.Count.ToString());

                            // 遍历每一条新报警
                            foreach (var r in list)
                            {

                                // 把这条报警放入栈，等 uiPanel9 每 2.5 秒取一条显示
                                _alarmStack.Push(r);

                                // 更新已加载的最大 ID，下次只查更新的记录
                                if (r.RecordId > _lastRecordId)
                                {
                                    _lastRecordId = r.RecordId;
                                }
                            }
                        }));
                    }
                }
                catch
                {
                    // 数据库异常静默处理，不弹窗不崩溃
                }
                finally
                {
                    // 无论成功失败都释放锁，允许下一次查询
                    _checkingAlarm = false;
                }
            });
        }

        // ==================== uiPanel9 逐条显示（2.5秒，纯UI） ====================

        /// <summary>
        /// 从队列取一条报警显示到 uiPanel9；队列空且5秒无报警则隐藏
        /// </summary>
        private void ShowNextAlarm()
        {
            // 如果栈里还有报警
            if (_alarmStack.Count > 0)
            {
                // 取出栈的一条报警
                var r = _alarmStack.Pop();

                // 拼接显示文本：故障描述 · 级别级 · 时间，例如"屏幕花屏 · 紧急级 · 10:18:28"
                string detail = r.FaultDesc + " · " + GetLevel(r.Urgency) + "级 · " + r.ReportTime.ToString("HH:mm:ss");

                // 往左侧报警流添加一张卡片（级别、设备名+故障、时间），逐条显示
                AddAlarmCard(GetLevel(r.Urgency), r.EquipmentName + "·" + r.FaultDesc, r.ReportTime.ToString("HH:mm:ss"));

                // 左侧符号标签显示"新报警：设备名"
                uiSymbolLabel1.Text = "新报警：" + r.EquipmentName;

                // 右侧文本标签显示"故障描述 · 级别级 · 时间"
                uiLabel31.Text = detail;

                // 显示 uiPanel9 面板
                uiPanel9.Visible = true;

                // 把面板置顶，避免被其他控件盖住
                uiPanel9.BringToFront();

                // 记录本次报警显示时间，用于"5秒无报警自动隐藏"判断
                _lastAlarmTime = DateTime.Now;
            }
            // 否则（队列空了），检查距上次报警是否超过 5 秒
            else if ((DateTime.Now - _lastAlarmTime).TotalSeconds > 5)
            {
                // 超过 5 秒没有新报警，隐藏 uiPanel9
                uiPanel9.Visible = false;
            }
        }

        // ==================== 仪表盘刷新（2秒，后台查询） ====================

        /// <summary>
        /// 刷新全部仪表盘统计：数据库查询放后台线程，UI线程只做赋值
        /// 结果通过 BeginInvoke 回到UI线程更新控件
        /// </summary>
        private void RefreshAllAsync()
        {
            // 上次还没查完就跳过，避免任务堆积
            if (_refreshing)
            {
                // 直接返回，不再发起新的查询
                return;
            }

            // 标记正在查询，防止重复并发查询
            _refreshing = true;

            // 放到后台线程执行数据库查询
            Task.Run(() =>
            {
                // 用 try-finally 保证锁一定被释放
                try
                {
                    // 在后台线程查询全部仪表盘统计数据
                    var s = _bll.GetDashboardStats();

                    // 在后台线程查询各分类设备数量
                    var cats = _bll.GetCategoryCounts();

                    // 回到 UI 线程更新全部控件
                    this.BeginInvoke(new Action(() =>
                    {
                        // 设备总数，防止除零时取 1
                        int total = s.TotalEquipment > 0 ? s.TotalEquipment : 1;

                        // 面板1：设备在线率 = (总数-待维修)/总数*100
                        int onlineRate = (int)((decimal)(s.TotalEquipment - s.PendingMaintenance) / total * 100);

                        // 显示在线率数值（带百分号）
                        SetLabel(uiLabel5, onlineRate + "%");

                        // 设置在线率进度条
                        SetBar(uiProcessBar1, onlineRate);

                        // 面板2：系统负载 = 使用中设备/总数*100
                        int loadRate = (int)((decimal)s.InUseEquipment / total * 100);

                        // 显示系统负载数值（带百分号）
                        SetLabel(uiLabel6, loadRate + "%");

                        // 设置系统负载进度条
                        SetBar(uiProcessBar2, loadRate);

                        // 面板3：供电稳定度（从数据库统计，暂无供电监测表时按可用设备占比计算）
                        SetLabel(uiLabel8, s.PowerStability + "%");

                        // 设置供电稳定度进度条
                        SetBar(uiProcessBar3, s.PowerStability);

                        // 面板5：显示设备总数
                        SetLabel(uiLabel13, s.TotalEquipment.ToString());

                        // 面板6：显示故障/维修中设备数
                        SetLabel(uiLabel15, s.MaintenanceCount.ToString());

                        // 面板7：显示待处理工单数
                        SetLabel(uiLabel17, s.PendingOrderCount.ToString());

                        // 面板8：显示借用中设备数
                        SetLabel(uiLabel19, s.BorrowedCount.ToString());

                        // 从分类字典中取各分类设备数量
                        int us = Gc(cats, "超声设备");     // 超声设备数量
                        int mo = Gc(cats, "监护设备");     // 监护设备数量
                        int im = Gc(cats, "影像");         // 影像设备数量
                        int ls = Gc(cats, "生命支持");     // 生命支持设备数量
                        int lb = Gc(cats, "检验设备");     // 检验设备数量

                        // 计算分类总数，防止除零时取 1
                        int safe = (us + mo + im + ls + lb) > 0 ? (us + mo + im + ls + lb) : 1;

                        // 超声设备：显示个数
                        SetLabel(uiLabel22, us.ToString());

                        // 超声设备：显示占比进度条
                        SetBar(uiProcessBar4, (int)((decimal)us / safe * 100));

                        // 监护设备：显示个数
                        SetLabel(uiLabel23, mo.ToString());

                        // 监护设备：显示占比进度条
                        SetBar(uiProcessBar5, (int)((decimal)mo / safe * 100));

                        // 影像设备：显示个数
                        SetLabel(uiLabel25, im.ToString());

                        // 影像设备：显示占比进度条
                        SetBar(uiProcessBar6, (int)((decimal)im / safe * 100));

                        // 生命支持设备：显示个数
                        SetLabel(uiLabel27, ls.ToString());

                        // 生命支持设备：显示占比进度条
                        SetBar(uiProcessBar7, (int)((decimal)ls / safe * 100));

                        // 检验设备：显示个数
                        SetLabel(uiLabel29, lb.ToString());

                        // 检验设备：显示占比进度条
                        SetBar(uiProcessBar8, (int)((decimal)lb / safe * 100));
                    }));
                }
                catch
                {
                    // 数据库异常静默处理，不弹窗不崩溃
                }
                finally
                {
                    // 无论成功失败都释放锁，允许下一次刷新
                    _refreshing = false;
                }
            });
        }

        // ==================== UI 帮助方法 ====================

        /// <summary>
        /// 线程安全地给 UILabel 赋值
        /// </summary>
        /// <param name="lbl">要设置文本的标签控件</param>
        /// <param name="t">要设置的文本内容</param>
        private void SetLabel(UILabel lbl, string t)
        {
            // 如果当前不在 UI 线程上
            if (lbl.InvokeRequired)
            {
                // 通过 Invoke 回到 UI 线程再赋值
                lbl.Invoke(new Action(() => lbl.Text = t));
            }
            else
            {
                // 已经在 UI 线程，直接赋值
                lbl.Text = t;
            }
        }

        /// <summary>
        /// 线程安全地给 UIProcessBar 设置进度值
        /// </summary>
        /// <param name="bar">要设置进度的进度条控件</param>
        /// <param name="v">进度值（0~100，越界会自动夹取）</param>
        private void SetBar(UIProcessBar bar, int v)
        {
            // 如果进度值小于 0，强制为 0
            if (v < 0)
            {
                v = 0;
            }

            // 如果进度值大于 100，强制为 100
            if (v > 100)
            {
                v = 100;
            }

            // 如果当前不在 UI 线程上
            if (bar.InvokeRequired)
            {
                // 通过 Invoke 回到 UI 线程再赋值
                bar.Invoke(new Action(() => bar.Value = v));
            }
            else
            {
                // 已经在 UI 线程，直接赋值
                bar.Value = v;
            }
        }

        /// <summary>
        /// 往实时报警流（uiFlowLayoutPanel1）添加一张报警卡片，最多保留 20 条
        /// </summary>
        /// <param name="level">报警级别（特急/紧急/普通）</param>
        /// <param name="info">报警内容（设备名+故障描述）</param>
        /// <param name="time">报警时间</param>
        private void AddAlarmCard(string level, string info, string time)
        {
            // 创建一张新的报警卡片
            var card = new AlarmCard();

            // 把级别、内容、时间填入卡片
            card.SetAlarmInfo(level, info, time);

            // 卡片宽度跟随报警流面板宽度（留 20 像素边距）
            card.Width = uiFlowLayoutPanel1.ClientSize.Width - 20;

            // 把卡片添加到报警流面板（SunnyUI 内部真正的 FlowLayoutPanel）
            uiFlowLayoutPanel1.Add(card);

            // 新卡片放到最前面，让最新报警显示在顶部
            uiFlowLayoutPanel1.FlowLayoutPanel.Controls.SetChildIndex(card, 0);

            // 每次加入数据后，滚动条回到最上面
            uiFlowLayoutPanel1.FlowLayoutPanel.AutoScrollPosition = new Point(0, 0);

            // 如果卡片超过 20 张，移除最早的一张
            if (uiFlowLayoutPanel1.FlowLayoutPanel.Controls.Count > 20)
            {
                // 最新卡片在索引 0，最早的卡片在最后
                uiFlowLayoutPanel1.FlowLayoutPanel.Controls.RemoveAt(uiFlowLayoutPanel1.FlowLayoutPanel.Controls.Count - 1);
            }

            // 卡片数量变化后，动态调整面板高度（延伸到 uiPanel9 上方停止）
            UpdateFlowPanelHeight();
        }

        /// <summary>
        /// 把数据库里的英文紧急级别翻译成中文
        /// </summary>
        /// <param name="u">英文级别（Urgent/Normal/Low）</param>
        /// <returns>中文级别（特急/紧急/普通）</returns>
        private string GetLevel(string u)
        {
            // 如果是 Urgent，返回特急
            if (u == "Urgent")
            {
                return "特急";
            }

            // 如果是 Normal，返回紧急
            if (u == "Normal")
            {
                return "紧急";
            }

            // 如果是 Low，返回普通
            if (u == "Low")
            {
                return "普通";
            }

            // 其他情况原样返回
            return u;
        }

        /// <summary>
        /// 安全地从分类字典里取值，键不存在时返回 0
        /// </summary>
        /// <param name="d">分类名称到数量的字典</param>
        /// <param name="k">要查找的分类名</param>
        /// <returns>该分类的设备数量</returns>
        private int Gc(Dictionary<string, int> d, string k)
        {
            // 如果字典里存在这个分类
            if (d.ContainsKey(k))
            {
                // 返回它的数量
                return d[k];
            }

            // 不存在则返回 0
            return 0;
        }
    }
}















