namespace HospitalEquipment.Model
{
    public class StatisticCardDto
    {
        public int TotalCount { get; set; } // 设备总数
        public int NormalCount { get; set; } // 正常设备数量
        public int MaintenanceCount { get; set; } // 维修中设备数量
        public int BorrowedCount { get; set; } // 借用中设备数量
        public int ScrappedCount { get; set; } // 报废设备数量
    }
}
