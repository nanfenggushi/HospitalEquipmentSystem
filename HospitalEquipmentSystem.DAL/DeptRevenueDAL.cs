using HospitalEquipment.Model;
using HospitalEquipment.Model.management;
using HospitalEquipment.Util;
using HospitalEquipmentSystem.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DataReaderMapper = HospitalEquipmentSystem.Common.DataReaderMapper;

namespace HospitalEquipment.DAL
{
    public class DeptRevenueDAL
    {
        /// <summary>
        /// 按期间查询科室收入(联表带科室名称,按金额倒序)
        /// </summary>
        public async Task<List<DeptRevenue>> GetByPeriodAsync(string period)
        {
            string sql = @"SELECT r.RevenueId, r.DeptId, d.DeptName, r.Period, r.Amount, r.Remark, r.CreatedAt
FROM DeptRevenue r
LEFT JOIN Departments d ON r.DeptId = d.DeptId
WHERE r.Period = @Period
ORDER BY r.Amount DESC";
            using (SqlDataReader reader=await DbHelper.ExecuteReaderAsync(sql,new SqlParameter("@Period",period)).ConfigureAwait(false))
            {
                if (reader == null) return new List<DeptRevenue>();
                return DataReaderMapper.MapToList<DeptRevenue>(reader);
            }
        }

        /// <summary>
        /// 获取全部收入期间(按时间倒序,带出最新期间作为默认)
        /// </summary>
        public async Task<List<string>> GetPeriodsAsync()
        {
            string sql = "SELECT DISTINCT Period FROM DeptRevenue ORDER BY Period DESC";
            var periods=new List<string>();
            using (SqlDataReader reader=await DbHelper.ExecuteReaderAsync(sql).ConfigureAwait(false))
            {
                if (reader == null) return periods;
                while (reader.Read()) 
                {
                    if(!reader.IsDBNull(0)) periods.Add(reader.GetString(0).Trim());
                }
            }
            return periods;
        }

        /// <summary>
        /// 新增科室收入
        /// </summary>
        public async Task<int> InsertAsync(DeptRevenue model)
        {
            string sql = @"INSERT INTO DeptRevenue (DeptId, Period, Amount, Remark, CreatedAt)
                           VALUES (@DeptId, @Period, @Amount, @Remark, SYSDATETIME())";
            SqlParameter[] parameters =
            {
                new SqlParameter("@DeptId",model.DeptId),
                new SqlParameter("@Period",model.Period),
                new SqlParameter("@Amount",model.Amount),
                new SqlParameter("@Remark",model.Remark ?? ""),
                new SqlParameter("@RevenueId",model.RevenueId)
            };
            return await DbHelper.ExecuteNonQueryAsync(sql, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 更新科室收入
        /// </summary>
        public async Task<int> UpdateAsync(DeptRevenue model)
        {
            string sql = @"UPDATE DeptRevenue SET DeptId = @DeptId, Period = @Period,
                           Amount = @Amount, Remark = @Remark WHERE RevenueId = @RevenueId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@DeptId",model.DeptId),
                new SqlParameter("@Period",model.Period),
                new SqlParameter("@Amount",model.Amount),
                new SqlParameter("@Remark",model.Remark ?? "")
            };
            return await DbHelper.ExecuteNonQueryAsync(sql, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除科室收入
        /// </summary>
        public async Task<int> DeleteAsync(int revenueId) 
        {
            string sql = "DELETE FROM DeptRevenue WHERE RevenueId = @RevenueId";
            return await DbHelper.ExecuteNonQueryAsync(sql, new SqlParameter("@RevenueId",revenueId)).ConfigureAwait(false);

        }

        /// <summary>
        /// 检查同期间同科室是否已存在(排除指定ID)
        /// </summary>
        public async Task<bool> ExistsAsync(string period, int deptId, int excludeId)
        {
            string sql = "SELECT COUNT(*) FROM DeptRevenue WHERE Period = @Period AND DeptId = @DeptId AND RevenueId <> @ExcludeId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Period", period),
                new SqlParameter("@DeptId", deptId),
                new SqlParameter("@ExcludeId", excludeId)
            };
            object result = await DbHelper.ExecuteScalarAsync(sql, parameters).ConfigureAwait(false);
            return result != null && Convert.ToInt32(result) > 0;
        }
    }
}
