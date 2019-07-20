using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Devices.Mindray
{
    internal partial class UCBC_2800Config : UCDevicesConfig
    {
        BC_2800 bc2800;
        public UCBC_2800Config()
        {
            InitializeComponent();
        }
        public UCBC_2800Config(BC_2800 info)
            : base(info)
        {
            InitializeComponent();
            bc2800 = info;
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="hasErrors"></param>
        /// <returns></returns>
        protected override Config GetConfig(out int hasErrors)
        {
            Config cf= base.GetConfig(out hasErrors);
            BC_2800Config config = (BC_2800Config)cf;
            config.SerialPortConfig = ucSerialPortConfig1.GetSerialPortConfig();
            return cf;
        }



        private void NewUCNX500IVCConfig_Load(object sender, EventArgs e)
        {
            if (this.bc2800 != null)
            {
                this.ucSerialPortConfig1.LoadData(((BC_2800Config)bc2800.Config).SerialPortConfig);
            }
        }

    }
}
