using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Devices
{
    internal partial class UCDevicesConfig : UserControl
    {
        protected  IDevices dev;
        public UCDevicesConfig()
        {
            InitializeComponent();
        }
        public UCDevicesConfig(IDevices info)
        {
            InitializeComponent();
            dev = info;
        }

        protected  void UCDevicesConfig_Load(object sender, EventArgs e)
        {
            if (dev != null)
            {
                ucDevicesInfo1.LoadDate(dev.Info);
                ucResultConfig1.LoadDate(dev.Config.ResultConfig);
                this.checkBoxAutoConnect.Checked = dev.Config.AutoConnect;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int hasErrors = -1;
            dev.Config = GetConfig(out hasErrors);
            if (hasErrors > -1)
            {
                tabControl1.SelectedIndex = hasErrors;
                MessageBox.Show("此配置有错误");
                return;
            }
            dev.Config.AutoConnect = checkBoxAutoConnect.Checked;
            if (dev.Config.AutoConnect)
                dev.Restart();
            dev.SaveConfig();
            if (Parent is Form)
                ((Form)this.Parent).DialogResult = DialogResult.OK;
        }

        protected virtual Config GetConfig(out int hasErrors)
        {
            hasErrors = -1;
            Config cf = dev.Config;
            dev.Config.AutoConnect = checkBoxAutoConnect.Checked;
            bool resuErrors = false;
            dev.Config.ResultConfig = ucResultConfig1.GetResultConfig(out resuErrors);
            if (resuErrors)
                hasErrors = 1;
            return cf;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Parent is Form)
            {
                Form form = (Form)this.Parent;
                form.DialogResult = DialogResult.Cancel;
                form.Close();
            }
        }

        internal Devices.DevicesForm ShowForm()
        {
            Devices.DevicesForm form = new DevicesForm();
            form.Width = 800;
            form.Height = 460;
            this.Width = 800;
            form.Text = dev.Info.Name;
            this.Dock = DockStyle.Fill;
            form.Controls.Add(this);
            return form;
        }
    }
}
