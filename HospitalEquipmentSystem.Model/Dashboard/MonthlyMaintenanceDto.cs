namespace HospitalEquipment.Model.Dashboard
{
    /// <summary>
    /// 月度维保统计数据传输对象 (DTO)
    /// </summary>
    public class MonthlyMaintenanceDto
    {
        /// <summary>
        /// 年份 (如 2026)
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 月份 (如 7)
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 月份标签 (如 "7月")
        /// </summary>
        public string MonthLabel => $"{Month}月";
        /// <summary>
        /// 新增故障数
        /// </summary>
        public double FaultCount { get; set; }
        /// <summary>
        /// 完成维修数
        /// </summary>
        public double RepairedCount { get; set; }
    }
}
