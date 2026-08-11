using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.Common
{
    /// <summary>
    /// 高性能通用页面管理器 (支持页面缓存，防闪退，秒开)
    /// 彻底解耦：支持任意容器(Panel/UIPanel等)，支持任意页面类型(Form/UserControl等)
    /// </summary>
    public static class PageManager
    {
        // 核心：页面缓存池。以类型为 Key，支持 Form、UserControl 等任意 Control
        private static readonly Dictionary<Type, Control> PageCache = new Dictionary<Type, Control>();

        /// <summary>
        /// 显示无参构造页面（优先从缓存加载）
        /// </summary>
        /// <typeparam name="T">页面类型(Form或UserControl)</typeparam>
        /// <param name="container">承载页面的容器(如Panel, UIPanel)</param>
        /// <param name="autoScale">是否自动缩放</param>
        public static void ShowPage<T>(Control container, bool autoScale = false) where T : Control, new()
        {
            Type type = typeof(T);

            // 1. 如果缓存中已有该页面，直接显示，实现秒切
            if (PageCache.TryGetValue(type, out Control existingPage) && !existingPage.IsDisposed)
            {
                SwitchToPage(container, existingPage);
                return;
            }

            // 2. 如果没有，则创建新页面
            T newPage = new T();
            SetupAndShowPage(container, newPage, autoScale);
            PageCache[type] = newPage; // 加入缓存
        }

        /// <summary>
        /// 显示带参数实例化的页面 (如告警跳转传递参数)
        /// </summary>
        public static void ShowPage<T>(Control container, T page, bool autoScale = false) where T : Control
        {
            if (page == null) return;
            Type type = typeof(T);

            // 如果该类型页面已经存在，为了加载带有新参数的页面，我们需要替换它
            if (PageCache.TryGetValue(type, out Control oldPage))
            {
                if (oldPage != page && !oldPage.IsDisposed)
                {
                    oldPage.Visible = false; // 立即隐藏

                    // 延迟销毁旧页面，完美避开后台任务导致的闪退。增加句柄判断防止报错
                    if (container.IsHandleCreated)
                    {
                        container.BeginInvoke(new Action(() => DisposePage(oldPage)));
                    } else
                    {
                        DisposePage(oldPage);
                    }
                }
            }

            SetupAndShowPage(container, page, autoScale);
            PageCache[type] = page; // 更新缓存
        }

        /// <summary>
        /// 统一的页面初始化与显示逻辑
        /// </summary>
        private static void SetupAndShowPage(Control container, Control page, bool autoScale)
        {
            container.SuspendLayout();

            // 如果传入的是窗体(Form)，需要进行窗体专属的特殊处理，如果是UserControl则不需要
            if (page is Form form)
            {
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.StartPosition = FormStartPosition.Manual;
            }

            if (autoScale)
            {
                float scaleX = (float)container.ClientSize.Width / page.Width;
                float scaleY = (float)container.ClientSize.Height / page.Height;
                float scale = Math.Min(scaleX, scaleY);
                if (scale > 1.0f) scale = 1.0f;
                page.Scale(new SizeF(scale, scale));
                page.Left = (container.ClientSize.Width - page.Width) / 2;
                page.Top = (container.ClientSize.Height - page.Height) / 2;
            } else
            {
                page.Dock = DockStyle.Fill;
            }

            container.Controls.Add(page);
            SwitchToPage(container, page);

            container.ResumeLayout(true);
        }

        /// <summary>
        /// 切换显示指定的页面，隐藏其余所有缓存的页面
        /// </summary>
        private static void SwitchToPage(Control container, Control targetPage)
        {
            foreach (Control ctrl in container.Controls)
            {
                if (ctrl == targetPage)
                {
                    ctrl.Visible = true;
                    ctrl.BringToFront(); // 目标窗体显示
                }
                // 仅隐藏窗体、用户控件或占位符，不干涉容器内自带的其他控件（如边框线、背景图等）
                else if (ctrl is Form || ctrl is UserControl || ctrl.Name == "DevPlaceholder")
                {
                    ctrl.Visible = false; // 其他页面仅仅隐藏，不销毁！
                }
            }
        }

        /// <summary>
        /// 显示开发中的占位提示页
        /// </summary>
        public static void ShowPlaceholder(Control container, string message, Color? backColor = null)
        {
            // 隐藏现有所有窗体/用户控件
            foreach (Control ctrl in container.Controls)
            {
                if (ctrl is Form || ctrl is UserControl)
                {
                    ctrl.Visible = false;
                }
            }

            // 查找是否已经存在占位符，避免重复创建
            Label placeholder = null;
            foreach (Control ctrl in container.Controls)
            {
                if (ctrl is Label lbl && ctrl.Name == "DevPlaceholder")
                {
                    placeholder = lbl;
                    break;
                }
            }

            if (placeholder == null)
            {
                placeholder = new Label {
                    Name = "DevPlaceholder",
                    Font = new Font("微软雅黑", 18F),
                    ForeColor = Color.Gray,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                container.Controls.Add(placeholder);
            }

            placeholder.Text = message;
            if (backColor.HasValue) placeholder.BackColor = backColor.Value;

            placeholder.Visible = true;
            placeholder.BringToFront();
        }

        /// <summary>
        /// 注销/切换用户时调用此方法，清理所有缓存的页面
        /// </summary>
        public static void ClearCache()
        {
            foreach (var kvp in PageCache)
            {
                DisposePage(kvp.Value);
            }
            PageCache.Clear();
        }

        /// <summary>
        /// 安全销毁页面的通用方法
        /// </summary>
        private static void DisposePage(Control page)
        {
            if (page != null && !page.IsDisposed)
            {
                try
                {
                    if (page is Form f) f.Close(); // Form必须先Close再Dispose
                    page.Dispose();
                } catch { }
            }
        }
    }
}