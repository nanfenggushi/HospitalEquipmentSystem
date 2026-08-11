using System;

namespace HospitalEquipment.Model.Dashboard
{
    /// <summary>
    /// 设备健康度评分（Health Score）
    /// 综合 维修频率/停机时长/老化系数/维护按时率 四维加权，输出 0-100 分与健康等级
    /// </summary>
    public class DeviceHealthDto
    {
        /// <summary>设备ID</summary>
        public int EquipmentId { get; set; }

        /// <summary>设备编号</summary>
        public string EquipmentNo { get; set; }

        /// <summary>设备名称</summary>
        public string EquipmentName { get; set; }

        /// <summary>设备型号</summary>
        public string Model { get; set; }

        /// <summary>设备状态（Idle/InUse/Maintenance/Borrowed/Scrapped）</summary>
        public string Status { get; set; }

        /// <summary>所属科室</summary>
        public string DeptName { get; set; }

        /// <summary>设备分类</summary>
        public string CategoryName { get; set; }

        // ===== 四维原始指标 =====

        /// <summary>近90天维修次数（频率维度）</summary>
        public int RepairCount90d { get; set; }

        /// <summary>历史累计维修次数</summary>
        public int TotalRepairCount { get; set; }

        /// <summary>平均停机小时数/次（近90天）</summary>
        public decimal AvgDowntimeHours { get; set; }

        /// <summary>累计维修费用（元）</summary>
        public decimal TotalRepairCost { get; set; }

        /// <summary>已使用年限（按采购日期算）</summary>
        public double UsedYears { get; set; }

        /// <summary>设计寿命（年）</summary>
        public int? ServiceLife { get; set; }

        /// <summary>是否维护超期（NextMaintainDate 已过）</summary>
        public bool IsMaintainOverdue { get; set; }

        /// <summary>距下次维护剩余天数（负数=已超期）</summary>
        public int? DaysToNextMaintain { get; set; }

        // ===== 评分结果（BLL 计算后填充） =====

        /// <summary>健康度综合评分 0-100</summary>
        public double Score { get; set; }

        /// <summary>健康等级：优(≥85)/良(≥70)/中(≥55)/差(&lt;55)</summary>
        public string Level { get; set; }

        /// <summary>建议操作（根据最低分维度生成）</summary>
        public string Suggestion { get; set; }

        /// <summary>状态中文显示（便于界面直接展示）</summary>
        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case "Idle": return "空闲";
                    case "InUse": return "使用中";
                    case "Maintenance": return "维修中";
                    case "Borrowed": return "已借出";
                    case "Scrapped": return "已报废";
                    default: return Status ?? "";
                }
            }
        }
    }
}
