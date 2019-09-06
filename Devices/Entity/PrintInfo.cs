using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{

    [Serializable]
    public class PrintInfo
    {

        public PrintInfo()
        {
            Author = "咸菜";
            Name = "基础打印";
            Describe = "基础打印";
            Dll = "Print.dll";
            ClassName = "Devices.Print.UniversalPrint";
            UsableDeviceType = DeviceType.MaxAndMin;
            Images = new List<string> { "print.jpg" };
        }

        public string Author { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public string Dll
        {
            get;
            set;
        }

        public string ClassName
        {
            get;
            set;
        }

        public DeviceType UsableDeviceType { get; set; }

        public List<string> Images{ get; set; }

    }
}
