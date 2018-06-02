using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TOF_FieldData
{
    public class QcResult
    {
        private Logger logger = Logger.Instance;

        private List<string> fileLines;

        public bool PmtGainCalibrationPassed { get; set; }
        public int PmtGainCalibrationIterations { get; set; }
        public double PmtGainCalibrationAvgError { get; set; }
        public double PmtGainCalibrationMaxError { get; set; }

        public QcResult(string filePath)
        {
            fileLines = new List<string>(File.ReadAllLines(filePath));
            ReadPMTGainCalibration();
            logger.Log($"Found QCResult {filePath}");
            logger.Log($"PMT Gain Calibration Passed: {PmtGainCalibrationPassed}");
        }

        private void ReadPMTGainCalibration(){
            
            var startIndex = FindHeaderWithText("PMT Gain Calibration:");
            if (startIndex != -1 && fileLines.Count >= startIndex + 14)
            {
                var sectionLines = fileLines.GetRange(startIndex, 14);
                PmtGainCalibrationPassed = sectionLines[0].Contains("PASSED");

                var start = sectionLines[3].IndexOf(':');
                var end = sectionLines[3].IndexOf('[');
                PmtGainCalibrationIterations = Int32.Parse(GetTextBetween(sectionLines[3], start, end));

                start = sectionLines[4].IndexOf(':');
                end = sectionLines[4].IndexOf('%');
                PmtGainCalibrationAvgError = Double.Parse(GetTextBetween(sectionLines[4], start, end));

                start = sectionLines[5].IndexOf(':');
                end = sectionLines[5].IndexOf('%');
                PmtGainCalibrationMaxError = Double.Parse(GetTextBetween(sectionLines[5], start, end));
            }
        }

        private string GetTextBetween( string value, int start, int end )
        {
            return value.Substring(start + 1, end - start - 1).Trim();
        }

        private int FindHeaderWithText(string text)
        {
            var i = 0;
            foreach (var line in fileLines)
            {
                if (line.Contains(text))
                {
                    if(fileLines[i-1].Contains('*') && fileLines[i + 1].Contains('*'))
                    return i;
                }

                ++i;
            }
            return -1;
        }
    }
}
