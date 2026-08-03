using HospitalEquipment.Util;
using HospitalEquipmentSystem.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalEquipment.DAL
{
    public class MaintenanceDAL
    {
        /// <summary>
        /// 查询工单列表（支持多条件筛选）
        /// </summary>
        /// <param name="urgency">紧急度筛选：null=全部，否则匹配 Low/Normal/Urgent</param>
        /// <param name="dept">科室筛选：null=全部，否则匹配科室名</param>
        /// <param name="keyword">关键字搜索：匹配工单号/设备名/故障描述</param>
        /// <param name="stage">进度阶段筛选：null=全部</param>
        public DataTable GetOrderList(string urgency = null, string dept = null,
                                       string keyword = null, string stage = null)
        {
            string sql = @"
                SELECT m.RecordId, m.RepairNo, e.EquipmentName, 
                       m.FaultType, m.FaultDesc, m.Urgency,
                       m.ProgressStage, d.DeptName, 
                       ISNULL(u.RealName, '未指派') AS RepairerName,
                       m.ReportTime, m.DowntimeHours, m.Status
                FROM MaintenanceRecords m
                LEFT JOIN Equipment e ON m.EquipmentId = e.EquipmentId
                LEFT JOIN Departments d ON m.ReportDeptId = d.DeptId
                LEFT JOIN Users u ON m.AssignedTo = u.UserId
                WHERE 1=1";

            var parameters = new List<SqlParameter>();

            // 紧急度筛选
            if (!string.IsNullOrEmpty(urgency))
            {
                sql += " AND m.Urgency = @Urgency";
                parameters.Add(new SqlParameter("@Urgency", urgency));
            }

            // 科室筛选
            if (!string.IsNullOrEmpty(dept))
            {
                sql += " AND d.DeptName = @Dept";
                parameters.Add(new SqlParameter("@Dept", dept));
            }

            // 关键字模糊搜索（工单号 / 设备名 / 故障描述）
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND (m.RepairNo LIKE @Keyword OR e.EquipmentName LIKE @Keyword OR m.FaultDesc LIKE @Keyword)";
                parameters.Add(new SqlParameter("@Keyword", $"%{keyword}%"));
            }

            // 进度阶段筛选（看板段点击时使用）
            if (!string.IsNullOrEmpty(stage))
            {
                sql += " AND m.ProgressStage = @Stage";
                parameters.Add(new SqlParameter("@Stage", stage));
            }

            sql += " ORDER BY m.ReportTime DESC";
            return DbHelper.GetDataTable(sql, parameters.ToArray());
        }

        /// <summary>
        /// 查询各阶段工单数量（KPI 看板用）
        /// </summary>
        public DataTable GetStageCounts()
        {
            string sql = @"
                SELECT ProgressStage, COUNT(*) AS Cnt
                FROM MaintenanceRecords
                GROUP BY ProgressStage";
            return DbHelper.GetDataTable(sql);
        }

        /// <summary>
        /// KPI 综合统计：各阶段数量 + 超期数量 + 紧急数量
        /// </summary>
        public DataTable GetKpiSummary()
        {
            string sql = @"
                SELECT 
                    ISNULL(SUM(CASE WHEN ProgressStage = 'Pending' THEN 1 ELSE 0 END), 0) AS PendingCnt,
                    ISNULL(SUM(CASE WHEN ProgressStage = 'Assigned' THEN 1 ELSE 0 END), 0) AS AssignedCnt,
                    ISNULL(SUM(CASE WHEN ProgressStage = 'InProgress' THEN 1 ELSE 0 END), 0) AS InProgressCnt,
                    ISNULL(SUM(CASE WHEN ProgressStage = 'Done' THEN 1 ELSE 0 END), 0) AS DoneCnt,
                    ISNULL(SUM(CASE WHEN Urgency = 'Urgent' THEN 1 ELSE 0 END), 0) AS UrgentCnt,
                    ISNULL(SUM(CASE WHEN Status = 'Overdue' THEN 1 ELSE 0 END), 0) AS OverdueCnt,
                    ISNULL(SUM(ISNULL(DowntimeHours, 0)), 0) AS TotalDownHours,
                    ISNULL(SUM(CASE WHEN Status = 'Completed' THEN 1 ELSE 0 END), 0) AS CompletedCnt
                FROM MaintenanceRecords";
            return DbHelper.GetDataTable(sql);
        }

        /// <summary>
        /// 维修员工作负载：按 AssignedTo 分组统计进行中的工单
        /// </summary>
        public DataTable GetRepairerWorkloads()
        {
            string sql = @"
                SELECT ISNULL(u.RealName, '未指派') AS RepairerName, 
                       COUNT(*) AS TaskCount
                FROM MaintenanceRecords m
                LEFT JOIN Users u ON m.AssignedTo = u.UserId
                WHERE m.ProgressStage IN ('Pending', 'Assigned', 'InProgress')
                GROUP BY u.RealName
                ORDER BY TaskCount DESC";
            return DbHelper.GetDataTable(sql);
        }

        /// <summary>
        /// 告警列表：最近的特急/超期工单
        /// </summary>
        public DataTable GetAlerts(int topN = 5)
        {
            string sql = $@"
                SELECT TOP {topN} m.RepairNo, e.EquipmentName, m.FaultDesc, m.Urgency,
                       DATEDIFF(HOUR, m.ReportTime, GETDATE()) AS HoursSince,
                       m.DowntimeHours, m.Status
                FROM MaintenanceRecords m
                LEFT JOIN Equipment e ON m.EquipmentId = e.EquipmentId
                WHERE m.ProgressStage != 'Done'
                  AND m.Status != 'Completed'
                ORDER BY 
                    CASE m.Urgency WHEN 'Urgent' THEN 0 ELSE 1 END,
                    ISNULL(m.DowntimeHours, 0) DESC";
            return DbHelper.GetDataTable(sql);
        }

        /// <summary>
        /// 新增工单
        /// </summary>
        public int InsertOrder(string repairNo, int equipmentId, int reportDeptId, int reporterId,
                                string faultType, string faultDesc, string urgency)
        {
            string sql = @"
                INSERT INTO MaintenanceRecords 
                    (RepairNo, EquipmentId, ReportDeptId, ReporterId, FaultType, FaultDesc, 
                     Urgency, ProgressStage, Status, ReportTime)
                VALUES 
                    (@RepairNo, @EquipmentId, @ReportDeptId, @ReporterId, @FaultType, @FaultDesc,
                     @Urgency, 'Pending', 'Pending', GETDATE())";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@RepairNo", repairNo),
                new SqlParameter("@EquipmentId", equipmentId),
                new SqlParameter("@ReportDeptId", reportDeptId),
                new SqlParameter("@ReporterId", reporterId),
                new SqlParameter("@FaultType", faultType),
                new SqlParameter("@FaultDesc", faultDesc),
                new SqlParameter("@Urgency", urgency),
            };
            return DbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 修改工单信息
        /// </summary>
        public int UpdateOrder(int recordId, int equipmentId, int reportDeptId,
                                string faultType, string faultDesc, string urgency)
        {
            string sql = @"
                UPDATE MaintenanceRecords 
                SET EquipmentId = @EquipmentId, ReportDeptId = @ReportDeptId,
                    FaultType = @FaultType, FaultDesc = @FaultDesc, Urgency = @Urgency
                WHERE RecordId = @RecordId";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@EquipmentId", equipmentId),
                new SqlParameter("@ReportDeptId", reportDeptId),
                new SqlParameter("@FaultType", faultType),
                new SqlParameter("@FaultDesc", faultDesc),
                new SqlParameter("@Urgency", urgency),
            };
            return DbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 获取设备列表（供下拉选择）
        /// </summary>
        public DataTable GetEquipmentList()
        {
            string sql = "SELECT EquipmentId, EquipmentName FROM Equipment ORDER BY EquipmentName";
            return DbHelper.GetDataTable(sql);
        }

        /// <summary>
        /// 获取科室列表（供下拉选择）
        /// </summary>
        public DataTable GetDeptList()
        {
            string sql = "SELECT DeptId, DeptName FROM Departments ORDER BY DeptName";
            return DbHelper.GetDataTable(sql);
        }

        /// <summary>
        /// 获取工程师列表（供指派下拉选择）
        /// </summary>
        public DataTable GetEngineerList()
        {
            string sql = "SELECT UserId, RealName FROM Users WHERE Role='repair' ORDER BY RealName";
            return DbHelper.GetDataTable(sql);
        }

        /// <summary>
        /// 指派维修人：更新 AssignedTo 并将进度改为 Assigned
        /// </summary>
        public int AssignRepairer(int recordId, int engineerId)
        {
            string sql = @"
                UPDATE MaintenanceRecords 
                SET AssignedTo = @EngineerId, ProgressStage = 'Assigned'
                WHERE RecordId = @RecordId AND ProgressStage = 'Pending'";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@EngineerId", engineerId));
        }

        /// <summary>
        /// 开始维修：已指派 → 处理中
        /// </summary>
        public int StartRepair(int recordId)
        {
            string sql = @"
                UPDATE MaintenanceRecords 
                SET ProgressStage = 'InProgress', Status = 'InProgress'
                WHERE RecordId = @RecordId AND ProgressStage = 'Assigned'";

            return DbHelper.ExecuteNonQuery(sql, new SqlParameter("@RecordId", recordId));
        }

        /// <summary>
        /// 完成维修：处理中 → 已完成
        /// </summary>
        public int CompleteRepair(int recordId)
        {
            string sql = @"
                UPDATE MaintenanceRecords 
                SET ProgressStage = 'Done', Status = 'Completed'
                WHERE RecordId = @RecordId AND ProgressStage = 'InProgress'";

            return DbHelper.ExecuteNonQuery(sql, new SqlParameter("@RecordId", recordId));
        }

        /// <summary>
        /// 根据 RecordId 获取单条工单详情
        /// </summary>
        public DataTable GetOrderById(int recordId)
        {
            string sql = @"
                SELECT m.*, e.EquipmentName, d.DeptName, ISNULL(u.RealName, '') AS RepairerName
                FROM MaintenanceRecords m
                LEFT JOIN Equipment e ON m.EquipmentId = e.EquipmentId
                LEFT JOIN Departments d ON m.ReportDeptId = d.DeptId
                LEFT JOIN Users u ON m.AssignedTo = u.UserId
                WHERE m.RecordId = @RecordId";
            return DbHelper.GetDataTable(sql, new SqlParameter("@RecordId", recordId));
        }

        /// <summary>
        /// 删除工单
        /// </summary>
        public int DeleteOrder(int recordId)
        {
            string sql = "DELETE FROM MaintenanceRecords WHERE RecordId = @RecordId";
            return DbHelper.ExecuteNonQuery(sql, new SqlParameter("@RecordId", recordId));
        }
    }
}
