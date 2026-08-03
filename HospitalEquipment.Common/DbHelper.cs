using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HospitalEquipment.Util {
    public class DbHelper {
        private static readonly string connStr;
        static DbHelper() {
            connStr = ConfigurationManager.ConnectionStrings["connStr"].ToString();
        }

        /// <summary>
        /// 执行增、删、改
        /// </summary>
        /// <param name="sql">需要执行的Sql语句</param>
        /// <param name="parameters">Sql语句里的参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters) {
            /*
             * params 是 C# 里的一个关键字，作用是：让方法可以接收数量不固定的参数，
             * 调用时既可以传数组，也可以直接罗列多个值（甚至不传）
             * 
             * 使用规则（几个限制）
             * 1.只能放在参数列表的最后一个位置
             * 2.一个方法里只能有一个 params 参数
             * 3.类型必须是数组（一维数组）
             * 4.调用时如果不传，方法内部拿到的是长度为 0 的空数组，不是 null
             */

            try {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sql, conn)) {
                    conn.Open();
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
            return 0;
        }

        /// <summary>
        /// 返回查询结果的第一行第一列
        /// </summary>
        /// <param name="sql">需要执行的Sql语句</param>
        /// <param name="parameters">Sql语句里的参数</param>
        /// <returns>object</returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters) {
            try {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sql, conn)) {
                    conn.Open();
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
            return null;
        }

        /// <summary>
        /// 查询表数据，返回SqlDataReader用于逐行读取
        /// </summary>
        /// <param name="sql">需要执行的Sql语句</param>
        /// <param name="parameters">Sql语句里的参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] parameters) {
            try {
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
            return null;
        }

        /// <summary>
        /// 根据条件缓存一个表到内存中
        /// </summary>
        /// <param name="sql">需要执行的Sql语句</param>
        /// <param name="parameters">Sql语句里的参数</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTable(string sql, params SqlParameter[] parameters) {
            try {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sql, conn)) {
                    conn.Open();
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
            return null;
        }

        /// <summary>
        /// 将 DataTable 的改动（增/删/改）同步回数据库
        /// </summary>
        /// <param name="dt">已做过增删改操作的DataTable</param>
        /// <param name="selectSql">用于生成Adapter的原始查询SQL（必须能唯一定位到表和主键）</param>
        public static int UpdateDataTable(DataTable dt, string selectSql) {
            using (SqlConnection conn = new SqlConnection(connStr)) {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectSql, conn)) {
                    // 自动根据SelectCommand生成 Insert/Update/Delete 语句
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                    // 把 DataTable 中的改动同步回数据库
                    int affectedRows = adapter.Update(dt);
                    dt.AcceptChanges(); // 提交后清除RowState标记

                    // 返回受影响的行数
                    return affectedRows;
                }
            }
        }
    }
}


