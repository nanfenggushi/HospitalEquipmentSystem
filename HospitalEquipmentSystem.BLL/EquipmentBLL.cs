using HospitalEquipment.DAL;
using HospitalEquipment.Model.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalEquipment.BLL
{
    public class EquipmentBLL
    {
        private readonly EquipmentDAL _equipmentDAL = new EquipmentDAL();

        /// <summary>
        /// 获取各个设备类型的使用率
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryUsageDto>> GetCategoryUsageDataAsync()
        {
            return await _equipmentDAL.GetCategoryUsageDataAsync();
        }

        /// <summary>
        /// 获取首页仪表盘的设备统计卡片汇总数据
        /// </summary>
        /// <returns></returns>
        public async Task<StatisticCardDto> GetStatisticCardDataAsync()
        {
            return await _equipmentDAL.GetStatisticCardData();
        }

        /// <summary>
        /// 获取月度维保趋势数据
        /// </summary>
        public List<MaintenanceTrendDto> GetMaintenanceTrend()
        {
            return _equipmentDAL.GetMaintenanceTrend();
        }
    }
}
