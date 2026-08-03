using HospitalEquipment.Model;
using HospitalEquipmentSystem.Common;

namespace HospitalEquipment.DAL
{
    public class EquipmentDAL
    {
        /// <summary>
        /// 执行 SQL 聚合查询，按设备状态（使用中/维修/借用/报废）统计数量
        /// </summary>
        /// <returns>返回首行映射的卡片统计 DTO</returns>
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
    }
}
