using HospitalEquipmentSystem.Common;
using System;
using System.Data;
using System.Data.SqlClient;

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
            //
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

        /// <summary>检查用户名是否已存在</summary>
        public bool IsUsernameExists(string username)
        {
            string sql = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
            object result = DbHelper.ExecuteScalar(sql, new SqlParameter("@Username", username));
            return Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// 获取所有启用中的科室（供下拉框绑定）
        /// </summary>
        public DataTable GetActiveDepartments()
        {
            string sql = @"
                SELECT DeptId, DeptName, DeptCode, Location, Phone
                FROM Departments
                WHERE IsActive = 1
                ORDER BY DeptId";
            return DbHelper.GetDataTable(sql);
        }

        /// <summary>
        /// 获取数据库中已出现过的角色（去重并翻译为中文名），供下拉框绑定
        ///
        /// 注意：所有中文字面量必须用 N'...' 包裹，告诉 SQL Server 按 NVARCHAR(Unicode)
        /// 解析；否则若数据库 collation 是 SQL_Latin1_General_* 这类非中文 collation，
        /// 中文字符会被替换成 '?'，导致 ComboBox 显示为 ????。
        /// </summary>
        public DataTable GetExistingRoles()
        {
            string sql = @"
                SELECT DISTINCT Role,
                       CASE Role
                           WHEN 'admin'  THEN N'系统管理员'
                           WHEN 'repair' THEN N'维修人员'
                           WHEN 'doctor' THEN N'医生'
                       END AS RoleName
                FROM Users
                WHERE Role IS NOT NULL AND LTRIM(RTRIM(Role)) <> ''
                ORDER BY Role";
            return DbHelper.GetDataTable(sql);
        }

        /// <summary>注册新用户（完整版，带真实姓名、手机号、科室、职称）</summary>
        public int RegisterUserFull(string username, string password, string realName,
            string role, int deptId, string phone, string title)
        {
            string sql = @"   
            INSERT INTO Users (Username, PasswordHash, RealName, Role, DeptId, Phone, Title, IsActive, CreatedAt)
            VALUES (@Username, @Password, @RealName, @Role, @DeptId, @Phone, @Title, 1, GETDATE());
            SELECT SCOPE_IDENTITY();";
            object result = DbHelper.ExecuteScalar(sql,
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password),
                new SqlParameter("@RealName", realName),
                new SqlParameter("@Role", role),
                new SqlParameter("@DeptId", deptId),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Title", title));
            return Convert.ToInt32(result);
        }
    }
}
