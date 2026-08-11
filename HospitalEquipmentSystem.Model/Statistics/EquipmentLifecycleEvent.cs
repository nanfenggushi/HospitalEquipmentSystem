using System;

namespace HospitalEquipment.Model.Dashboard
{
    /// <summary>
    /// 设备全生命周期事件（"一机一档"时间轴条目）
    /// 聚合：建档/入库/维修/借用/归还 等设备所有关键节点
    /// </summary>
    public class EquipmentLifecycleEvent
    {
        /// <summary>设备ID（用于山海鲸跨数据源联动过滤）</summary>
        public int EquipmentId { get; set; }

        /// <summary>设备名称（用于联动展示）</summary>
        public string EquipmentName { get; set; }

        /// <summary>事件发生时间</summary>
        public DateTime EventTime { get; set; }

        /// <summary>事件类型代码：Purchase建档/Inbound入库/Repair报修/RepairDone维修完成/Borrow借用/Return归还/Scrapped报废</summary>
        public string EventType { get; set; }

        /// <summary>事件类型中文显示</summary>
        public string EventTypeText { get; set; }

        /// <summary>事件标题（如：入库、报修）</summary>
        public string Title { get; set; }

        /// <summary>事件详情描述</summary>
        public string Detail { get; set; }

        /// <summary>操作人/关联人</summary>
        public string OperatorName { get; set; }
    }
}
