using HospitalEquipment.Model;
using HospitalEquipment.Model.Dashboard;
using HospitalEquipmentSystem.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HospitalEquipment.DAL
{
    public class EquipmentDAL
    {
        /// <summary>
        /// 获取首页仪表盘的设备统计卡片汇总数据
        /// </summary>
        public StatisticCardDto GetStatisticCardData()
        {
            string sql = @"SELECT 
                         COUNT(*) AS TotalCount,
                         SUM(CASE WHEN Status IN ('Idle', 'InUse') THEN 1 ELSE 0 END) AS NormalCount,
                         SUM(CASE WHEN Status = 'Maintenance' THEN 1 ELSE 0 END) AS MaintenanceCount,
                         SUM(CASE WHEN Status = 'Borrowed' THEN 1 ELSE 0 END) AS BorrowedCount,
                         SUM(CASE WHEN Status = 'Scrapped' THEN 1 ELSE 0 END) AS ScrappedCount,
                         SUM(CASE WHEN Status = 'Idle' THEN 1 ELSE 0 END) AS IdleCount,
                         SUM(CASE WHEN Status = 'InUse' THEN 1 ELSE 0 END) AS InUseCount
                         FROM Equipment
                         WHERE IsActive = 1;";
            return DataReaderMapper.MapToList<StatisticCardDto>(DbHelper.ExecuteReader(sql))[0];
        }

        /// <summary>可借用的设备（空闲且无进行中的借用记录）</summary>
        public static List<Equipment> GetAvailable()
        {
            DataTable dt = DbHelper.GetDataTable(@"SELECT e.* FROM Equipment e
                WHERE e.IsActive = 1 AND e.Status = 'Idle'
                AND NOT EXISTS (SELECT 1 FROM BorrowRecords b
                    WHERE b.EquipmentId = e.EquipmentId AND b.Status IN ('Pending', 'Approved'))
                ORDER BY e.EquipmentName");
            return MapTableToList(dt);
        }

        /// <summary>全部在用设备（筛选下拉用）</summary>
        public static List<Equipment> GetAll()
        {
            DataTable dt = DbHelper.GetDataTable(@"SELECT * FROM Equipment
                WHERE IsActive = 1 ORDER BY EquipmentName");
            return MapTableToList(dt);
        }

        /// <summary>按 Id 查询设备</summary>
        public static Equipment GetById(int equipmentId)
        {
            DataTable dt = DbHelper.GetDataTable("SELECT * FROM Equipment WHERE EquipmentId = @id",
                new SqlParameter("@id", equipmentId));
            if (dt.Rows.Count == 0) return null;
            return MapTableToList(dt)[0];
        }

        /// <summary>更新设备状态</summary>
        public static int SetStatus(int equipmentId, string status)
        {
            return DbHelper.ExecuteNonQuery(@"UPDATE Equipment SET Status = @status, UpdatedAt = GETDATE()
                WHERE EquipmentId = @id",
                new SqlParameter("@status", status),
                new SqlParameter("@id", equipmentId));
        }

        /// <summary>获取激活状态的设备总数（IsActive = 1）</summary>
        public int GetActiveCount()
        {
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Equipment WHERE IsActive = @Active",
                new SqlParameter("@Active", 1));
            return result != null ? (int)result : 0;
        }

        /// <summary>获取使用中的设备数（Status = 'InUse' 且激活）</summary>
        public int GetInUseCount()
        {
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Equipment WHERE Status = @Status AND IsActive = @Active",
                new SqlParameter("@Status", "InUse"),
                new SqlParameter("@Active", 1));
            return result != null ? (int)result : 0;
        }

        /// <summary>获取故障/维修中的设备数（Status = 'Maintenance' 且激活）</summary>
        public int GetMaintenanceCount()
        {
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Equipment WHERE Status = @Status AND IsActive = @Active",
                new SqlParameter("@Status", "Maintenance"),
                new SqlParameter("@Active", 1));
            return result != null ? (int)result : 0;
        }

        /// <summary>获取借用中的设备数（Status = 'Borrowed' 且激活）</summary>
        public int GetBorrowedCount()
        {
            object result = DbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Equipment WHERE Status = @Status AND IsActive = @Active",
                new SqlParameter("@Status", "Borrowed"),
                new SqlParameter("@Active", 1));
            return result != null ? (int)result : 0;
        }

        /// <summary>获取所有激活的设备列表（按 ID 排序）</summary>
        public List<Equipment> GetAllActive()
        {
            string sql = @"SELECT * FROM Equipment WHERE IsActive = @Active ORDER BY EquipmentId";
            using (var reader = DbHelper.ExecuteReader(sql,
                new SqlParameter("@Active", 1)))
            {
                return reader != null ? DataReaderMapper.MapToList<Equipment>(reader) : new List<Equipment>();
            }
        }

        private static List<Equipment> MapTableToList(DataTable dt)
        {
            var list = new List<Equipment>();
            foreach (DataRow row in dt.Rows)
            {
                var e = new Equipment
                {
                    EquipmentId = Convert.ToInt32(row["EquipmentId"]),
                    EquipmentNo = Convert.ToString(row["EquipmentNo"]),
                    EquipmentName = Convert.ToString(row["EquipmentName"]),
                    Model = row["Model"] == DBNull.Value ? null : Convert.ToString(row["Model"]),
                    Manufacturer = row["Manufacturer"] == DBNull.Value ? null : Convert.ToString(row["Manufacturer"]),
                    SupplierId = row["SupplierId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["SupplierId"]),
                    CategoryId = row["CategoryId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["CategoryId"]),
                    DeptId = row["DeptId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["DeptId"]),
                    Location = row["Location"] == DBNull.Value ? null : Convert.ToString(row["Location"]),
                    ResponsibleUserId = row["ResponsibleUserId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["ResponsibleUserId"]),
                    Price = row["Price"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(row["Price"]),
                    PurchaseDate = row["PurchaseDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["PurchaseDate"]),
                    WarrantyMonths = row["WarrantyMonths"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["WarrantyMonths"]),
                    ServiceLife = row["ServiceLife"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["ServiceLife"]),
                    LastMaintainDate = row["LastMaintainDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["LastMaintainDate"]),
                    NextMaintainDate = row["NextMaintainDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NextMaintainDate"]),
                    Status = Convert.ToString(row["Status"]),
                    Remarks = row["Remarks"] == DBNull.Value ? null : Convert.ToString(row["Remarks"]),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    UpdatedAt = row["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["UpdatedAt"])
                };
                list.Add(e);
            }
            return list;
        }


        public List<CategoryUsageDto> GetCategoryUsageData()
        {
            string sql = @"SELECT 
                         c.CategoryName,
                         CAST(
                            CASE 
                            WHEN COUNT(e.EquipmentId) = 0 THEN 0.0
       ELSE SUM(CASE WHEN e.Status IN ('InUse', 'Borrowed') THEN 1.0 ELSE 0 END) * 100.0 / COUNT(e.EquipmentId)
                         END AS DECIMAL(5, 1)
                         ) AS UsageRate
                         FROM EquipmentCategories c
             LEFT JOIN Equipment e ON c.CategoryId = e.CategoryId AND e.IsActive = 1 AND e.Status != 'Scrapped'
                        GROUP BY c.CategoryId, c.CategoryName;
                         ";

            return DataReaderMapper.MapToList<CategoryUsageDto>(DbHelper.ExecuteReader(sql));
        }
    }
}
