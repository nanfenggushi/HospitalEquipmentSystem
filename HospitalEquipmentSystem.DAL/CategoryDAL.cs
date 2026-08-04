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
        public List<Category> GetAll()
        {
            string sql = @"SELECT CategoryId, CategoryName AS Name, ParentId, SortOrder, CategoryCode AS Code, Description 
                   FROM EquipmentCategories ORDER BY SortOrder";
            SqlDataReader reader = DbHelper.ExecuteReader(sql);
            if (reader == null) return new List<Category>();
            return HospitalEquipmentSystem.Common.DataReaderMapper.MapToList<Category>(reader);
        }

        /// <summary>
        /// 根据ID获取分类
        /// </summary>
        public Category GetById(int id)
        {
            string sql = @"SELECT CategoryId, CategoryName AS Name, ParentId, SortOrder, CategoryCode AS Code, Description 
                   FROM EquipmentCategories WHERE CategoryId = @Id";
            SqlDataReader reader = DbHelper.ExecuteReader(sql, new SqlParameter("@Id", id));
            List<Category> list = HospitalEquipmentSystem.Common.DataReaderMapper.MapToList<Category>(reader);
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        public int Insert(Category category)
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
            return DbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 更新分类
        /// </summary>
        public int Update(Category category)
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
            return DbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        public int Delete(int id)
        {
            string sql = "DELETE FROM EquipmentCategories WHERE CategoryId = @Id";
            return DbHelper.ExecuteNonQuery(sql, new SqlParameter("@Id", id));
        }

        /// <summary>
        /// 获取某个分类下的子分类数量
        /// </summary>
        public int GetChildCount(int parentId)
        {
            string sql = "SELECT COUNT(*) FROM EquipmentCategories WHERE ParentId = @ParentId";
            object result = DbHelper.ExecuteScalar(sql, new SqlParameter("@ParentId", parentId));
            return result != null ? Convert.ToInt32(result) : 0;
        }
        /// <summary>
        /// 获取某个分类下的设备数量
        /// </summary>
        public int GetEquipmentCount(int categoryId)
        {
            string sql = "SELECT COUNT(*) FROM Equipment WHERE CategoryId = @CategoryId";
            object result = DbHelper.ExecuteScalar(sql, new SqlParameter("@CategoryId", categoryId));
            return result != null ? Convert.ToInt32(result) : 0;
        }
    }
}
