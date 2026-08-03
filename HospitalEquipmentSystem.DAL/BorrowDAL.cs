using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HospitalEquipment.Model;
using HospitalEquipment.Util;
using HospitalEquipmentSystem.Common;

namespace HospitalEquipment.DAL
{
    /// <summary>
    /// 借用记录数据访问
    /// </summary>
    public static class BorrowDAL
    {
        private const string BaseSelect = @"SELECT b.BorrowId, b.BorrowNo, b.EquipmentId, b.ApplicantId, b.ApplicantDeptId, b.Purpose,
            b.ExpectedReturnDate, b.ApproverId, b.ApproveDate, b.Status, b.ActualReturnDate, b.ReturnNote, b.Remarks,
            b.CreatedAt, b.UpdatedAt,
            e.EquipmentNo, e.EquipmentName, e.Status AS EquipmentStatus,
            u.RealName AS ApplicantName, d.DeptName AS ApplicantDeptName, au.RealName AS ApproverName
            FROM BorrowRecords b
            LEFT JOIN Equipment e ON b.EquipmentId = e.EquipmentId
            LEFT JOIN Users u ON b.ApplicantId = u.UserId
            LEFT JOIN Departments d ON b.ApplicantDeptId = d.DeptId
            LEFT JOIN Users au ON b.ApproverId = au.UserId";

        /// <summary>
        /// 分页查询借用记录
        /// </summary>
        public static List<BorrowRecord> GetPage(string keyword, string status, int equipmentId,
            DateTime? from, DateTime? to, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            string kw = "%" + (keyword ?? "").Trim() + "%";
            string where = @"WHERE (@keyword = '' OR b.BorrowNo LIKE @kw OR e.EquipmentName LIKE @kw
                OR e.EquipmentNo LIKE @kw OR u.RealName LIKE @kw)
                AND (@status = '' OR b.Status = @status)
                AND (@equipmentId = 0 OR b.EquipmentId = @equipmentId)
                AND (@from IS NULL OR b.ExpectedReturnDate >= @from)
                AND (@to IS NULL OR b.ExpectedReturnDate <= @to)";

            DataTable dt = DbHelper.GetDataTable(@"SELECT COUNT(*) AS Cnt FROM BorrowRecords b
                LEFT JOIN Equipment e ON b.EquipmentId = e.EquipmentId
                LEFT JOIN Users u ON b.ApplicantId = u.UserId
                LEFT JOIN Departments d ON b.ApplicantDeptId = d.DeptId " + where,
                MakeFilterParams(keyword, status, equipmentId, from, to).ToArray());
            if (dt.Rows.Count > 0)
            {
                total = Convert.ToInt32(dt.Rows[0]["Cnt"]);
            }

            string sql = BaseSelect + " " + where + @" ORDER BY b.BorrowId DESC
                OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

            var pars = MakeFilterParams(keyword, status, equipmentId, from, to);
            pars.Add(new SqlParameter("@offset", (pageIndex - 1) * pageSize));
            pars.Add(new SqlParameter("@pageSize", pageSize));

            DataTable rows = DbHelper.GetDataTable(sql, pars.ToArray());

            return MapTableToList(rows);
        }

        private static List<SqlParameter> MakeFilterParams(string keyword, string status, int equipmentId,
            DateTime? from, DateTime? to)
        {
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@keyword", keyword ?? ""));
            pars.Add(new SqlParameter("@kw", "%" + (keyword ?? "").Trim() + "%"));
            pars.Add(new SqlParameter("@status", status ?? ""));
            pars.Add(new SqlParameter("@equipmentId", equipmentId));
            pars.Add(new SqlParameter("@from", from.HasValue ? (object)from.Value.Date : DBNull.Value));
            pars.Add(new SqlParameter("@to", to.HasValue ? (object)to.Value.Date : DBNull.Value));
            return pars;
        }

        /// <summary>按 Id 查询单条记录</summary>
        public static BorrowRecord GetById(int id)
        {
            DataTable dt = DbHelper.GetDataTable(BaseSelect + " WHERE b.BorrowId = @id",
                new SqlParameter("@id", id));
            if (dt.Rows.Count == 0) return null;
            return MapTableToList(dt)[0];
        }

        /// <summary>查询某日应还的借用记录（日历视图）</summary>
        public static List<BorrowRecord> GetByReturnDate(DateTime date)
        {
            DataTable dt = DbHelper.GetDataTable(BaseSelect + " WHERE b.ExpectedReturnDate = @date",
                new SqlParameter("@date", date.Date));

            return MapTableToList(dt);
        }

        /// <summary>所有应还日期（用于日历加粗标记）</summary>
        public static List<DateTime> GetReturnDates()
        {
            var list = new List<DateTime>();
            using (var reader = DbHelper.ExecuteReader(
                "SELECT DISTINCT ExpectedReturnDate FROM BorrowRecords WHERE ExpectedReturnDate IS NOT NULL"))
            {
                
                while (reader.Read())
                {
                    list.Add(Convert.ToDateTime(reader[0]));
                }
            }
            return list;
        }

