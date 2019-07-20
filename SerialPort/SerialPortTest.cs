using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
namespace SerialPort
{
    public partial class SerialPortTest : Form
    {

        List<MySerialPort> list;
        MySerialPort serial;
        EncodingInfo encodingInfo;
        public SerialPortTest()
        {
            InitializeComponent();
            list = new List<MySerialPort>();
        }

        private void SerialPortTest_Load(object sender, EventArgs e)
        {
            this.comboBoxComs.DataSource = SerialPort.MySerialPort.GetPortNames();
            this.comboBoxBaud.DataSource = new int[] { 2400, 4800, 9600, 14400, 19200, 38400, 56000, 57600, 115200 };
            this.comboBoxParity.DataSource = Enum.GetValues(typeof(Parity));
            this.comboBoxStopBits.DataSource = Enum.GetValues(typeof(StopBits));
            comboBoxStopBits.SelectedIndex = 1;
            this.comboBoxEncodeing.DataSource = System.Text.Encoding.GetEncodings();
            this.comboBoxEncodeing.DisplayMember = "DisplayName";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serial == null)
            {
                int databit = int.Parse(textBoxDatabit.Text);
                serial = new MySerialPort(this.comboBoxComs.SelectedItem.ToString(), (int)comboBoxBaud.SelectedItem,
                   (Parity)comboBoxParity.SelectedItem, databit, (StopBits)comboBoxStopBits.SelectedItem);
                serial.SerialDataReceivedCompleteEventHandler += serial_SerialDataReceivedCompleteEventHandler;
                serial.DtrEnable = true;
                serial.RtsEnable = true;
                list.Add(serial);
            }
            if (serial.IsOpen)
            {
                serial.Close();
                button2.Text = "连接";
            }
            else
            {
                serial.Open();
                button2.Text = "关闭";
                
            }
            
            comboBoxComs_SelectedIndexChanged(null, null);
        }

        void serial_SerialDataReceivedCompleteEventHandler(object arg1, SerialDataReceivedCompleteEventArgs arg2)
        {
            StringBuilder str=new StringBuilder();
            MySerialPort prot = (MySerialPort)arg1;
            if (radioButton1.Checked)
                arg2.Data.Any<byte>(by => { str.Append(by.ToString("X2") + " "); return false; });
            else if (radioButton2.Checked)
                arg2.Data.Any<byte>(by => { str.Append(by.ToString() + " "); return false; });
            else
                str =new StringBuilder(encodingInfo.GetEncoding().GetString(arg2.Data));

            this.Invoke(new Action(() => this.textBox1.Text = prot.PortName+"接收到数据--------->\r\n" + str.ToString()+"\r\n"));
        }

        private void comboBoxComs_SelectedIndexChanged(object sender, EventArgs e)
        {
            serial = null;
            if (comboBoxComs.SelectedItem == null)
                return;
            string comname = comboBoxComs.SelectedItem.ToString();

            serial = list.FirstOrDefault(o => o.PortName == comname);
            if (serial == null || !serial.IsOpen)
            {
                label6.ForeColor = Color.Red;
                this.button2.Text = "连接";
            }
            else
            {
                this.button2.Text = "关闭";
                label6.ForeColor = Color.Green;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serial == null || !serial.IsOpen)
                button2_Click(null, null);

            string text = textBoxSend.Text;
            byte[] buffer = null;
            if (this.radioButtonSend10.Checked || this.radioButtonSend16.Checked)
            {
                text = text.Replace("\r\n", "");
                string[] ss = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                int parse = radioButtonSend10.Checked ? 10 : 16;
                try
                {
                    buffer = ss.Select<string, byte>(o => Convert.ToByte(o, parse)).ToArray();
                }
                catch (Exception)
                {
                    MessageBox.Show("数据中包含不合法的数字");
                    return;
                }
            }
            else
                buffer = encodingInfo.GetEncoding().GetBytes(text);

            if (buffer != null && buffer.Length > 0)
                serial.Write(buffer, 0, buffer.Length);

        }

        private void comboBoxEncodeing_SelectedIndexChanged(object sender, EventArgs e)
        {
            encodingInfo=(System.Text.EncodingInfo)comboBoxEncodeing.SelectedItem;
        }
    }
}
