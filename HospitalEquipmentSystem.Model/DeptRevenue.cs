using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalEquipment.Model
{
    public class DeptRevenue
    {
        /// <summary>
        /// 科室收入实体：某科室某期间的收入金额
        /// </summary>
        
        public int RevenueId { get; set; }
        public int DeptId {  get; set; }
        public string DeptName { get; set; }
        public string Period { get; set; }
        public decimal Amount {  get; set; }
        public string Remark {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
