using System;
using System.Collections.Generic;
using HospitalEquipment.DAL;
using HospitalEquipment.Model;
using HospitalEquipment.Model.management;

namespace HospitalEquipment.BLL
{
    /// <summary>
    /// 借用业务逻辑：申请校验、审批流转、归还验收、超期标记、统计
    /// </summary>
    public static class BorrowBLL
    {
        /// <summary>分页查询借用记录</summary>
        public static List<BorrowRecord> Search(string keyword, string status, int equipmentId,
            DateTime? from, DateTime? to, int pageIndex, int pageSize, out int total)
        {
            var list = BorrowDAL.GetPage(keyword, status, equipmentId, from, to, pageIndex, pageSize, out total);
            return list ?? new List<BorrowRecord>();
        }

        /// <summary>查询单条记录</summary>
        public static BorrowRecord GetById(int borrowId)
        {
            return BorrowDAL.GetById(borrowId);
        }

        /// <summary>
        /// 提交借用申请
        /// </summary>
        public static BLLResult Apply(int equipmentId, int applicantId, int? applicantDeptId,
            string purpose, DateTime expectedReturnDate, string remarks)
        {
            purpose = (purpose ?? "").Trim();
            remarks = (remarks ?? "").Trim();

            if (string.IsNullOrEmpty(purpose))
                return BLLResult.Fail("请填写借用用途");

            if (expectedReturnDate.Date < DateTime.Today)
                return BLLResult.Fail("预计归还日期不能早于今天");

            Equipment eq = EquipmentDAL.GetById(equipmentId);
            if (eq == null)
                return BLLResult.Fail("设备不存在");
            if (!eq.IsActive)
                return BLLResult.Fail("设备已停用，不可借用");
            if (eq.Status != BorrowConst.EqIdle)
                return BLLResult.Fail("该设备当前状态为「" + BorrowConst.EquipmentStatusText(eq.Status) + "」，不可借用");
            if (BorrowDAL.HasActiveBorrow(equipmentId))
                return BLLResult.Fail("该设备已有进行中的借用记录，请选择其他设备");

            var r = new BorrowRecord
            {
                BorrowNo = BorrowDAL.GenerateBorrowNo(),
                EquipmentId = equipmentId,
                ApplicantId = applicantId,
                ApplicantDeptId = applicantDeptId,
                Purpose = purpose,
                ExpectedReturnDate = expectedReturnDate.Date,
                Status = BorrowConst.Pending,
                Remarks = remarks
            };

            int id = BorrowDAL.Insert(r);
            if (id <= 0)
                return BLLResult.Fail("申请保存失败，请稍后重试");

            return BLLResult.Ok("借用申请提交成功，单号：" + r.BorrowNo);
        }

        /// <summary>审批通过（设备科）：待审批 → 借用中，设备标记为已借出</summary>
        public static BLLResult Approve(int borrowId, int approverId)
        {
            BorrowRecord r = BorrowDAL.GetById(borrowId);
            if (r == null)
                return BLLResult.Fail("借用记录不存在");
            if (r.Status != BorrowConst.Pending)
                return BLLResult.Fail("当前状态为「" + BorrowConst.StatusText(r.Status) + "」，不可审批");

            int n = BorrowDAL.ApproveWithEquipment(borrowId, approverId,r.EquipmentId);
            if (n <= 0)
                return BLLResult.Fail("审批失败，请稍后重试");

            return BLLResult.Ok("已审批通过，设备「" + r.EquipmentName + "」标记为借出");
        }

        /// <summary>审批驳回：待审批 → 已驳回</summary>
        public static BLLResult Reject(int borrowId, int approverId)
        {
            BorrowRecord r = BorrowDAL.GetById(borrowId);
            if (r == null)
                return BLLResult.Fail("借用记录不存在");
            if (r.Status != BorrowConst.Pending)
                return BLLResult.Fail("当前状态为「" + BorrowConst.StatusText(r.Status) + "」，不可审批");

            int n = BorrowDAL.Approve(borrowId, approverId, BorrowConst.Rejected);
            if (n <= 0)
                return BLLResult.Fail("驳回失败，请稍后重试");

            return BLLResult.Ok("已驳回该借用申请");
        }

