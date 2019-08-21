using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices.Abaxis
{
    [Serializable]
    public class Vetscan_VS2Config : Config
    {
       public Vetscan_VS2Config()
       {
           ConnectTypes = (int)(ConnectType.HTTP);
           CurrentConnectType = ConnectType.HTTP;
       }


        public String Address { get; set; }

        public string LoginName { get; set; }

        public string LoginPassword { get; set; }
    }
}
