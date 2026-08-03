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

        // DataTable → List<Dto> 转换
        private List<MaintenanceRecordDto> DataTableToList(DataTable dt)
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
                    ProgressStageText = MaintenanceHelper.StageToCn(row["ProgressStage"].ToString()),
                    DeptName = row["DeptName"].ToString(),
                    RepairerName = row["RepairerName"].ToString(),
                    ReportTime = Convert.ToDateTime(row["ReportTime"]),
                    DowntimeHours = row["DowntimeHours"] as decimal?,
                    StatusText = MaintenanceHelper.StatusToCn(row["Status"].ToString()),
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
        /// 单条工单详情
        /// </summary>
        public DataTable GetOrderById(int recordId)
        {
            return dal.GetOrderById(recordId);
        }
    }
}
