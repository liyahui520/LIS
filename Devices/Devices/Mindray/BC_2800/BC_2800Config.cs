using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices.Mindray
{
    [Serializable]
    public class BC_2800Config : Config
    {
       public BC_2800Config()
       {
           ConnectTypes = (int)ConnectType.RS_232;
           CurrentConnectType = ConnectType.RS_232;
       }

        /// <summary>
        /// 串口配置
        /// </summary>
       public SerialPort.SerialPortConfig SerialPortConfig { get; set; }
    }
}
