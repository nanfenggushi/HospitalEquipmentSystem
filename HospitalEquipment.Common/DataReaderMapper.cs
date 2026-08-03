using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalEquipment.Util
{
    /// <summary>
    /// 数据读取器映射助手：把 SqlDataReader 的查询结果自动转成实体对象列表
    /// 同时提供在 Panel 中承载窗体、显示占位页的工具方法
    /// </summary>
    public static class DataReaderMapper
    {
        /// <summary>
        /// 把 SqlDataReader 的每一行映射成一个 T 类型对象，最后返回对象列表
        /// </summary>
        /// <typeparam name="T">目标实体类型（必须有无参构造函数）</typeparam>
        /// <param name="reader">数据库查询结果读取器</param>
        /// <returns>实体对象列表</returns>
        public static List<T> MapToList<T>(SqlDataReader reader) where T : new()
        {
            var list = new List<T>();

            // 反射获取 T 类型的所有公开属性
            var properties = typeof(T).GetProperties();

            // 逐行读取查询结果
            while (reader.Read())
            {
                T item = new T();

                // 遍历该实体的每个属性
                foreach (var prop in properties)
                {
                    // 如果查询结果里有这个列，并且值不是空（DBNull）
                    if (HasColumn(reader, prop.Name) && !reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                    {
                        // 把数据库的值赋给对象的对应属性
                        prop.SetValue(item, reader[prop.Name]);
                    }
                }

                // 把填充好的实体加入列表
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// 判断 SqlDataReader 的查询结果中是否存在指定名称的列
        /// </summary>
        /// <param name="reader">数据库查询结果读取器</param>
        /// <param name="columnName">要查找的列名</param>
        /// <returns>存在返回 true，不存在返回 false</returns>
        private static bool HasColumn(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 在指定 Panel 中显示 T 类型的窗体（泛型版）
        /// 自动清除 Panel 原有控件，将窗体嵌入 Panel 中显示
        /// </summary>
        /// <typeparam name="T">窗体类型，必须继承 Form 且有无参构造函数</typeparam>
        /// <param name="panel">要承载窗体的 Panel 容器</param>
        /// <param name="autoScale">是否自动缩放适配 Panel 大小</param>
        public static void ShowFormInPanel<T>(Panel panel, bool autoScale = false) where T : Form, new()
        {
            // 清空容器原有内容
            panel.Controls.Clear();

            // 创建窗体实例
            T form = new T();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.Manual;

            if (autoScale)
            {
                // 自动缩放模式：按比例缩放到适配 Panel
                float scaleX = (float)panel.ClientSize.Width / form.Width;
                float scaleY = (float)panel.ClientSize.Height / form.Height;
                float scale = Math.Min(scaleX, scaleY);

                // 只缩小不放大，防止模糊
                if (scale > 1.0f)
                {
                    scale = 1.0f;
                }

                form.Scale(new SizeF(scale, scale));

                // 居中显示
                form.Left = (panel.ClientSize.Width - form.Width) / 2;
                form.Top = (panel.ClientSize.Height - form.Height) / 2;
            }
            else
            {
                // 普通模式：Dock 填充
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
