using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices.IDEXX
{
    [Serializable]
    public class IDEXX_VetLab_StationConfig : Config
    {
        public IDEXX_VetLab_StationConfig()
        {
            ConnectTypes = (int)ConnectType.Other;
            CurrentConnectType = ConnectType.Other;
        }

        public string PDFPath { get; set; }
        public string ResultPath { get; set; }
        public string RequestPath { get; set; }
        public string DateFormat { get; set; }

    }
}
