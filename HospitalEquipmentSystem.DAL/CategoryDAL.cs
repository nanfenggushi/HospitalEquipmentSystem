using HospitalEquipment.Model.management;
using HospitalEquipment.Util;
using HospitalEquipmentSystem.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalEquipment.DAL.management
{
    /// <summary>
    /// 设备分类数据访问层
    /// </summary>
    public class CategoryDAL
    {
        /// <summary>
        /// 获取全部分类(扁平列表)
        /// </summary>
        public async Task<List<Category>> GetAll()
        {
            string sql = @"SELECT CategoryId, CategoryName AS Name, ParentId, SortOrder, CategoryCode AS Code, Description 
                   FROM EquipmentCategories ORDER BY SortOrder";
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql).ConfigureAwait(false))
            {
                if (reader == null) return new List<Category>();
                return HospitalEquipmentSystem.Common.DataReaderMapper.MapToList<Category>(reader);
            }
        }

        /// <summary>
        /// 根据ID获取分类
        /// </summary>
        public async Task<Category> GetById(int id)
        {
            string sql = @"SELECT CategoryId, CategoryName AS Name, ParentId, SortOrder, CategoryCode AS Code, Description 
                   FROM EquipmentCategories WHERE CategoryId = @Id";
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, new SqlParameter("@Id", id)).ConfigureAwait(false))
            {
                List<Category> list = HospitalEquipmentSystem.Common.DataReaderMapper.MapToList<Category>(reader);
                return list.Count > 0 ? list[0] : null;
            }
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        public async Task<int> Insert(Category category)
        {
            string sql = @"INSERT INTO EquipmentCategories (CategoryName, ParentId, SortOrder, CategoryCode, Description)
                           VALUES (@Name, @ParentId, @SortOrder, @Code, @Description)";
            SqlParameter[] parameters = {
                new SqlParameter("@Name", category.Name),
                new SqlParameter("@ParentId", category.ParentId),
                new SqlParameter("@SortOrder", category.SortOrder),
                new SqlParameter("@Code", category.Code ?? ""),
                new SqlParameter("@Description", category.Description ?? "")
            };
            return await DbHelper.ExecuteNonQueryAsync(sql, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 更新分类
        /// </summary>
        public async Task<int> Update(Category category)
        {
            string sql = @"UPDATE EquipmentCategories SET CategoryName = @Name, ParentId = @ParentId,
                          SortOrder = @SortOrder, CategoryCode = @Code, Description = @Description WHERE CategoryId = @Id";
            SqlParameter[] parameters = {
                new SqlParameter("@Id", category.CategoryId),
                new SqlParameter("@Name", category.Name),
                new SqlParameter("@ParentId", category.ParentId),
                new SqlParameter("@SortOrder", category.SortOrder),
                new SqlParameter("@Code", category.Code ?? ""),
                new SqlParameter("@Description", category.Description ?? "")
            };
            return await DbHelper.ExecuteNonQueryAsync(sql, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        public async Task<int> Delete(int id)
        {
            string sql = "DELETE FROM EquipmentCategories WHERE CategoryId = @Id";
            return await DbHelper.ExecuteNonQueryAsync(sql, new SqlParameter("@Id", id)).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取某个分类下的子分类数量
        /// </summary>
        public async Task<int> GetChildCount(int parentId)
        {
            string sql = "SELECT COUNT(*) FROM EquipmentCategories WHERE ParentId = @ParentId";
            object result = await DbHelper.ExecuteScalarAsync(sql, new SqlParameter("@ParentId", parentId)).ConfigureAwait(false);
            return result != null ? Convert.ToInt32(result) : 0;
        }
        /// <summary>
        /// 获取某个分类下的设备数量
        /// </summary>
        public async Task<int> GetEquipmentCount(int categoryId)
        {
            string sql = "SELECT COUNT(*) FROM Equipment WHERE CategoryId = @CategoryId";
            object result = await DbHelper.ExecuteScalarAsync(sql, new SqlParameter("@CategoryId", categoryId)).ConfigureAwait(false);
            return result != null ? Convert.ToInt32(result) : 0;
        }
    }
}
