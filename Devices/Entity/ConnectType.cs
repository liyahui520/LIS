using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    [Serializable]
    /// <summary>
    /// 设备支持的通信协议,可共存
    /// </summary>
    public enum ConnectType
    {
        Unknown=0,
        UDP = 1,
        TCP = 2,
        HTTP = 4,
        HTTPS = 8,
        FTP = 16,
        USB2 = 32,
        USB3 = 64,
        RS_232 = 128,
        RS_485 = 256,
        RS_422 = 512,

        Other=Int32.MaxValue
        
    }
}
