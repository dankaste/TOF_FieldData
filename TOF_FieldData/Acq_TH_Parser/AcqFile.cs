using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Parsers
{
    public class AcqFile
    {
        private Logger logger = Logger.Instance;

        private List<string> fileLines;

        public bool PmtGainCalibrationPassed { get; set; }
        public int PmtGainCalibrationIterations { get; set; }
        public double PmtGainCalibrationAvgError { get; set; }
        public double PmtGainCalibrationMaxError { get; set; }

        public AcqFile(string filePath)
        {
            fileLines = new List<string>(File.ReadAllLines(filePath));
			/*ReadPMTGainCalibration();
            logger.Log($"Found QCResult {filePath}");
            logger.Log($"PMT Gain Calibration Passed: {PmtGainCalibrationPassed}");*/
		}
	
    }
}
