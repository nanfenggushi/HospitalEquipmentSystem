using HospitalEquipment.Model.Dashboard;
using HospitalEquipment.Model.management;
using HospitalEquipmentSystem.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HospitalEquipment.DAL
{
    public class EquipmentDAL
    {
        /// <summary>
        /// 获取首页仪表盘的设备统计卡片汇总数据
        /// </summary>
        public StatisticCardDto GetStatisticCardData()
        {
            string sql = @"SELECT 
                         COUNT(*) AS TotalCount,
                         SUM(CASE WHEN Status IN ('Idle', 'InUse') THEN 1 ELSE 0 END) AS NormalCount,
                         SUM(CASE WHEN Status = 'Maintenance' THEN 1 ELSE 0 END) AS MaintenanceCount,
                         SUM(CASE WHEN Status = 'Borrowed' THEN 1 ELSE 0 END) AS BorrowedCount,
                         SUM(CASE WHEN Status = 'Scrapped' THEN 1 ELSE 0 END) AS ScrappedCount,
                         SUM(CASE WHEN Status = 'Idle' THEN 1 ELSE 0 END) AS IdleCount,
                         SUM(CASE WHEN Status = 'InUse' THEN 1 ELSE 0 END) AS InUseCount
                         FROM Equipment
                         WHERE IsActive = 1;";
            return DataReaderMapper.MapToList<StatisticCardDto>(DbHelper.ExecuteReader(sql))[0];
        }

        /// <summary>可借用的设备（空闲且无进行中的借用记录）</summary>
        public static List<Equipment> GetAvailable()
        {
            DataTable dt = DbHelper.GetDataTable(@"SELECT e.* FROM Equipment e
                WHERE e.IsActive = 1 AND e.Status = 'Idle'
                AND NOT EXISTS (SELECT 1 FROM BorrowRecords b
                    WHERE b.EquipmentId = e.EquipmentId AND b.Status IN ('Pending', 'Approved'))
                ORDER BY e.EquipmentName");
            return MapTableToList(dt);
        }

        /// <summary>全部在用设备（筛选下拉用）</summary>
        public static List<Equipment> GetAll()
        {
            DataTable dt = DbHelper.GetDataTable(@"SELECT * FROM Equipment
                WHERE IsActive = 1 ORDER BY EquipmentName");
            return MapTableToList(dt);
        }

        /// <summary>更新设备状态</summary>
        public static int SetStatus(int equipmentId, string status)
        {
            return DbHelper.ExecuteNonQuery(@"UPDATE Equipment SET Status = @status, UpdatedAt = GETDATE()
                WHERE EquipmentId = @id",
                new SqlParameter("@status", status),
                new SqlParameter("@id", equipmentId));
        }

        /// <summary>获取激活状态的设备总数（IsActive = 1）</summary>
        public int GetActiveCount()
        {
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Equipment WHERE IsActive = @Active",
                new SqlParameter("@Active", 1));
            return result != null ? (int)result : 0;
        }

        /// <summary>获取使用中的设备数（Status = 'InUse' 且激活）</summary>
        public int GetInUseCount()
        {
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Equipment WHERE Status = @Status AND IsActive = @Active",
                new SqlParameter("@Status", "InUse"),
                new SqlParameter("@Active", 1));
            return result != null ? (int)result : 0;
        }

        /// <summary>获取故障/维修中的设备数（Status = 'Maintenance' 且激活）</summary>
        public int GetMaintenanceCount()
        {
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Equipment WHERE Status = @Status AND IsActive = @Active",
                new SqlParameter("@Status", "Maintenance"),
                new SqlParameter("@Active", 1));
            return result != null ? (int)result : 0;
        }

        /// <summary>获取借用中的设备数（Status = 'Borrowed' 且激活）</summary>
        public int GetBorrowedCount()
        {
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Equipment WHERE Status = @Status AND IsActive = @Active",
                new SqlParameter("@Status", "Borrowed"),
                new SqlParameter("@Active", 1));
            return result != null ? (int)result : 0;
        }

        /// <summary>获取所有激活的设备列表（按 ID 排序）</summary>
        public List<Equipment> GetAllActive()
        {
            string sql = @"SELECT * FROM Equipment WHERE IsActive = @Active ORDER BY EquipmentId";
            using (var reader = DbHelper.ExecuteReader(sql,
                new SqlParameter("@Active", 1)))
            {
                return reader != null ? DataReaderMapper.MapToList<Equipment>(reader) : new List<Equipment>();
            }
        }

        private static List<Equipment> MapTableToList(DataTable dt)
        {
            var list = new List<Equipment>();
            foreach (DataRow row in dt.Rows)
            {
                var e = new Equipment {
                    EquipmentId = Convert.ToInt32(row["EquipmentId"]),
                    EquipmentNo = Convert.ToString(row["EquipmentNo"]),
                    EquipmentName = Convert.ToString(row["EquipmentName"]),
                    Model = row["Model"] == DBNull.Value ? null : Convert.ToString(row["Model"]),
                    Manufacturer = row["Manufacturer"] == DBNull.Value ? null : Convert.ToString(row["Manufacturer"]),
                    SupplierId = row["SupplierId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["SupplierId"]),
                    CategoryId = row["CategoryId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["CategoryId"]),
                    DeptId = row["DeptId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["DeptId"]),
                    Location = row["Location"] == DBNull.Value ? null : Convert.ToString(row["Location"]),
                    ResponsibleUserId = row["ResponsibleUserId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["ResponsibleUserId"]),
                    Price = row["Price"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(row["Price"]),
                    PurchaseDate = row["PurchaseDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["PurchaseDate"]),
                    WarrantyMonths = row["WarrantyMonths"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["WarrantyMonths"]),
                    ServiceLife = row["ServiceLife"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["ServiceLife"]),
                    LastMaintainDate = row["LastMaintainDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["LastMaintainDate"]),
                    NextMaintainDate = row["NextMaintainDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NextMaintainDate"]),
                    Status = Convert.ToString(row["Status"]),
                    Remarks = row["Remarks"] == DBNull.Value ? null : Convert.ToString(row["Remarks"]),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    UpdatedAt = row["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["UpdatedAt"])
                };
                list.Add(e);
            }
            return list;
        }


        public List<CategoryUsageDto> GetCategoryUsageData()
        {
            string sql = @"SELECT 
                         c.CategoryName,
                         CAST(
                            CASE 
                            WHEN COUNT(e.EquipmentId) = 0 THEN 0.0
       ELSE SUM(CASE WHEN e.Status IN ('InUse', 'Borrowed') THEN 1.0 ELSE 0 END) * 100.0 / COUNT(e.EquipmentId)
                         END AS DECIMAL(5, 1)
                         ) AS UsageRate
                         FROM EquipmentCategories c
             LEFT JOIN Equipment e ON c.CategoryId = e.CategoryId AND e.IsActive = 1 AND e.Status != 'Scrapped'
                        GROUP BY c.CategoryId, c.CategoryName;
                         ";

            return DataReaderMapper.MapToList<CategoryUsageDto>(DbHelper.ExecuteReader(sql));
        }

        /// <summary>
        /// 分页查询设备列表（支持搜索、状态、科室过滤）
        /// </summary>
        /// <summary>
        /// 分页查询设备列表（支持搜索、状态、科室过滤）
        /// </summary>
        public async Task<(List<Equipment> list, int total)> GetPaged(
            int pageIndex,
            int pageSize,
            string keyword = "",
            string status = "",
            int? deptId = null)
        {
            // 构建 WHERE 条件（不包含参数对象）
            var conditions = new List<string> { "e.IsActive = 1" };
            var conditionParams = new List<SqlParameter>(); // 用于计数的参数

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                conditions.Add("(e.EquipmentNo LIKE @Keyword OR e.EquipmentName LIKE @Keyword OR e.Model LIKE @Keyword)");
                conditionParams.Add(new SqlParameter("@Keyword", $"%{keyword}%"));
            }
            // 改为（兼容空字符串表示"全部"）
            if (!string.IsNullOrWhiteSpace(status))
            {
                conditions.Add("e.Status = @Status");
                conditionParams.Add(new SqlParameter("@Status", status));
            }

            if (deptId.HasValue && deptId.Value > 0)
            {
                conditions.Add("e.DeptId = @DeptId");
                conditionParams.Add(new SqlParameter("@DeptId", deptId.Value));
            }

            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";

            // 1. 查询总数（使用 conditionParams）
            string countSql = $"SELECT COUNT(*) FROM Equipment e {whereClause}";
            object countResult = await DbHelper.ExecuteScalarAsync(countSql, conditionParams.ToArray()).ConfigureAwait(false);
            int total = countResult != null ? Convert.ToInt32(countResult) : 0;

            // 2. 查询分页数据（重新创建参数对象，不重用之前的）
            int offset = (pageIndex - 1) * pageSize;

            // 重新构建参数列表（全部是新实例）
            var pagedParams = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
                pagedParams.Add(new SqlParameter("@Keyword", $"%{keyword}%"));

            if (!string.IsNullOrWhiteSpace(status) && status != "全部")
                pagedParams.Add(new SqlParameter("@Status", status));

            if (deptId.HasValue && deptId.Value > 0)
                pagedParams.Add(new SqlParameter("@DeptId", deptId.Value));

            pagedParams.Add(new SqlParameter("@Offset", offset));
            pagedParams.Add(new SqlParameter("@PageSize", pageSize));

            string sql = $@"
        SELECT 
            e.EquipmentId, e.EquipmentNo, e.EquipmentName, e.Model, e.Manufacturer,
            e.SupplierId, e.CategoryId, e.DeptId, e.Location, e.ResponsibleUserId,
            e.Price, e.PurchaseDate, e.WarrantyMonths, e.ServiceLife,
            e.LastMaintainDate, e.NextMaintainDate, e.Status, e.Remarks,
            e.IsActive, e.CreatedAt, e.UpdatedAt,
            d.DeptName,
            c.CategoryName,
            s.SupplierName,
            u.RealName AS ResponsibleUserRealName
        FROM Equipment e
        LEFT JOIN Departments d ON e.DeptId = d.DeptId
        LEFT JOIN EquipmentCategories c ON e.CategoryId = c.CategoryId
        LEFT JOIN Suppliers s ON e.SupplierId = s.SupplierId
        LEFT JOIN Users u ON e.ResponsibleUserId = u.UserId
        {whereClause}
        ORDER BY e.EquipmentId DESC
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, pagedParams.ToArray()).ConfigureAwait(false))
            {
                if (reader == null) return (new List<Equipment>(), total);
                var list = DataReaderMapper.MapToList<Equipment>(reader);
                return (list, total);
            }
        }

        /// <summary>
        /// 根据ID获取设备详情（含关联名称）
        /// </summary>
        public static Equipment GetById(int id)
        {
            string sql = @"
        SELECT 
            e.*,
            d.DeptName,
            c.CategoryName,
            s.SupplierName,
            u.RealName AS ResponsibleUserRealName
        FROM Equipment e
        LEFT JOIN Departments d ON e.DeptId = d.DeptId
        LEFT JOIN EquipmentCategories c ON e.CategoryId = c.CategoryId
        LEFT JOIN Suppliers s ON e.SupplierId = s.SupplierId
        LEFT JOIN Users u ON e.ResponsibleUserId = u.UserId
        WHERE e.EquipmentId = @Id AND e.IsActive = 1";
            SqlDataReader reader = DbHelper.ExecuteReader(sql, new SqlParameter("@Id", id));
            var list = DataReaderMapper.MapToList<Equipment>(reader);
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 新增设备
        /// </summary>
        public int Insert(Equipment eq)
        {
            string sql = @"
                INSERT INTO Equipment (
                    EquipmentNo, EquipmentName, Model, Manufacturer,
                    SupplierId, CategoryId, DeptId, Location,
                    ResponsibleUserId, Price, PurchaseDate,
                    WarrantyMonths, ServiceLife, LastMaintainDate,
                    NextMaintainDate, Status, Remarks, IsActive, CreatedAt
                ) VALUES (
                    @No, @Name, @Model, @Manufacturer,
                    @SupplierId, @CategoryId, @DeptId, @Location,
                    @ResponsibleUserId, @Price, @PurchaseDate,
                    @WarrantyMonths, @ServiceLife, @LastMaintainDate,
                    @NextMaintainDate, @Status, @Remarks, 1, GETDATE()
                )";
            var p = GetParameters(eq);
            return DbHelper.ExecuteNonQuery(sql, p);
        }

        /// <summary>
        /// 更新设备
        /// </summary>
        public int Update(Equipment eq)
        {
            string sql = @"
                UPDATE Equipment SET
                    EquipmentNo = @No,
                    EquipmentName = @Name,
                    Model = @Model,
                    Manufacturer = @Manufacturer,
                    SupplierId = @SupplierId,
                    CategoryId = @CategoryId,
                    DeptId = @DeptId,
                    Location = @Location,
                    ResponsibleUserId = @ResponsibleUserId,
                    Price = @Price,
                    PurchaseDate = @PurchaseDate,
                    WarrantyMonths = @WarrantyMonths,
                    ServiceLife = @ServiceLife,
                    LastMaintainDate = @LastMaintainDate,
                    NextMaintainDate = @NextMaintainDate,
                    Status = @Status,
                    Remarks = @Remarks,
                    UpdatedAt = GETDATE()
                WHERE EquipmentId = @Id";
            var p = GetParameters(eq);
            var list = new List<SqlParameter>(p) { new SqlParameter("@Id", eq.EquipmentId) };
            return DbHelper.ExecuteNonQuery(sql, list.ToArray());
        }

        /// <summary>
        /// 软删除设备（IsActive = 0）
        /// </summary>
        public int Delete(int id)
        {
            string sql = "UPDATE Equipment SET IsActive = 0, UpdatedAt = GETDATE() WHERE EquipmentId = @Id";
            return DbHelper.ExecuteNonQuery(sql, new SqlParameter("@Id", id));
        }

        private SqlParameter[] GetParameters(Equipment eq)
        {
            return new SqlParameter[] {
                new SqlParameter("@No", eq.EquipmentNo ?? ""),
                new SqlParameter("@Name", eq.EquipmentName ?? ""),
                new SqlParameter("@Model", eq.Model ?? ""),
                new SqlParameter("@Manufacturer", eq.Manufacturer ?? ""),
                new SqlParameter("@SupplierId", (object)eq.SupplierId ?? DBNull.Value),
                new SqlParameter("@CategoryId", (object)eq.CategoryId ?? DBNull.Value),
                new SqlParameter("@DeptId", (object)eq.DeptId ?? DBNull.Value),
                new SqlParameter("@Location", eq.Location ?? ""),
                new SqlParameter("@ResponsibleUserId", (object)eq.ResponsibleUserId ?? DBNull.Value),
                new SqlParameter("@Price", (object)eq.Price ?? DBNull.Value),
                new SqlParameter("@PurchaseDate", (object)eq.PurchaseDate ?? DBNull.Value),
                new SqlParameter("@WarrantyMonths", (object)eq.WarrantyMonths ?? DBNull.Value),
                new SqlParameter("@ServiceLife", (object)eq.ServiceLife ?? DBNull.Value),
                new SqlParameter("@LastMaintainDate", (object)eq.LastMaintainDate ?? DBNull.Value),
                new SqlParameter("@NextMaintainDate", (object)eq.NextMaintainDate ?? DBNull.Value),
                new SqlParameter("@Status", eq.Status ?? "Idle"),
                new SqlParameter("@Remarks", eq.Remarks ?? "")
            };
        }

        /// <summary>
        /// 获取所有启用科室（用于筛选下拉）
        /// </summary>
        public async Task<List<KeyValuePair<int, string>>> GetDepartments()
        {
            string sql = "SELECT DeptId, DeptName FROM Departments WHERE IsActive = 1 ORDER BY DeptName";
            var list = new List<KeyValuePair<int, string>>();
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql).ConfigureAwait(false))
            {
                if (reader == null)
                    return list;  // 连接失败返回空列表
                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    list.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取设备状态列表（用于下拉筛选）
        /// </summary>
        public List<string> GetStatusList()
        {
            return new List<string> { "全部", "Idle", "InUse", "Maintenance", "Borrowed", "Scrapped" };
        }

        /// <summary>
        /// 获取状态对应的中文显示
        /// </summary>
        public static string GetStatusChinese(string status)
        {
            switch (status)
            {
                case "Idle":
                    return "闲置";
                case "InUse":
                    return "使用中";
                case "Maintenance":
                    return "维修中";
                case "Borrowed":
                    return "已借用";
                case "Scrapped":
                    return "已报废";
                default:
                    return status;
            }
        }

        /// <summary>获取最近6个月的月度维保工单趋势</summary>
        public List<MaintenanceTrendDto> GetMaintenanceTrend()
        {
            string sql = @"SELECT 
                         DATENAME(MONTH, ReportTime) + '月' AS Month,
                         COUNT(*) AS Count
                         FROM MaintenanceRecords
                         WHERE ReportTime >= DATEADD(MONTH, -5, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
                         GROUP BY MONTH(ReportTime)
                         ORDER BY MONTH(ReportTime);";
            return DataReaderMapper.MapToList<MaintenanceTrendDto>(DbHelper.ExecuteReader(sql));
        }
    }
}
