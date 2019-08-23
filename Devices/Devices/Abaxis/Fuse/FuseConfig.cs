using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices.Abaxis
{
    [Serializable]
    public class FuseConfig : Config
    {
       public FuseConfig()
       {
           ConnectTypes = (int)(ConnectType.HTTP);
           CurrentConnectType = ConnectType.HTTP;
       }


        public String Address { get; set; }

        public string LoginName { get; set; }

        public string LoginPassword { get; set; }
    }
}
