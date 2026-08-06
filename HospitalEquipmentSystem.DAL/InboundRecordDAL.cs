using HospitalEquipment.Model;
using HospitalEquipmentSystem.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalEquipment.DAL
{
    public class InboundRecordDAL
    {
        /// <summary>
        /// 分页查询入库记录列表
        /// </summary>
        /// <param name="pageIndex">页码（从1开始）</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="keyword">搜索关键字（入库单号）</param>
        /// <param name="auditStatus">审核状态筛选</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        public async Task<(List<InboundRecord>list,int total)> GetPaged(
            int pageIndex,
            int pageSize,
            string keyword = "",
            string auditStatus = "",
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            //构建 where 条件
            var conditions= new List<string>();
            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                conditions.Add("i.InboundNo LIKE @Keyword");
                parameters.Add(new SqlParameter("@Keyword", $"%{keyword}%"));
            }
            if (!string.IsNullOrEmpty(auditStatus)&&auditStatus!="全部")
            {
                conditions.Add("i.AuditStatus = @AuditStatus");
                parameters.Add(new SqlParameter("@AuditStatus", auditStatus));
            }
            if (startDate.HasValue)
            {
                conditions.Add("i.InboundDate >= @StartDate");
                parameters.Add(new SqlParameter("@StartDate", startDate.Value.Date));
            }
            if (endDate.HasValue)
            {
                conditions.Add("i.InboundDate <= @EndDate");
                parameters.Add(new SqlParameter("@EndDate", endDate.Value.Date.AddDays(1).AddSeconds(-1)));
            }

            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";

            // 查询总数
            string countSql = $"SELECT COUNT(*) FROM InboundRecords i {whereClause}";
            object countResult = await DbHelper.ExecuteScalarAsync(countSql, parameters.ToArray()).ConfigureAwait(false);
            int total = countResult != null ? Convert.ToInt32(countResult) : 0;

            // 查询分页数据
            int offset = (pageIndex - 1) * pageSize;
            var pagedParams = new List<SqlParameter>(parameters);
            pagedParams.Add(new SqlParameter("@Offset", offset));
            pagedParams.Add(new SqlParameter("@PageSize", pageSize));
                
            string sql = $@"
                SELECT 
                    i.InboundId, i.EquipmentId, i.InboundNo, i.Supplier,
                    i.PurchasePrice, i.Quantity, i.InboundDate, i.OperatorId,
                    i.AuditStatus, i.Remarks, i.CreatedAt,
                    e.EquipmentName, e.EquipmentNo,
                    u.RealName AS OperatorName
                FROM InboundRecords i
                LEFT JOIN Equipment e ON i.EquipmentId = e.EquipmentId
                LEFT JOIN Users u ON i.OperatorId = u.UserId
                {whereClause}
                ORDER BY i.InboundId DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, pagedParams.ToArray()).ConfigureAwait(false))
            {
                if (reader == null) return (new List<InboundRecord>(), total);
                var list = DataReaderMapper.MapToList<InboundRecord>(reader);
                return (list, total);
            }
        }
        /// <summary>
        /// 根据ID获取入库记录详情
        /// </summary>
        public async Task<InboundRecord> GetById(int id)
        {
            string sql = @"
                SELECT 
                    i.InboundId, i.EquipmentId, i.InboundNo, i.Supplier,
                    i.PurchasePrice, i.Quantity, i.InboundDate, i.OperatorId,
                    i.AuditStatus, i.Remarks, i.CreatedAt,
                    e.EquipmentName, e.EquipmentNo,
                    u.RealName AS OperatorName
                FROM InboundRecords i
                LEFT JOIN Equipment e ON i.EquipmentId = e.EquipmentId
                LEFT JOIN Users u ON i.OperatorId = u.UserId
                WHERE i.InboundId = @Id";
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, new SqlParameter("@Id", id)).ConfigureAwait(false))
            {
                List<InboundRecord> list = DataReaderMapper.MapToList<InboundRecord>(reader);
                return list.Count > 0 ? list[0] : null;
            }
        }
        /// <summary>
        /// 根据入库单号获取记录
        /// </summary>
        public async Task<InboundRecord> GetByNo(string inboundNo)
        {
            string sql = @"
                SELECT 
                    i.InboundId, i.EquipmentId, i.InboundNo, i.Supplier,
                    i.PurchasePrice, i.Quantity, i.InboundDate, i.OperatorId,
                    i.AuditStatus, i.Remarks, i.CreatedAt,
                    e.EquipmentName, e.EquipmentNo,
                    u.RealName AS OperatorName
                FROM InboundRecords i
                LEFT JOIN Equipment e ON i.EquipmentId = e.EquipmentId
                LEFT JOIN Users u ON i.OperatorId = u.UserId
                WHERE i.InboundNo = @InboundNo";
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, new SqlParameter("@InboundNo", inboundNo)).ConfigureAwait(false))
            {
                List<InboundRecord> list = DataReaderMapper.MapToList<InboundRecord>(reader);
                return list.Count > 0 ? list[0] : null;
            }
        }
        /// <summary>
        /// 新增入库记录
        /// </summary>
        public async Task<int> Insert(InboundRecord record)
        {
            string sql = @"
                INSERT INTO InboundRecords 
                (EquipmentId, InboundNo, Supplier, PurchasePrice, Quantity, 
                 InboundDate, OperatorId, AuditStatus, Remarks, CreatedAt)
                VALUES 
                (@EquipmentId, @InboundNo, @Supplier, @PurchasePrice, @Quantity,
                 @InboundDate, @OperatorId, @AuditStatus, @Remarks, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT)";

            SqlParameter[] parameters = {
                new SqlParameter("@EquipmentId", record.EquipmentId),
                new SqlParameter("@InboundNo", record.InboundNo ?? ""),
                new SqlParameter("@Supplier", record.Supplier ?? ""),
                new SqlParameter("@PurchasePrice", (object)record.PurchasePrice ?? DBNull.Value),
                new SqlParameter("@Quantity", record.Quantity),
                new SqlParameter("@InboundDate", record.InboundDate.Date),
                new SqlParameter("@OperatorId", (object)record.OperatorId ?? DBNull.Value),
                new SqlParameter("@AuditStatus", record.AuditStatus ?? "Pending"),
                new SqlParameter("@Remarks", record.Remarks ?? "")
            };

            object result = await DbHelper.ExecuteScalarAsync(sql, parameters).ConfigureAwait(false);
            return result != null ? Convert.ToInt32(result) : 0;
        }
        /// <summary>
        /// 更新入库记录
        /// </summary>
        public async Task<int> Update(InboundRecord record)
        {
            string sql = @"
                UPDATE InboundRecords SET
                    EquipmentId = @EquipmentId,
                    Supplier = @Supplier,
                    PurchasePrice = @PurchasePrice,
                    Quantity = @Quantity,
                    InboundDate = @InboundDate,
                    OperatorId = @OperatorId,
                    Remarks = @Remarks
                WHERE InboundId = @InboundId AND AuditStatus = 'Pending'";

            SqlParameter[] parameters = {
                new SqlParameter("@InboundId", record.InboundId),
                new SqlParameter("@EquipmentId", record.EquipmentId),
                new SqlParameter("@Supplier", record.Supplier ?? ""),
                new SqlParameter("@PurchasePrice", (object)record.PurchasePrice ?? DBNull.Value),
                new SqlParameter("@Quantity", record.Quantity),
                new SqlParameter("@InboundDate", record.InboundDate.Date),
                new SqlParameter("@OperatorId", (object)record.OperatorId ?? DBNull.Value),
                new SqlParameter("@Remarks", record.Remarks ?? "")
            };

            return await DbHelper.ExecuteNonQueryAsync(sql, parameters).ConfigureAwait(false);
        }
        /// <summary>
        /// 删除入库记录（仅待审核状态可删除）
        /// </summary>
        public async Task<int> Delete(int id)
        {
            string sql = "DELETE FROM InboundRecords WHERE InboundId = @Id AND AuditStatus = 'Pending'";
            return await DbHelper.ExecuteNonQueryAsync(sql, new SqlParameter("@Id", id)).ConfigureAwait(false);
        }

        /// <summary>
        /// 更新审核状态
        /// </summary>
        public async Task<int> UpdateAuditStatus(int id, string auditStatus)
        {
            string sql = "UPDATE InboundRecords SET AuditStatus = @AuditStatus WHERE InboundId = @Id";
            return await DbHelper.ExecuteNonQueryAsync(sql,
                new SqlParameter("@AuditStatus", auditStatus),
                new SqlParameter("@Id", id)).ConfigureAwait(false);
        }

        /// <summary>
        /// 生成入库单号：RK-2026-03-001
        /// </summary>
        public async Task<string> GenerateInboundNo()
        {
            string prefix = $"RK-{DateTime.Now:yyyy-MM}";
            object result = await DbHelper.ExecuteScalarAsync(
                "SELECT COUNT(*) FROM InboundRecords WHERE InboundNo LIKE @Prefix + '%'",
                new SqlParameter("@Prefix", prefix)).ConfigureAwait(false);
            int count = result != null ? Convert.ToInt32(result) : 0;
            return $"{prefix}-{(count + 1):D3}";
        }

        /// <summary>
        /// 获取所有设备列表（用于下拉框）
        /// </summary>
        public async Task<List<KeyValuePair<int, string>>> GetEquipmentList()
        {
            string sql = "SELECT EquipmentId, EquipmentName FROM Equipment WHERE IsActive = 1 ORDER BY EquipmentName";
            var list = new List<KeyValuePair<int, string>>();
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql).ConfigureAwait(false))
            {
                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    list.Add(new KeyValuePair<int, string>(
                        reader.GetInt32(0),
                        reader.GetString(1)
                    ));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有供应商列表（用于下拉框）
        /// </summary>
        public async Task<List<string>> GetSupplierList()
        {
            string sql = "SELECT DISTINCT SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName";
            var list = new List<string>();
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql).ConfigureAwait(false))
            {
                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    list.Add(reader.GetString(0));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有操作人列表（用于下拉框）
        /// </summary>
        public async Task<List<KeyValuePair<int, string>>> GetOperatorList()
        {
            string sql = "SELECT UserId, RealName FROM Users WHERE IsActive = 1 ORDER BY RealName";
            var list = new List<KeyValuePair<int, string>>();
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql).ConfigureAwait(false))
            {
                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    list.Add(new KeyValuePair<int, string>(
                        reader.GetInt32(0),
                        reader.GetString(1)
                    ));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取审核状态列表
        /// </summary>
        public List<KeyValuePair<string, string>> GetAuditStatusList()
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("", "全部"),
                new KeyValuePair<string, string>("Pending", "待审核"),
                new KeyValuePair<string, string>("Approved", "已通过"),
                new KeyValuePair<string, string>("Rejected", "已驳回")
            };
        }

        /// <summary>
        /// 根据设备ID获取设备编号
        /// </summary>
        public async Task<string> GetEquipmentNoById(int equipmentId)
        {
            object result = await DbHelper.ExecuteScalarAsync(
                "SELECT EquipmentNo FROM Equipment WHERE EquipmentId = @EquipmentId",
                new SqlParameter("@EquipmentId", equipmentId)).ConfigureAwait(false);
            return result?.ToString() ?? "";
        }

        /// <summary>
        /// 根据设备ID获取设备分类名称
        /// </summary>
        public async Task<string> GetCategoryNameByEquipmentId(int equipmentId)
        {
            object result = await DbHelper.ExecuteScalarAsync(
                @"SELECT c.CategoryName 
                  FROM Equipment e
                  LEFT JOIN EquipmentCategories c ON e.CategoryId = c.CategoryId
                  WHERE e.EquipmentId = @EquipmentId",
                new SqlParameter("@EquipmentId", equipmentId)).ConfigureAwait(false);
            return result?.ToString() ?? "";
        }

        /// <summary>
        /// 检查入库单号是否已存在
        /// </summary>
        public async Task<bool> IsInboundNoExists(string inboundNo, int excludeId = 0)
        {
            string sql = "SELECT COUNT(*) FROM InboundRecords WHERE InboundNo = @InboundNo AND InboundId != @Id";
            object result = await DbHelper.ExecuteScalarAsync(sql,
                new SqlParameter("@InboundNo", inboundNo),
                new SqlParameter("@Id", excludeId)).ConfigureAwait(false);
            return result != null && Convert.ToInt32(result) > 0;
        }
    }
}

