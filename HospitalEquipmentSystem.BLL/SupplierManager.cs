using HospitalEquipment.DAL;
using HospitalEquipment.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalEquipment.BLL
{
    public class SupplierManager
    {
        private SupplierDAL dal = new SupplierDAL();

        public async Task<List<Supplier>> GetAll() => await dal.GetAll().ConfigureAwait(false);
        public async Task<(List<Supplier> list, int total)> GetPaged(int pageIndex, int pageSize, string keyword = "")
        {
            if (pageIndex < 1) pageIndex = 1;
            return await dal.GetPaged(pageIndex, pageSize, keyword).ConfigureAwait(false);
        }
        public async Task<List<Supplier>> GetActive() => await dal.GetActive().ConfigureAwait(false);
        public async Task<Supplier> GetById(int id) => await dal.GetById(id).ConfigureAwait(false);

        public async Task<bool> Insert(Supplier supplier)
        {
            await Validate(supplier).ConfigureAwait(false);
            return await dal.Insert(supplier).ConfigureAwait(false) > 0;
        }

        public async Task<bool> Update(Supplier supplier)
        {
            await Validate(supplier).ConfigureAwait(false);
            return await dal.Update(supplier).ConfigureAwait(false) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            if (await dal.GetEquipmentCount(id).ConfigureAwait(false) > 0)
                throw new Exception("该供应商已被设备引用，无法删除！");
            return await dal.Delete(id).ConfigureAwait(false) > 0;
        }

        public async Task<List<Supplier>> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await dal.GetAll().ConfigureAwait(false);
            return await dal.Search(keyword).ConfigureAwait(false);
        }

        private async Task Validate(Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.SupplierName))
                throw new Exception("供应商名称不能为空");

            if (await dal.IsNameExists(supplier.SupplierName, supplier.SupplierId).ConfigureAwait(false))
                throw new Exception($"供应商 '{supplier.SupplierName}' 已存在！");
        }
    }
}
