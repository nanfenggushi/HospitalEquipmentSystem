using System.Collections.Generic;
using System.Data.SqlClient;
using HospitalEquipmentSystem.Common;

namespace HospitalEquipment.DAL
{
    /// <summary>
    /// 设备分类统计（监控中心仪表盘专用）
    /// </summary>
    public class EquipmentCategoryDAL
    {
        /// <summary>
        /// 统计每个设备分类下的激活设备数量
        /// </summary>
        public Dictionary<string, int> GetEquipmentCountByCategory()
        {
            var result = new Dictionary<string, int>();
            string sql = @"
                SELECT EC.CategoryName, COUNT(E.EquipmentId) AS Cnt
                FROM Equipment E
                INNER JOIN EquipmentCategories EC ON E.CategoryId = EC.CategoryId
                WHERE E.IsActive = @Active
                GROUP BY EC.CategoryName";
            using (var reader = DbHelper.ExecuteReader(sql,
                new SqlParameter("@Active", 1)))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        string name = reader["CategoryName"].ToString();
                        int count = (int)reader["Cnt"];
                        result[name] = count;
                    }
                }
            }
            return result;
        }
    }
}
