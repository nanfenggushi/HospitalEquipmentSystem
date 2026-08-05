namespace HospitalEquipment.Model
{
    public class MaintenanceRecordDto
    {
        /// <summary>RecordId（内部使用，不绑定到表格列）</summary>
        public int RecordId { get; set; }

        public string RepairNo { get; set; }

        public string EquipmentName { get; set; }

        public string FaultType { get; set; }

        public string FaultDesc { get; set; }

        /// <summary>紧急度（中文：紧急/普通/低）</summary>
        public string UrgencyText { get; set; }

        /// <summary>进度阶段（中文：待分配/已指派/处理中/已完成）</summary>
        public string ProgressStageText { get; set; }

        /// <summary>进度阶段原始值（英文：Pending/Assigned/InProgress/Done），用于程序判断</summary>
        public string ProgressStage { get; set; }

        public string DeptName { get; set; }

        public string RepairerName { get; set; }

        public System.DateTime ReportTime { get; set; }

        /// <summary>停机时长（小时）</summary>
        public decimal? DowntimeHours { get; set; }

        /// <summary>状态（中文：待处理/处理中/已完成/已超期/已取消）</summary>
        public string StatusText { get; set; }

        /// <summary>维修结果</summary>
        public string RepairResult { get; set; }

        /// <summary>维修费用</summary>
        public decimal? RepairCost { get; set; }

        /// <summary>完成时间</summary>
        public System.DateTime? CompleteTime { get; set; }
    }
}
