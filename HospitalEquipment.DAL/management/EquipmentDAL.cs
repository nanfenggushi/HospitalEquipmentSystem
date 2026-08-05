using HospitalEquipment.Model.management;
using HospitalEquipment.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace HospitalEquipment.DAL.management
{
    public class EquipmentDAL
    {
        /// <summary>
        /// 分页查询设备列表（支持搜索、状态、科室过滤）
        /// </summary>
        /// <summary>
        /// 分页查询设备列表（支持搜索、状态、科室过滤）
        /// </summary>
        public (List<Equipment> list, int total) GetPaged(
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
            object countResult = DbHelper.ExecuteScalar(countSql, conditionParams.ToArray());
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

            SqlDataReader reader = DbHelper.ExecuteReader(sql, pagedParams.ToArray());
            if (reader == null) return (new List<Equipment>(), total);
            var list = DataReaderMapper.MapToList<Equipment>(reader);
            return (list, total);
        }

        /// <summary>
        /// 根据ID获取设备详情
        /// </summary>
        public Equipment GetById(int id)
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
                WHERE e.EquipmentId = @Id";
            SqlDataReader reader = DbHelper.ExecuteReader(sql, new SqlParameter("@Id", id));
            List<Equipment> list = DataReaderMapper.MapToList<Equipment>(reader);
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
        public List<KeyValuePair<int, string>> GetDepartments()
        {
            string sql = "SELECT DeptId, DeptName FROM Departments WHERE IsActive = 1 ORDER BY DeptName";
            SqlDataReader reader = DbHelper.ExecuteReader(sql);
            var list = new List<KeyValuePair<int, string>>();
            if (reader == null)
                return list;  // 连接失败返回空列表
            while (reader.Read())
            {
                list.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
            }
            reader.Close();
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
    }
}