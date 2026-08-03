using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 设备分类实体：用于设备按类别统计和展示
    /// </summary>
    public class EquipmentCategory
    {
        /// <summary>分类主键 ID</summary>
        public int CategoryId { get; set; }

        /// <summary>分类名称，如“超声设备”</summary>
        public string CategoryName { get; set; }

        /// <summary>分类编码，用于系统内部标识</summary>
        public string CategoryCode { get; set; }

        /// <summary>上级分类 ID；无上级分类时为 null</summary>
        public int? ParentId { get; set; }

        /// <summary>排序号，数字越小越靠前</summary>
        public int SortOrder { get; set; }

        /// <summary>是否启用</summary>
        public bool IsActive { get; set; }

        /// <summary>分类记录创建时间</summary>
        public DateTime CreatedAt { get; set; }
    }
}
