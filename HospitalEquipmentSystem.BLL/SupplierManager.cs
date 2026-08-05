using HospitalEquipment.DAL;
using HospitalEquipment.Model;
using System;
using System.Collections.Generic;

namespace HospitalEquipment.BLL
{
    public class SupplierManager
    {
        private SupplierDAL dal = new SupplierDAL();

        public List<Supplier> GetAll() => dal.GetAll();
        public List<Supplier> GetActive() => dal.GetActive();
        public Supplier GetById(int id) => dal.GetById(id);

        public bool Insert(Supplier supplier)
        {
            Validate(supplier);
            return dal.Insert(supplier) > 0;
        }

        public bool Update(Supplier supplier)
        {
            Validate(supplier);
            return dal.Update(supplier) > 0;
        }

        public bool Delete(int id)
        {
            if (dal.GetEquipmentCount(id) > 0)
                throw new Exception("该供应商已被设备引用，无法删除！");
            return dal.Delete(id) > 0;
        }

        public List<Supplier> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return dal.GetAll();
            return dal.Search(keyword);
        }

        private void Validate(Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.SupplierName))
                throw new Exception("供应商名称不能为空");

            if (dal.IsNameExists(supplier.SupplierName, supplier.SupplierId))
                throw new Exception($"供应商 '{supplier.SupplierName}' 已存在！");
        }
    }
}