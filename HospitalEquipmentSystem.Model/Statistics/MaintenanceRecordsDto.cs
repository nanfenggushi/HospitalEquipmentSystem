using System;

namespace HospitalEquipment.Model.Dashboard
{
    public class MaintenanceRecordsDto
    {
        /// <summary>
        /// 工单记录ID（分配操作要用，对应 MaintenanceRecords.RecordId）
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// 工单号
        /// </summary>
        public string RepairNo { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; }
        /// <summary>
        /// 故障现象
        /// </summary>
        public string FaultDesc { get; set; }
        /// <summary>
        /// 紧急程度
        /// </summary>
        public string Urgency { get; set; }
        /// <summary>
        /// 故障时间
        /// </summary>
        public DateTime? ReportTime { get; set; }
    }
}
