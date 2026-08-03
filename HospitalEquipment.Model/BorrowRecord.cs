using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 设备借用记录实体：对应数据库中的设备借用/归还记录表
    /// </summary>
    public class BorrowRecord
    {
        /// <summary>借用记录主键 ID</summary>
        public int BorrowId { get; set; }

        /// <summary>借用单号，如 BORROW20260803001</summary>
        public string BorrowNo { get; set; }

        /// <summary>借用设备 ID（关联 Equipment）</summary>
        public int EquipmentId { get; set; }

        /// <summary>申请人用户 ID（关联 User）</summary>
        public int ApplicantId { get; set; }

        /// <summary>申请人所在科室 ID（关联 Department）</summary>
        public int? ApplicantDeptId { get; set; }

        /// <summary>借用用途说明</summary>
        public string Purpose { get; set; }

        /// <summary>预计归还日期</summary>
        public DateTime ExpectedReturnDate { get; set; }

        /// <summary>审批人用户 ID；未审批时为 null</summary>
        public int? ApproverId { get; set; }

        /// <summary>审批时间；未审批时为 null</summary>
        public DateTime? ApproveDate { get; set; }

        /// <summary>借用状态：如待审批/已审批/已归还/已逾期等</summary>
        public string Status { get; set; }

        /// <summary>实际归还日期；未归还时为 null</summary>
        public DateTime? ActualReturnDate { get; set; }

        /// <summary>归还备注</summary>
        public string ReturnNote { get; set; }

        /// <summary>其他备注信息</summary>
        public string Remarks { get; set; }

        /// <summary>记录创建时间</summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>记录最后更新时间；未更新时为 null</summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
