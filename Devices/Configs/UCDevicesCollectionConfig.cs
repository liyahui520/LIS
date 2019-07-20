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
    public partial class UCDevicesCollectionConfig : UserControl
    {
        List<IDevices> addedDevs;
        List<DevicesInformation> devs;
        public UCDevicesCollectionConfig()
        {
            InitializeComponent();
        }

        public static Form ShowForm()
        {
            UCDevicesCollectionConfig uc = new UCDevicesCollectionConfig();
            Devices.DevicesForm form = new DevicesForm();
            form.Width = 800;
            form.Height = 600;
            uc.Dock = DockStyle.Fill;
            form.Controls.Add(uc);
            return form;
        }

        private void UCDevicesCollectionConfig_Load(object sender, EventArgs e)
        {
            addedDevs = Devices.DevicesCollection.Devices;
            SetListViewDateSet(addedDevs);

            devs = Devices.DevicesCollection.GetCanAddDevices();
            var list = devs.Select<DevicesInformation, dynamic>(o => new { Text = o.Name, Tag = o }).ToList();
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Tag";
        }

        private void ListViewAddItem(IDevices item)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = item.Info.Name;
            lvi.Tag = item;
            Image image = item.Info.GetImage();
            if (image != null)
            {
                imageList1.Images.Add(item.Info.Id, image);
                lvi.ImageKey = item.Info.Id;
            }
            listView1.Items.Add(lvi);
        }

        private void SetListViewDateSet(List<IDevices> devs)
        {
            this.listView1.Items.Clear();
            if (devs == null)
                return;
            foreach (var item in devs)
                ListViewAddItem(item);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DevicesInformation info = comboBox1.SelectedValue as DevicesInformation;
            if (info == null)
                return;
            IDevices newDev = Devices.DevicesCollection.CreateDevices(null, info.ClassInfo);
            if (newDev.ShowConfigForm() == DialogResult.OK)
            {
                if (addedDevs == null)
                    addedDevs = new List<IDevices>();
                DevicesCollection.Add(newDev);
                newDev.CommandCompleted += DevicesCollection.commandCompleted;
                newDev.StateChanged += DevicesCollection.stateChanged;
                ListViewAddItem(newDev);
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            ((IDevices)item.Tag).ShowConfigForm();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                this.contextMenuStrip1.Show(listView1.PointToScreen(e.Location));
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定要删除此项?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                IDevices dev = (IDevices)item.Tag;
                if (dev.State == DevicesState.Opened)
                    dev.Close();
                this.listView1.Items.Remove(item);
                Devices.DevicesCollection.Remove(dev);
            }
        }

        private void 查看队列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            ((IDevices)item.Tag).ShowQueueForm();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            ((IDevices)item.Tag).ShowConfigForm();
        }

        private void 开启ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            ((IDevices)item.Tag).Open();
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            ((IDevices)item.Tag).Close();
        }

    }
}
