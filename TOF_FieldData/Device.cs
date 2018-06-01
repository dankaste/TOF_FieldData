using System;
using System.Collections.Generic;

namespace TOF_FieldData
{
    public class Device
    {
        public String petGantrySerialNumber { get; set; }
        public String systemSerialNumber { get; set; }

        public IEnumerable<QcResult> qcResults { get; set; }

        public Device()
        {
        }
    }
}
