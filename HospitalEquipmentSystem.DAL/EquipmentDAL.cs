using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HospitalEquipment.Model;
using HospitalEquipmentSystem.Common;

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
                         SUM(CASE WHEN Status = 'Scrapped' THEN 1 ELSE 0 END) AS ScrappedCount
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
    }
}
