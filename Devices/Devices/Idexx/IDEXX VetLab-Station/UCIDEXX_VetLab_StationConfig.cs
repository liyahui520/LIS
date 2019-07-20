using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Devices.IDEXX
{
    internal partial class UCDRI_CHEM_NX500iVCConfig : UCDevicesConfig
    {
        IDEXX_VetLab_Station idexxInfo;
        public UCDRI_CHEM_NX500iVCConfig()
        {
            InitializeComponent();
        }
        public UCDRI_CHEM_NX500iVCConfig(IDEXX_VetLab_Station info)
            : base(info)
        {
            InitializeComponent();
            idexxInfo = info;
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="hasErrors"></param>
        /// <returns></returns>
        protected override Config GetConfig(out int hasErrors)
        {
            Config cf = base.GetConfig(out hasErrors);
            IDEXX_VetLab_StationConfig config = (IDEXX_VetLab_StationConfig)cf;
            config.CurrentConnectType = ConnectType.Other;
            config.PDFPath = textBoxPDF.Text;
            config.RequestPath = textBoxRequest.Text;
            config.ResultPath = textBoxResult.Text;
            config.DateFormat = comboBoxDate.SelectedItem.ToString();
            return cf;
        }



        private void NewUCNX500IVCConfig_Load(object sender, EventArgs e)
        {
            if (this.idexxInfo != null)
            {
                IDEXX_VetLab_StationConfig config = (IDEXX_VetLab_StationConfig)idexxInfo.Config;
                config.CurrentConnectType = ConnectType.Other;
                textBoxPDF.Text = config.PDFPath;
                textBoxRequest.Text = config.RequestPath;
                textBoxResult.Text = config.ResultPath;
                comboBoxDate.SelectedItem = config.DateFormat;
            }
        }

    }
}
