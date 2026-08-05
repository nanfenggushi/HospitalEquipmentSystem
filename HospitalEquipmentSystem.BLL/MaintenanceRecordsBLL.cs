using HospitalEquipment.DAL;
using HospitalEquipment.Model.Dashboard;
using System;
using System.Collections.Generic;

namespace HospitalEquipment.BLL
{
    public class MaintenanceRecordsBLL
    {
        private readonly MaintenanceRecordDAL _maintenanceRecordDAL = new MaintenanceRecordDAL();


        public List<MonthlyMaintenanceDto> GetRecent6MonthsTrend()
        {
            DateTime baseDate = DateTime.Now;
            // 生成查询的开始时间
            DateTime startDate = new DateTime(baseDate.AddMonths(-5).Year, baseDate.AddMonths(-5).Month, 1);

            // 1. 一次性从数据库查出有数据的实体列表
            List<MonthlyMaintenanceDto> dbList = _maintenanceRecordDAL.GetMaintenanceCounts(startDate);

            // 2. 补全连续 6 个月的列表 (如果某月数据库没数据，自动补 0)
            List<MonthlyMaintenanceDto> fullList = new List<MonthlyMaintenanceDto>();
            for (int i = 5; i >= 0; i--)
            {
                DateTime dt = baseDate.AddMonths(-i);
                var match = dbList.Find(x => x.Year == dt.Year && x.Month == dt.Month);

                // 如果数据库有就用数据库的，没有就补一个 FaultCount=0, RepairedCount=0 的实体
                fullList.Add(match ?? new MonthlyMaintenanceDto { Year = dt.Year, Month = dt.Month });
            }

            return fullList;
        }
    }
}
