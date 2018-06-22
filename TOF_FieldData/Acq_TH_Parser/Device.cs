using System;
using System.Collections.Generic;
using System.Linq;

namespace Parsers
{
    public class Device
    {
        public String PetGantrySerialNumber { get; set; }
        public String SystemSerialNumber { get; set; }
        public int NumberPassedResults { get; set; }
        public double AveragePmtGainCalibrationIterations { get; set; }
        public double MaxPmtGainCalibrationAverageError { get; set; }
        public double MinPmtGainCalibrationAverageError { get; set; }
        public double AveragePmtGainCalibrationMaxError { get; set; }

        public IEnumerable<QcResult> QCResults { get; set; }

        public Device()
        {
        }

        public void Build()
        {
            NumberPassedResults = QCResults.Count(r => r.PmtGainCalibrationPassed);
            AveragePmtGainCalibrationIterations = QCResults.Average(r => r.PmtGainCalibrationIterations);
            MaxPmtGainCalibrationAverageError = QCResults.Max(r => r.PmtGainCalibrationAvgError);
            MinPmtGainCalibrationAverageError = QCResults.Min(r => r.PmtGainCalibrationAvgError);
            AveragePmtGainCalibrationMaxError = QCResults.Average(r => r.PmtGainCalibrationMaxError);
        }
    }
}
