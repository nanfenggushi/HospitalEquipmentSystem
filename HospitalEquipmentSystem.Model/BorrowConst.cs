namespace HospitalEquipment.Model
{
    /// <summary>
    /// 借用状态与设备状态常量及中文转换
    /// </summary>
    public static class BorrowConst
    {
        // 借用记录状态（BorrowRecords.Status）
        public const string Pending = "Pending";      // 待审批
        public const string Approved = "Approved";    // 审批通过（借用中）
        public const string Rejected = "Rejected";    // 已驳回
        public const string Returned = "Returned";    // 已归还
        public const string Overdue = "Overdue";      // 已超期

        // 设备状态（Equipment.Status）
        public const string EqIdle = "Idle";
        public const string EqBorrowed = "Borrowed";
        public const string EqInUse = "InUse";
        public const string EqMaintenance = "Maintenance";
        public const string EqScrapped = "Scrapped";

        /// <summary>借用状态中文显示</summary>
        public static string StatusText(string status)
        {
            switch (status)
            {
                case Pending: return "待审批";
                case Approved: return "借用中";
                case Rejected: return "已驳回";
                case Returned: return "已归还";
                case Overdue: return "已超期";
                default: return status ?? "";
            }
        }

        /// <summary>设备状态中文显示</summary>
        public static string EquipmentStatusText(string status)
        {
            switch (status)
            {
                case EqIdle: return "空闲";
                case EqBorrowed: return "已借出";
                case EqInUse: return "使用中";
                case EqMaintenance: return "维修中";
                case EqScrapped: return "已报废";
                default: return status ?? "";
            }
        }
    }
}
