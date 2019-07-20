using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Devices.Fujifilm
{
    [Serializable]
    public class FF_6450Config : Config
    {
        public FF_6450Config()
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
