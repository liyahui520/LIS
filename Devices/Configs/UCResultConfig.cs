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
    public partial class UCResultConfig : UserControl
    {
        public List<ResultConfig> configs;
        public UCResultConfig()
        {
            InitializeComponent();
        }
        public UCResultConfig(List<ResultConfig> list)
        {
            InitializeComponent();
        }

        public List<ResultConfig> GetResultConfig(out bool hasErrors)
        {
            hasErrors = false;
            if (this.tabControl1.TabPages.Count > 0)
            {
                List<ResultConfig> rcs = new List<ResultConfig>();
                foreach (TabPage item in this.tabControl1.TabPages)
                {
                    UCResultItemConfig uic = (UCResultItemConfig)item.Controls[0];
                    ResultConfig rc = uic.GetResultConfig();
                    if (rc == null)
                    {
                        tabControl1.SelectedTab = item;
                        hasErrors = true;
                    }
                    rcs.Add(rc);
                }
                return rcs;
            }
            return null;
        }

        private void UCResultConfig_Load(object sender, EventArgs e)
        {
            if (configs != null)
            {
                this.tabControl1.Controls.Clear();
                foreach (var item in configs)
                {
                    TabPage tp = new TabPage();
                    tp.Text = item.Name;
                    tp.Tag = item;
                    UCResultItemConfig itemconfig = new UCResultItemConfig(item);
                    itemconfig.Dock = DockStyle.Fill;
                    itemconfig.ResultNameChanged += name => tp.Text = name;
                    tp.Controls.Add(itemconfig);
                    this.tabControl1.TabPages.Add(tp);
                }
                tabControl1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TabPage tp = new TabPage();
            UCResultItemConfig itemconfig = new UCResultItemConfig();
            tp.Text = "新建项";
            itemconfig.ResultNameChanged += name=> tp.Text = name;
            itemconfig.Dock = DockStyle.Fill;
            tp.Controls.Add(itemconfig);
            this.tabControl1.TabPages.Add(tp);
            this.tabControl1.SelectedTab = tp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TabPage tp = this.tabControl1.SelectedTab;
            if (tp != null)
            {
                this.tabControl1.TabPages.Remove(tp);
            }
        }

        public void LoadDate(List<ResultConfig> list)
        {
            configs = list;
            UCResultConfig_Load(null,null);
        }

    }
}
