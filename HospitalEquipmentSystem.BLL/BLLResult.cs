namespace HospitalEquipment.BLL
{
    /// <summary>
    /// 业务操作结果
    /// </summary>
    public class BLLResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public BLLResult() { }

        public BLLResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static BLLResult Ok(string message = null)
        {
            return new BLLResult(true, message ?? "操作成功");
        }

        public static BLLResult Fail(string message)
        {
            return new BLLResult(false, message ?? "操作失败");
        }
    }
}
