using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Devices.Control
{
    internal partial class UCDevicesInfo : UserControl
    {
        public UCDevicesInfo()
        {
            InitializeComponent();
        }
        public void LoadDate(DevicesInformation info)
        {
            labelName.Text = info.Name;
            labelBread.Text = info.Brand;
            labelModel.Text = info.Model;
            labelURL.Text = info.Url;
            Image image=info.GetImage();
            if (image != null)
                this.pictureBox1.Image = image;
        }
    }
}
