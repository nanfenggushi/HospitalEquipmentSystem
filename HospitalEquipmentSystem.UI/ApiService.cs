using HospitalEquipment.BLL;
using HospitalEquipment.Model.Dashboard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace HospitalEquipmentSystem.UI
{
    internal static class ApiService
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        private static readonly string Prefix = "http://localhost:8123/";
        private static readonly HealthAnalysisManager HealthBll = new HealthAnalysisManager();

        public static void Start()
        {
            try
            {
                var listener = new HttpListener();
                listener.Prefixes.Add(Prefix);
                listener.Start();

                var thread = new Thread(() =>
                {
                    while (listener.IsListening)
                    {
                        try
                        {
                            var ctx = listener.GetContext();
                            ThreadPool.QueueUserWorkItem(_ => Handle(ctx));
                        }
                        catch
                        {
                        }
                    }
                });
                thread.IsBackground = true;
                thread.Start();

                Log("服务已启动 " + Prefix);
            }
            catch (Exception ex)
            {
                Log("[启动失败] " + ex.Message + "（可能端口被占用，请先关闭旧服务进程）");
            }
        }

        private static void Handle(HttpListenerContext ctx)
        {
            try
            {
                var path = ctx.Request.Url.AbsolutePath.TrimEnd('/');
                var period = ctx.Request.QueryString["period"] ?? "";

                // ===== 设备健康分析类接口（走 BLL 计算，非纯 SQL） =====
                var bllData = ResolveBll(path, ctx.Request.QueryString["equipmentId"] ?? "");
                if (bllData != null)
                {
                    WriteJson(ctx, bllData, HttpStatusCode.OK);
                    return;
                }

                var sql = ResolveSql(path, period);
                if (sql == null)
                {
                    WriteJson(ctx, "{\"message\":\"not found\"}", HttpStatusCode.NotFound);
                    return;
                }

                var data = LoadTable(sql, new[] { new SqlParameter("@Period", (object)period ?? "") });
                if (path == "/api/MaintenanceDisplay") AddMaintenanceDisplay(data);
                else if (path == "/api/Equipment") AddEquipmentStatusText(data);
                else if (path == "/api/BorrowRecords") AddBorrowStatusText(data);

                WriteJson(ctx, data, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log("[请求异常] " + ex.Message);
                WriteJson(ctx, "{\"error\":\"" + Escape(ex.Message) + "\"}", HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 设备健康分析类接口：通过 BLL 计算后转换为 JSON 字典列表
        /// 返回 null 表示路径不属于本类接口，交由 ResolveSql 处理
        /// </summary>
        private static List<Dictionary<string, object>> ResolveBll(string path, string equipmentId)
        {
            switch (path)
            {
                case "/api/stats/deviceHealth":
                    return ToRowList(HealthBll.GetHealthScores());

                case "/api/stats/predictiveMaintenance":
                    return ToRowList(HealthBll.GetPredictiveMaintenance());

                case "/api/stats/lifecycle":
                    int eid;
                    if (!int.TryParse(equipmentId, out eid) || eid <= 0)
                        return null;   // 缺少设备ID参数，交给 404 处理
                    return ToRowList(HealthBll.GetLifecycleEvents(eid));

                case "/api/stats/lifecycleAll":
                    return ToRowList(HealthBll.GetAllLifecycleEvents());

                default:
                    return null;
            }
        }

        /// <summary>
        /// 将 BLL 返回的实体列表转换为 JSON 字典列表（与 LoadTable 输出格式一致）
        /// </summary>
        private static List<Dictionary<string, object>> ToRowList<T>(List<T> list)
        {
            var rows = new List<Dictionary<string, object>>();
            if (list == null) return rows;

            foreach (var item in list)
            {
                var row = new Dictionary<string, object>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (!prop.CanRead) continue;
                    var value = prop.GetValue(item);
                    if (value == null)
                    {
                        row[prop.Name] = null;
                    }
                    else if (value is DateTime)
                    {
                        row[prop.Name] = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else if (value is DateTime?)
                    {
                        row[prop.Name] = ((DateTime?)value).HasValue
                            ? ((DateTime?)value).Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : (object)null;
                    }
                    else
                    {
                        row[prop.Name] = value;
                    }
                }
                rows.Add(row);
            }
            return rows;
        }

        private static string ResolveSql(string path, string period)
        {
            switch (path)
            {
                case "/api/MaintenanceDisplay":
                    return @"
SELECT m.RepairNo, e.EquipmentName, m.FaultType, m.FaultDesc,
       ISNULL(m.RepairResult, '') AS RepairResult, m.Status, m.ReportTime
FROM MaintenanceRecords m
LEFT JOIN Equipment e ON m.EquipmentId = e.EquipmentId
WHERE m.RepairNo IS NOT NULL
ORDER BY m.ReportTime DESC";

                case "/api/Equipment":
                    return @"
SELECT e.*, d.DeptName, c.CategoryName, s.SupplierName, u.RealName AS ResponsibleUserRealName
FROM Equipment e
LEFT JOIN Departments d ON e.DeptId = d.DeptId
LEFT JOIN EquipmentCategories c ON e.CategoryId = c.CategoryId
LEFT JOIN Suppliers s ON e.SupplierId = s.SupplierId
LEFT JOIN Users u ON e.ResponsibleUserId = u.UserId
ORDER BY e.EquipmentId DESC";

                case "/api/BorrowRecords":
                    return @"
SELECT b.*, e.EquipmentNo, e.EquipmentName, e.Status AS EquipmentStatus,
       ap.RealName AS ApplicantName, d.DeptName AS ApplicantDeptName, ap2.RealName AS ApproverName
FROM BorrowRecords b
LEFT JOIN Equipment e ON b.EquipmentId = e.EquipmentId
LEFT JOIN Users ap ON b.ApplicantId = ap.UserId
LEFT JOIN Departments d ON b.ApplicantDeptId = d.DeptId
LEFT JOIN Users ap2 ON b.ApproverId = ap2.UserId
ORDER BY b.BorrowId DESC";

                case "/api/InboundRecords":
                    return @"
SELECT i.*, e.EquipmentName
FROM InboundRecords i
LEFT JOIN Equipment e ON i.EquipmentId = e.EquipmentId
ORDER BY i.InboundId DESC";

                case "/api/stats/equipmentByDept":
                    return @"
SELECT d.DeptName AS Name, COUNT(e.EquipmentId) AS Value
FROM Equipment e
LEFT JOIN Departments d ON e.DeptId = d.DeptId
GROUP BY d.DeptName
ORDER BY Value DESC";

                case "/api/stats/equipmentByCategory":
                    return @"
SELECT c.CategoryName AS Name, COUNT(e.EquipmentId) AS Value
FROM Equipment e
LEFT JOIN EquipmentCategories c ON e.CategoryId = c.CategoryId
GROUP BY c.CategoryName
ORDER BY Value DESC";

                case "/api/stats/maintenanceByStatus":
                    return @"
SELECT CASE Status
    WHEN 'Pending' THEN N'待处理' WHEN 'Assigned' THEN N'已派单'
    WHEN 'InProgress' THEN N'处理中' WHEN 'Completed' THEN N'已完成'
    WHEN 'Done' THEN N'已完成' WHEN 'Cancelled' THEN N'已取消'
    ELSE Status END AS Name,
    COUNT(*) AS Value
FROM MaintenanceRecords
GROUP BY Status
ORDER BY Value DESC";

                case "/api/stats/borrowByStatus":
                    return @"
SELECT CASE Status
    WHEN 'Pending' THEN N'待审批' WHEN 'Approved' THEN N'借用中'
    WHEN 'Rejected' THEN N'已驳回' WHEN 'Returned' THEN N'已归还'
    WHEN 'Overdue' THEN N'已超期' ELSE Status END AS Name,
    COUNT(*) AS Value
FROM BorrowRecords
GROUP BY Status
ORDER BY Value DESC";

                case "/api/stats/revenueByDept":
                    return @"
SELECT d.DeptName AS Name, SUM(r.Amount) AS Value
FROM DeptRevenue r
LEFT JOIN Departments d ON r.DeptId = d.DeptId
WHERE r.Period = ISNULL(NULLIF(@Period, ''), (SELECT MAX(Period) FROM DeptRevenue))
GROUP BY d.DeptName
ORDER BY Value DESC";

                case "/api/stats/equipmentShortage":
                    return @"
SELECT c.CategoryName AS Name,
       ISNULL(SUM(CASE WHEN e.Status = 'Idle' THEN 1 ELSE 0 END), 0) AS Value,
       c.MinAvailableCount AS MinAvailable
FROM EquipmentCategories c
LEFT JOIN Equipment e ON e.CategoryId = c.CategoryId AND e.IsActive = 1
WHERE c.MinAvailableCount > 0
GROUP BY c.CategoryName, c.MinAvailableCount
HAVING ISNULL(SUM(CASE WHEN e.Status = 'Idle' THEN 1 ELSE 0 END), 0) < c.MinAvailableCount
ORDER BY Value ASC";

                case "/api/stats/borrowTotal":
                    return "SELECT COUNT(*) AS Value FROM BorrowRecords";

                case "/api/stats/maintenanceTotal":
                    return "SELECT COUNT(*) AS Value FROM MaintenanceRecords";

                case "/api/stats/borrowingDevices":
                    return "SELECT COUNT(*) AS Value FROM Equipment WHERE Status = 'Borrowed' AND IsActive = 1";

                default:
                    return null;
            }
        }

        private static List<Dictionary<string, object>> LoadTable(string sql)
        {
            return LoadTable(sql, null);
        }

        private static List<Dictionary<string, object>> LoadTable(string sql, SqlParameter[] parameters)
        {
            var rows = new List<Dictionary<string, object>>();
            using (var cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                using (var cmd = new SqlCommand(sql, cn))
                {
                    if (parameters != null)
                    {
                        foreach (var p in parameters)
                        {
                            if (p != null)
                            {
                                if (p.Value == null) p.Value = DBNull.Value;
                                cmd.Parameters.Add(p);
                            }
                        }
                    }
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (var i = 0; i < r.FieldCount; i++)
                            {
                                var v = r.GetValue(i);
                                if (v == DBNull.Value) v = null;
                                else if (v is DateTime) v = ((DateTime)v).ToString("yyyy-MM-dd HH:mm:ss");
                                row[r.GetName(i)] = v;
                            }
                            rows.Add(row);
                        }
                    }
                }
            }
            return rows;
        }

        private static void AddMaintenanceDisplay(List<Dictionary<string, object>> rows)
        {
            foreach (var row in rows)
            {
                var statusCn = GetMaintenanceStatusCn(Str(row, "Status"));
                var resultTxt = Str(row, "RepairResult");
                if (resultTxt.Length == 0) resultTxt = "-";
                row["DisplayText"] = "【" + Str(row, "RepairNo") + "】" + Str(row, "EquipmentName")
                    + " ｜ " + Str(row, "FaultDesc") + " ｜ 结果：" + resultTxt + " ｜ 状态：" + statusCn;
                row["StatusText"] = statusCn;
            }
        }

        private static void AddEquipmentStatusText(List<Dictionary<string, object>> rows)
        {
            foreach (var row in rows)
            {
                string text;
                switch (Str(row, "Status"))
                {
                    case "Idle": text = "空闲"; break;
                    case "InUse": text = "使用中"; break;
                    case "Maintenance": text = "维修中"; break;
                    case "Borrowed": text = "已借出"; break;
                    case "Scrapped": text = "已报废"; break;
                    default: text = Str(row, "Status"); break;
                }
                row["StatusText"] = text;
            }
        }

        private static void AddBorrowStatusText(List<Dictionary<string, object>> rows)
        {
            foreach (var row in rows)
            {
                string text;
                switch (Str(row, "Status"))
                {
                    case "Pending": text = "待审批"; break;
                    case "Approved": text = "借用中"; break;
                    case "Rejected": text = "已驳回"; break;
                    case "Returned": text = "已归还"; break;
                    case "Overdue": text = "已超期"; break;
                    default: text = Str(row, "Status"); break;
                }
                row["StatusText"] = text;
            }
        }

        private static string GetMaintenanceStatusCn(string status)
        {
            switch (status)
            {
                case "Pending": return "待处理";
                case "Assigned": return "已派单";
                case "InProgress": return "处理中";
                case "Completed": return "已完成";
                case "Done": return "已完成";
                case "Cancelled": return "已取消";
                default: return status;
            }
        }

        private static string Str(Dictionary<string, object> row, string key)
        {
            object v;
            if (row.TryGetValue(key, out v) && v != null) return Convert.ToString(v);
            return string.Empty;
        }

        private static string Escape(string s)
        {
            if (s == null) return string.Empty;
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        private static void WriteJson(HttpListenerContext ctx, object payload, HttpStatusCode status)
        {
            var json = new JavaScriptSerializer().Serialize(payload);
            var bytes = Encoding.UTF8.GetBytes(json);
            var resp = ctx.Response;
            resp.StatusCode = (int)status;
            resp.ContentType = "application/json; charset=utf-8";
            resp.ContentLength64 = bytes.Length;
            resp.OutputStream.Write(bytes, 0, bytes.Length);
            resp.OutputStream.Close();
        }

        private static void Log(string message)
        {
            try
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "api.log");
                File.AppendAllText(file, string.Format("[{0:yyyy-MM-dd HH:mm:ss}] {1}{2}",
                    DateTime.Now, message, Environment.NewLine));
            }
            catch
            {
            }
        }
    }
}
