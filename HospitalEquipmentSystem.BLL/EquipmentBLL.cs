using HospitalEquipment.DAL;
using HospitalEquipment.Model;

namespace HospitalEquipment.BLL
{
    public class EquipmentBLL
    {
        private readonly EquipmentDAL _equipmentDAL = new EquipmentDAL();

        /// <summary>
        /// 获取首页仪表盘的设备统计卡片汇总数据
        /// </summary>
        /// <returns></returns>
        public StatisticCardDto GetStatisticCardData()
        {
            return _equipmentDAL.GetStatisticCardData();
        }
    }
}