        /// <summary>新增借用申请，返回新记录 Id</summary>
        public static int Insert(BorrowRecord r)
        {
            object o = DbHelper.ExecuteScalar(@"INSERT INTO BorrowRecords
                (BorrowNo, EquipmentId, ApplicantId, ApplicantDeptId, Purpose, ExpectedReturnDate, Status, Remarks, CreatedAt, UpdatedAt)
                VALUES (@BorrowNo, @EquipmentId, @ApplicantId, @ApplicantDeptId, @Purpose, @ExpectedReturnDate, @Status, @Remarks, GETDATE(), GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT)",
                new SqlParameter("@BorrowNo", r.BorrowNo),
                new SqlParameter("@EquipmentId", r.EquipmentId),
                new SqlParameter("@ApplicantId", r.ApplicantId),
                new SqlParameter("@ApplicantDeptId", (object)r.ApplicantDeptId ?? DBNull.Value),
                new SqlParameter("@Purpose", (object)r.Purpose ?? DBNull.Value),
                new SqlParameter("@ExpectedReturnDate", r.ExpectedReturnDate.Date),
                new SqlParameter("@Status", r.Status),
                new SqlParameter("@Remarks", (object)r.Remarks ?? DBNull.Value));
            if (o == null || o == DBNull.Value) return 0;
            return Convert.ToInt32(o);
        }

        /// <summary>审批（通过/驳回），仅待审批状态可操作</summary>
        public static int Approve(int id, int approverId, string toStatus)
        {
            return DbHelper.ExecuteNonQuery(@"UPDATE BorrowRecords
                SET Status = @toStatus, ApproverId = @approverId, ApproveDate = GETDATE(), UpdatedAt = GETDATE()
                WHERE BorrowId = @id AND Status = 'Pending'",
                new SqlParameter("@toStatus", toStatus),
                new SqlParameter("@approverId", approverId),
                new SqlParameter("@id", id));
        }

        /// <summary>审批通过并联动设备状态（同一事务）</summary>
        public static int ApproveWithEquipment(int borrowId,int approverId,int equipmentId)
        {
            return DbHelper.ExecuteInTransaction((conn, tx) =>
            {
                int n = DbHelper.ExecuteNonQuery(conn, tx,
                 @"UPDATE BorrowRecords
              SET Status = 'Approved', ApproverId = @approverId, ApproveDate = GETDATE(), UpdatedAt = GETDATE()
              WHERE BorrowId = @id AND Status = 'Pending'",
            new SqlParameter("@approverId", approverId),
            new SqlParameter("@id", borrowId));

                if (n>0)
                
                    DbHelper.ExecuteNonQuery(conn,tx,
                      @"UPDATE Equipment SET Status = 'Borrowed', UpdatedAt = GETDATE() WHERE EquipmentId = @id",
                       new SqlParameter("@id",equipmentId));
                return n;
            });
        }

        /// <summary>归还验收并联动设备状态/维修工单（同一事务）</summary>
        public static int ReturnBorrowFull(int borrowId, string returnNote, DateTime returnDate, int equipmentId,
            bool hasDamage, string damageDesc, int operatorId, int? operatorDeptId)
        {
            return DbHelper.ExecuteInTransaction((conn, tx) =>
            {
                int n = DbHelper.ExecuteNonQuery(conn,tx, 
                    @"UPDATE BorrowRecords
              SET Status = 'Returned', ActualReturnDate = @date, ReturnNote = @note, UpdatedAt = GETDATE()
              WHERE BorrowId = @id AND Status IN ('Approved', 'Overdue')",
                    new SqlParameter("@date",returnDate.Date),
                    new SqlParameter("@note",(object)returnNote??DBNull.Value),
                    new SqlParameter("@id",borrowId));

                if (n <= 0) return 0;

                DbHelper.ExecuteNonQuery(conn, tx,
                    @"UPDATE Equipment SET Status = @status, UpdatedAt = GETDATE() WHERE EquipmentId = @id",
                    new SqlParameter("@status",hasDamage?BorrowConst.EqMaintenance:BorrowConst.EqIdle),
                    new SqlParameter("@id",equipmentId)
                    );

                if (hasDamage)
                {
                    string repairNo = "R-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + equipmentId;
                    DbHelper.ExecuteNonQuery(conn, tx,
                        @"INSERT INTO MaintenanceRecords
                  (RepairNo, EquipmentId, ReporterId, ReportDeptId, FaultDesc, FaultType, Urgency, ProgressStage, Status, ReportTime, CreatedAt)
                  VALUES (@RepairNo, @EquipmentId, @ReporterId, @ReportDeptId, @FaultDesc, '借用损坏', 'Normal', 'Pending', 'Pending', GETDATE(), GETDATE())",
                        new SqlParameter("@RepairNo", repairNo),
                        new SqlParameter("@EquipmentId", equipmentId),
                        new SqlParameter("@ReporterId", operatorId),
                        new SqlParameter("@ReportDeptId", (object)operatorDeptId ?? DBNull.Value),
                        new SqlParameter("@FaultDesc", damageDesc));
                }
                return n;
            });
        }

