using System;

namespace HospitalEquipment.Model.Dashboard
{
    /// <summary>
    /// 预测性维护（Predictive Maintenance）
    /// 基于历史维修间隔计算 MTBF，预测每台设备的故障风险与下次故障时间
    /// </summary>
    public class PredictiveMaintenanceDto
    {
        /// <summary>设备ID</summary>
        public int EquipmentId { get; set; }

        /// <summary>设备编号</summary>
        public string EquipmentNo { get; set; }

        /// <summary>设备名称</summary>
        public string EquipmentName { get; set; }

        /// <summary>设备型号</summary>
        public string Model { get; set; }

        /// <summary>所属科室</summary>
        public string DeptName { get; set; }

        /// <summary>历史维修次数</summary>
        public int RepairCount { get; set; }

        /// <summary>平均无故障间隔 MTBF（天），无历史数据时用型号均值或默认值</summary>
        public double MtbfDays { get; set; }

        /// <summary>距离上次维修/上次故障的天数</summary>
        public double DaysSinceLastRepair { get; set; }

        /// <summary>上次维修时间（无则为采购时间）</summary>
        public DateTime? LastRepairTime { get; set; }

        /// <summary>预测下次故障日期（上次维修时间 + MTBF）</summary>
        public DateTime? PredictedNextFailDate { get; set; }

        /// <summary>故障风险等级：高/中/低</summary>
        public string RiskLevel { get; set; }

        /// <summary>故障风险值 0-100（越高越危险）</summary>
        public double RiskScore { get; set; }

        /// <summary>建议操作</summary>
        public string Suggestion { get; set; }
    }
}
