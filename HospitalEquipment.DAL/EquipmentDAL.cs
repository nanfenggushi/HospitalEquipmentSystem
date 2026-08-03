using System.Collections.Generic;              // 引用泛型集合，List<T> 在这里
using System.Data.SqlClient;                   // 引用 SqlParameter，用于参数化查询防注入
using HospitalEquipment.Model;                 // 引用实体层，Equipment 在这里
using HospitalEquipment.Util;                  // 引用工具层，DbHelper、DataReaderMapper 在这里

namespace HospitalEquipment.DAL                 // 声明当前代码所属的命名空间（数据访问层）
{
    /// <summary>
    /// 设备表（Equipment）数据访问层
    /// 安全说明：所有 SQL 均使用 SqlParameter 参数化查询，禁止字符串拼接用户输入，防止 SQL 注入
    /// </summary>
    public class EquipmentDAL
    {
        /// <summary>
        /// 获取激活状态的设备总数（IsActive = 1）
        /// </summary>
        /// <returns>激活设备总数，查询失败返回 0</returns>
        public int GetActiveCount()
        {
            // 调用 DbHelper 执行单值查询，条件是 IsActive 等于参数 @Active
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Equipment WHERE IsActive = @Active",   // 参数化 SQL，不拼接用户输入
                new SqlParameter("@Active", 1));                            // 传入参数：激活标记为 1
            // 查询结果不为空则转成 int，为空则返回 0
            return result != null ? (int)result : 0;
        }

        /// <summary>
        /// 获取使用中的设备数（Status = 'InUse' 且激活）
        /// </summary>
        /// <returns>使用中设备数，查询失败返回 0</returns>
        public int GetInUseCount()
        {
            // 调用 DbHelper 执行单值查询，条件是状态为使用中且激活
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Equipment WHERE Status = @Status AND IsActive = @Active",  // 参数化 SQL
                new SqlParameter("@Status", "InUse"),                        // 传入参数：状态为使用中
                new SqlParameter("@Active", 1));                             // 传入参数：激活标记为 1
            // 查询结果不为空则转成 int，为空则返回 0
            return result != null ? (int)result : 0;
        }

        /// <summary>
        /// 获取故障/维修中的设备数（Status = 'Maintenance' 且激活）
        /// </summary>
        /// <returns>维修中设备数，查询失败返回 0</returns>
        public int GetMaintenanceCount()
        {
            // 调用 DbHelper 执行单值查询，条件是状态为维修中且激活
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Equipment WHERE Status = @Status AND IsActive = @Active",  // 参数化 SQL
                new SqlParameter("@Status", "Maintenance"),                  // 传入参数：状态为维修中
                new SqlParameter("@Active", 1));                             // 传入参数：激活标记为 1
            // 查询结果不为空则转成 int，为空则返回 0
            return result != null ? (int)result : 0;
        }

        /// <summary>
        /// 获取借用中的设备数（Status = 'Borrowed' 且激活）
        /// </summary>
        /// <returns>借用中设备数，查询失败返回 0</returns>
        public int GetBorrowedCount()
        {
            // 调用 DbHelper 执行单值查询，条件是状态为借用中且激活
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Equipment WHERE Status = @Status AND IsActive = @Active",  // 参数化 SQL
                new SqlParameter("@Status", "Borrowed"),                     // 传入参数：状态为借用中
                new SqlParameter("@Active", 1));                             // 传入参数：激活标记为 1
            // 查询结果不为空则转成 int，为空则返回 0
            return result != null ? (int)result : 0;
        }

        /// <summary>
        /// 获取所有激活的设备列表（按 ID 排序）
        /// 使用 DataReaderMapper 把查询结果映射为 Equipment 实体列表
        /// </summary>
        /// <returns>激活设备实体列表</returns>
        public List<Equipment> GetAllActive()
        {
            // 定义查询 SQL，条件是激活，按设备ID排序
            string sql = @"SELECT * FROM Equipment WHERE IsActive = @Active ORDER BY EquipmentId";

            // 用 using 确保读取器用完自动释放
            using (var reader = DbHelper.ExecuteReader(sql,
                new SqlParameter("@Active", 1)))                            // 传入参数：激活标记为 1
            {
                // 读取器不为空则映射为实体列表，为空则返回空列表
                return reader != null ? DataReaderMapper.MapToList<Equipment>(reader) : new List<Equipment>();
            }
        }
    }
}
