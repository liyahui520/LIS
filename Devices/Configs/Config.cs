using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    [Serializable]
    public class  Config
    {
       /// <summary>
       /// 可以使用的通讯协议
       /// </summary>
       public virtual int ConnectTypes { get; set; }

       /// <summary>
       /// 当前使用的通讯方式
       /// </summary>
       public ConnectType CurrentConnectType { get; set; }



        /// <summary>
        /// 是否自动重连
        /// </summary>
       public bool AutoConnect { get; set; }

       public virtual List<ResultConfig> ResultConfig { get; set; }

    }
}
