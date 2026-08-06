using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HospitalEquipmentSystem.Common
{
    /// <summary>
    /// 数据库操作通用辅助类 (针对 SQL Server)
    /// </summary>
    public static class DbHelper
    {
        // 读取连接字符串，并做非空防御校验
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["connStr"]?.ConnectionString
            ?? throw new InvalidOperationException("未在 App.config 或 Web.config 中配置名为 'connStr' 的连接字符串。");

        #region 私有核心辅助方法 (减少重复代码，保证参数安全)

        /// <summary>
        /// 准备并配置 SqlCommand 对象
        /// </summary>
        private static SqlCommand PrepareCommand(SqlConnection conn, string sql, CommandType cmdType, SqlParameter[] parameters)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand(sql, conn) { CommandType = cmdType };

            if (parameters != null && parameters.Length > 0)
            {
                foreach (var p in parameters)
                {
                    if (p != null)
                    {
                        // 1. 克隆参数对象，防止同一个 SqlParameter 在循环/多次调用中重复添加报错
                        SqlParameter clonedParam = (SqlParameter)((ICloneable)p).Clone();

                        // 2. 自动将 C# 的 null 转换为数据库的 DBNull.Value，防止 ADO.NET 传参报错
                        if (clonedParam.Value == null)
                        {
                            clonedParam.Value = DBNull.Value;
                        }

                        cmd.Parameters.Add(clonedParam);
                    }
                }
            }

            return cmd;
        }

        /// <summary>
        /// 异步准备并配置 SqlCommand 对象
        /// </summary>
        private static async Task<SqlCommand> PrepareCommandAsync(SqlConnection conn, string sql, CommandType cmdType, SqlParameter[] parameters)
        {
            if (conn.State != ConnectionState.Open)
            {
                await conn.OpenAsync().ConfigureAwait(false);
            }

            SqlCommand cmd = new SqlCommand(sql, conn) { CommandType = cmdType };

            if (parameters != null && parameters.Length > 0)
            {
                foreach (var p in parameters)
                {
                    if (p != null)
                    {
                        SqlParameter clonedParam = (SqlParameter)((ICloneable)p).Clone();

                        if (clonedParam.Value == null)
                        {
                            clonedParam.Value = DBNull.Value;
                        }

                        cmd.Parameters.Add(clonedParam);
                    }
                }
            }

            return cmd;
        }

        #endregion

        /// <summary>
        /// 执行 SQL 语句，返回受影响的行数（用于 INSERT、UPDATE、DELETE）
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">可变 SQL 参数列表</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = PrepareCommand(conn, sql, CommandType.Text, parameters))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 在事务中执行一组数据库操作（成功自动提交，异常自动回滚）
        /// </summary>
        /// <param name="action">在事务中执行的委托（返回受影响行数）</param>
        /// <returns>委托返回的结果</returns>
        public static int ExecuteInTransaction(Func<SqlConnection, SqlTransaction, int> action)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlTransaction tx = conn.BeginTransaction();
                try
                {
                    int result = action(conn, tx);
                    tx.Commit();
                    return result;
                }
                catch
                {
                    try { tx.Rollback(); } catch { /* 回滚失败则保持原异常 */ }
                    throw;
                }
            }
        }

        /// <summary>
        /// 在指定连接和事务中执行 SQL，返回受影响行数（供事务内使用）
        /// </summary>
        public static int ExecuteNonQuery(SqlConnection conn, SqlTransaction tx, string sql, params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = PrepareCommand(conn, sql, CommandType.Text, parameters))
            {
                cmd.Transaction = tx;
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 执行 SQL 查询，返回第一行第一列的值（用于 COUNT、SUM、获取自增 ID 等）
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">可变 SQL 参数列表</param>
        /// <returns>查询结果的第一行第一列</returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = PrepareCommand(conn, sql, CommandType.Text, parameters))
            {
                object val = cmd.ExecuteScalar();
                // 如果数据库返回 DBNull，转换为 C# 的 null 方便上层判断
                return (val == DBNull.Value) ? null : val;
            }
        }

        /// <summary>
        /// 异步执行 SQL 语句，返回受影响的行数
        /// </summary>
        public static async Task<int> ExecuteNonQueryAsync(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = await PrepareCommandAsync(conn, sql, CommandType.Text, parameters).ConfigureAwait(false))
            {
                return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 异步执行 SQL 查询，返回第一行第一列的值
        /// </summary>
        public static async Task<object> ExecuteScalarAsync(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = await PrepareCommandAsync(conn, sql, CommandType.Text, parameters).ConfigureAwait(false))
            {
                object val = await cmd.ExecuteScalarAsync().ConfigureAwait(false);
                return (val == DBNull.Value) ? null : val;
            }
        }

        /// <summary>
        /// 异步执行 SQL 查询，返回 SqlDataReader 用于逐行读取
        /// </summary>
        public static async Task<SqlDataReader> ExecuteReaderAsync(string sql, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(ConnStr);
            try
            {
                SqlCommand cmd = await PrepareCommandAsync(conn, sql, CommandType.Text, parameters).ConfigureAwait(false);
                return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection).ConfigureAwait(false);
            }
            catch
            {
                conn.Dispose();
                throw;
            }
        }

        /// <summary>
        /// 执行 SQL 查询，返回 SqlDataReader 用于逐行读取（请注意：外部读取完毕后必须 Close/Dispose Reader）
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">可变 SQL 参数列表</param>
        /// <returns>SqlDataReader 对象</returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(ConnStr);
            try
            {
                SqlCommand cmd = PrepareCommand(conn, sql, CommandType.Text, parameters);
                // CommandBehavior.CloseConnection 保证了在关闭 Reader 的同时自动关闭 Connection
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            } catch
            {
                // 关键点：如果执行 ExecuteReader 出现 SQL 异常，必须立即关闭并释放 Connection，防止连接池泄漏
                conn.Dispose();
                throw; // 重新抛出原始异常，让上层知道真正的错误
            }
        }

        /// <summary>
        /// 执行 SQL 查询，返回充填好的 DataTable
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">可变 SQL 参数列表</param>
        /// <returns>DataTable 内存数据表</returns>
        public static DataTable GetDataTable(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = PrepareCommand(conn, sql, CommandType.Text, parameters))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// 将 DataTable 的改动（增/删/改）批量同步回数据库
        /// </summary>
        /// <param name="dt">已做过增删改操作的 DataTable</param>
        /// <param name="selectSql">用于生成 Adapter 的原始查询 SQL（必须包含主键列）</param>
        /// <returns>受影响的行数</returns>
        public static int UpdateDataTable(DataTable dt, string selectSql)
        {
            if (dt == null) return 0;

            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectSql, conn))
            using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
            {
                conn.Open();
                int affectedRows = adapter.Update(dt);
                dt.AcceptChanges(); // 提交修改，重置 DataRow 的 RowState
                return affectedRows;
            }
        }

    }
}
