using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalEquipment.BLL
{
    /// <summary>
    /// AI 故障识别 Mock 实现（开发/测试用）。
    /// 后期替换为真实 AI 实现时，只需新建类实现 IFaultRecognizer 接口即可。
    /// </summary>
    public class MockFaultRecognizer : IFaultRecognizer
    {
        /// <summary>
        /// 模拟 AI 识别：返回候选列表第一个故障类型。
        /// 模拟 1.5 秒延迟，置信度固定 0.85。
        /// </summary>
        public async Task<FaultRecognitionResult> RecognizeAsync(byte[] photoData, List<string> candidateFaultTypes)
        {
            // 模拟 AI 分析延迟
            await Task.Delay(1500).ConfigureAwait(false);

            if (candidateFaultTypes == null || candidateFaultTypes.Count == 0)
            {
                return new FaultRecognitionResult
                {
                    FaultType = "未识别",
                    Confidence = 0
                };
            }

            return new FaultRecognitionResult
            {
                FaultType = candidateFaultTypes[0],
                Confidence = 0.85m
            };
        }
    }
}
