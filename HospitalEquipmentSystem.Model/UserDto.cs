namespace HospitalEquipment.Model
{
    /// <summary>
    /// 登录用户信息
    /// </summary>
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string RealName { get; set; }
        public string Role { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public string AvatarUrl { get; set; }

        /// <summary>下拉显示：姓名（科室·职务）</summary>
        public string DisplayName { get { return RealName + "（" + DeptName + "·" + UserRoleText.ToCn(Role) + "）"; } }
    }

    /// <summary>
    /// 职务常量与中文映射
    /// </summary>
    public static class UserRoleText
    {
        public const string Admin = "admin";
        public const string Doctor = "doctor";
        public const string Repair = "repair";

        public static string ToCn(string role)
        {
            switch (role)
            {
                case Admin: return "管理员";
                case Doctor: return "医生";
                case Repair: return "工程师";
                default: return role ?? "";
            }
        }
    }
}
