using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 设备实体类（对应数据库 Equipment 表）
    /// 用于在 UI 层展示设备信息和统计
    /// </summary>
    public class Equipment
    {
        /// <summary>设备主键ID</summary>
        public int EquipmentId { get; set; }

        /// <summary>设备编号（唯一，如 MY-2026-088）</summary>
        public string EquipmentNo { get; set; }

        /// <summary>设备名称（如 免疫分析仪）</summary>
        public string EquipmentName { get; set; }

        /// <summary>设备型号</summary>
        public string Model { get; set; }

        /// <summary>生产厂家</summary>
        public string Manufacturer { get; set; }

        /// <summary>供应商ID（关联 Suppliers 表）</summary>
        public int? SupplierId { get; set; }

        /// <summary>设备分类ID（关联 EquipmentCategories 表）</summary>
        public int? CategoryId { get; set; }

        /// <summary>所属科室ID（关联 Departments 表）</summary>
        public int? DeptId { get; set; }

        /// <summary>存放位置</summary>
        public string Location { get; set; }

        /// <summary>责任人用户ID</summary>
        public int? ResponsibleUserId { get; set; }

        /// <summary>采购价格</summary>
        public decimal? Price { get; set; }

        /// <summary>采购日期</summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>保修月数</summary>
        public int? WarrantyMonths { get; set; }

        /// <summary>预计使用寿命（年）</summary>
        public int? ServiceLife { get; set; }

        /// <summary>上次保养日期</summary>
        public DateTime? LastMaintainDate { get; set; }

        /// <summary>下次保养日期</summary>
        public DateTime? NextMaintainDate { get; set; }

        /// <summary>设备状态：Idle空闲 / InUse使用中 / Maintenance维修中 / Borrowed借用中 / Scrapped报废</summary>
        public string Status { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }

        /// <summary>是否激活（软删除标记，1=有效，0=已删除）</summary>
        public bool IsActive { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>最后更新时间</summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
