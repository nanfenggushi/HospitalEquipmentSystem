using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 设备（对应 Equipment 表）
    /// </summary>
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string EquipmentNo { get; set; }
        public string EquipmentName { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public int? DeptId { get; set; }
        public string Location { get; set; }
        public int? ResponsibleUserId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int? WarrantyMonths { get; set; }
        public int? ServiceLife { get; set; }
        public DateTime? LastMaintainDate { get; set; }
        public DateTime? NextMaintainDate { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
