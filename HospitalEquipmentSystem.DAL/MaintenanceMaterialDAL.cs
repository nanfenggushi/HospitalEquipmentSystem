using HospitalEquipment.Model;
using HospitalEquipmentSystem.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HospitalEquipment.DAL
{
    /// <summary>
    /// 维修物料记录数据访问层
    /// </summary>
    public class MaintenanceMaterialDAL
    {
        /// <summary>
        /// 查询某维修单的所有物料记录
        /// </summary>
        public async Task<List<MaintenanceMaterial>> GetByRecordId(int recordId)
        {
            string sql = @"
                SELECT Id, RecordId, MaterialId, ISNULL(MaterialName, '') AS MaterialName,
                       Quantity, UnitPrice, (Quantity * UnitPrice) AS Subtotal
                FROM MaintenanceMaterials
                WHERE RecordId = @RecordId
                ORDER BY Id";

            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, new SqlParameter("@RecordId", recordId)).ConfigureAwait(false))
            {
                if (reader == null) return new List<MaintenanceMaterial>();
                return DataReaderMapper.MapToList<MaintenanceMaterial>(reader);
            }
        }

        /// <summary>
        /// 批量插入物料记录，在事务内执行，全部成功或全部回滚
        /// </summary>
        public async Task BatchInsert(List<MaintenanceMaterial> materials)
        {
            if (materials == null || materials.Count == 0) return;

            string sql = @"
                INSERT INTO MaintenanceMaterials (RecordId, MaterialId, MaterialName, Quantity, UnitPrice, Subtotal)
                VALUES (@RecordId, @MaterialId, @MaterialName, @Quantity, @UnitPrice, @Subtotal)";

            await Task.Run(() =>
            {
                DbHelper.ExecuteInTransaction((conn, tx) =>
                {
                    int total = 0;
                    foreach (var item in materials)
                    {
                        SqlParameter[] parameters = {
                            new SqlParameter("@RecordId", item.RecordId),
                            new SqlParameter("@MaterialId", (object)item.MaterialId ?? DBNull.Value),
                            new SqlParameter("@MaterialName", item.MaterialName ?? ""),
                            new SqlParameter("@Quantity", item.Quantity),
                            new SqlParameter("@UnitPrice", item.UnitPrice),
                            new SqlParameter("@Subtotal", item.Subtotal)
                        };

                        total += DbHelper.ExecuteNonQuery(conn, tx, sql, parameters);
                    }
                    return total;
                });
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// 物理删除某维修单的所有物料记录
        /// （维修员重新确认物料清单时先清旧记录，再 BatchInsert 新记录）
        /// </summary>
        public async Task<int> DeleteByRecordId(int recordId)
        {
            string sql = "DELETE FROM MaintenanceMaterials WHERE RecordId = @RecordId";
            return await DbHelper.ExecuteNonQueryAsync(sql, new SqlParameter("@RecordId", recordId)).ConfigureAwait(false);
        }

        /// <summary>
        /// 计算某维修单的物料总金额（没有物料时返回 0）
        /// </summary>
        public async Task<decimal> GetTotalCost(int recordId)
        {
            string sql = "SELECT ISNULL(SUM(Subtotal), 0) FROM MaintenanceMaterials WHERE RecordId = @RecordId";
            object result = await DbHelper.ExecuteScalarAsync(sql, new SqlParameter("@RecordId", recordId)).ConfigureAwait(false);
            return result != null ? Convert.ToDecimal(result) : 0m;
        }
    }
}
