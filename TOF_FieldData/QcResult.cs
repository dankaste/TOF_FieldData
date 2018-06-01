using System;
namespace TOF_FieldData
{
    public class QcResult
    {
        public QcResult(string filePath)
        {
            Logger.Instance.Log($"Found QCResult {filePath}");
        }
    }
}
