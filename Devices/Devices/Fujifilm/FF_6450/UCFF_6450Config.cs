using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Devices.Fujifilm
{
    internal partial class UCFF_6450Config : UCDevicesConfig
    {
        FF_6450 ff6450;
        public UCFF_6450Config()
        {
            InitializeComponent();
        }
        public UCFF_6450Config(FF_6450 info)
            : base(info)
        {
            InitializeComponent();
            ff6450 = info;
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="hasErrors"></param>
        /// <returns></returns>
        protected override Config GetConfig(out int hasErrors)
        {
            Config cf= base.GetConfig(out hasErrors);
            FF_6450Config config = (FF_6450Config)cf;
            config.CurrentConnectType = radioButtonTCP.Checked ? ConnectType.TCP : ConnectType.RS_232;
            config.SerialPortConfig = ucSerialPortConfig1.GetSerialPortConfig();
            return cf;
        }



        private void NewUCNX500IVCConfig_Load(object sender, EventArgs e)
        {
            if (this.ff6450 != null)
            {
                if (ff6450.Config.CurrentConnectType == ConnectType.TCP)
                    radioButtonTCP.Checked = true;
                this.ucSerialPortConfig1.LoadData(((FF_6450Config)ff6450.Config).SerialPortConfig);
            }
        }

    }
}
