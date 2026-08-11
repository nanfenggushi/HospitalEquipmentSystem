using HospitalEquipment.DAL;
using HospitalEquipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalEquipment.BLL
{
    /// <summary>
    /// 科室收入业务逻辑层
    /// </summary>
    public class DeptRevenueManager
    {
        private DeptRevenueDAL dal=new DeptRevenueDAL();

        /// <summary>
        /// 按期间查询科室收入
        /// </summary>
        public async Task<List<DeptRevenue>> GetByPeriodAsync(string period)
        {
            return await dal.GetByPeriodAsync(period).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取全部收入期间
        /// </summary>
        public async Task<List<string>> GetPeriodsAsync()
        {
            return await dal.GetPeriodsAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 新增或更新科室收入,成功返回 null,失败返回错误信息
        /// </summary>
        public async Task<string> AddOrUpdateAsync(DeptRevenue model)
        {
            string error= Validate(model);
            if (error != null) return error;

            bool exists=await dal.ExistsAsync(model.Period,model.DeptId,model.RevenueId).ConfigureAwait(false);
            if (exists) return "该科室在此期间已录入收入，请勿重复添加";

            int rows = model.RevenueId > 0
                ? await dal.UpdateAsync(model).ConfigureAwait(false)
                : await dal.InsertAsync(model).ConfigureAwait(false);
            return rows > 0 ? null : "保存失败，请重试";
        }

        /// <summary>
        /// 校验:期间格式/科室/金额/备注长度
        /// </summary>
        private string Validate(DeptRevenue model)
        {
            if (model.DeptId <= 0) return "请选择科室";
            if (string.IsNullOrWhiteSpace(model.Period) || !Regex.IsMatch(model.Period.Trim(), @"^\d{4}-\d{2}$"))
                return "期间格式不正确，应为 yyyy-MM";
            if (model.Amount < 0) return "金额不能为负数";
            if (!string.IsNullOrEmpty(model.Remark) && model.Remark.Length > 200) return "备注不能超过200字";
            return null;
        }

        /// <summary>
        /// 删除科室收入,成功返回 null,失败返回错误信息
        /// </summary>
        public async Task<string> DeleteAsync(int revenueId)
        {
            int rows = await dal.DeleteAsync(revenueId).ConfigureAwait(false);
            return rows > 0 ? null : "删除失败，记录不存在";
        }
    }
}
