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
    internal partial class UCDRI_CHEM_NX500iVCConfig : UCDevicesConfig
    {
        DRI_CHEM_NX500iVC fuji500;
        public UCDRI_CHEM_NX500iVCConfig()
        {
            InitializeComponent();
        }
        public UCDRI_CHEM_NX500iVCConfig(DRI_CHEM_NX500iVC info)
            : base(info)
        {
            InitializeComponent();
            fuji500 = info;
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="hasErrors"></param>
        /// <returns></returns>
        protected override Config GetConfig(out int hasErrors)
        {
            Config cf= base.GetConfig(out hasErrors);
            DRI_CHEM_NX500iVCConfig config = (DRI_CHEM_NX500iVCConfig)cf;
            config.CurrentConnectType = radioButtonTCP.Checked ? ConnectType.TCP : ConnectType.RS_232;
            config.SerialPortConfig = ucSerialPortConfig1.GetSerialPortConfig();
            return cf;
        }



        private void NewUCNX500IVCConfig_Load(object sender, EventArgs e)
        {
            if (this.fuji500 != null)
            {
                if (fuji500.Config.CurrentConnectType == ConnectType.TCP)
                    radioButtonTCP.Checked = true;
                this.ucSerialPortConfig1.LoadData(((DRI_CHEM_NX500iVCConfig)fuji500.Config).SerialPortConfig);
            }
        }

    }
}
