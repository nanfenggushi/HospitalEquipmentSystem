namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 维修员登录上下文。
    /// 目前未接入登录功能，固定使用维修员（李工）作为操作人；
    /// 测试时修改 UserId 即可切换不同维修员（如 5=李工，6=王工）。
    /// 后续接入登录时，在登录成功后赋值即可。
    /// </summary>
    public static class LoginRepairer
    {
        public const int UserId = 5;
        public const string RealName = "李工";
        public const string Role = "repair";

        public static string DisplayName
        {
            get { return RealName + "（维修员）"; }
        }
    }
}
