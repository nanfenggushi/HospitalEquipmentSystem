using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 设备入库记录实体：记录设备采购/入库信息
    /// </summary>
    public class InboundRecord
    {
        /// <summary>入库记录主键 ID</summary>
        public int InboundId { get; set; }

        /// <summary>入库设备 ID（关联 Equipment）</summary>
        public int EquipmentId { get; set; }

        /// <summary>入库单号，如 INBOUND20260803001</summary>
        public string InboundNo { get; set; }

        /// <summary>供应商名称</summary>
        public string Supplier { get; set; }

        /// <summary>采购单价；未录入时为 null</summary>
        public decimal? PurchasePrice { get; set; }

        /// <summary>入库数量</summary>
        public int Quantity { get; set; }

        /// <summary>入库日期</summary>
        public DateTime InboundDate { get; set; }

        /// <summary>经办人用户 ID（关联 User）</summary>
        public int? OperatorId { get; set; }

        /// <summary>审核状态：如待审核/已通过/已驳回</summary>
        public string AuditStatus { get; set; }

        /// <summary>入库备注</summary>
        public string Remarks { get; set; }

        /// <summary>记录创建时间</summary>
        public DateTime CreatedAt { get; set; }
    }
}
