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
    /// <summary>
    /// 登录相关数据访问
    /// </summary>
    public class LoginDAL
    {
        /// <summary>在用人员列表（供登录下拉选择）</summary>
        public DataTable GetActiveUsers()
        {
            string sql = @"
                SELECT u.UserId, u.Username, u.RealName, u.Role, u.DeptId,
                       ISNULL(d.DeptName, '') AS DeptName
                FROM Users u
                LEFT JOIN Departments d ON u.DeptId = d.DeptId
                WHERE u.IsActive = 1
                ORDER BY u.UserId";
            return DbHelper.GetDataTable(sql);
        }

        /// <summary>按人员+密码校验登录</summary>
        public DataTable GetByIdAndPwd(int userId, string password)
        {
            string sql = @"
                SELECT u.UserId, u.Username, u.RealName, u.Role, u.DeptId,
                       ISNULL(d.DeptName, '') AS DeptName
                FROM Users u
                LEFT JOIN Departments d ON u.DeptId = d.DeptId
                WHERE u.UserId = @UserId AND u.PasswordHash = @Password AND u.IsActive = 1";
            return DbHelper.GetDataTable(sql,
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Password", password));
        }

        /// <summary>回写最后登录时间</summary>
        public int UpdateLastLogin(int userId)
        {
            string sql = "UPDATE Users SET LastLoginAt = GETDATE() WHERE UserId = @UserId";
            return DbHelper.ExecuteNonQuery(sql, new SqlParameter("@UserId", userId));
        }
    }
}
