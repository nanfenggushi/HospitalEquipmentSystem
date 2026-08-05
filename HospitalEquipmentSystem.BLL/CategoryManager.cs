using HospitalEquipment.DAL.management;
using HospitalEquipment.Model.management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalEquipment.BLL.management
{
    /// <summary>
    /// 设备分类业务逻辑层
    /// </summary>
    public class CategoryManager
    {
        private CategoryDAL dal = new CategoryDAL();

        /// <summary>
        /// 获取全部分类(扁平列表)
        /// </summary>
        public List<Category> GetAll()
        {
            return dal.GetAll();
        }

        /// <summary>
        /// 根据ID获取分类
        /// </summary>
        public Category GetById(int id)
        {
            return dal.GetById(id);
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        public bool Insert(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new Exception("分类名称不能为空");
            return dal.Insert(category) > 0;
        }

        /// <summary>
        /// 更新分类
        /// </summary>
        public bool Update(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new Exception("分类名称不能为空");
            return dal.Update(category) > 0;
        }

        public bool Delete(int id)
        {
            if (dal.GetChildCount(id) > 0)
                throw new Exception("该分类下还有子分类，请先删除子分类");
            if (dal.GetEquipmentCount(id) > 0)
                throw new Exception("该分类下存在设备，请先移除或重新分类设备");
            return dal.Delete(id) > 0;
        }

        public int GetEquipmentCount(int categoryId)
        {
            return dal.GetEquipmentCount(categoryId);
        }
    }
}
