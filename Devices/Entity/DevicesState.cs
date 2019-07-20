using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    [Serializable]
   public enum DevicesState
    {
       Opened=1,
       Error =2,
       Closed=4,
       Unknown=10
    }
}
