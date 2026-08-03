namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 当前登录用户上下文。
    /// 目前未接入登录功能，固定使用设备科管理员（张伟）作为操作人；
    /// 后续接入登录时，在登录成功后赋值即可。
    /// </summary>
    public static class LoginUser
    {
        public const int UserId = 2;
        public const string RealName = "张伟";
        public const string Role = "admin";
        public const int DeptId = 1;

        public static string DisplayName
        {
            get { return RealName + "（设备科）"; }
        }
    }
}
