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

        public QcResult(string filePath)
        {
            fileLines = new List<string>(File.ReadAllLines(filePath));
            ReadPMTGainCalibration();
            logger.Log($"Found QCResult {filePath}");
            logger.Log($"PMT Gain Calibration Passed: {PmtGainCalibrationPassed}");
        }

        private void ReadPMTGainCalibration(){
            var startIndex = -1;
            var i = 0;
            foreach( var line in fileLines ){
                if( line.Contains("PMT Gain Calibration:"))
                {
                    startIndex = i;
                    break;
                }

                ++i;
            }

            if (startIndex != -1 && fileLines.Count >= startIndex + 14)
            {
                var sectionLines = fileLines.GetRange(startIndex, 14);
                PmtGainCalibrationPassed = sectionLines[0].Contains("PASSED");
            }
        }
    }
}
