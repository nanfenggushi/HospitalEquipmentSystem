using System.Collections.Generic;
using HospitalEquipment.DAL;
using HospitalEquipment.Model;

namespace HospitalEquipment.BLL
{
    /// <summary>
    /// 监控中心业务逻辑层：把多个 DAL 的数据组装成 UI 需要的结果
    /// </summary>
    public class MonitorCenterBLL
    {
        private readonly EquipmentDAL _equipmentDal = new EquipmentDAL();
        private readonly MaintenanceRecordDAL _maintenanceDal = new MaintenanceRecordDAL();
        private readonly EquipmentCategoryDAL _categoryDal = new EquipmentCategoryDAL();

        /// <summary>
        /// 获取仪表盘所需的全部统计数据
        /// </summary>
        public DashboardStats GetDashboardStats()
        {
            var stats = new DashboardStats();
            stats.TotalEquipment = _equipmentDal.GetActiveCount();
            stats.InUseEquipment = _equipmentDal.GetInUseCount();
            stats.PendingMaintenance = _maintenanceDal.GetPendingCount();
            stats.UrgentAlarms = _maintenanceDal.GetUrgentAlarmCount();
            stats.MaintenanceCount = _equipmentDal.GetMaintenanceCount();
            stats.PendingOrderCount = _maintenanceDal.GetPendingOrderCount();
            stats.BorrowedCount = _equipmentDal.GetBorrowedCount();
            return stats;
        }

        /// <summary>
        /// 获取所有未完成的维修报警记录
        /// </summary>
        public List<MaintenanceRecord> GetActiveAlarms()
        {
            return _maintenanceDal.GetActiveRecords();
        }

        /// <summary>
        /// 增量获取新报警记录
        /// </summary>
        /// <param name="lastRecordId">上次已加载的最大记录ID</param>
        public List<MaintenanceRecord> GetNewAlarms(int lastRecordId)
        {
            return _maintenanceDal.GetNewRecords(lastRecordId);
        }

        /// <summary>
        /// 获取各设备分类下的设备数量
        /// </summary>
        public Dictionary<string, int> GetCategoryCounts()
        {
            return _categoryDal.GetEquipmentCountByCategory();
        }
    }

    /// <summary>
    /// 仪表盘统计数据 DTO
    /// </summary>
    public class DashboardStats
    {
        public int TotalEquipment { get; set; }
        public int InUseEquipment { get; set; }
        public int PendingMaintenance { get; set; }
        public int UrgentAlarms { get; set; }
        public int MaintenanceCount { get; set; }
        public int PendingOrderCount { get; set; }
        public int BorrowedCount { get; set; }
    }
}
