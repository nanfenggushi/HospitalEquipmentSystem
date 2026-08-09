using HospitalEquipment.Model;
using HospitalEquipment.Model.Dashboard;
using HospitalEquipmentSystem.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HospitalEquipment.DAL
{
    /// <summary>
    /// 维修工单增量查询（监控中心报警流专用）
    /// 安全说明：所有 SQL 均使用 SqlParameter 参数化查询，防止 SQL 注入
    /// </summary>
    public class MaintenanceRecordDAL
    {
        /// <summary>
        /// 获取待维修数（Status 为 InProgress 或 Pending 的记录数）
        /// </summary>
        public int GetPendingCount()
        {
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM MaintenanceRecords WHERE Status = @Status1 OR Status = @Status2",
                new SqlParameter("@Status1", "InProgress"),
                new SqlParameter("@Status2", "Pending"));
            return result != null ? (int)result : 0;
        }

        /// <summary>
        /// 获取紧急报警数（Urgency='Urgent' 且状态为 InProgress）
        /// </summary>
        public int GetUrgentAlarmCount()
        {
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM MaintenanceRecords WHERE Status = @Status AND Urgency = @Urgency",
                new SqlParameter("@Status", "InProgress"),
                new SqlParameter("@Urgency", "Urgent"));
            return result != null ? (int)result : 0;
        }

        /// <summary>
        /// 获取待处理工单数（Status = 'Pending'）
        /// </summary>
        public int GetPendingOrderCount()
        {
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM MaintenanceRecords WHERE Status = @Status",
                new SqlParameter("@Status", "Pending"));
            return result != null ? (int)result : 0;
        }

        /// <summary>
        /// 获取所有未完成的维修记录（含设备名称和设备编号）
        /// 通过 INNER JOIN 关联设备表
        /// </summary>
        public List<MaintenanceRecord> GetActiveRecords()
        {
            string sql = @"
                SELECT MR.*, E.EquipmentName, E.EquipmentNo
                FROM MaintenanceRecords MR
                INNER JOIN Equipment E ON MR.EquipmentId = E.EquipmentId
                WHERE MR.Status = @Status1 OR MR.Status = @Status2
                ORDER BY MR.ReportTime DESC";
            using (var reader = DbHelper.ExecuteReader(sql,
                new SqlParameter("@Status1", "InProgress"),
                new SqlParameter("@Status2", "Pending")))
            {
                return reader != null ? DataReaderMapper.MapToList<MaintenanceRecord>(reader) : new List<MaintenanceRecord>();
            }
        }

        /// <summary>
        /// 增量获取新维修记录（RecordId 大于 lastRecordId）
        /// </summary>
        /// <param name="lastRecordId">上次已加载的最大记录ID</param>
        public List<MaintenanceRecord> GetNewRecords(int lastRecordId)
        {
            string sql = @"
                SELECT MR.*, E.EquipmentName, E.EquipmentNo
                FROM MaintenanceRecords MR
                INNER JOIN Equipment E ON MR.EquipmentId = E.EquipmentId
                WHERE MR.RecordId > @LastId
                ORDER BY MR.ReportTime DESC";
            using (var reader = DbHelper.ExecuteReader(sql,
                new SqlParameter("@LastId", lastRecordId)))
            {
                return reader != null ? DataReaderMapper.MapToList<MaintenanceRecord>(reader) : new List<MaintenanceRecord>();
            }
        }

        /// <summary>
        /// 获取最近六个月每个月的故障数以及完成维修数
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public async Task<List<MonthlyMaintenanceDto>> GetMaintenanceCountsAsync(DateTime startDate)
        {
            string sql = @"SELECT 
                        Y AS Year,                      
                        M AS Month,                     
                        SUM(FaultCount) AS FaultCount,  
                        SUM(RepairedCount) AS RepairedCount 
                    FROM (
                        SELECT YEAR(ReportTime) AS Y, MONTH(ReportTime) AS M, 1 AS FaultCount, 0 AS RepairedCount
                        FROM MaintenanceRecords 
                        WHERE ReportTime >= @StartDate

                        UNION ALL

                        SELECT YEAR(CompleteTime) AS Y, MONTH(CompleteTime) AS M, 0 AS FaultCount, 1 AS RepairedCount
                        FROM MaintenanceRecords 
                        WHERE Status = 'Completed' 
                          AND CompleteTime IS NOT NULL 
                          AND CompleteTime >= @StartDate
                    ) AS Combined
                    GROUP BY Y, M
                    ORDER BY Y, M;";

            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, new SqlParameter("StartDate", startDate)))
            {
                return DataReaderMapper.MapToList<MonthlyMaintenanceDto>(reader);
            }
        }

        /// <summary>
        /// 获取报警列表数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<MaintenanceRecordsDto>> GetAlarmListAsync()
        {
            string sql = @"SELECT Equipment.EquipmentName, MaintenanceRecords.FaultDesc, 
                        MaintenanceRecords.Urgency, MaintenanceRecords.ReportTime
                        FROM MaintenanceRecords
                        JOIN Equipment ON MaintenanceRecords.EquipmentId = Equipment.EquipmentId
                        WHERE ProgressStage= 'Pending'
                        ORDER BY CASE Urgency 
                                     WHEN 'Urgent' THEN 1
                                     WHEN 'Normal' THEN 2
                                     WHEN 'Low' THEN 3
                                 END, 
                        ReportTime DESC
                        ";

            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql))
            {
                return DataReaderMapper.MapToList<MaintenanceRecordsDto>(reader);
            }
        }
    }
}
