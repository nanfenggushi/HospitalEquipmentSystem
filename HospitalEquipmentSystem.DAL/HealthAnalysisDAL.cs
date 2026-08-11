using HospitalEquipment.Model.Dashboard;
using HospitalEquipmentSystem.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalEquipment.DAL
{
    /// <summary>
    /// 设备健康分析数据访问层
    /// 提供：设备健康度评分原始数据、预测性维护(MTBF)、设备全生命周期事件
    /// 安全说明：全部使用 SqlParameter 参数化查询，防止 SQL 注入
    /// </summary>
    public class HealthAnalysisDAL
    {
        /// <summary>
        /// 获取全部在用设备的健康度原始指标（评分计算在 BLL）
        /// </summary>
        public List<DeviceHealthDto> GetHealthRawData()
        {
            string sql = @"
SELECT 
    e.EquipmentId, e.EquipmentNo, e.EquipmentName, e.Model, e.Status,
    ISNULL(d.DeptName, '')  AS DeptName,
    ISNULL(c.CategoryName, '') AS CategoryName,
    -- 1) 维修频率：近90天维修次数 / 历史累计次数
    (SELECT COUNT(*) FROM MaintenanceRecords m 
      WHERE m.EquipmentId = e.EquipmentId 
        AND m.ReportTime >= DATEADD(DAY, -90, GETDATE())) AS RepairCount90d,
    (SELECT COUNT(*) FROM MaintenanceRecords m 
      WHERE m.EquipmentId = e.EquipmentId) AS TotalRepairCount,
    -- 2) 停机时长：近90天平均每次停机小时数
    ISNULL((
      SELECT AVG(CAST(ISNULL(m.DowntimeHours,0) AS DECIMAL(10,2))) 
      FROM MaintenanceRecords m 
      WHERE m.EquipmentId = e.EquipmentId 
        AND m.ReportTime >= DATEADD(DAY, -90, GETDATE())
    ), 0) AS AvgDowntimeHours,
    -- 累计维修费用
    ISNULL((SELECT SUM(ISNULL(m.RepairCost,0)) FROM MaintenanceRecords m 
      WHERE m.EquipmentId = e.EquipmentId), 0) AS TotalRepairCost,
    -- 3) 老化：已使用年限（年，含小数）
    ISNULL(DATEDIFF(DAY, e.PurchaseDate, GETDATE()), 0) / 365.0 AS UsedYears,
    e.ServiceLife,
    -- 4) 维护按时率：是否已超期 / 距下次维护天数
    CASE WHEN e.NextMaintainDate IS NOT NULL AND e.NextMaintainDate < GETDATE() THEN 1 ELSE 0 END AS IsMaintainOverdue,
    CASE WHEN e.NextMaintainDate IS NOT NULL 
         THEN DATEDIFF(DAY, GETDATE(), e.NextMaintainDate) END AS DaysToNextMaintain
