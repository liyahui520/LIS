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
    internal partial class UCDevicesState : UserControl
    {
        IDevices dev;
        private Action<UCDevicesState, IDevices> devicesSelected;
        public UCDevicesState(IDevices _dev)
        {
            InitializeComponent();
            if (_dev == null)
                return;
            dev = _dev;
            this.pictureBox1.Image = dev.Info.GetImage();
            this.labelName.Text = dev.Info.Name;
            dev.StateChanged += dev_StateChanged;
            dev_StateChanged(dev, dev.State);
        }
        public UCDevicesState()
        {
            InitializeComponent();
        }
        void dev_StateChanged(IDevices obj, DevicesState e)
        {
            switch (e)
            {
                case DevicesState.Opened:
                    this.labelName.ForeColor = Color.Green;
                    break;
                case DevicesState.Error:
                    this.labelName.ForeColor = Color.Yellow;
                    break;
                case DevicesState.Closed:
                    this.labelName.ForeColor = Color.Black;
                    break;
                case DevicesState.Unknown:
                    this.labelName.ForeColor = Color.Red;
                    break;
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            labelName.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            labelName.Visible = false;
        }

        public event Action<UCDevicesState, IDevices> DevicesSelected
        {
            add { devicesSelected += value; }
            remove { devicesSelected -= value; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        public void Select(bool selected)
        {
            if (selected)
                pcolor = Color.Blue;
            else
               pcolor=Color.FromArgb(224, 224, 224);
            this.panel1.Refresh();
        }
        private Color pcolor = Color.FromArgb(224, 224, 224);
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen1 = new Pen(pcolor, (float)1);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            e.Graphics.DrawRectangle(pen1, 0, 0, this.panel1.Width-1, this.panel1.Height-1);
        }

    }
}
