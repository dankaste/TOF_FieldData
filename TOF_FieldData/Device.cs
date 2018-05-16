using System;
using System.Collections.Generic;

namespace TOF_FieldData
{
    public class Device
    {
        private String deviceName;
        private String petGantrySerialNumber;
        private String systemSerialNumber;

        private List<QcResult> qcResults = new List<QcResult>();

        public Device()
        {
        }
    }
}
