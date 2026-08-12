namespace HospitalEquipment.BLL
{
    /// <summary>
    /// AI 故障识别结果
    /// </summary>
    public class FaultRecognitionResult
    {
        /// <summary>
        /// 识别出的故障类型
        /// </summary>
        public string FaultType { get; set; }

        /// <summary>
        /// 置信度，范围 0.00 ~ 1.00
        /// </summary>
        public decimal Confidence { get; set; }
    }
}
