using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DevicesTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(Devices.Fujifilm.UCNX500iVCConfig.ShowForm());
            Application.Run(new FormTest());
            Application.Exit();
            //Application.Run(Devices.Control.UCDevicesCollectionConfig.ShowForm());
            //Application.Run(new Devices.Control.FormSelectDev());
        }
    }
}
