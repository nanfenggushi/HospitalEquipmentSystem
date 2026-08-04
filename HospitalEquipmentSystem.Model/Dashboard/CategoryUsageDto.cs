namespace HospitalEquipment.Model.Dashboard
{
    /// <summary>
    /// 各类设备使用率统计 DTO
    /// </summary>
    public class CategoryUsageDto
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 使用率（%）
        /// </summary>
        public double UsageRate { get; set; }
    }
}
