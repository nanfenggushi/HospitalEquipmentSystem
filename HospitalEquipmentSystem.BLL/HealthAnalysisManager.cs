using HospitalEquipment.DAL;
using HospitalEquipment.Model.Dashboard;
using System;
using System.Collections.Generic;

namespace HospitalEquipment.BLL
{
    /// <summary>
    /// 设备健康分析业务逻辑层
    /// 1) 设备健康度评分（0-100 加权）
    /// 2) 预测性维护（MTBF 风险等级）
    /// 3) 设备全生命周期时间轴
    /// </summary>
    public class HealthAnalysisManager
    {
        private readonly HealthAnalysisDAL dal = new HealthAnalysisDAL();

        #region ===== 1. 设备健康度评分 =====

        /// <summary>
        /// 获取全部在用设备的健康度评分列表（含四维加权分数与建议）
        /// </summary>
        public List<DeviceHealthDto> GetHealthScores()
        {
            var list = dal.GetHealthRawData();
            foreach (var d in list)
            {
                d.Score = ComputeHealthScore(d);
                d.Level = GetHealthLevel(d.Score);
                d.Suggestion = BuildHealthSuggestion(d);
            }
            return list;
        }

        /// <summary>
        /// 健康度加权评分（满分100）
        /// 权重：维修频率 40 + 停机时长 25 + 老化程度 20 + 维护按时率 15
        /// </summary>
        private double ComputeHealthScore(DeviceHealthDto d)
        {
            double score = 0;

            // ---- 1) 维修频率（40 分）：近90天维修0次=满分，每次扣10分，最低0 ----
            double freqScore = 40 - d.RepairCount90d * 10;
            if (freqScore < 0) freqScore = 0;

            // ---- 2) 停机时长（25 分）：平均每次停机0h=满分，每10小时扣5分，最低0 ----
            double downScore = 25 - (double)d.AvgDowntimeHours / 10 * 5;
            if (downScore < 0) downScore = 0;

            // ---- 3) 老化程度（20 分）：已用年限/设计寿命 比值越高扣越多，最低0 ----
            double ageScore = 20;
            if (d.ServiceLife.HasValue && d.ServiceLife.Value > 0)
            {
                double used = d.UsedYears;
                double ratio = used / d.ServiceLife.Value;   // 0~1 及以上
                if (ratio > 1) ratio = 1;
                ageScore = 20 * (1 - ratio);
            }

            // ---- 4) 维护按时率（15 分）：超期直接清零，剩余≤30天按比例扣 ----
            double maintScore = 15;
            if (d.IsMaintainOverdue)
            {
                maintScore = 0;
            }
            else if (d.DaysToNextMaintain.HasValue && d.DaysToNextMaintain.Value <= 30)
            {
                maintScore = 15.0 * d.DaysToNextMaintain.Value / 30;
            }

            score = freqScore + downScore + ageScore + maintScore;

            // 报废设备强制为最低分
            if (d.Status == "Scrapped") score = 0;

            return Math.Round(Math.Max(0, Math.Min(100, score)), 1);
        }

        /// <summary>健康等级：≥85优 / ≥70良 / ≥55中 / &lt;55差</summary>
        private string GetHealthLevel(double score)
        {
            if (score >= 85) return "优";
            if (score >= 70) return "良";
            if (score >= 55) return "中";
            return "差";
        }

        /// <summary>根据最低分维度生成建议操作</summary>
        private string BuildHealthSuggestion(DeviceHealthDto d)
        {
            if (d.Status == "Scrapped") return "设备已报废，建议走资产处置流程。";

            var reasons = new List<string>();

            if (d.RepairCount90d >= 2)
                reasons.Add("近90天维修频繁，建议安排深度检修或评估更换");
            if (d.AvgDowntimeHours >= 24)
                reasons.Add("停机时长偏高，建议排查易损件并备货");
            if (d.IsMaintainOverdue)
                reasons.Add("保养已超期，请立即安排定期维护");
            if (d.DaysToNextMaintain.HasValue && d.DaysToNextMaintain.Value <= 30 && d.DaysToNextMaintain.Value >= 0)
                reasons.Add($"距下次保养仅剩{d.DaysToNextMaintain.Value}天，请预约维护");
            if (d.UsedYears >= 8)
                reasons.Add("设备使用年限较长，建议关注老化风险并规划更新");

            return reasons.Count > 0 ? string.Join("；", reasons) + "。" : "设备状态良好，保持常规巡检即可。";
        }

