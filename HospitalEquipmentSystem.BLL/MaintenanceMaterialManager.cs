using HospitalEquipment.DAL;
using HospitalEquipment.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HospitalEquipment.BLL
{
    /// <summary>
    /// 维修物料管理核心编排类。
    /// 负责协调 AI 故障分析 → 物料推荐 → 物料保存的完整流程。
    /// </summary>
    public class MaintenanceMaterialManager
    {
        private readonly MaterialDAL _materialDal = new MaterialDAL();
        private readonly MaintenanceMaterialDAL _mmDal = new MaintenanceMaterialDAL();
        private readonly MaintenanceDAL _maintenanceDal = new MaintenanceDAL();
        private readonly IFaultRecognizer _recognizer;

        /// <summary>
        /// 构造函数注入 IFaultRecognizer（后期替换实现只需改这里）
        /// </summary>
        public MaintenanceMaterialManager(IFaultRecognizer recognizer)
        {
            _recognizer = recognizer ?? throw new ArgumentNullException(nameof(recognizer));
        }

        /// <summary>
        /// 编排 AI 故障分析全流程：
        /// 1. 获取工单 → 拿设备分类 ID
        /// 2. 获取候选故障类型列表
        /// 3. 调 AI 识别
        /// 4. 写入结果
        /// 5. 返回
        /// </summary>
        public async Task<FaultRecognitionResult> AnalyzeFaultPhoto(int recordId, byte[] photoData)
        {
            // 1. 获取工单信息
            DataTable dt = _maintenanceDal.GetOrderById(recordId);
            if (dt.Rows.Count == 0)
                throw new ArgumentException($"工单 {recordId} 不存在。");

            DataRow row = dt.Rows[0];
            int categoryId = Convert.ToInt32(row["CategoryId"]);

            // 2. 获取该设备分类下的候选故障类型
            List<string> candidates = await _materialDal.GetDistinctFaultTypes(categoryId).ConfigureAwait(false);
            // 分类下无物料记录时，兜底查全部故障类型
            if (candidates == null || candidates.Count == 0)
            {
                candidates = await _materialDal.GetAllFaultTypes().ConfigureAwait(false);
            }
            if (candidates == null || candidates.Count == 0)
            {
                return new FaultRecognitionResult
                {
                    FaultType = "无可用故障类型",
                    Confidence = 0
                };
            }

            // 3. 调用 AI 识别
            FaultRecognitionResult result = await _recognizer.RecognizeAsync(photoData, candidates).ConfigureAwait(false);

            // 4. 只将有效结果写入数据库（未识别/无可用故障类型等不写入）
            if (!string.IsNullOrEmpty(result.FaultType) && result.FaultType != "未识别" && result.FaultType != "无可用故障类型")
            {
                _maintenanceDal.UpdateAiResult(recordId, result.FaultType, result.Confidence);
            }

            // 5. 返回
            return result;
        }

        /// <summary>
        /// 获取推荐物料列表（根据 AI 识别出的故障类型 + 设备分类）
        /// </summary>
        public async Task<List<Material>> GetRecommendedMaterials(int recordId)
        {
            // 1. 获取工单信息
            DataTable dt = _maintenanceDal.GetOrderById(recordId);
            if (dt.Rows.Count == 0) return new List<Material>();

            DataRow row = dt.Rows[0];
            string aiFaultType = row["AiFaultType"]?.ToString() ?? "";
            if (string.IsNullOrEmpty(aiFaultType))
                return new List<Material>();

            int categoryId = Convert.ToInt32(row["CategoryId"]);

            // 2. 严格按 设备分类 + AI识别故障类型 查询推荐物料
            var materials = await _materialDal.GetByCategoryAndFault(categoryId, aiFaultType).ConfigureAwait(false);

            return materials ?? new List<Material>();
        }

        /// <summary>
        /// 保存维修物料清单（先删旧记录，再批量插入）
        /// </summary>
        public async Task SaveMaintenanceMaterials(int recordId, List<MaintenanceMaterial> materials)
        {
            if (materials == null || materials.Count == 0) return;

            // 确保所有记录关联正确的 RecordId
            foreach (var item in materials)
            {
                item.RecordId = recordId;
            }

            // 删旧 → 插新
            await _mmDal.DeleteByRecordId(recordId).ConfigureAwait(false);
            await _mmDal.BatchInsert(materials).ConfigureAwait(false);
        }

        /// <summary>
        /// 计算维修单物料总金额
        /// </summary>
        public async Task<decimal> GetTotalAmount(int recordId)
        {
            return await _mmDal.GetTotalCost(recordId).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取某维修单已有的物料记录
        /// </summary>
        public async Task<List<MaintenanceMaterial>> GetExistingMaterials(int recordId)
        {
            return await _mmDal.GetByRecordId(recordId).ConfigureAwait(false);
        }
    }
}
