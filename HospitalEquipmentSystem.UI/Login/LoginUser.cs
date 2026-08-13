using HospitalEquipment.Model;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 当前登录用户上下文
    /// </summary>
    public static class LoginUser
    {
        public static int UserId { get; private set; }
        public static string Username { get; private set; }
        public static string RealName { get; private set; }
        public static string Role { get; private set; }
        public static int DeptId { get; private set; }
        public static string DeptName { get; private set; }

        /// <summary>主窗体显示：当前用户：张伟（设备科·管理员）</summary>
        public static string DisplayName
        {
            get { return RealName + "（" + DeptName + "·" + UserRoleText.ToCn(Role) + "）"; }
        }

        /// <summary>登录成功后写入登录信息</summary>
        public static void SetUser(UserDto user)
        {
            UserId = user.UserId;
            Username = user.Username;
            RealName = user.RealName;
            Role = user.Role;
            DeptId = user.DeptId;
            DeptName = user.DeptName;
        }

        /// <summary>注销/退出登录时清空</summary>
        public static void Reset()
        {
            UserId = 0; Username = null; RealName = null;
            Role = null; DeptId = 0; DeptName = null;
        }
    }
}
