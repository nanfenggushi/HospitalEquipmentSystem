using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 借用记录（对应 BorrowRecords 表，字段与数据库列一致，用于 DataReaderMapper 反射映射）
    /// </summary>
    public class BorrowRecord
    {
        public int BorrowId { get; set; }
        public string BorrowNo { get; set; }
        public int EquipmentId { get; set; }
        public int ApplicantId { get; set; }
        public int? ApplicantDeptId { get; set; }
        public string Purpose { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public int? ApproverId { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string Status { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public string ReturnNote { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // ---- JOIN 展示字段（不落库，由查询别名填充） ----
        public string EquipmentNo { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentStatus { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantDeptName { get; set; }
        public string ApproverName { get; set; }

        /// <summary>状态中文显示</summary>
        public string StatusText
        {
            get { return BorrowConst.StatusText(Status); }
        }

        /// <summary>超期天数（已超期或借用中且未归还）</summary>
        public int OverdueDays
        {
            get
            {
                if (ActualReturnDate == null &&
                    (Status == BorrowConst.Overdue || Status == BorrowConst.Approved))
                {
                    return Math.Max(0, (DateTime.Today - ExpectedReturnDate.Date).Days);
                }
                return 0;
            }
        }
    }
}
