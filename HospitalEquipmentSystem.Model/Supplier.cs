using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 供应商实体类，对应数据库 Suppliers 表
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// 供应商ID（主键，数据库自增）
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// 供应商名称（必填）
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 供应商编码（用于内部管理，如 GY-001）
        /// </summary>
        public string SupplierCode { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactPerson { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 官网地址
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否启用（软删除标记，true=启用，false=禁用）
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 创建时间（数据库自动生成）
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}