        /// <summary>登记归还</summary>
        public static int ReturnBorrow(int id, string returnNote, DateTime returnDate)
        {
            return DbHelper.ExecuteNonQuery(@"UPDATE BorrowRecords
                SET Status = 'Returned', ActualReturnDate = @date, ReturnNote = @note, UpdatedAt = GETDATE()
                WHERE BorrowId = @id AND Status IN ('Approved', 'Overdue')",
                new SqlParameter("@date", returnDate.Date),
                new SqlParameter("@note", (object)returnNote ?? DBNull.Value),
                new SqlParameter("@id", id));
        }

        /// <summary>自动标记超期：已审批通过且应还日期已过 → 超期</summary>
        public static int MarkOverdue()
        {
            return DbHelper.ExecuteNonQuery(@"UPDATE BorrowRecords
                SET Status = 'Overdue', UpdatedAt = GETDATE()
                WHERE Status = 'Approved' AND ExpectedReturnDate < CAST(GETDATE() AS DATE)");
        }

        /// <summary>按状态统计数量</summary>
        public static int CountByStatus(string status)
        {
            object o = DbHelper.ExecuteScalar("SELECT COUNT(*) FROM BorrowRecords WHERE Status = @status",
                new SqlParameter("@status", status));
            return o == null || o == DBNull.Value ? 0 : Convert.ToInt32(o);
        }

        /// <summary>今日到期数量（已审批通过且应还日期为今天）</summary>
        public static int CountDueToday()
        {
            object o = DbHelper.ExecuteScalar(@"SELECT COUNT(*) FROM BorrowRecords
                WHERE Status = 'Approved' AND ExpectedReturnDate = CAST(GETDATE() AS DATE)");
            return o == null || o == DBNull.Value ? 0 : Convert.ToInt32(o);
        }

        /// <summary>设备是否已有进行中的借用（待审批/借用中）</summary>
        public static bool HasActiveBorrow(int equipmentId)
        {
            object o = DbHelper.ExecuteScalar(@"SELECT COUNT(*) FROM BorrowRecords
                WHERE EquipmentId = @equipmentId AND Status IN ('Pending', 'Approved')",
                new SqlParameter("@equipmentId", equipmentId));
            return o != null && o != DBNull.Value && Convert.ToInt32(o) > 0;
        }

        /// <summary>生成借用单号：B-年月-序号</summary>
        public static string GenerateBorrowNo()
        {
            string ym = DateTime.Now.ToString("yyyy-MM");
            object o = DbHelper.ExecuteScalar(@"
        MERGE dbo.BorrowNoCounter WITH (HOLDLOCK) AS t
        USING (SELECT @ym) AS s(ym) ON t.YearMonth = s.ym
        WHEN MATCHED THEN UPDATE SET LastSeq = t.LastSeq + 1
        WHEN NOT MATCHED THEN INSERT (YearMonth, LastSeq) VALUES (@ym, 1)
        OUTPUT INSERTED.LastSeq;",
                new SqlParameter("@ym", ym));
            int n = o == null || o == DBNull.Value ? 1 : Convert.ToInt32(o);
            return string.Format("B-{0}-{1:D3}", ym, n);
        }

        /// <summary>归还损坏时创建维修工单</summary>
        public static int CreateMaintenance(int equipmentId, string faultDesc, int reporterId, int? reportDeptId)
        {
            string repairNo = "R-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + equipmentId;
            return DbHelper.ExecuteNonQuery(@"INSERT INTO MaintenanceRecords
                (RepairNo, EquipmentId, ReporterId, ReportDeptId, FaultDesc, FaultType, Urgency, ProgressStage, Status, ReportTime, CreatedAt)
                VALUES (@RepairNo, @EquipmentId, @ReporterId, @ReportDeptId, @FaultDesc, '借用损坏', 'Normal', 'Pending', 'Pending', GETDATE(), GETDATE())",
                new SqlParameter("@RepairNo", repairNo),
                new SqlParameter("@EquipmentId", equipmentId),
                new SqlParameter("@ReporterId", reporterId),
                new SqlParameter("@ReportDeptId", (object)reportDeptId ?? DBNull.Value),
                new SqlParameter("@FaultDesc", faultDesc));
        }

        // ==================== 统计图表 ====================

        /// <summary>借用次数 TOP10 设备（柱状图）</summary>
        public static List<CountItem> GetTopEquipment(int top)
        {
            return MapCountTable(DbHelper.GetDataTable(@"SELECT TOP (@top) ISNULL(e.EquipmentName, '未知设备') AS Name, COUNT(*) AS Value
                FROM BorrowRecords b LEFT JOIN Equipment e ON b.EquipmentId = e.EquipmentId
                GROUP BY e.EquipmentId, e.EquipmentName ORDER BY COUNT(*) DESC",
                new SqlParameter("@top", top)));
        }

        /// <summary>科室借用占比（饼图）</summary>
        public static List<CountItem> GetByDept()
        {
            return MapCountTable(DbHelper.GetDataTable(@"SELECT ISNULL(d.DeptName, '未分配') AS Name, COUNT(*) AS Value
                FROM BorrowRecords b LEFT JOIN Departments d ON b.ApplicantDeptId = d.DeptId
                GROUP BY ISNULL(d.DeptName, '未分配') ORDER BY COUNT(*) DESC"));
        }

        /// <summary>按月统计借用数量（折线图）</summary>
        public static List<CountItem> GetBorrowByMonth()
        {
            return MapCountTable(DbHelper.GetDataTable(@"SELECT CONVERT(varchar(7), CreatedAt, 120) AS Name, COUNT(*) AS Value
                FROM BorrowRecords GROUP BY CONVERT(varchar(7), CreatedAt, 120)"));
        }

        /// <summary>按月统计归还数量（折线图）</summary>
        public static List<CountItem> GetReturnByMonth()
        {
            return MapCountTable(DbHelper.GetDataTable(@"SELECT CONVERT(varchar(7), ActualReturnDate, 120) AS Name, COUNT(*) AS Value
                FROM BorrowRecords WHERE ActualReturnDate IS NOT NULL GROUP BY CONVERT(varchar(7), ActualReturnDate, 120)"));
        }

        /// <summary>设备状态分布（环形图）</summary>
        public static List<CountItem> GetEquipmentStatus()
        {
            return MapCountTable(DbHelper.GetDataTable(@"SELECT Status AS Name, COUNT(*) AS Value
                FROM Equipment WHERE IsActive = 1 GROUP BY Status"));
        }

        private static List<CountItem> MapCountTable(DataTable dt)
        {
            var list = new List<CountItem>();
            
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new CountItem
                {
                    Name = Convert.ToString(row["Name"]),
                    Value = Convert.ToInt32(row["Value"])
                });
            }
            return list;
        }

        private static List<BorrowRecord> MapTableToList(DataTable dt)
        {
            var list = new List<BorrowRecord>();
            foreach (DataRow row in dt.Rows)
            {
                var r = new BorrowRecord
                {
                    BorrowId = Convert.ToInt32(row["BorrowId"]),
                    BorrowNo = Convert.ToString(row["BorrowNo"]),
                    EquipmentId = Convert.ToInt32(row["EquipmentId"]),
                    ApplicantId = Convert.ToInt32(row["ApplicantId"]),
                    ApplicantDeptId = row["ApplicantDeptId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["ApplicantDeptId"]),
                    Purpose = row["Purpose"] == DBNull.Value ? null : Convert.ToString(row["Purpose"]),
                    ExpectedReturnDate = Convert.ToDateTime(row["ExpectedReturnDate"]),
                    ApproverId = row["ApproverId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["ApproverId"]),
                    ApproveDate = row["ApproveDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["ApproveDate"]),
                    Status = Convert.ToString(row["Status"]),
                    ActualReturnDate = row["ActualReturnDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["ActualReturnDate"]),
                    ReturnNote = row["ReturnNote"] == DBNull.Value ? null : Convert.ToString(row["ReturnNote"]),
                    Remarks = row["Remarks"] == DBNull.Value ? null : Convert.ToString(row["Remarks"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    UpdatedAt = row["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["UpdatedAt"]),
                    EquipmentNo = row["EquipmentNo"] == DBNull.Value ? null : Convert.ToString(row["EquipmentNo"]),
                    EquipmentName = row["EquipmentName"] == DBNull.Value ? null : Convert.ToString(row["EquipmentName"]),
                    EquipmentStatus = row["EquipmentStatus"] == DBNull.Value ? null : Convert.ToString(row["EquipmentStatus"]),
                    ApplicantName = row["ApplicantName"] == DBNull.Value ? null : Convert.ToString(row["ApplicantName"]),
                    ApplicantDeptName = row["ApplicantDeptName"] == DBNull.Value ? null : Convert.ToString(row["ApplicantDeptName"]),
                    ApproverName = row["ApproverName"] == DBNull.Value ? null : Convert.ToString(row["ApproverName"])
                };
                list.Add(r);
            }
            return list;
        }
    }
}
