using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 科室实体：医院科室/部门信息
    /// </summary>
    public class Department
    {
        /// <summary>科室主键 ID</summary>
        public int DeptId { get; set; }

        /// <summary>科室名称，如“设备科”</summary>
        public string DeptName { get; set; }

        /// <summary>科室编码，用于系统内部标识</summary>
        public string DeptCode { get; set; }

        /// <summary>科室所在位置</summary>
        public string Location { get; set; }

        /// <summary>科室联系电话</summary>
        public string Phone { get; set; }

        /// <summary>是否启用；停用科室不参与业务</summary>
        public bool IsActive { get; set; }

        /// <summary>科室记录创建时间</summary>
        public DateTime CreatedAt { get; set; }
    }
}
