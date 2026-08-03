using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 供应商实体：设备采购来源的供应商信息
    /// </summary>
    public class Supplier
    {
        /// <summary>供应商主键 ID</summary>
        public int SupplierId { get; set; }

        /// <summary>供应商名称</summary>
        public string SupplierName { get; set; }

        /// <summary>联系人姓名</summary>
        public string ContactPerson { get; set; }

        /// <summary>联系电话</summary>
        public string Phone { get; set; }

        /// <summary>是否启用</summary>
        public bool IsActive { get; set; }

        /// <summary>供应商记录创建时间</summary>
        public DateTime CreatedAt { get; set; }
    }
}
