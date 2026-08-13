using HospitalEquipment.DAL;
using HospitalEquipment.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace HospitalEquipment.BLL
{
    /// <summary>
    /// 登录业务逻辑
    /// </summary>
    public class LoginBLL
    {
        private readonly LoginDAL dal = new LoginDAL();

        /// <summary>登录下拉用：在用人员列表</summary>
        public List<UserDto> GetLoginUsers()
        {
            var list = new List<UserDto>();
            foreach (DataRow row in dal.GetActiveUsers().Rows)
            {
                list.Add(ToDto(row));
            }
            return list;
        }

        /// <summary>人脸识别登录：按用户 ID 完成启用状态校验并回写最后登录时间</summary>
        public BLLResult LoginByFace(int userId, out UserDto user)
        {
            user = null;
            foreach (UserDto item in GetLoginUsers())
            {
                if (item.UserId == userId)
                {
                    user = item;
                    dal.UpdateLastLogin(userId);
                    return BLLResult.Ok();
                }
            }

            return BLLResult.Fail("该人员不存在或已被停用");
        }

        /// <summary>校验登录，成功返回用户信息</summary>
        public BLLResult login(int userId, string password, out UserDto user)
        {
            user = null;
            if (string.IsNullOrEmpty(password))
                return BLLResult.Fail("请输入密码");

            DataTable dt = dal.GetByIdAndPwd(userId, password);
            if (dt.Rows.Count == 0)
                return BLLResult.Fail("账号或密码错误");

            user = ToDto(dt.Rows[0]);
            dal.UpdateLastLogin(user.UserId);
            return BLLResult.Ok();
        }

        private static UserDto ToDto(DataRow row)
        {
            return new UserDto {
                UserId = Convert.ToInt32(row["UserId"]),
                Username = row["Username"].ToString(),
                RealName = row["RealName"].ToString(),
                Role = row["Role"].ToString(),
                DeptId = row["DeptId"] == DBNull.Value ? 0 : Convert.ToInt32(row["DeptId"]),
                DeptName = row["DeptName"].ToString(),
                AvatarUrl = row["AvatarUrl"].ToString()
            };
        }
    }
}
