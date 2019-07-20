using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    /// <summary>
    /// 设备使用的通信协议
    /// </summary>
    public interface IProtocol
    {
        void Init(IDevices devices);
        bool Start();
        bool Close();

        Exception Error { get; set; }
    }
}