FROM Equipment e
LEFT JOIN Departments d ON e.DeptId = d.DeptId
LEFT JOIN EquipmentCategories c ON e.CategoryId = c.CategoryId
WHERE e.IsActive = 1
ORDER BY e.EquipmentId";

            using (var reader = DbHelper.ExecuteReader(sql))
            {
                return reader != null ? DataReaderMapper.MapToList<DeviceHealthDto>(reader) : new List<DeviceHealthDto>();
            }
        }

        /// <summary>
        /// 预测性维护：统计每台设备的维修次数、最近维修时间、MTBF 间隔
        /// MTBF = 相邻两次维修报告时间的平均间隔天数
        /// </summary>
        public List<PredictiveMaintenanceDto> GetPredictiveMaintenanceRaw()
        {
            string sql = @"
;WITH Repairs AS (
    SELECT m.EquipmentId, m.ReportTime,
           LAG(m.ReportTime) OVER (PARTITION BY m.EquipmentId ORDER BY m.ReportTime) AS PrevTime
    FROM MaintenanceRecords m
    WHERE m.ReportTime IS NOT NULL
),
Intervals AS (
    SELECT EquipmentId, DATEDIFF(DAY, PrevTime, ReportTime) AS IntervalDays
    FROM Repairs
    WHERE PrevTime IS NOT NULL
)
SELECT 
    e.EquipmentId, e.EquipmentNo, e.EquipmentName, e.Model,
    ISNULL(d.DeptName, '') AS DeptName,
    -- 每台设备累计维修次数
    (SELECT COUNT(*) FROM MaintenanceRecords m 
      WHERE m.EquipmentId = e.EquipmentId) AS RepairCount,
    -- 平均维修间隔（天）
    ISNULL((SELECT AVG(CAST(IntervalDays AS FLOAT)) FROM Intervals i 
      WHERE i.EquipmentId = e.EquipmentId), 0) AS MtbfDays,
    -- 距离上次维修（或采购）的天数
    ISNULL(DATEDIFF(DAY, 
        (SELECT TOP 1 ReportTime FROM MaintenanceRecords m 
          WHERE m.EquipmentId = e.EquipmentId ORDER BY m.ReportTime DESC), 
        GETDATE()),
        DATEDIFF(DAY, e.PurchaseDate, GETDATE())) AS DaysSinceLastRepair,
    -- 上次维修时间
    (SELECT TOP 1 ReportTime FROM MaintenanceRecords m 
      WHERE m.EquipmentId = e.EquipmentId ORDER BY m.ReportTime DESC) AS LastRepairTime
FROM Equipment e
LEFT JOIN Departments d ON e.DeptId = d.DeptId
WHERE e.IsActive = 1 AND e.Status <> 'Scrapped'
ORDER BY e.EquipmentId";

            using (var reader = DbHelper.ExecuteReader(sql))
            {
                return reader != null ? DataReaderMapper.MapToList<PredictiveMaintenanceDto>(reader) : new List<PredictiveMaintenanceDto>();
            }
        }

        /// <summary>
        /// 获取设备生命周期事件（建档/入库/报修/维修完成/借用/归还/报废）
        /// 已按 设备ID + 时间倒序 排列，直接可用于一机一档时间轴展示
        /// </summary>
        /// <param name="equipmentId">设备ID；传 null 或 &lt;=0 时返回全部在用设备的事件</param>
        public List<EquipmentLifecycleEvent> GetLifecycleEvents(int? equipmentId = null)
        {
            string sql = @"
SELECT ev.EquipmentId, ev.EquipmentName, ev.EventTime, ev.EventType, ev.EventTypeText, ev.Title, ev.Detail, ev.OperatorName
FROM (
    -- 建档（采购）
    SELECT 
        e.EquipmentId,
        ISNULL(e.EquipmentName, '') AS EquipmentName,
        ISNULL(e.PurchaseDate, e.CreatedAt) AS EventTime,
        'Purchase' AS EventType,
        N'建档' AS EventTypeText,
        N'设备建档' AS Title,
        N'采购日期：' + CONVERT(NVARCHAR(10), ISNULL(e.PurchaseDate, e.CreatedAt), 120) + 
        N'，价格：' + ISNULL(CONVERT(NVARCHAR(20), e.Price), N'—') + N' 元，保修：' + 
        ISNULL(CONVERT(NVARCHAR(10), e.WarrantyMonths), N'—') + N' 个月' AS Detail,
        '' AS OperatorName
    FROM Equipment e
    WHERE e.IsActive = 1

    UNION ALL

    -- 入库记录
    SELECT 
        i.EquipmentId,
        ISNULL(e2.EquipmentName, '') AS EquipmentName,
        i.InboundDate AS EventTime,
        'Inbound' AS EventType,
        N'入库' AS EventTypeText,
        N'设备入库' AS Title,
        N'单号：' + ISNULL(i.InboundNo, '') + N'，供应商：' + ISNULL(i.Supplier, '') + 
        N'，数量：' + CONVERT(NVARCHAR(10), i.Quantity) + N'，单价：' + 
        ISNULL(CONVERT(NVARCHAR(20), i.PurchasePrice), N'—') + N' 元' AS Detail,
        ISNULL(u.RealName, '') AS OperatorName
    FROM InboundRecords i
    LEFT JOIN Users u ON i.OperatorId = u.UserId
    LEFT JOIN Equipment e2 ON i.EquipmentId = e2.EquipmentId
    WHERE e2.IsActive = 1

    UNION ALL

    -- 维修报修
    SELECT 
        m.EquipmentId,
        ISNULL(e3.EquipmentName, '') AS EquipmentName,
        m.ReportTime AS EventTime,
        'Repair' AS EventType,
        N'报修' AS EventTypeText,
        N'设备报修' AS Title,
        N'故障：' + ISNULL(m.FaultDesc, '') + N'，类型：' + ISNULL(m.FaultType, '') + 
        N'，紧急度：' + CASE ISNULL(m.Urgency,'') WHEN 'Urgent' THEN N'特急' 
            WHEN 'Normal' THEN N'紧急' WHEN 'Low' THEN N'普通' ELSE ISNULL(m.Urgency,'') END AS Detail,
        ISNULL(r.RealName, '') AS OperatorName
    FROM MaintenanceRecords m
    LEFT JOIN Users r ON m.ReporterId = r.UserId
    LEFT JOIN Equipment e3 ON m.EquipmentId = e3.EquipmentId
    WHERE e3.IsActive = 1

    UNION ALL

    -- 维修完成
    SELECT 
        m.EquipmentId,
        ISNULL(e4.EquipmentName, '') AS EquipmentName,
        m.CompleteTime AS EventTime,
        'RepairDone' AS EventType,
        N'维修完成' AS EventTypeText,
        N'维修完成' AS Title,
        N'结果：' + ISNULL(m.RepairResult, '') + N'，费用：' + ISNULL(CONVERT(NVARCHAR(20), m.RepairCost), N'—') + 
        N' 元，停机：' + ISNULL(CONVERT(NVARCHAR(10), m.DowntimeHours), N'0') + N' 小时' AS Detail,
        ISNULL(f.RealName, '') AS OperatorName
    FROM MaintenanceRecords m
    LEFT JOIN Users f ON m.AssignedTo = f.UserId
    LEFT JOIN Equipment e4 ON m.EquipmentId = e4.EquipmentId
    WHERE m.CompleteTime IS NOT NULL AND e4.IsActive = 1

    UNION ALL

    -- 借用申请
    SELECT 
        b.EquipmentId,
        ISNULL(e5.EquipmentName, '') AS EquipmentName,
        b.CreatedAt AS EventTime,
        'Borrow' AS EventType,
        N'借用' AS EventTypeText,
        N'设备借用' AS Title,
        N'用途：' + ISNULL(b.Purpose, '') + N'，预计归还：' + 
        CONVERT(NVARCHAR(10), b.ExpectedReturnDate, 120) AS Detail,
        ISNULL(a.RealName, '') AS OperatorName
    FROM BorrowRecords b
    LEFT JOIN Users a ON b.ApplicantId = a.UserId
    LEFT JOIN Equipment e5 ON b.EquipmentId = e5.EquipmentId
    WHERE e5.IsActive = 1

    UNION ALL

    -- 归还
    SELECT 
        b.EquipmentId,
        ISNULL(e6.EquipmentName, '') AS EquipmentName,
        b.ActualReturnDate AS EventTime,
        'Return' AS EventType,
        N'归还' AS EventTypeText,
        N'设备归还' AS Title,
        N'归还备注：' + ISNULL(b.ReturnNote, '') AS Detail,
        '' AS OperatorName
    FROM BorrowRecords b
    LEFT JOIN Equipment e6 ON b.EquipmentId = e6.EquipmentId
    WHERE b.ActualReturnDate IS NOT NULL AND e6.IsActive = 1

    UNION ALL

    -- 报废
    SELECT 
        e.EquipmentId,
        ISNULL(e.EquipmentName, '') AS EquipmentName,
        ISNULL(e.UpdatedAt, e.CreatedAt) AS EventTime,
        'Scrapped' AS EventType,
        N'报废' AS EventTypeText,
        N'设备报废' AS Title,
        N'报废备注：' + ISNULL(e.Remarks, '') AS Detail,
        '' AS OperatorName
    FROM Equipment e
    WHERE e.Status = 'Scrapped' AND e.IsActive = 1
) AS ev
WHERE ev.EventTime IS NOT NULL
    AND (@EquipmentId IS NULL OR @EquipmentId <= 0 OR ev.EquipmentId = @EquipmentId)
ORDER BY ev.EquipmentId, ev.EventTime DESC";

            var parameters = new SqlParameter("@EquipmentId", (object)equipmentId ?? DBNull.Value);
            using (var reader = DbHelper.ExecuteReader(sql, parameters))
            {
                return reader != null ? DataReaderMapper.MapToList<EquipmentLifecycleEvent>(reader) : new List<EquipmentLifecycleEvent>();
            }
        }
    }
}
