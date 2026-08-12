using HospitalEquipment.DAL;
using HospitalEquipment.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalEquipment.BLL
{
    /// <summary>
    /// 物料管理业务逻辑
    /// </summary>
    public class MaterialManager
    {
        private readonly MaterialDAL dal = new MaterialDAL();

        /// <summary>
        /// 分页获取物料列表
        /// </summary>
        public async Task<(List<Material> list, int total)> GetPagedMaterials(int page, int size)
        {
            return await dal.GetAll(page, size).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取所有可用物料（不分页，供下拉框使用）
        /// </summary>
        public async Task<List<Material>> GetAllMaterials()
        {
            return await dal.GetAllMaterials().ConfigureAwait(false);
        }

        /// <summary>
        /// 根据 ID 获取单条物料
        /// </summary>
        public async Task<Material> GetById(int id)
        {
            return await dal.GetById(id).ConfigureAwait(false);
        }

        /// <summary>
        /// 新增或更新物料（含字段校验）
        /// </summary>
        /// <param name="material">物料实体</param>
        /// <returns>操作后的 MaterialId</returns>
        /// <exception cref="ArgumentException">校验不通过时抛出</exception>
        public async Task<int> SaveMaterial(Material material)
        {
            if (material == null)
                throw new ArgumentException("物料对象不能为空。");

            if (string.IsNullOrWhiteSpace(material.MaterialName))
                throw new ArgumentException("物料名称不能为空。");

            if (string.IsNullOrWhiteSpace(material.FaultType))
                throw new ArgumentException("故障类型不能为空。");

            if (material.UnitPrice <= 0)
                throw new ArgumentException("单价必须大于 0。");

            if (material.DefaultQuantity <= 0)
                throw new ArgumentException("默认用量必须大于 0。");

            if (material.MaterialId == 0)
            {
                // 新增
                return await dal.Insert(material).ConfigureAwait(false);
            }
            else
            {
                // 编辑
                await dal.Update(material).ConfigureAwait(false);
                return material.MaterialId;
            }
        }

        /// <summary>
        /// 删除物料（软删除）
        /// </summary>
        public async Task DeleteMaterial(int id)
        {
            await dal.Delete(id).ConfigureAwait(false);
        }

        /// <summary>
        /// 按设备分类 + 故障类型获取推荐物料列表
        /// </summary>
        public async Task<List<Material>> GetRecommendedMaterials(int categoryId, string faultType)
        {
            return await dal.GetByCategoryAndFault(categoryId, faultType).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取某设备分类下可选的故障类型列表（供 AI 候选用）
        /// </summary>
        public async Task<List<string>> GetAvailableFaultTypes(int categoryId)
        {
            return await dal.GetDistinctFaultTypes(categoryId).ConfigureAwait(false);
        }
    }
}
