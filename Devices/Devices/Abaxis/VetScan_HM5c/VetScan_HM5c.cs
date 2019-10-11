using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices.Abaxis
{
    public class VetScan_HM5c:Abaxis_Fuse
    {
        public VetScan_HM5c(string fileName)
    : base(fileName)
        {
            FuseCode = "VetScan HM5c";
            Protocol = new FuseProtocol.HTTPProtocol();
            Protocol.Init(this);
        }


        public override string RequestCode => "HEM";

        /// <summary>
        /// 设备信息
        /// </summary>

        protected override DevicesInformation DefaultInfo()
        {
            DevicesInformation info = new DevicesInformation
            {
                Num = 7,
                Brand = "爱倍思(Abaxis)",
                Model = "Abaxis VetScan HM5c",
                Name = "爱倍思 HM5c血常规",
                Remarks = "爱倍思 HM5c血常规",
                Code = "Abaxis_VetScan_HM5c",
                Url = "https://www.abaxis.com/veterinary/products/vetscan-vs2",
                ImagePath = "VS2_600x600_FE.png",
            };
            return info;
        }
    }
}
