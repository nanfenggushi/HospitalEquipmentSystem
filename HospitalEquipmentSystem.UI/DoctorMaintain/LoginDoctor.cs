namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 医生登录上下文。
    /// 目前未接入登录功能，固定使用护士（李护士）作为操作人；
    /// 测试时修改 UserId 即可切换不同医生。
    /// 后续接入登录时，在登录成功后赋值即可。
    /// </summary>
    public static class LoginDoctor
    {
        public const int UserId = 3;
        public const int DeptId = 2;
        public const string RealName = "李护士";
        public const string Role = "doctor";

        public static string DisplayName
        {
            get { return RealName + "（医护）"; }
        }
    }
}
