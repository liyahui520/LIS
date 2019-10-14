using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
namespace Devices.Abaxis
{
    
    [Serializable]
    public class Vetscan_VS2 : Abaxis_Fuse
    {
        public Vetscan_VS2(string fileName)
            : base(fileName)
        {
            FuseCode = "VetScan VS2";
            Protocol = new FuseProtocol.HTTPProtocol();
            Protocol.Init(this);
        }


        public override string RequestCode => "HEM";

        /// <summary>
        /// 设备信息
        /// </summary>

        protected override  DevicesInformation DefaultInfo()
        {
            DevicesInformation info = new DevicesInformation
            {
                Num = 6,
                Brand = "爱倍思(Abaxis)",
                Model = "Abaxis Vetscan-VS2",
                Name = "爱倍思 VS2生化分析仪",
                Remarks = "爱倍思 VS2生化分析仪",
                Code = "Abaxis_Vetscan_VS2",
                Url = "https://www.abaxis.com/veterinary/products/vetscan-vs2",
                ImagePath = "VS2_600x600_FE.png",
            };
            return info;
        }

    }
}
