namespace HospitalEquipment.Model.Dashboard
{
    /// <summary>
    /// 月度维保趋势 DTO
    /// </summary>
    public class MaintenanceTrendDto
    {
        /// <summary>
        /// 月份（如 3月、4月）
        /// </summary>
        public string Month { get; set; }
        /// <summary>
        /// 维保工单数
        /// </summary>
        public int Count { get; set; }
    }
}
