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
 //========== 以下为扩展字段（非数据库列，用于显示关联信息） ==========

        /// <summary>
        /// 设备名称（关联 Equipment 表查询得到）
        /// </summary>
        public string EquipmentName { get; set; }

        /// <summary>
        /// 设备编号（关联 Equipment 表查询得到）
        /// </summary>
        public string EquipmentNo { get; set; }

        /// <summary>
        /// 操作人姓名（关联 Users 表查询得到）
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 审核状态中文显示
        /// </summary>
        public string AuditStatusText
        {
            get
            {
                switch (AuditStatus)
                {
                    case "Pending": return "待审核";
                    case "Approved": return "已通过";
                    case "Rejected": return "已驳回";
                    default: return AuditStatus ?? "";
                }
            }
        }
    }
}
