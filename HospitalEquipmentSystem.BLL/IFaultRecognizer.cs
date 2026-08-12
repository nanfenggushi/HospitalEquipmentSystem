using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalEquipment.BLL
{
    /// <summary>
    /// AI 故障识别接口。
    /// 开发阶段使用 MockFaultRecognizer，后期对接真实 AI 只需新建类实现此接口即可。
    /// </summary>
    public interface IFaultRecognizer
    {
        /// <summary>
        /// 根据设备照片和候选故障列表，识别故障类型
        /// </summary>
        /// <param name="photoData">照片的字节数组</param>
        /// <param name="candidateFaultTypes">候选故障类型列表（来自 MaterialDAL.GetDistinctFaultTypes）</param>
        /// <returns>识别结果（故障类型 + 置信度）</returns>
        Task<FaultRecognitionResult> RecognizeAsync(byte[] photoData, List<string> candidateFaultTypes);
    }
}
