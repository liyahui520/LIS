using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices.Fujifilm
{
    [Serializable]
    public class DRI_CHEM_NX700iVCConfig : Config
    {
        public DRI_CHEM_NX700iVCConfig()
       {
           ConnectTypes = (int)(ConnectType.TCP | ConnectType.RS_232);
           CurrentConnectType = ConnectType.RS_232;
       }

        /// <summary>
        /// 串口配置
        /// </summary>
       public SerialPort.SerialPortConfig SerialPortConfig { get; set; }
    }
}
