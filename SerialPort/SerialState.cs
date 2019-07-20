using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerialPort
{
    /// <summary>
    /// 串口连接状态
    /// </summary>
   public  enum SerialConnectionState
    {

       /// <summary>
       /// 串口未打开,请检查数据库是否连接正确,COM口是否配置正确
       /// </summary>
       Closed=1,


       /// <summary>
       /// 不能发送数据,请检查终端设备是否开启,或者连接项是否配置正确
       /// </summary>
       NoTXD = 2,

       /// <summary>
       /// 正确
       /// </summary>
       Opened = 100
    }
}