        /// <summary>
        /// 归还验收：登记归还并联动设备状态；
        /// 无损坏 → 设备恢复空闲；有损坏 → 设备转维修并自动创建维修工单
        /// </summary>
        public static BLLResult ReturnBorrow(int borrowId, string returnNote, bool hasDamage,
            string damageDesc, int operatorId, int? operatorDeptId)
        {
            BorrowRecord r = BorrowDAL.GetById(borrowId);
            if (r == null)
                return BLLResult.Fail("借用记录不存在");
            if (r.Status != BorrowConst.Approved && r.Status != BorrowConst.Overdue)
                return BLLResult.Fail("当前状态为「" + BorrowConst.StatusText(r.Status) + "」，不可归还");

            if (hasDamage && string.IsNullOrWhiteSpace(damageDesc))
                return BLLResult.Fail("设备损坏时请填写损坏情况说明");

            int n = BorrowDAL.ReturnBorrowFull(borrowId, (returnNote ?? "").Trim(), DateTime.Today, r.EquipmentId,
                hasDamage, hasDamage ? damageDesc.Trim() : null, operatorId, operatorDeptId);
            if (n <= 0)
                return BLLResult.Fail("归还登记失败，请稍后重试");

            if (hasDamage)
            {
                return BLLResult.Ok("归还成功。设备存在损坏，已转为维修状态并自动生成维修工单");
            }
            return BLLResult.Ok("归还成功，设备「" + r.EquipmentName + "」已恢复空闲");
        }

        /// <summary>自动标记超期（启动或操作后调用）</summary>
        public static void MarkOverdue()
        {
            BorrowDAL.MarkOverdue();
        }

        // ==================== 统计卡片 ====================

        public static int PendingCount()
        {
            return BorrowDAL.CountByStatus(BorrowConst.Pending);
        }

        public static int BorrowingCount()
        {
            return BorrowDAL.CountByStatus(BorrowConst.Approved);
        }

        public static int OverdueCount()
        {
            return BorrowDAL.CountByStatus(BorrowConst.Overdue);
        }

        public static int DueTodayCount()
        {
            return BorrowDAL.CountDueToday();
        }

        // ==================== 统计图表 ====================

        public static List<CountItem> TopEquipment()
        {
            return BorrowDAL.GetTopEquipment(10);
        }

        public static List<CountItem> ByDept()
        {
            return BorrowDAL.GetByDept();
        }

        public static List<CountItem> BorrowByMonth()
        {
            return BorrowDAL.GetBorrowByMonth();
        }

        public static List<CountItem> ReturnByMonth()
        {
            return BorrowDAL.GetReturnByMonth();
        }

        public static List<CountItem> EquipmentStatus()
        {
            return BorrowDAL.GetEquipmentStatus();
        }

        public static List<DateTime> ReturnDates()
        {
            return BorrowDAL.GetReturnDates();
        }

        public static List<BorrowRecord> GetByReturnDate(DateTime date)
        {
            return BorrowDAL.GetByReturnDate(date);
        }

        // ==================== 设备 ====================

        /// <summary>可借用设备列表（空闲且无进行中借用）</summary>
        public static List<Equipment> GetAvailableEquipment()
        {
            var list = EquipmentDAL.GetAvailable();
            return list ?? new List<Equipment>();
        }

        /// <summary>全部在用设备（筛选下拉）</summary>
        public static List<Equipment> GetAllEquipment()
        {
            var list = EquipmentDAL.GetAll();
            return list ?? new List<Equipment>();
        }

        /// <summary>设备可借用性校验，返回 null 表示可借用，否则返回原因</summary>
        public static string GetEquipmentAvailableMessage(int equipmentId)
        {
            Equipment eq = EquipmentDAL.GetById(equipmentId);
            if (eq == null)
                return "设备不存在";
            if (!eq.IsActive)
                return "设备已停用，不可借用";
            if (eq.Status != BorrowConst.EqIdle)
                return "该设备当前为「" + BorrowConst.EquipmentStatusText(eq.Status) + "」，不可借用";
            if (BorrowDAL.HasActiveBorrow(equipmentId))
                return "该设备已有进行中的借用记录";
            return null;
        }
    }
}
