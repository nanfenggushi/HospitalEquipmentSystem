using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 维修工单实体类（对应数据库 MaintenanceRecords 表）
    /// 报警流、uiPanel9 浮窗的主要数据来源
    /// </summary>
    public class MaintenanceRecord
    {
        /// <summary>维修记录主键ID（用于增量检测）</summary>
        public int RecordId { get; set; }

        /// <summary>关联的设备ID</summary>
        public int EquipmentId { get; set; }

        /// <summary>维修单号（如 BX-2026-0715-01）</summary>
        public string RepairNo { get; set; }

        /// <summary>报修人用户ID</summary>
        public int? ReporterId { get; set; }

        /// <summary>报修科室ID</summary>
        public int? ReportDeptId { get; set; }

        /// <summary>故障描述（如 屏幕花屏 / 探头异常）</summary>
        public string FaultDesc { get; set; }

        /// <summary>故障类型（电气故障/机械故障/软件故障）</summary>
        public string FaultType { get; set; }

        /// <summary>紧急程度：Urgent特急 / Normal紧急 / Low普通</summary>
        public string Urgency { get; set; }

        /// <summary>进度阶段：Pending待派单 / Assigned已派单 / InProgress维修中 / Done已完成</summary>
        public string ProgressStage { get; set; }

        /// <summary>维修负责人用户ID</summary>
        public int? AssignedTo { get; set; }

        /// <summary>维修结果说明</summary>
        public string RepairResult { get; set; }

        /// <summary>维修费用</summary>
        public decimal? RepairCost { get; set; }

        /// <summary>停机小时数</summary>
        public decimal? DowntimeHours { get; set; }

        /// <summary>报修时间（报警流显示的时间）</summary>
        public DateTime ReportTime { get; set; }

        /// <summary>完成时间</summary>
        public DateTime? CompleteTime { get; set; }

        /// <summary>工单状态：Pending待处理 / InProgress维修中 / Completed已完成</summary>
        public string Status { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreatedAt { get; set; }

        // ====== 关联数据（通过 JOIN 查出，用于界面展示）======

        /// <summary>设备名称（关联 Equipment 表查出）</summary>
        public string EquipmentName { get; set; }

        /// <summary>设备编号（关联 Equipment 表查出）</summary>
        public string EquipmentNo { get; set; }

        /// <summary>故障照片文件路径（可空）</summary>
        public string PhotoPath { get; set; }

        /// <summary>AI 识别出的故障类型（可空）</summary>
        public string AiFaultType { get; set; }

        /// <summary>AI 置信度 0.00~1.00（可空）</summary>
        public decimal? AiConfidence { get; set; }
    }
}
