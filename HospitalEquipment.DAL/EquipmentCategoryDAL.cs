using System.Collections.Generic;              // 引用泛型集合，Dictionary 在这里
using System.Data.SqlClient;                   // 引用 SqlParameter，用于参数化查询防注入
using HospitalEquipment.Util;                  // 引用工具层，DbHelper 在这里

namespace HospitalEquipment.DAL                 // 声明当前代码所属的命名空间（数据访问层）
{
    /// <summary>
    /// 设备分类表（EquipmentCategories）数据访问层
    /// 安全说明：查询为纯静态 SQL + 参数化 @Active，无用户输入拼接，无注入风险
    /// </summary>
    public class EquipmentCategoryDAL
    {
        /// <summary>
        /// 统计每个设备分类下的激活设备数量
        /// 通过 Equipment 和 EquipmentCategories 联表，按分类名分组
        /// </summary>
        /// <returns>分类名称到设备数量的字典</returns>
        public Dictionary<string, int> GetEquipmentCountByCategory()
        {
            // 创建返回结果字典
            var result = new Dictionary<string, int>();

            // 定义查询 SQL：按分类名分组统计每个分类下的设备数量
            string sql = @"
                SELECT EC.CategoryName, COUNT(E.EquipmentId) AS Cnt    -- 取分类名和该分类的设备数量
                FROM Equipment E                                       -- 主表：设备
                INNER JOIN EquipmentCategories EC ON E.CategoryId = EC.CategoryId  -- 关联分类表
                WHERE E.IsActive = @Active                             -- 只统计激活设备
                GROUP BY EC.CategoryName";                             // 按分类名分组

            // 用 using 确保读取器用完自动释放
            using (var reader = DbHelper.ExecuteReader(sql,
                new SqlParameter("@Active", 1)))                       // 传入参数：激活标记为 1
            {
                // 如果读取器不为空
                if (reader != null)
                {
                    // 逐行读取查询结果
                    while (reader.Read())
                    {
                        // 取出当前行的分类名称
                        string name = reader["CategoryName"].ToString();

                        // 取出当前行的设备数量
                        int count = (int)reader["Cnt"];

                        // 把"分类名 → 数量"存入字典
                        result[name] = count;
                    }
                }
            }

            // 返回分类统计字典
            return result;
        }
    }
}
