using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using SerialPort;
namespace Devices
{
    public partial class UCSerialPortConfig : UserControl
    {
        SerialPortConfig spc;
        public UCSerialPortConfig(SerialPortConfig config)
        {
            InitializeComponent();
            spc = config;
            LoadData();
        }
        public UCSerialPortConfig()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData(SerialPortConfig config)
        {
            spc = config;
            UCSerialPort_Load(null,null);
        }
        public SerialPortConfig GetSerialPortConfig()
        {
            SerialPortConfig config = new SerialPortConfig();
            config.PortName = comboBoxComs.SelectedValue.ToString();
            config.Baud = Convert.ToInt32(comboBoxBaud.SelectedValue);
            config.Parity = (Parity)comboBoxParity.SelectedItem;
            config.StopBits=(StopBits)comboBoxStopBits.SelectedItem;
            config.EncodingName = comboBoxEncodeing.SelectedValue.ToString();
            config.DataBits = int.Parse(this.textBoxDataBits.Text);
            return config;
        }
        private void UCSerialPort_Load(object sender, EventArgs e)
        {
            if (this.spc != null)
            {
                comboBoxComs.SelectedItem = spc.PortName;
                comboBoxBaud.SelectedItem = spc.Baud;
                comboBoxParity.SelectedItem = spc.Parity;
                comboBoxStopBits.SelectedItem = spc.StopBits;
                comboBoxEncodeing.SelectedValue = spc.EncodingName;
                textBoxDataBits.Text = spc.DataBits.ToString();
            }
        }
        private void LoadData()
        {
            this.comboBoxComs.DataSource = SerialPort.MySerialPort.GetPortNames();
            this.comboBoxBaud.DataSource = new int[] { 2400, 4800, 9600, 14400, 19200, 38400, 56000, 57600, 115200 };
            comboBoxBaud.SelectedIndex = 2;
            this.comboBoxParity.DataSource = Enum.GetValues(typeof(Parity));
            this.comboBoxStopBits.DataSource = Enum.GetValues(typeof(StopBits));
            comboBoxStopBits.SelectedIndex = 1;
            this.comboBoxEncodeing.DataSource = System.Text.Encoding.GetEncodings();
            this.comboBoxEncodeing.DisplayMember = "DisplayName";
            this.comboBoxEncodeing.ValueMember = "Name";
            comboBoxEncodeing.SelectedValue = "gb2312";
        }
    }
}
