using System.Collections.Generic;              // 引用泛型集合，List<T>、Dictionary 在这里
using HospitalEquipment.DAL;                   // 引用数据访问层，三个 DAL 类在这里
using HospitalEquipment.Model;                 // 引用实体层，MaintenanceRecord 在这里

namespace HospitalEquipment.BLL                 // 声明当前代码所属的命名空间（业务逻辑层）
{
    /// <summary>
    /// 监控中心业务逻辑层（BLL）
    /// 职责：把多个 DAL 的数据组装成 UI 需要的结果，UI 只调用 BLL，不直接碰数据库
    /// 架构分层：UI → BLL → DAL → DbHelper → 数据库
    /// </summary>
    public class MonitorCenterBLL
    {
        // ====== 数据访问层实例 ======
        private readonly EquipmentDAL _equipmentDal = new EquipmentDAL();              // 设备表数据访问对象
        private readonly MaintenanceRecordDAL _maintenanceDal = new MaintenanceRecordDAL();  // 维修记录表数据访问对象
        private readonly EquipmentCategoryDAL _categoryDal = new EquipmentCategoryDAL();    // 设备分类表数据访问对象

        /// <summary>
        /// 获取仪表盘所需的全部统计数据
        /// 包括：设备总数、使用中、待维修、紧急报警、故障维修中、待处理工单、借用中
        /// </summary>
        /// <returns>打包好的 DashboardStats 统计对象</returns>
        public DashboardStats GetDashboardStats()
        {
            // 创建统计结果对象
            var stats = new DashboardStats();

            // 查询设备总数（IsActive=1）并存入结果对象
            stats.TotalEquipment = _equipmentDal.GetActiveCount();

            // 查询使用中的设备数（Status='InUse'）并存入结果对象
            stats.InUseEquipment = _equipmentDal.GetInUseCount();

            // 查询待维修数（维修表 InProgress 或 Pending 状态的记录数）并存入结果对象
            stats.PendingMaintenance = _maintenanceDal.GetPendingCount();

            // 查询紧急报警数（Urgency='Urgent' 且 InProgress）并存入结果对象
            stats.UrgentAlarms = _maintenanceDal.GetUrgentAlarmCount();

            // 查询故障/维修中的设备数（设备表 Status='Maintenance'）并存入结果对象
            stats.MaintenanceCount = _equipmentDal.GetMaintenanceCount();

            // 查询待处理工单数（维修表 Status='Pending'）并存入结果对象
            stats.PendingOrderCount = _maintenanceDal.GetPendingOrderCount();

            // 查询借用中的设备数（设备表 Status='Borrowed'）并存入结果对象
            stats.BorrowedCount = _equipmentDal.GetBorrowedCount();

            // 返回组装好的统计结果
            return stats;
        }

        /// <summary>
        /// 获取当前所有未完成的维修报警记录（Status 为 InProgress 或 Pending）
        /// </summary>
        /// <returns>未完成维修记录列表</returns>
        public List<MaintenanceRecord> GetActiveAlarms()
        {
            // 直接调用维修记录 DAL 查询并返回
            return _maintenanceDal.GetActiveRecords();
        }

        /// <summary>
        /// 增量获取新报警记录（RecordId 大于 lastRecordId 的记录）
        /// 用于每10秒定时器检测新数据
        /// </summary>
        /// <param name="lastRecordId">上次已加载的最大记录ID</param>
        /// <returns>比 lastRecordId 更新的维修记录列表</returns>
        public List<MaintenanceRecord> GetNewAlarms(int lastRecordId)
        {
            // 直接调用维修记录 DAL 的增量查询并返回
            return _maintenanceDal.GetNewRecords(lastRecordId);
        }

        /// <summary>
        /// 获取各设备分类下的设备数量（用于 uiTitlePanel2 分类统计）
        /// </summary>
        /// <returns>分类名称到设备数量的字典</returns>
        public Dictionary<string, int> GetCategoryCounts()
        {
            // 直接调用分类 DAL 统计并返回
            return _categoryDal.GetEquipmentCountByCategory();
        }
    }

    /// <summary>
    /// 仪表盘统计数据 DTO（Data Transfer Object）
    /// 把多个表的统计结果打包，一次传给 UI 层
    /// </summary>
    public class DashboardStats
    {
        /// <summary>设备总数</summary>
        public int TotalEquipment { get; set; }

        /// <summary>使用中的设备数（设备在线）</summary>
        public int InUseEquipment { get; set; }

        /// <summary>待维修数（维修中+待处理工单）</summary>
        public int PendingMaintenance { get; set; }

        /// <summary>紧急报警数</summary>
        public int UrgentAlarms { get; set; }

        /// <summary>故障/维修中的设备数</summary>
        public int MaintenanceCount { get; set; }

        /// <summary>待处理工单数</summary>
        public int PendingOrderCount { get; set; }

        /// <summary>借用中的设备数</summary>
        public int BorrowedCount { get; set; }
    }
}
