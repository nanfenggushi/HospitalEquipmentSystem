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
                                       string keyword = null, string stage = null,
                                       DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            string sql = @"
                SELECT m.RecordId, m.RepairNo, e.EquipmentName, 
                       m.FaultType, m.FaultDesc, m.Urgency,
                       m.ProgressStage, d.DeptName, 
                       ISNULL(u.RealName, '未指派') AS RepairerName,
                       m.ReportTime, m.DowntimeHours, m.Status,
                       ISNULL(m.RejectReason, '') AS RejectReason,
                       ISNULL(m.PhotoPath, '') AS PhotoPath
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

            // 日期范围筛选
            if (dateFrom.HasValue)
            {
                sql += " AND m.ReportTime >= @DateFrom";
                parameters.Add(new SqlParameter("@DateFrom", dateFrom.Value));
            }
            if (dateTo.HasValue)
            {
                sql += " AND m.ReportTime < DATEADD(DAY, 1, @DateTo)";
                parameters.Add(new SqlParameter("@DateTo", dateTo.Value));
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
                     @Urgency, 'Pending', 'Pending', GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

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
            object result = DbHelper.ExecuteScalar(sql, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
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
                SET AssignedTo = @EngineerId, 
                    ProgressStage = 'Assigned', 
                    RejectReason = NULL
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
                SELECT m.*, e.EquipmentName, e.CategoryId, d.DeptName, ISNULL(u.RealName, '') AS RepairerName,
                       ISNULL(m.PhotoPath, '') AS PhotoPath,
                       ISNULL(m.AiFaultType, '') AS AiFaultType,
                       ISNULL(m.AiConfidence, 0) AS AiConfidence
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

        /// <summary>
        /// 医生端：删除自己的工单（限定 ReporterId，且状态为 Pending 或 Assigned，即未被维修员接单前）
        /// </summary>
        public int DeleteOrderByIdAndReporter(int recordId, int reporterId)
        {
            string sql = @"DELETE FROM MaintenanceRecords 
                            WHERE RecordId = @RecordId 
                              AND ReporterId = @ReporterId 
                              AND ProgressStage IN ('Pending', 'Assigned')";
            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@ReporterId", reporterId));
        }

        // ==================== 维修员端方法 ====================

        /// <summary>
        /// 维修员工作台：按维修员ID+阶段查询工单列表
        /// </summary>
        /// <param name="repairerId">维修员用户ID</param>
        /// <param name="stage">阶段：Assigned(待接单)/InProgress(处理中)/Done(已完成)，null=全部</param>
        public DataTable GetRepairerOrders(int repairerId, string stage = null)
        {
            string sql = @"
                SELECT m.RecordId, m.RepairNo, e.EquipmentName, 
                       m.FaultType, m.FaultDesc, m.Urgency,
                       m.ProgressStage, d.DeptName, 
                       ISNULL(u.RealName, '') AS RepairerName,
                       m.ReportTime, m.DowntimeHours, m.Status,
                       m.RepairResult, m.RepairCost, m.CompleteTime,
                       ISNULL(m.RejectReason, '') AS RejectReason,
                       ISNULL(m.PhotoPath, '') AS PhotoPath
                FROM MaintenanceRecords m
                LEFT JOIN Equipment e ON m.EquipmentId = e.EquipmentId
                LEFT JOIN Departments d ON m.ReportDeptId = d.DeptId
                LEFT JOIN Users u ON m.AssignedTo = u.UserId
                WHERE m.AssignedTo = @RepairerId";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@RepairerId", repairerId)
            };

            if (!string.IsNullOrEmpty(stage))
            {
                sql += " AND m.ProgressStage = @Stage";
                parameters.Add(new SqlParameter("@Stage", stage));
            }

            sql += " ORDER BY CASE m.ProgressStage WHEN 'Assigned' THEN 0 WHEN 'InProgress' THEN 1 WHEN 'Done' THEN 2 END, m.ReportTime DESC";
            return DbHelper.GetDataTable(sql, parameters.ToArray());
        }

        /// <summary>
        /// 维修员工作台：统计各阶段工单数量
        /// </summary>
        public DataTable GetRepairerStageCounts(int repairerId)
        {
            string sql = @"
                SELECT 
                    ISNULL(SUM(CASE WHEN ProgressStage = 'Assigned' THEN 1 ELSE 0 END), 0) AS PendingAccept,
                    ISNULL(SUM(CASE WHEN ProgressStage = 'InProgress' THEN 1 ELSE 0 END), 0) AS InProgress,
                    ISNULL(SUM(CASE WHEN ProgressStage = 'Done' THEN 1 ELSE 0 END), 0) AS Completed
                FROM MaintenanceRecords
                WHERE AssignedTo = @RepairerId";
            return DbHelper.GetDataTable(sql, new SqlParameter("@RepairerId", repairerId));
        }

        /// <summary>
        /// 维修员接单：Assigned → InProgress（同时验证是自己的单）
        /// </summary>
        public int AcceptOrder(int recordId, int repairerId)
        {
            string sql = @"
                UPDATE MaintenanceRecords 
                SET ProgressStage = 'InProgress', Status = 'InProgress'
                WHERE RecordId = @RecordId 
                  AND AssignedTo = @RepairerId 
                  AND ProgressStage = 'Assigned'";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@RepairerId", repairerId));
        }

        /// <summary>
        /// 维修员拒绝接单：Assigned → Pending（清空指派人，记录拒绝理由）
        /// </summary>
        public int RejectOrder(int recordId, int repairerId, string reason)
        {
            string sql = @"UPDATE MaintenanceRecords 
                        SET ProgressStage = 'Pending', 
                            AssignedTo = NULL, 
                            Status = 'Pending', 
                            RejectReason = @Reason 
                        WHERE RecordId = @RecordId 
                          AND AssignedTo = @RepairerId 
                          AND ProgressStage = 'Assigned'";
            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@RepairerId", repairerId),
                new SqlParameter("@Reason", reason ?? ""));
        }

        /// <summary>
        /// 维修员提交维修结果：InProgress → Done（填写维修结果、成本、完成时间）
        /// </summary>
        public int SubmitRepairResult(int recordId, string repairResult, decimal? repairCost, int? downtimeHours)
        {
            string sql = @"
                UPDATE MaintenanceRecords 
                SET ProgressStage = 'Done', 
                    Status = 'Completed',
                    RepairResult = @RepairResult,
                    RepairCost = @RepairCost,
                    DowntimeHours = @DowntimeHours,
                    CompleteTime = GETDATE()
                WHERE RecordId = @RecordId 
                  AND ProgressStage = 'InProgress'";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@RepairResult", (object)repairResult ?? DBNull.Value),
                new SqlParameter("@RepairCost", (object)repairCost ?? DBNull.Value),
                new SqlParameter("@DowntimeHours", (object)downtimeHours ?? DBNull.Value));
        }

        /// <summary>
        /// 管理员端：单独更新停机时长
        /// </summary>
        public int UpdateDowntimeHours(int recordId, int downtimeHours)
        {
            string sql = @"UPDATE MaintenanceRecords 
                           SET DowntimeHours = @DowntimeHours 
                           WHERE RecordId = @RecordId";
            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@DowntimeHours", downtimeHours),
                new SqlParameter("@RecordId", recordId));
        }

        // ==================== 医生端方法 ====================

        /// <summary>
        /// 医生端：查询某医生报修的工单列表
        /// </summary>
        /// <param name="reporterId">报修人 UserId</param>
        /// <param name="stage">阶段筛选：null=全部</param>
        public DataTable GetDoctorOrders(int reporterId, string stage = null)
        {
            string sql = @"
                SELECT m.RecordId, m.RepairNo, e.EquipmentName, 
                       m.FaultType, m.FaultDesc, m.Urgency,
                       m.ProgressStage, d.DeptName, 
                       ISNULL(u.RealName, '') AS RepairerName,
                       m.ReportTime, m.DowntimeHours, m.Status,
                       m.RepairResult, m.RepairCost, m.CompleteTime,
                       ISNULL(m.PhotoPath, '') AS PhotoPath
                FROM MaintenanceRecords m
                LEFT JOIN Equipment e ON m.EquipmentId = e.EquipmentId
                LEFT JOIN Departments d ON m.ReportDeptId = d.DeptId
                LEFT JOIN Users u ON m.AssignedTo = u.UserId
                WHERE m.ReporterId = @ReporterId";

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ReporterId", reporterId));

            if (!string.IsNullOrEmpty(stage))
            {
                sql += " AND m.ProgressStage = @Stage";
                parameters.Add(new SqlParameter("@Stage", stage));
            }

            sql += " ORDER BY m.ReportTime DESC";
            return DbHelper.GetDataTable(sql, parameters.ToArray());
        }

        /// <summary>
        /// 医生端：统计某医生各阶段工单数量
        /// </summary>
        public DataTable GetDoctorStageCounts(int reporterId)
        {
            string sql = @"
                SELECT 
                    ISNULL(SUM(CASE WHEN ProgressStage = 'Pending' THEN 1 ELSE 0 END), 0) AS Pending,
                    ISNULL(SUM(CASE WHEN ProgressStage = 'Assigned' THEN 1 ELSE 0 END), 0) AS Assigned,
                    ISNULL(SUM(CASE WHEN ProgressStage = 'InProgress' THEN 1 ELSE 0 END), 0) AS InProgress,
                    ISNULL(SUM(CASE WHEN ProgressStage = 'Done' THEN 1 ELSE 0 END), 0) AS Done,
                    COUNT(*) AS Total
                FROM MaintenanceRecords
                WHERE ReporterId = @ReporterId";

            return DbHelper.GetDataTable(sql, new SqlParameter("@ReporterId", reporterId));
        }

        /// <summary>
        /// 医生端：按科室获取设备列表（报修时下拉用）
        /// </summary>
        public DataTable GetEquipmentByDept(int deptId)
        {
            string sql = @"SELECT EquipmentId, EquipmentName, DeptId 
                           FROM Equipment 
                           WHERE IsActive = 1 AND DeptId = @DeptId
                           ORDER BY EquipmentName";
            return DbHelper.GetDataTable(sql, new SqlParameter("@DeptId", deptId));
        }

        /// <summary>
        /// 医生端：搜索全部设备（关键字过滤）
        /// </summary>
        public DataTable SearchEquipment(string keyword)
        {
            string sql = @"SELECT EquipmentId, EquipmentName, DeptId 
                           FROM Equipment 
                           WHERE IsActive = 1 AND EquipmentName LIKE '%' + @Keyword + '%'
                           ORDER BY EquipmentName";
            return DbHelper.GetDataTable(sql, new SqlParameter("@Keyword", keyword ?? ""));
        }

        /// <summary>
        /// 医生端：查询某医生借用中的设备（含待审批/已审批/超期状态，去重）
        /// 返回字段：EquipmentId, EquipmentName, DeptId(=ApplicantDeptId 即借用设备所属科室)
        /// </summary>
        public DataTable GetBorrowedEquipmentByApplicant(int applicantId)
        {
            string sql = @"
                SELECT DISTINCT e.EquipmentId, e.EquipmentName, b.ApplicantDeptId AS DeptId
                FROM BorrowRecords b
                INNER JOIN Equipment e ON b.EquipmentId = e.EquipmentId
                WHERE b.ApplicantId = @ApplicantId
                  AND b.Status IN ('Pending', 'Approved', 'Overdue')
                ORDER BY e.EquipmentName";
            return DbHelper.GetDataTable(sql, new SqlParameter("@ApplicantId", applicantId));
        }

        /// <summary>
        /// 查询维修表中已有的故障类型（DISTINCT，下拉框绑定用）
        /// </summary>
        public DataTable GetDistinctFaultTypes()
        {
            string sql = @"SELECT DISTINCT FaultType FROM MaintenanceRecords
                           WHERE FaultType IS NOT NULL AND LTRIM(RTRIM(FaultType)) <> ''
                           ORDER BY FaultType";
            return DbHelper.GetDataTable(sql);
        }

        /// <summary>
        /// 查询维修表中已有的紧急程度值（DISTINCT，下拉框绑定用）
        /// </summary>
        public DataTable GetDistinctUrgencies()
        {
            string sql = @"SELECT DISTINCT Urgency FROM MaintenanceRecords
                           WHERE Urgency IS NOT NULL AND LTRIM(RTRIM(Urgency)) <> ''
                           ORDER BY Urgency";
            return DbHelper.GetDataTable(sql);
        }

        // ==================== AI / 照片相关方法 ====================

        /// <summary>
        /// 写入故障照片路径（医生上传照片后调用）
        /// </summary>
        public int UpdatePhotoPath(int recordId, string photoPath)
        {
            string sql = "UPDATE MaintenanceRecords SET PhotoPath = @PhotoPath WHERE RecordId = @RecordId";
            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@PhotoPath", photoPath ?? ""));
        }

        /// <summary>
        /// 写入 AI 识别结果（Agent 分析完成后调用）
        /// </summary>
        public int UpdateAiResult(int recordId, string faultType, decimal confidence)
        {
            string sql = @"UPDATE MaintenanceRecords 
                           SET AiFaultType = @FaultType, AiConfidence = @Confidence 
                           WHERE RecordId = @RecordId";
            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@FaultType", faultType ?? ""),
                new SqlParameter("@Confidence", confidence));
        }

        /// <summary>
        /// 更新工单故障类型字段（维修员确认 / AI 自动确认后调用）
        /// </summary>
        public int UpdateFaultType(int recordId, string faultType)
        {
            string sql = @"UPDATE MaintenanceRecords 
                           SET FaultType = @FaultType 
                           WHERE RecordId = @RecordId";
            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@FaultType", faultType),
                new SqlParameter("@RecordId", recordId));
        }
    }
}
