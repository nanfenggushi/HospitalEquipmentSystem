using System;

namespace HospitalEquipment.Model.Dashboard
{
    public class MaintenanceRecordsDto
    {
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
