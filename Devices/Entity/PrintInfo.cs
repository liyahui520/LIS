using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{

    [Serializable]
    public class PrintInfo
    {
        private string dll = "";
        private string className = "";
        public PrintInfo()
        {
            Author = "咸菜";
            Name = "基础打印";
            Describe = "基础打印";
            Dll = "Print.dll";
            className = "Devices.Print.UniversalPrint";
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

    }
}
