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
    internal partial class UCDRI_CHEM_NX700iVCConfig : UCDevicesConfig
    {
        DRI_CHEM_NX700iVC fuji700;
        public UCDRI_CHEM_NX700iVCConfig()
        {
            InitializeComponent();
        }
        public UCDRI_CHEM_NX700iVCConfig(DRI_CHEM_NX700iVC info)
            : base(info)
        {
            InitializeComponent();
            fuji700 = info;
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="hasErrors"></param>
        /// <returns></returns>
        protected override Config GetConfig(out int hasErrors)
        {
            Config cf= base.GetConfig(out hasErrors);
            DRI_CHEM_NX700iVCConfig config = (DRI_CHEM_NX700iVCConfig)cf;
            config.CurrentConnectType = radioButtonTCP.Checked ? ConnectType.TCP : ConnectType.RS_232;
            config.SerialPortConfig = ucSerialPortConfig1.GetSerialPortConfig();
            return cf;
        }



        private void NewUCNX500IVCConfig_Load(object sender, EventArgs e)
        {
            if (this.fuji700 != null)
            {
                if (fuji700.Config.CurrentConnectType == ConnectType.TCP)
                    radioButtonTCP.Checked = true;
                this.ucSerialPortConfig1.LoadData(((DRI_CHEM_NX700iVCConfig)fuji700.Config).SerialPortConfig);
            }
        }


        private void panel3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofdl = new OpenFileDialog();
            if (ofdl.ShowDialog() == DialogResult.OK)
            {
                this.fuji700.Config = Tool.GetObjectByXML<DRI_CHEM_NX700iVCConfig>(ofdl.FileName);
                base.dev = fuji700;
                base.UCDevicesConfig_Load(null,null);
            }
        }


    }
}
