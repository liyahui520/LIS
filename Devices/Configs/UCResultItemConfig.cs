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
    public partial class UCResultItemConfig : UserControl
    {
        List<ResultItemConfig> list;
        public UCResultItemConfig()
        {
            InitializeComponent();
            dataGridViewItems.AutoGenerateColumns = false;
            list = new List<ResultItemConfig>();
            list.Add(new ResultItemConfig { Code="New", EnglishName="New", Id=1, Max="0", Min="0", Name="新建项" });
            this.dataGridViewItems.DataSource = list;
        }
        public UCResultItemConfig(ResultConfig config)
        {
            InitializeComponent();
            dataGridViewItems.AutoGenerateColumns = false;
            textBoxName.Text = config.Name;
            textBoxCode.Text = config.Code;
            textBoxFormula.Text = config.Formula;
            list = config.Items;
            this.dataGridViewItems.DataSource = list;
        }

        public ResultConfig GetResultConfig()
        {
            ResultConfig rc = new ResultConfig();
            rc.Name = textBoxName.Text.Trim();
            rc.Formula = textBoxFormula.Text.Trim();
            if (string.IsNullOrEmpty(rc.Name))
                return null;
            if (!string.IsNullOrEmpty(rc.Formula) && ResultConfig.GetFormula(rc.Formula) == null)
                return null;
            rc.Code = textBoxCode.Text;
            rc.Items = list.Where(o => !string.IsNullOrEmpty(o.Code)).ToList(); ;
            return rc;
        }
        private event Action<String> resultNameChanged;
        public event Action<String> ResultNameChanged
        {
            add
            {
                resultNameChanged += value;
            }
            remove
            {
                resultNameChanged -= value;
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (resultNameChanged != null)
                resultNameChanged(textBoxName.Text);
        }

        private void 添加项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list == null)
                list = new List<ResultItemConfig>();
            list.Add(new ResultItemConfig());
            dataGridViewItems.DataSource = null;
            dataGridViewItems.DataSource = list;

            dataGridViewItems.Rows[list.Count - 1].Selected = true;
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows != null)
            {
                foreach (DataGridViewRow item in dataGridViewItems.SelectedRows)
                    list.Remove((ResultItemConfig)item.DataBoundItem);
                this.dataGridViewItems.DataSource = null;
                this.dataGridViewItems.DataSource = list;
            }
        }

        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows != null)
            {
                ResultItemConfig item = (ResultItemConfig)dataGridViewItems.SelectedRows[0].DataBoundItem;
                int index = list.IndexOf(item);
                if (index == 0)
                    return;
                list.Remove(item);
                list.Insert(index-1,item);
                this.dataGridViewItems.DataSource = null;
                this.dataGridViewItems.DataSource = list;
            }
        }

        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows != null)
            {
                ResultItemConfig item = (ResultItemConfig)dataGridViewItems.SelectedRows[0].DataBoundItem;
                int index = list.IndexOf(item);
                if (index == list.Count-1)
                    return;
                list.Remove(item);
                list.Insert(index + 1, item);
                this.dataGridViewItems.DataSource = null;
                this.dataGridViewItems.DataSource = list;
            }
        }

        private void 置顶ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows != null)
            {
                ResultItemConfig item = (ResultItemConfig)dataGridViewItems.SelectedRows[0].DataBoundItem;
                list.Remove(item);
                list.Insert(0, item);
                this.dataGridViewItems.DataSource = null;
                this.dataGridViewItems.DataSource = list;
            }
        }

        private void 置底ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows != null)
            {
                ResultItemConfig item = (ResultItemConfig)dataGridViewItems.SelectedRows[0].DataBoundItem;
                list.Remove(item);
                list.Add(item);
                this.dataGridViewItems.DataSource = null;
                this.dataGridViewItems.DataSource = list;
            }
        }
    }
}
