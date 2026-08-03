using System.Collections.Generic;              // 引用泛型集合，List<T> 在这里
using System.Data.SqlClient;                   // 引用 SqlParameter，用于参数化查询防注入
using HospitalEquipment.Model;                 // 引用实体层，MaintenanceRecord 在这里
using HospitalEquipment.Util;                  // 引用工具层，DbHelper、DataReaderMapper 在这里

namespace HospitalEquipment.DAL                 // 声明当前代码所属的命名空间（数据访问层）
{
    /// <summary>
    /// 维修工单表（MaintenanceRecords）数据访问层
    /// 安全说明：所有 SQL 均使用 SqlParameter 参数化查询，禁止字符串拼接用户输入，防止 SQL 注入
    /// </summary>
    public class MaintenanceRecordDAL
    {
        /// <summary>
        /// 获取待维修数（Status 为 InProgress 或 Pending 的记录数）
        /// </summary>
        /// <returns>待维修记录数，查询失败返回 0</returns>
        public int GetPendingCount()
        {
            // 调用 DbHelper 执行单值查询，统计两个状态下的记录数
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM MaintenanceRecords WHERE Status = @Status1 OR Status = @Status2",
                new SqlParameter("@Status1", "InProgress"),     // 参数1：维修中
                new SqlParameter("@Status2", "Pending"));       // 参数2：待处理
            // 查询结果不为空则转成 int，为空则返回 0
            return result != null ? (int)result : 0;
        }

        /// <summary>
        /// 获取紧急报警数（Urgency='Urgent' 且状态为 InProgress）
        /// </summary>
        /// <returns>紧急报警数，查询失败返回 0</returns>
        public int GetUrgentAlarmCount()
        {
            // 调用 DbHelper 执行单值查询，统计维修中且紧急的记录数
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM MaintenanceRecords WHERE Status = @Status AND Urgency = @Urgency",
                new SqlParameter("@Status", "InProgress"),      // 参数1：维修中
                new SqlParameter("@Urgency", "Urgent"));        // 参数2：特急级别
            // 查询结果不为空则转成 int，为空则返回 0
            return result != null ? (int)result : 0;
        }

        /// <summary>
        /// 获取待处理工单数（Status = 'Pending'）
        /// </summary>
        /// <returns>待处理工单数，查询失败返回 0</returns>
        public int GetPendingOrderCount()
        {
            // 调用 DbHelper 执行单值查询，统计待处理状态的工单数
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM MaintenanceRecords WHERE Status = @Status",
                new SqlParameter("@Status", "Pending"));        // 参数：待处理状态
            // 查询结果不为空则转成 int，为空则返回 0
            return result != null ? (int)result : 0;
        }

        /// <summary>
        /// 获取所有未完成的维修记录（含设备名称和设备编号）
        /// 通过 INNER JOIN 关联设备表，一次性取出界面需要的展示字段
        /// </summary>
        /// <returns>未完成维修记录列表</returns>
        public List<MaintenanceRecord> GetActiveRecords()
        {
            // 定义查询 SQL：取维修表全部字段 + 设备名称 + 设备编号，只取维修中或待处理的，按报修时间倒序
            string sql = @"
                SELECT MR.*, E.EquipmentName, E.EquipmentNo
                FROM MaintenanceRecords MR
                INNER JOIN Equipment E ON MR.EquipmentId = E.EquipmentId
                WHERE MR.Status = @Status1 OR MR.Status = @Status2
                ORDER BY MR.ReportTime DESC";

            // 用 using 确保读取器用完自动释放
            using (var reader = DbHelper.ExecuteReader(sql,
                new SqlParameter("@Status1", "InProgress"),      // 参数1：维修中
                new SqlParameter("@Status2", "Pending")))        // 参数2：待处理
            {
                // 读取器不为空则映射为实体列表，为空则返回空列表
                return reader != null ? DataReaderMapper.MapToList<MaintenanceRecord>(reader) : new List<MaintenanceRecord>();
            }
        }

        /// <summary>
        /// 增量获取新维修记录（RecordId 大于 lastRecordId）
        /// 使用 @LastId 参数化，防止 SQL 注入
        /// </summary>
        /// <param name="lastRecordId">上次已加载的最大记录ID</param>
        /// <returns>比 lastRecordId 更新的维修记录列表</returns>
        public List<MaintenanceRecord> GetNewRecords(int lastRecordId)
        {
            // 定义查询 SQL：只取 ID 比上次最大 ID 更大的新记录，按报修时间倒序
            string sql = @"
                SELECT MR.*, E.EquipmentName, E.EquipmentNo
                FROM MaintenanceRecords MR
                INNER JOIN Equipment E ON MR.EquipmentId = E.EquipmentId
                WHERE MR.RecordId > @LastId
                ORDER BY MR.ReportTime DESC";

            // 用 using 确保读取器用完自动释放
            using (var reader = DbHelper.ExecuteReader(sql,
                new SqlParameter("@LastId", lastRecordId)))      // 参数：上次最大 ID
            {
                // 读取器不为空则映射为实体列表，为空则返回空列表
                return reader != null ? DataReaderMapper.MapToList<MaintenanceRecord>(reader) : new List<MaintenanceRecord>();
            }
        }
    }
}
