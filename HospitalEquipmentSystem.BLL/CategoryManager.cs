using HospitalEquipment.DAL.management;
using HospitalEquipment.Model.management;
using System;
using System.Collections.Generic;
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
        public async Task<List<Category>> GetAll()
        {
            return await dal.GetAll().ConfigureAwait(false);
        }

        /// <summary>
        /// 根据ID获取分类
        /// </summary>
        public async Task<Category> GetById(int id)
        {
            return await dal.GetById(id).ConfigureAwait(false);
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        public async Task<bool> Insert(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new Exception("分类名称不能为空");
            return await dal.Insert(category).ConfigureAwait(false) > 0;
        }

        /// <summary>
        /// 更新分类
        /// </summary>
        public async Task<bool> Update(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new Exception("分类名称不能为空");
            return await dal.Update(category).ConfigureAwait(false) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            if (await dal.GetChildCount(id).ConfigureAwait(false) > 0)
                throw new Exception("该分类下还有子分类，请先删除子分类");
            if (await dal.GetEquipmentCount(id).ConfigureAwait(false) > 0)
                throw new Exception("该分类下存在设备，请先移除或重新分类设备");
            return await dal.Delete(id).ConfigureAwait(false) > 0;
        }

        public async Task<int> GetEquipmentCount(int categoryId)
        {
            return await dal.GetEquipmentCount(categoryId).ConfigureAwait(false);
        }
    }
}
