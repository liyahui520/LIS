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
    internal partial class UCFuseConfig : UCDevicesConfig
    {
        public UCFuseConfig()
        {
            InitializeComponent();
        }
        public UCFuseConfig(IDevices info)
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
            FuseConfig config = (FuseConfig)cf;
            config.CurrentConnectType = radioButtonTCP.Checked ? ConnectType.HTTP : ConnectType.RS_232;
            config.Address = textBoxAddress.Text.Trim().ToString();
            config.LoginPassword = textBoxPass.Text.Trim().ToString();
            config.LoginName = textBoxName.Text.Trim().ToString();
            return cf;
        }



        private void NewUCFuseConfig_Load(object sender, EventArgs e)
        {
            if (dev != null && dev.Config!=null)
            {
                FuseConfig config = (FuseConfig)dev.Config;
                if (dev.Config.CurrentConnectType == ConnectType.HTTP)
                    radioButtonTCP.Checked = true;
                textBoxAddress.Text = config.Address;
                textBoxPass.Text = config.LoginPassword;
                textBoxName.Text = config.LoginName;
            }
        }
    }
}
