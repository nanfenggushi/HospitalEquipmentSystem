using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 维修物料记录实体类，对应数据库 MaintenanceMaterials 表
    /// </summary>
    public class MaintenanceMaterial
    {
        /// <summary>
        /// 主键（数据库自增）
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 关联维修单ID（外键，关联 MaintenanceRecords 表）
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// 关联物料字典ID（外键，关联 Materials 表，手动添加的物料为 null）
        /// </summary>
        public int? MaterialId { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 实际用量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 小计（只读，= Quantity × UnitPrice）
        /// </summary>
        public decimal Subtotal
        {
            get { return Quantity * UnitPrice; }
        }
    }
}
