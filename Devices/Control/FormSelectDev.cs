using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Devices.Control
{
    public partial class FormSelectDev : Form
    {
        private UCDevicesState selectedUCDev;
        private Action<IDevices> selectedDevices;
        private IDevices devices;

        public event Action<IDevices> SelectedDevicesEventHandler
        {
            add { selectedDevices += value; }
            remove { selectedDevices -= value; }
        }
        /// <summary>
        /// 选中项后是否关闭窗口
        /// </summary>
        public bool IsSelectedCloseFrom { get; set; }
        public IDevices Device
        {
            get { return devices; }
            private set
            {
                devices = value;
                if (selectedDevices != null)
                    selectedDevices(value);
            }
        }
        internal UCDevicesState SelectedUCDev
        {
            get { return selectedUCDev; }
            set { selectedUCDev = value; }
        }
        public FormSelectDev()
        {
            InitializeComponent();
        }
        public FormSelectDev(bool selectedCloseFrom)
        {
            InitializeComponent();
            this.IsSelectedCloseFrom = selectedCloseFrom;
        }

        private void FormSelectDev_Load(object sender, EventArgs e)
        {
            List<IDevices> devs = DevicesCollection.Devices;
            if (devs != null)
            {
                int index = 0;
                foreach (var item in devs)
                {
                    UCDevicesState ucs = new UCDevicesState(item);
                    ucs.Tag = item;
                    ucs.Location = new Point((index % 5) + 12 + ((index % 5) * 130), (index / 5) + 12 + ((index / 5) * 188));
                    ucs.Click += ucs_Click;
                    this.panel1.Controls.Add(ucs);
                    index++;
                }
            }
        }

        /// <summary>
        /// 点击某一项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucs_Click(object sender, EventArgs e)
        {
            UCDevicesState uc = (UCDevicesState)sender;
            MouseEventArgs args = (MouseEventArgs)e;
            IDevices dev = (IDevices)uc.Tag;
            if (selectedUCDev != null)
                selectedUCDev.Select(false);
            uc.Select(true);
            SelectedUCDev = uc;
            this.Device = dev;
            if (args.Button == MouseButtons.Right)
                contextMenuStrip1.Show(uc.PointToScreen(args.Location));
            else
            {
                if (IsSelectedCloseFrom)
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void 开启ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedUCDev != null)
            {
                IDevices dev = (IDevices)selectedUCDev.Tag;
                dev.Open();
            }
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedUCDev != null)
            {
                IDevices dev = (IDevices)selectedUCDev.Tag;
                dev.Close();
            }
        }

        private void 查看队列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedUCDev != null)
            {
                IDevices dev = (IDevices)selectedUCDev.Tag;
                dev.ShowQueueForm();
            }
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedUCDev != null)
            {
                IDevices dev = (IDevices)selectedUCDev.Tag;
                dev.ShowConfigForm();
            }
        }
    }
}
