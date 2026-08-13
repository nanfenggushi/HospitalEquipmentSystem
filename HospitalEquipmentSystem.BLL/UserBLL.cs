using HospitalEquipment.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalEquipment.BLL
{
    /// <summary>
    /// 用户管理业务逻辑层
    /// </summary>
    public class UserBLL
    {
        private readonly UserDAL dal = new UserDAL();

        /// <summary>
        /// 分页查询用户列表
        /// </summary>
        /// <param name="pageIndex">页码（从1开始）</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="keyword">搜索关键字（可选）</param>
        /// <returns>用户数据表 + 总记录数</returns>
        public (DataTable users, int total) GetPagedUsers(int pageIndex, int pageSize, string keyword = "")
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 5;
            return dal.GetPagedUsers(pageIndex, pageSize, keyword);
        }
    }
}
