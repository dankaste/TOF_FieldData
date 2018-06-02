using System;
using System.Collections.Generic;
using System.Linq;

namespace TOF_FieldData
{
    public class Device
    {
        public String PetGantrySerialNumber { get; set; }
        public String SystemSerialNumber { get; set; }
        public int NumberPassedResults { get; set; }

        public IEnumerable<QcResult> QCResults { get; set; }

        public Device()
        {
        }

        public void Build()
        {
            NumberPassedResults = QCResults.Count(r => r.PmtGainCalibrationPassed);
        }
    }
}
