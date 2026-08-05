namespace HospitalEquipment.Util
{
    /// <summary>
    /// 维修管理通用转换工具 — 统一中英文映射，避免各处重复 switch
    /// </summary>
    public static class MaintenanceHelper
    {
        // ==================== 紧急度 ====================

        public static string UrgencyToCn(string en) => en switch
        {
            "Urgent" => "紧急",
            "Normal" => "普通",
            "Low" => "低",
            _ => en ?? ""
        };

        public static string UrgencyToEn(string cn) => cn switch
        {
            "紧急" => "Urgent",
            "普通" => "Normal",
            "低" => "Low",
            _ => cn
        };

        // ==================== 进度阶段 ====================

        public static string StageToCn(string en) => en switch
        {
            "Pending" => "待分配",
            "Assigned" => "已指派",
            "InProgress" => "处理中",
            "Done" => "已完成",
            _ => en ?? ""
        };

        /// <summary>
        /// 维修员端进度阶段映射：同一个 ProgressStage 值，在维修员端显示不同文案
        /// </summary>
        public static string StageToRepairerCn(string en) => en switch
        {
            "Assigned" => "待接单",
            "InProgress" => "处理中",
            "Done" => "已完成",
            _ => en ?? ""
        };

        // ==================== 状态 ====================

        public static string StatusToCn(string en) => en switch
        {
            "Pending" => "待处理",
            "InProgress" => "处理中",
            "Completed" => "已完成",
            "Overdue" => "已超期",
            "Cancelled" => "已取消",
            _ => en ?? ""
        };
    }
}
