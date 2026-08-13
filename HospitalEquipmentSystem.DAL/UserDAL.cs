using HospitalEquipmentSystem.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HospitalEquipment.DAL
{
    /// <summary>
    /// 用户管理数据访问层 - 分页查询 Users 表
    /// </summary>
    public class UserDAL
    {
        /// <summary>
        /// 分页查询用户列表（JOIN Departments 获取科室名称）
        /// </summary>
        /// <param name="pageIndex">页码（从1开始）</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="keyword">搜索关键字（可选，匹配姓名/账号/手机号）</param>
        /// <returns>用户数据表 + 总记录数</returns>
        public (DataTable users, int total) GetPagedUsers(int pageIndex, int pageSize, string keyword = "")
        {
            // 1. 构建 WHERE 条件
            var conditions = new List<string>();
            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                conditions.Add("(u.RealName LIKE @Keyword OR u.Username LIKE @Keyword OR u.Phone LIKE @Keyword)");
                parameters.Add(new SqlParameter("@Keyword", $"%{keyword}%"));
            }

            string whereClause = conditions.Count > 0
                ? "WHERE " + string.Join(" AND ", conditions)
                : "";

            // 2. 查询总记录数
            string countSql = $@"
                SELECT COUNT(*)
                FROM Users u
                LEFT JOIN Departments d ON u.DeptId = d.DeptId
                {whereClause}";
            int total = Convert.ToInt32(DbHelper.ExecuteScalar(countSql, parameters.ToArray()));

            // 3. 分页查询（SQL Server 2012+ OFFSET/FETCH NEXT）
            int offset = (pageIndex - 1) * pageSize;

            string sql = $@"
                SELECT u.UserId, u.Username, u.RealName, u.Role,
                       u.Phone, u.Title, u.IsActive,
                       ISNULL(d.DeptName, '') AS DeptName
                FROM Users u
                LEFT JOIN Departments d ON u.DeptId = d.DeptId
                {whereClause}
                ORDER BY u.UserId
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var pagedParams = new List<SqlParameter>(parameters)
            {
                new SqlParameter("@Offset", offset),
                new SqlParameter("@PageSize", pageSize)
            };

            DataTable dt = DbHelper.GetDataTable(sql, pagedParams.ToArray());

            return (dt, total);
        }

        /// <summary>
        /// 感觉当前用户id修改头像地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newAvatarUrl"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> UpdateAvatarUrlInDatabaseAsync(int userId, string newAvatarUrl)
        {
            string sql = @"UPDATE Users
                         SET AvatarUrl = @url
                         WHERE UserId = @id;";

            bool status = await DbHelper.ExecuteNonQueryAsync(sql, new SqlParameter[] {
                new SqlParameter("@url", newAvatarUrl),
                new SqlParameter("@id", userId)
            }) > 0;

            if (status)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
