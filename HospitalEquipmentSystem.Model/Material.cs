using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 物料实体类，对应数据库 Materials 表
    /// </summary>
    public class Material
    {
        /// <summary>
        /// 物料ID（主键，数据库自增）
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// 物料名称（必填）
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 设备分类ID（外键，关联 EquipmentCategories 表）
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 适用故障类型
        /// </summary>
        public string FaultType { get; set; }

        /// <summary>
        /// 单价（元）
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 默认用量
        /// </summary>
        public int DefaultQuantity { get; set; }

        /// <summary>
        /// 计量单位（如：个、套、根、米）
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否启用（true=启用，false=禁用）
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
