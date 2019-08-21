using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Devices.Abaxis
{
    internal partial class UCVetscan_VS2Config : UCDevicesConfig
    {
        public UCVetscan_VS2Config()
        {
            InitializeComponent();
        }
        public UCVetscan_VS2Config(Vetscan_VS2 info)
            : base(info)
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="hasErrors"></param>
        /// <returns></returns>
        protected override Config GetConfig(out int hasErrors)
        {
            Config cf= base.GetConfig(out hasErrors);
            Vetscan_VS2Config config = (Vetscan_VS2Config)cf;
            config.CurrentConnectType = radioButtonTCP.Checked ? ConnectType.HTTP : ConnectType.RS_232;
            config.Address = textBoxAddress.Text.Trim().ToString();
            config.LoginPassword = textBoxPass.Text.Trim().ToString();
            config.LoginName = textBoxName.Text.Trim().ToString();
            return cf;
        }



        private void NewUCNX500IVCConfig_Load(object sender, EventArgs e)
        {
            if (dev != null && dev.Config!=null)
            {
                Vetscan_VS2Config config = (Vetscan_VS2Config)dev.Config;
                if (dev.Config.CurrentConnectType == ConnectType.HTTP)
                    radioButtonTCP.Checked = true;
                textBoxAddress.Text = config.Address;
                textBoxPass.Text = config.LoginPassword;
                textBoxName.Text = config.LoginName;
            }
        }
    }
}