        #endregion

        #region ===== 2. 预测性维护 =====

        /// <summary>
        /// 获取预测性维护列表（MTBF 风险）
        /// 规则：DaysSinceLastRepair 与 MtbfDays 的比值决定风险等级
        /// </summary>
        public List<PredictiveMaintenanceDto> GetPredictiveMaintenance()
        {
            var list = dal.GetPredictiveMaintenanceRaw();
            foreach (var d in list)
            {
                // 无历史维修记录时，使用默认 MTBF=90 天（设备到货后首次故障风险窗口）
                if (d.MtbfDays <= 0) d.MtbfDays = 90;

                // 上次维修时间为空（从未修过）时，用采购时间作为参考点
                if (!d.LastRepairTime.HasValue && d.DaysSinceLastRepair > 0)
                {
                    // DaysSinceLastRepair 在 SQL 里已回退到采购日期，无需处理
                }

                // 预测下次故障日期 = 上次维修时间 + MTBF
                if (d.LastRepairTime.HasValue)
                {
                    d.PredictedNextFailDate = d.LastRepairTime.Value.AddDays(d.MtbfDays);
                }

                // 风险值：已超过 MTBF 天数为 100，达到 60% 时开始线性上升
                double ratio = d.MtbfDays > 0 ? d.DaysSinceLastRepair / d.MtbfDays : 0;
                d.RiskScore = Math.Round(Math.Max(0, Math.Min(100, ratio * 100)), 1);

                // 风险等级
                if (ratio >= 1.0) d.RiskLevel = "高";
                else if (ratio >= 0.8) d.RiskLevel = "中";
                else if (ratio >= 0.5) d.RiskLevel = "关注";
                else d.RiskLevel = "低";

                d.Suggestion = BuildPredictiveSuggestion(d, ratio);
            }

            // 高/中风险排前面
            list.Sort((a, b) => b.RiskScore.CompareTo(a.RiskScore));
            return list;
        }

        private string BuildPredictiveSuggestion(PredictiveMaintenanceDto d, double ratio)
        {
            if (d.RiskLevel == "高")
            {
                return $"已超过平均故障间隔 {d.MtbfDays:0} 天，故障概率高，请尽快安排预防性检修。";
            }
            if (d.RiskLevel == "中")
            {
                return $"接近平均故障间隔（{d.MtbfDays:0}天），建议在未来 {(int)(d.MtbfDays - d.DaysSinceLastRepair)} 天内安排检修。";
            }
            if (d.RiskLevel == "关注")
            {
                return "开始进入风险窗口，建议列入近期巡检计划。";
            }
            return "处于安全运行期，保持常规巡检即可。";
        }

        #endregion

        #region ===== 3. 设备全生命周期时间轴 =====

        /// <summary>
        /// 获取指定设备的全生命周期事件（时间倒序）
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        public List<EquipmentLifecycleEvent> GetLifecycleEvents(int equipmentId)
        {
            if (equipmentId <= 0) return new List<EquipmentLifecycleEvent>();
            return dal.GetLifecycleEvents(equipmentId);
        }

        /// <summary>
        /// 获取全部在用设备的生命周期事件（含 EquipmentId/EquipmentName，供山海鲸联动过滤）
        /// </summary>
        public List<EquipmentLifecycleEvent> GetAllLifecycleEvents()
        {
            return dal.GetLifecycleEvents(null);
        }

        #endregion
    }
}
