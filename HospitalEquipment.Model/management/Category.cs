using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalEquipment.Model.management
{
    /// <summary>
    /// 设备分类实体
    /// </summary>
    public class Category
    {
        /// <summary>分类ID</summary>
        public int CategoryId { get; set; }

        /// <summary>分类名称</summary>
        public string Name { get; set; }

        /// <summary>上级分类ID(0表示根节点)</summary>
        public int? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int SortOrder { get; set; }

        /// <summary>分类编码</summary>
        public string Code { get; set; }

        /// <summary>备注说明</summary>
        public string Description { get; set; }
    }
}
