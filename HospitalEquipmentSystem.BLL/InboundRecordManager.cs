using HospitalEquipment.DAL;
using HospitalEquipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalEquipment.BLL
{
    public class InboundRecordManager
    {
        private InboundRecordDAL dal=new InboundRecordDAL();
        /// <summary>
        /// 分页查询入库记录
        /// </summary>     
        public async Task<(List<InboundRecord>list,int total)> GetPaged(
            int pageIndex,
            int pageSize,
           string keyword = "",
            string auditStatus = "",
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            if(pageIndex<1)pageIndex = 1;
            return await dal.GetPaged(pageIndex, pageSize, keyword, auditStatus, startDate, endDate).ConfigureAwait(false);
        }
        /// <summary>
        /// 根据ID获取入库记录
        /// </summary>
        public async Task<InboundRecord> GetById(int id) => await dal.GetById(id).ConfigureAwait(false);

        /// <summary>
        /// 根据入库单号获取记录
        /// </summary>
        public async Task<InboundRecord> GetByNo(string inboundNo) => await dal.GetByNo(inboundNo).ConfigureAwait(false);

        /// <summary>
        /// 新增入库记录
        /// </summary>
        public async Task<bool> Insert(InboundRecord record)
        {
            Validate(record);

            // 自动生成入库单号
            if (string.IsNullOrWhiteSpace(record.InboundNo))
            {
                record.InboundNo = await dal.GenerateInboundNo().ConfigureAwait(false);
            }

            // 检查单号是否重复
            if (await dal.IsInboundNoExists(record.InboundNo).ConfigureAwait(false))
            {
                throw new Exception($"入库单号 '{record.InboundNo}' 已存在！");
            }

            // 默认审核状态为待审核
            if (string.IsNullOrWhiteSpace(record.AuditStatus))
            {
                record.AuditStatus = "Pending";
            }

            return await dal.Insert(record).ConfigureAwait(false) > 0;
        }

        /// <summary>
        /// 更新入库记录（仅待审核状态可更新）
        /// </summary>
        public async Task<bool> Update(InboundRecord record)
        {
            Validate(record);

            var existing = await dal.GetById(record.InboundId).ConfigureAwait(false);
            if (existing == null)
                throw new Exception("入库记录不存在！");

            if (existing.AuditStatus != "Pending")
                throw new Exception($"当前状态为「{existing.AuditStatusText}」，不允许修改！");

            return await dal.Update(record).ConfigureAwait(false) > 0;
        }

        /// <summary>
        /// 删除入库记录（仅待审核状态可删除）
        /// </summary>
        public async Task<bool> Delete(int id)
        {
            var existing = await dal.GetById(id).ConfigureAwait(false);
            if (existing == null)
                throw new Exception("入库记录不存在！");

            if (existing.AuditStatus != "Pending")
                throw new Exception($"当前状态为「{existing.AuditStatusText}」，不允许删除！");

            return await dal.Delete(id).ConfigureAwait(false) > 0;
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        public async Task<bool> Approve(int id)
        {
            var existing = await dal.GetById(id).ConfigureAwait(false);
            if (existing == null)
                throw new Exception("入库记录不存在！");

            if (existing.AuditStatus != "Pending")
                throw new Exception($"当前状态为「{existing.AuditStatusText}」，不可审核！");

            return await dal.UpdateAuditStatus(id, "Approved").ConfigureAwait(false) > 0;
        }

        /// <summary>
        /// 审核驳回
        /// </summary>
        public async Task<bool> Reject(int id)
        {
            var existing = await dal.GetById(id).ConfigureAwait(false);
            if (existing == null)
                throw new Exception("入库记录不存在！");

            if (existing.AuditStatus != "Pending")
                throw new Exception($"当前状态为「{existing.AuditStatusText}」，不可审核！");

            return await dal.UpdateAuditStatus(id, "Rejected").ConfigureAwait(false) > 0;
        }

        /// <summary>
        /// 生成入库单号
        /// </summary>
        public async Task<string> GenerateInboundNo() => await dal.GenerateInboundNo().ConfigureAwait(false);

        /// <summary>
        /// 获取设备列表（用于下拉框）
        /// </summary>
        public async Task<List<KeyValuePair<int, string>>> GetEquipmentList() => await dal.GetEquipmentList().ConfigureAwait(false);

        /// <summary>
        /// 获取供应商列表（用于下拉框）
        /// </summary>
        public async Task<List<string>> GetSupplierList() => await dal.GetSupplierList().ConfigureAwait(false);

        /// <summary>
        /// 获取操作人列表（用于下拉框）
        /// </summary>
        public async Task<List<KeyValuePair<int, string>>> GetOperatorList() => await dal.GetOperatorList().ConfigureAwait(false);

        /// <summary>
        /// 获取审核状态列表
        /// </summary>
        public async Task<List<KeyValuePair<string, string>>> GetAuditStatusList() => await Task.FromResult(dal.GetAuditStatusList()).ConfigureAwait(false);

        /// <summary>
        /// 根据设备ID获取设备编号
        /// </summary>
        public async Task<string> GetEquipmentNoById(int equipmentId) => await dal.GetEquipmentNoById(equipmentId).ConfigureAwait(false);

        /// <summary>
        /// 根据设备ID获取分类名称
        /// </summary>
        public async Task<string> GetCategoryNameByEquipmentId(int equipmentId) => await dal.GetCategoryNameByEquipmentId(equipmentId).ConfigureAwait(false);

        /// <summary>
        /// 验证入库记录数据合法性
        /// </summary>
        private void Validate(InboundRecord record)
        {
            if (record.EquipmentId <= 0)
                throw new Exception("请选择设备！");

            if (string.IsNullOrWhiteSpace(record.Supplier))
                throw new Exception("请选择供应商！");

            if (record.PurchasePrice.HasValue && record.PurchasePrice.Value < 0)
                throw new Exception("采购价格不能为负数！");

            if (record.Quantity <= 0)
                throw new Exception("数量必须大于0！");
        }
    }
}
