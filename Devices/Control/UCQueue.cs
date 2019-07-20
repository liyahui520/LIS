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
    internal partial class UCQueue : UserControl
    {
        IDevices _dev;
        public UCQueue()
        {
            InitializeComponent();
        }
        public UCQueue(IDevices dev)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            this.Dock = DockStyle.Fill;
            _dev = dev;
        }

        public void DevCommandsChanged(IDevices arg1, Command arg2)
        {
            this.Invoke(new Action(() =>
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _dev.CMDS;
                    dataGridView1.Refresh();
                }));
        }

        private void UCQueue_Load(object sender, EventArgs e)
        {
            if (_dev.CMDS != null)
                this.dataGridView1.DataSource = _dev.CMDS;
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = this.dataGridView1.HitTest(e.X, e.Y);
                if (hit.RowIndex != -1)
                {
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                        item.Selected = item.Index == hit.RowIndex;
                    contextMenuStrip1.Show(this.PointToScreen(e.Location));
                }
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            int index = Convert.ToInt32(item.Tag);
            DataGridViewRow row = this.dataGridView1.SelectedRows[0];
            Command cmd = (Command)row.DataBoundItem;
            lock (_dev.CMDS)
            {
                _dev.CMDS.Remove(cmd);
                index = index == 0 ? 0 : row.Index + index;
                if (index > _dev.CMDS.Count)
                    index = _dev.CMDS.Count;
                _dev.CMDS.Insert(index, cmd);
            }
            _dev.SaveCmds();
            dataGridView1.Refresh();
            foreach (DataGridViewRow datarow in dataGridView1.Rows)
                datarow.Selected = index == datarow.Index;
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            int index = Convert.ToInt32(item.Tag);
            DataGridViewRow row = this.dataGridView1.SelectedRows[0];
            Command cmd = (Command)row.DataBoundItem;
            lock (_dev.CMDS)
                _dev.RemoveCommand(cmd.Id);
            dataGridView1.Refresh();
        }
    }
}
