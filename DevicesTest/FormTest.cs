using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json;
namespace DevicesTest
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            Devices.DevicesCollection.CommandCompleted += item_CommandCompleted;
            Devices.DevicesCollection.StartAll();
            List<Devices.IDevices> devs = Devices.DevicesCollection.Devices;
            if (devs != null)
            {
                var list = devs.Select<Devices.IDevices, dynamic>(o => new { Text = o.Info.Name, Tag = o }).ToList();
                comboBoxdevs.DataSource = list;
                comboBoxdevs.DisplayMember = "Text";
                comboBoxdevs.ValueMember = "Tag";
            }

            List<dynamic> kindOftypes = new List<dynamic>();
            foreach (var item in typeof(Devices.KindOfType).GetEnumValues())
                kindOftypes.Add(new { Text = item.ToString(), Tag = item });
            comboBoxkindOftypes.DataSource = kindOftypes;
            comboBoxkindOftypes.DisplayMember = "Text";
            comboBoxkindOftypes.ValueMember = "Tag";

            List<dynamic> setTypes = new List<dynamic>();
            foreach (var item in typeof(Devices.GenderType).GetEnumValues())
                setTypes.Add(new { Text = item.ToString(), Tag = item });
            comboBoxSex.DataSource = setTypes;
            comboBoxSex.DisplayMember = "Text";
            comboBoxSex.ValueMember = "Tag";
        }

        void item_CommandCompleted(Devices.Command sender, Devices.Result e)
        {
            if (e.ResultDatas == null)
                return;
            StringBuilder sb = new StringBuilder();
            sb.Append(e.Devices.Info.Name + "发来了检查结果:\r\n");
            foreach (var item in e.ResultDatas)
            {
                sb.Append(item.Display);
                sb.Append(":");
                sb.Append(item.Value);
                sb.Append("(" + item.Unit + ")");
                sb.Append("参考范围  ");
                if (!string.IsNullOrEmpty(item.Min))
                    sb.Append(item.Min + "--" + item.Max);
                sb.Append("\r\n");
            }
            this.Invoke(new Action(() => { label1.Text = sb.ToString(); }));

            if (e.Devices != null && e.Devices.PrintTool != null)
                e.Devices.PrintTool.Preview(e);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Devices.Command cmd = new Devices.Command();
            cmd.Id = this.textBoxNum.Text;
            cmd.Name = this.textBoxName.Text;
            cmd.PetId = this.textBoxPI.Text;
            cmd.PetName = this.textBoxPN.Text;
            cmd.Weight = double.Parse(this.textBoxWH.Text);
            cmd.Code = this.textBoxNum.Text;
            cmd.Customer = this.textBoxCS.Text;
            cmd.Date = DateTime.Now;
            cmd.Doctor = this.textBoxDC.Text;
            cmd.Age = this.textBoxAge.Text;
            cmd.KindOf = (Devices.KindOfType)comboBoxkindOftypes.SelectedValue;
            cmd.Gender = (Devices.GenderType)comboBoxSex.SelectedValue;
            cmd.CustomerId = "123";
            ((Devices.IDevices)comboBoxdevs.SelectedValue).AddCommand(cmd);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((Devices.IDevices)comboBoxdevs.SelectedValue).RemoveCommand(this.textBoxNum.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ((Devices.IDevices)comboBoxdevs.SelectedValue).ShowQueueForm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Devices.Control.FormDevsConfig form = new Devices.Control.FormDevsConfig();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ((Devices.IDevices)comboBoxdevs.SelectedValue).ShowConfigForm();
        }

        private void 串口工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerialPort.SerialPortTest test = new SerialPort.SerialPortTest();
            test.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Devices.Control.FormSelectDev();
            Devices.Control.FormSelectDev from = new Devices.Control.FormSelectDev();
            from.Show();
        }

        private void 启动所有设备ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Devices.DevicesCollection.StartAll();
        }

        private void 关闭所有设备ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Devices.DevicesCollection.CloseAll();
        }

        private void 结果查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormResult().Show();
        }
    }
}
