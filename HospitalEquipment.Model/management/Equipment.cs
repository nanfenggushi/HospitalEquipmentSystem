using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HospitalEquipment.Model.management
{
    /// <summary>
    /// 设备实体类，对应数据库 Equipment 表
    /// </summary>
    public class Equipment
    {
        /// <summary>设备ID（主键，自增）</summary>
        public int EquipmentId { get; set; }

        /// <summary>设备编号（唯一，如 CT-2026-001）</summary>
        public string EquipmentNo { get; set; }

        /// <summary>设备名称</summary>
        public string EquipmentName { get; set; }

        /// <summary>设备型号</summary>
        public string Model { get; set; }

        /// <summary>制造商名称（直接存储，或通过 ManufacturerId 关联，此处保留）</summary>
        public string Manufacturer { get; set; }

        /// <summary>供应商ID（关联 Suppliers 表）</summary>
        public int? SupplierId { get; set; }

        /// <summary>分类ID（关联 EquipmentCategories 表）</summary>
        public int? CategoryId { get; set; }

        /// <summary>科室ID（关联 Departments 表）</summary>
        public int? DeptId { get; set; }

        /// <summary>存放位置</summary>
        public string Location { get; set; }

        /// <summary>责任人用户ID（关联 Users 表）</summary>
        public int? ResponsibleUserId { get; set; }

        /// <summary>采购价格（元）</summary>
        public decimal? Price { get; set; }

        /// <summary>采购日期</summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>保修期限（月）</summary>
        public int? WarrantyMonths { get; set; }

        /// <summary>使用年限（年）</summary>
        public int? ServiceLife { get; set; }

        /// <summary>最近维护日期</summary>
        public DateTime? LastMaintainDate { get; set; }

        /// <summary>下次维护日期</summary>
        public DateTime? NextMaintainDate { get; set; }

        /// <summary>设备状态：Idle/InUse/Maintenance/Borrowed/Scrapped</summary>
        public string Status { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }

        /// <summary>是否启用（软删除标记）</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>创建时间</summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>最后更新时间</summary>
        public DateTime? UpdatedAt { get; set; }


        // ========== 以下为扩展字段（非数据库列，用于显示关联信息） ==========

        /// <summary>科室名称（关联查询得到）</summary>
        public string DeptName { get; set; }

        /// <summary>分类名称（关联查询得到）</summary>
        public string CategoryName { get; set; }

        /// <summary>供应商名称（关联查询得到）</summary>
        public string SupplierName { get; set; }

        /// <summary>责任人姓名（关联查询得到）</summary>
        public string ResponsibleUserRealName { get; set; }
    }
}
