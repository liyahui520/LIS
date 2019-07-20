using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
namespace SerialPort
{
    [Serializable]
   public class SerialPortConfig
    {
       /// <summary>
       /// 串口名
       /// </summary>
       public string PortName { get; set; }

       /// <summary>
       /// 波特率
       /// </summary>
       public int Baud { get; set; }

       /// <summary>
       /// 校验方式
       /// </summary>
       public Parity Parity { get; set; }

       /// <summary>
       /// 停止位
       /// </summary>
       public StopBits StopBits { get; set; }

       /// <summary>
       /// 数据位
       /// </summary>
       public int DataBits { get; set; }

       /// <summary>
       /// 数据编码格式
       /// </summary>
       public string EncodingName { get; set; }

       /// <summary>
       /// 数据缓冲区大小
       /// </summary>
       public int BufferSize { get; set; }
    }
}
