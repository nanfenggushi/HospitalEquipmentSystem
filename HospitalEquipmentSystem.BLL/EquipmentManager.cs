using HospitalEquipment.DAL;
using HospitalEquipment.DAL.management;
using HospitalEquipment.Model.management;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalEquipment.BLL.management
{
    public class EquipmentManager
    {
        private EquipmentDAL dal = new EquipmentDAL();

        /// <summary>
        /// 分页查询设备列表
        /// </summary>
        public async Task<(List<Equipment> list, int total)> GetPaged(
            int pageIndex,
            int pageSize,
            string keyword = "",
            string status = "",
            int? deptId = null)
        {
            if (pageIndex < 1) pageIndex = 1;
            return await dal.GetPaged(pageIndex, pageSize, keyword, status, deptId).ConfigureAwait(false);
        }

        /// <summary>
        /// 根据ID获取设备详情
        /// </summary>
        public Equipment GetById(int id) => EquipmentDAL.GetById(id);

        /// <summary>
        /// 新增设备
        /// </summary>
        public bool Insert(Equipment eq)
        {
            Validate(eq);
            return dal.Insert(eq) > 0;
        }

        /// <summary>
        /// 更新设备
        /// </summary>
        public bool Update(Equipment eq)
        {
            Validate(eq);
            return dal.Update(eq) > 0;
        }

        /// <summary>
        /// 删除设备（软删除）
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id) > 0;
        }

        /// <summary>
        /// 获取所有启用科室
        /// </summary>
        public async Task<List<KeyValuePair<int, string>>> GetDepartments()
        {
            return await dal.GetDepartments().ConfigureAwait(false);
        }

        /// <summary>
        /// 获取状态列表（包含"全部"）
        /// </summary>
        public List<string> GetStatusList()
        {
            return dal.GetStatusList();
        }

        /// <summary>
        /// 验证设备数据合法性
        /// </summary>
        private void Validate(Equipment eq)
        {
            if (string.IsNullOrWhiteSpace(eq.EquipmentName))
                throw new Exception("设备名称不能为空");
            if (string.IsNullOrWhiteSpace(eq.EquipmentNo))
                throw new Exception("设备编号不能为空");
            // 可添加其他业务规则
        }
        /// <summary>
        /// 获取状态对应的中文显示
        /// </summary>
        public string GetStatusChinese(string status)
        {
            return EquipmentDAL.GetStatusChinese(status);
        }
    }
}
