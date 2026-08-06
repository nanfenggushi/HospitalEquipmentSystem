namespace HospitalEquipment.Model.Dashboard
{
    public class StatisticCardDto
    {
        /// <summary>
        /// 设备总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 正常设备数量
        /// </summary>
        public int NormalCount { get; set; }
        /// <summary>
        /// 闲置设备数量
        /// </summary>
        public int IdleCount { get; set; }
        /// <summary>
        /// 使用中中设备数量
        /// </summary>
        public int InUseCount { get; set; }
        /// <summary>
        /// 维修中设备数量
        /// </summary>
        public int MaintenanceCount { get; set; }
        /// <summary>
        /// 借用中设备数量
        /// </summary>
        public int BorrowedCount { get; set; }
        /// <summary>
        /// 报废设备数量
        /// </summary>
        public int ScrappedCount { get; set; }
    }
}
