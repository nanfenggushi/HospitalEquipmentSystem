using HospitalEquipment.Model;
using HospitalEquipment.DAL;
using HospitalEquipment.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalEquipment.BLL
{
    public class MaintenanceBLL
    {
        private readonly MaintenanceDAL dal = new MaintenanceDAL();

        // UI 调用这个方法拿数据
        public List<MaintenanceRecordDto> GetOrders(string urgency = null, string dept = null,
                                                     string keyword = null, string stage = null)
        {
            DataTable dt = dal.GetOrderList(urgency, dept, keyword, stage);
            return DataTableToList(dt);
        }

        // DataTable → List<Dto> 转换（管理员端用）
        private List<MaintenanceRecordDto> DataTableToList(DataTable dt)
        {
            return DataTableToList(dt, isRepairer: false);
        }

        // DataTable → List<Dto> 转换（维修员端用 StageToRepairerCn 映射阶段）
        private List<MaintenanceRecordDto> DataTableToList(DataTable dt, bool isRepairer)
        {
            var list = new List<MaintenanceRecordDto>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MaintenanceRecordDto
                {
                    RecordId = Convert.ToInt32(row["RecordId"]),
                    RepairNo = row["RepairNo"].ToString(),
                    EquipmentName = row["EquipmentName"].ToString(),
                    FaultType = row["FaultType"].ToString(),
                    FaultDesc = row["FaultDesc"].ToString(),
                    UrgencyText = MaintenanceHelper.UrgencyToCn(row["Urgency"].ToString()),
                    ProgressStage = row["ProgressStage"].ToString(),
                    ProgressStageText = isRepairer
                        ? MaintenanceHelper.StageToRepairerCn(row["ProgressStage"].ToString())
                        : MaintenanceHelper.StageToCn(row["ProgressStage"].ToString()),
                    DeptName = row["DeptName"].ToString(),
                    RepairerName = row["RepairerName"].ToString(),
                    ReportTime = Convert.ToDateTime(row["ReportTime"]),
                    DowntimeHours = row["DowntimeHours"] as decimal?,
                    StatusText = MaintenanceHelper.StatusToCn(row["Status"].ToString()),
                    RepairResult = row.Table.Columns.Contains("RepairResult") ? row["RepairResult"]?.ToString() : "",
                    RepairCost = row.Table.Columns.Contains("RepairCost") ? row["RepairCost"] as decimal? : null,
                    CompleteTime = row.Table.Columns.Contains("CompleteTime") && row["CompleteTime"] != DBNull.Value 
                        ? (DateTime?)row["CompleteTime"] : null,
                });
            }
            return list;
        }

        /// <summary>
        /// KPI 数据：返回一个字典，key=指标名，value=数值
        /// </summary>
        public Dictionary<string, int> GetKpiData()
        {
            var dt = dal.GetKpiSummary();
            var row = dt.Rows[0];
            return new Dictionary<string, int>
            {
                ["Pending"] = Convert.ToInt32(row["PendingCnt"]),
                ["Assigned"] = Convert.ToInt32(row["AssignedCnt"]),
                ["InProgress"] = Convert.ToInt32(row["InProgressCnt"]),
                ["Done"] = Convert.ToInt32(row["DoneCnt"]),
                ["Urgent"] = Convert.ToInt32(row["UrgentCnt"]),
                ["Overdue"] = Convert.ToInt32(row["OverdueCnt"]),
                ["Completed"] = Convert.ToInt32(row["CompletedCnt"]),
                ["TotalDownHours"] = Convert.ToInt32(row["TotalDownHours"]),
            };
        }

        /// <summary>
        /// 各阶段工单数量（看板段数字用）
        /// </summary>
        public Dictionary<string, int> GetBoardCounts()
        {
            var dt = dal.GetStageCounts();
            var result = new Dictionary<string, int>
            {
                { "Pending", 0 }, { "Assigned", 0 }, { "InProgress", 0 }, { "Done", 0 }
            };
            foreach (DataRow row in dt.Rows)
            {
                var stage = row["ProgressStage"].ToString();
                if (result.ContainsKey(stage))
                    result[stage] = Convert.ToInt32(row["Cnt"]);
            }
            return result;
        }

        /// <summary>
        /// 维修员负载列表
        /// </summary>
        public DataTable GetWorkloads()
        {
            return dal.GetRepairerWorkloads();
        }

        /// <summary>
        /// 告警列表
        /// </summary>
        public DataTable GetAlerts()
        {
            return dal.GetAlerts(5);
        }

        /// <summary>
        /// 生成工单号：BX-YYYY-MMDD-NN
        /// </summary>
        public string GenerateRepairNo()
        {
            string prefix = $"BX-{DateTime.Now:yyyy-MMdd}";
            var dt = dal.GetOrderList();
            int seq = 1;
            foreach (DataRow row in dt.Rows)
            {
                var no = row["RepairNo"].ToString();
                if (no.StartsWith(prefix))
                {
                    int.TryParse(no.Substring(no.LastIndexOf('-') + 1), out int n);
                    if (n >= seq) seq = n + 1;
                }
            }
            return $"{prefix}-{seq:D2}";
        }

        /// <summary>
        /// 新增工单
        /// </summary>
        public bool CreateOrder(int equipmentId, int deptId, int reporterId,
                                 string faultType, string faultDesc, string urgency)
        {
            string repairNo = GenerateRepairNo();
            return dal.InsertOrder(repairNo, equipmentId, deptId, reporterId,
                                    faultType, faultDesc, urgency) > 0;
        }

        /// <summary>
        /// 修改工单信息
        /// </summary>
        public bool UpdateOrder(int recordId, int equipmentId, int deptId,
                                 string faultType, string faultDesc, string urgency)
        {
            return dal.UpdateOrder(recordId, equipmentId, deptId,
                                    faultType, faultDesc, urgency) > 0;
        }

        /// <summary>
        /// 设备列表
        /// </summary>
        public DataTable GetEquipmentList()
        {
            return dal.GetEquipmentList();
        }

        /// <summary>
        /// 科室列表
        /// </summary>
        public DataTable GetDeptList()
        {
            return dal.GetDeptList();
        }

        /// <summary>
        /// 工程师列表
        /// </summary>
        public DataTable GetEngineerList()
        {
            return dal.GetEngineerList();
        }

        /// <summary>
        /// 指派维修人
        /// </summary>
        public bool AssignRepairer(int recordId, int engineerId)
        {
            return dal.AssignRepairer(recordId, engineerId) > 0;
        }

        /// <summary>
        /// 开始维修
        /// </summary>
        public bool StartRepair(int recordId)
        {
            return dal.StartRepair(recordId) > 0;
        }

        /// <summary>
        /// 完成维修
        /// </summary>
        public bool CompleteRepair(int recordId)
        {
            return dal.CompleteRepair(recordId) > 0;
        }

        /// <summary>
        /// 删除工单
        /// </summary>
        public bool DeleteOrder(int recordId)
        {
            return dal.DeleteOrder(recordId) > 0;
        }

        /// <summary>
        /// 医生端：删除自己的工单（只能删除 Pending 状态的）
        /// </summary>
        public bool DeleteDoctorOrder(int recordId, int reporterId)
        {
            return dal.DeleteOrderByIdAndReporter(recordId, reporterId) > 0;
        }

        /// <summary>
        /// 单条工单详情
        /// </summary>
        public DataTable GetOrderById(int recordId)
        {
            return dal.GetOrderById(recordId);
        }

        // ==================== 维修员端方法 ====================

        /// <summary>
        /// 维修员工作台：获取指定维修员的工单列表
        /// </summary>
        /// <param name="repairerId">维修员用户ID</param>
        /// <param name="stage">阶段筛选：Assigned/InProgress/Done/null(全部)</param>
        public List<MaintenanceRecordDto> GetRepairerOrders(int repairerId, string stage = null)
        {
            DataTable dt = dal.GetRepairerOrders(repairerId, stage);
            return DataTableToList(dt, isRepairer: true);
        }

        /// <summary>
        /// 维修员工作台：统计各阶段工单数量
        /// </summary>
        public Dictionary<string, int> GetRepairerStageCounts(int repairerId)
        {
            var dt = dal.GetRepairerStageCounts(repairerId);
            var row = dt.Rows[0];
            return new Dictionary<string, int>
            {
                ["PendingAccept"] = Convert.ToInt32(row["PendingAccept"]),
                ["InProgress"] = Convert.ToInt32(row["InProgress"]),
                ["Completed"] = Convert.ToInt32(row["Completed"]),
            };
        }

        /// <summary>
        /// 维修员接单（Assigned → InProgress）
        /// </summary>
        public bool AcceptOrder(int recordId, int repairerId)
        {
            return dal.AcceptOrder(recordId, repairerId) > 0;
        }

        /// <summary>
        /// 维修员提交维修结果（InProgress → Done）
        /// </summary>
        public bool SubmitRepairResult(int recordId, string repairResult, 
                                       decimal? repairCost, int? downtimeHours)
        {
            return dal.SubmitRepairResult(recordId, repairResult, repairCost, downtimeHours) > 0;
        }

        // ==================== 医生端方法 ====================

        /// <summary>
        /// 医生端：获取某医生报修的工单列表
        /// </summary>
        /// <param name="reporterId">报修人 UserId</param>
        /// <param name="stage">阶段筛选：null=全部</param>
        public List<MaintenanceRecordDto> GetDoctorOrders(int reporterId, string stage = null)
        {
            DataTable dt = dal.GetDoctorOrders(reporterId, stage);
            return DataTableToList(dt, isRepairer: false);
        }

        /// <summary>
        /// 医生端：统计某医生各阶段工单数量
        /// </summary>
        public Dictionary<string, int> GetDoctorStageCounts(int reporterId)
        {
            var dt = dal.GetDoctorStageCounts(reporterId);
            var row = dt.Rows[0];
            return new Dictionary<string, int>
            {
                ["Pending"] = Convert.ToInt32(row["Pending"]),
                ["Assigned"] = Convert.ToInt32(row["Assigned"]),
                ["InProgress"] = Convert.ToInt32(row["InProgress"]),
                ["Done"] = Convert.ToInt32(row["Done"]),
                ["Total"] = Convert.ToInt32(row["Total"]),
            };
        }

        /// <summary>
        /// 医生端：按科室获取设备列表
        /// </summary>
        public DataTable GetEquipmentByDept(int deptId)
        {
            return dal.GetEquipmentByDept(deptId);
        }

        /// <summary>
        /// 医生端：搜索全部设备
        /// </summary>
        public DataTable SearchEquipment(string keyword)
        {
            return dal.SearchEquipment(keyword);
        }

        /// <summary>
        /// 医生端：查询某医生借用中的设备（待审批/已审批/超期）
        /// </summary>
        public DataTable GetBorrowedEquipmentByApplicant(int applicantId)
        {
            return dal.GetBorrowedEquipmentByApplicant(applicantId);
        }

        /// <summary>
        /// 故障类型下拉框选项：返回字符串列表，避免 UI 层 DataRowView 问题
        /// </summary>
        public List<string> GetFaultTypeOptionsList()
        {
            DataTable dt = dal.GetDistinctFaultTypes();
            var list = new List<string>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                    list.Add(row["FaultType"].ToString());
            }
            if (list.Count == 0)
            {
                list.Add("硬件故障");
                list.Add("软件故障");
                list.Add("其他");
            }
            return list;
        }

        /// <summary>
        /// 紧急程度下拉框选项：返回 英文值→中文显示 字典
        /// </summary>
        public Dictionary<string, string> GetUrgencyOptionsDict()
        {
            DataTable src = dal.GetDistinctUrgencies();
            var dict = new Dictionary<string, string>();
            if (src != null)
            {
                foreach (DataRow row in src.Rows)
                {
                    string en = row["Urgency"].ToString();
                    dict[en] = MaintenanceHelper.UrgencyToCn(en);
                }
            }
            if (dict.Count == 0)
            {
                dict["Normal"] = "普通";
                dict["Urgent"] = "紧急";
            }
            return dict;
        }

        /// <summary>
        /// 医生端：提交报修（复用已有的 CreateOrder）
        /// </summary>
        public bool SubmitRepair(int equipmentId, int deptId, int reporterId,
                                  string faultType, string faultDesc, string urgency)
        {
            string repairNo = GenerateRepairNo();
            return dal.InsertOrder(repairNo, equipmentId, deptId, reporterId,
                                    faultType, faultDesc, urgency) > 0;
        }
    }
}
