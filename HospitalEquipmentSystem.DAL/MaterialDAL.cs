using HospitalEquipment.Model;
using HospitalEquipmentSystem.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HospitalEquipment.DAL
{
    /// <summary>
    /// 物料数据访问层
    /// </summary>
    public class MaterialDAL
    {
        /// <summary>
        /// 分页查询物料，按 CreatedAt 倒序，只返回启用的记录
        /// </summary>
        public async Task<(List<Material> list, int total)> GetAll(int pageIndex, int pageSize)
        {
            // 查询总数
            string countSql = "SELECT COUNT(*) FROM Materials WHERE IsActive = 1";
            object countResult = await DbHelper.ExecuteScalarAsync(countSql).ConfigureAwait(false);
            int total = countResult != null ? Convert.ToInt32(countResult) : 0;

            // 分页数据
            int offset = (pageIndex - 1) * pageSize;
            string sql = @"
                SELECT MaterialId, MaterialName,
                       CategoryId, FaultType, UnitPrice, DefaultQuantity,
                       ISNULL(Unit, '') AS Unit, ISNULL(Description, '') AS Description,
                       IsActive, CreatedAt
                FROM Materials
                WHERE IsActive = 1
                ORDER BY CreatedAt DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            SqlParameter[] parameters = {
                new SqlParameter("@Offset", offset),
                new SqlParameter("@PageSize", pageSize)
            };

            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, parameters).ConfigureAwait(false))
            {
                if (reader == null) return (new List<Material>(), total);
                var list = DataReaderMapper.MapToList<Material>(reader);
                return (list, total);
            }
        }

        /// <summary>
        /// 获取所有可用物料（不分页，供下拉框使用）
        /// </summary>
        public async Task<List<Material>> GetAllMaterials()
        {
            string sql = @"
                SELECT MaterialId, MaterialName,
                       CategoryId, FaultType, UnitPrice, DefaultQuantity,
                       ISNULL(Unit, '') AS Unit, ISNULL(Description, '') AS Description,
                       IsActive, CreatedAt
                FROM Materials
                WHERE IsActive = 1
                ORDER BY MaterialName";

            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql).ConfigureAwait(false))
            {
                if (reader == null) return new List<Material>();
                return DataReaderMapper.MapToList<Material>(reader);
            }
        }

        /// <summary>
        /// 按主键查询单条物料记录
        /// </summary>
        public async Task<Material> GetById(int materialId)
        {
            string sql = @"
                SELECT MaterialId, MaterialName,
                       CategoryId, FaultType, UnitPrice, DefaultQuantity,
                       ISNULL(Unit, '') AS Unit, ISNULL(Description, '') AS Description,
                       IsActive, CreatedAt
                FROM Materials
                WHERE MaterialId = @Id";

            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, new SqlParameter("@Id", materialId)).ConfigureAwait(false))
            {
                List<Material> list = DataReaderMapper.MapToList<Material>(reader);
                return list.Count > 0 ? list[0] : null;
            }
        }

        /// <summary>
        /// 插入新物料
        /// </summary>
        public async Task<int> Insert(Material material)
        {
            string sql = @"
                INSERT INTO Materials (MaterialName, CategoryId, FaultType, UnitPrice, DefaultQuantity, Unit, Description, IsActive)
                VALUES (@Name, @CategoryId, @FaultType, @UnitPrice, @DefaultQuantity, @Unit, @Description, @IsActive);
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@Name", material.MaterialName),
                new SqlParameter("@CategoryId", material.CategoryId),
                new SqlParameter("@FaultType", material.FaultType),
                new SqlParameter("@UnitPrice", material.UnitPrice),
                new SqlParameter("@DefaultQuantity", material.DefaultQuantity),
                new SqlParameter("@Unit", material.Unit ?? ""),
                new SqlParameter("@Description", material.Description ?? ""),
                new SqlParameter("@IsActive", material.IsActive)
            };

            object result = await DbHelper.ExecuteScalarAsync(sql, parameters).ConfigureAwait(false);
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// 按 MaterialId 更新所有字段
        /// </summary>
        public async Task<int> Update(Material material)
        {
            string sql = @"
                UPDATE Materials SET
                    MaterialName    = @Name,
                    CategoryId      = @CategoryId,
                    FaultType       = @FaultType,
                    UnitPrice       = @UnitPrice,
                    DefaultQuantity = @DefaultQuantity,
                    Unit            = @Unit,
                    Description     = @Description,
                    IsActive        = @IsActive
                WHERE MaterialId = @Id";

            SqlParameter[] parameters = {
                new SqlParameter("@Id", material.MaterialId),
                new SqlParameter("@Name", material.MaterialName),
                new SqlParameter("@CategoryId", material.CategoryId),
                new SqlParameter("@FaultType", material.FaultType),
                new SqlParameter("@UnitPrice", material.UnitPrice),
                new SqlParameter("@DefaultQuantity", material.DefaultQuantity),
                new SqlParameter("@Unit", material.Unit ?? ""),
                new SqlParameter("@Description", material.Description ?? ""),
                new SqlParameter("@IsActive", material.IsActive)
            };

            return await DbHelper.ExecuteNonQueryAsync(sql, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 软删除物料（将 IsActive 设为 0）
        /// </summary>
        public async Task<int> Delete(int materialId)
        {
            string sql = "UPDATE Materials SET IsActive = 0 WHERE MaterialId = @Id";
            return await DbHelper.ExecuteNonQueryAsync(sql, new SqlParameter("@Id", materialId)).ConfigureAwait(false);
        }

        /// <summary>
        /// 按设备分类 + 故障类型查询启用的物料（物料推荐的数据来源）
        /// </summary>
        public async Task<List<Material>> GetByCategoryAndFault(int categoryId, string faultType)
        {
            string sql = @"
                SELECT MaterialId, MaterialName,
                       CategoryId, FaultType, UnitPrice, DefaultQuantity,
                       ISNULL(Unit, '') AS Unit, ISNULL(Description, '') AS Description,
                       IsActive, CreatedAt
                FROM Materials
                WHERE CategoryId = @CategoryId
                  AND FaultType = @FaultType
                  AND IsActive = 1
                ORDER BY MaterialName";

            SqlParameter[] parameters = {
                new SqlParameter("@CategoryId", categoryId),
                new SqlParameter("@FaultType", faultType)
            };

            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, parameters).ConfigureAwait(false))
            {
                if (reader == null) return new List<Material>();
                return DataReaderMapper.MapToList<Material>(reader);
            }
        }

        /// <summary>
        /// 获取某设备分类下所有不重复的故障类型（AI 候选故障列表的数据来源）
        /// </summary>
        public async Task<List<string>> GetDistinctFaultTypes(int categoryId)
        {
            string sql = @"
                SELECT DISTINCT FaultType
                FROM Materials
                WHERE CategoryId = @CategoryId
                  AND IsActive = 1
                ORDER BY FaultType";

            var list = new List<string>();
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, new SqlParameter("@CategoryId", categoryId)).ConfigureAwait(false))
            {
                if (reader == null) return list;
                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    list.Add(reader["FaultType"].ToString());
                }
            }
            return list;
        }

        /// <summary>
        /// 获取全部不重复的故障类型（不分设备分类，兜底用）
        /// </summary>
        public async Task<List<string>> GetAllFaultTypes()
        {
            string sql = @"
                SELECT DISTINCT FaultType
                FROM Materials
                WHERE IsActive = 1
                ORDER BY FaultType";

            var list = new List<string>();
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql).ConfigureAwait(false))
            {
                if (reader == null) return list;
                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    list.Add(reader["FaultType"].ToString());
                }
            }
            return list;
        }
    }
}
