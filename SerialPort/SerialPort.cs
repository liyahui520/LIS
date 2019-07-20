using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.ComponentModel;
namespace SerialPort
{
    public class MySerialPort : System.IO.Ports.SerialPort
    {
        private Action<object, SerialDataReceivedCompleteEventArgs> serialDataReceivedComplete;
        private SerialConnectionState spe;
        private Exception _ex;

        public MySerialPort()
            : base()
        {
            SetEvent();
        }

        public MySerialPort(IContainer container)
            : base(container)
        {
            SetEvent();
        }
        public MySerialPort(string name)
            : base(name)
        {
            SetEvent();
        }
        public MySerialPort(string portName, int baudRate)
            : base(portName, baudRate)
        {
            SetEvent();
        }
        public MySerialPort(string portName, int baudRate, Parity parity)
            : base(portName, baudRate, parity)
        {
            SetEvent();
        }
        public MySerialPort(string portName, int baudRate, Parity parity, int dataBits)
            : base(portName, baudRate, parity, dataBits)
        {
            SetEvent();
        }
        public MySerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
            : base(portName, baudRate, parity, dataBits, stopBits)
        {
            SetEvent();
        }
        public SerialConnectionState State
        {
            get { return spe; }
            set { spe = value; }
        }
        public Exception Erroe
        {
            get { return _ex; }
            set { _ex = value; }
        }



        public new bool Open()
        {
            try
            {
                base.Open();
                if (CDHolding && CtsHolding)
                {
                    State = SerialConnectionState.Opened;
                    return true;
                }
                State = SerialConnectionState.NoTXD;
                return true;
            }
            catch (Exception ex)
            {
                State = SerialConnectionState.Closed;
                _ex = ex;
                return false;
            }
        }


        public bool Send(byte[] buffer)
        {
            List<byte> temp = new List<byte>();
            temp.Add(2);
            temp.AddRange(buffer);
            ParityReplace = ParityData(buffer);
            temp.Add(ParityReplace);
            Write(temp.ToArray(), 0, buffer.Length + 2);
            return true;
        }
        public bool Send(string str)
        {
#if DEBUG
            System.Console.Write(str + "\r\n");
#endif
            byte[] buffer = this.Encoding.GetBytes(str);
            return Send(buffer);
        }

        /// <summary>
        /// 接收到串口设备的完整数据时处理
        /// </summary>
        public event Action<object, SerialDataReceivedCompleteEventArgs> SerialDataReceivedCompleteEventHandler
        {
            add
            {
                serialDataReceivedComplete += value;
            }
            remove
            {
                serialDataReceivedComplete -= value;
            }
        }
        private void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[this.BytesToRead];
            int read = 0;
            int index = 0;
            int count = buffer.Length;
            do
            {
                index += read;
                count -= read;
                read = Read(buffer, index, count);
            } while (read != 0);
            if (serialDataReceivedComplete != null)
                serialDataReceivedComplete(this, new SerialDataReceivedCompleteEventArgs { Data = buffer, EventType = e.EventType });
        }

        private void SetEvent()
        {
            DataReceived += port_DataReceived;
            ErrorReceived += MySerialPort_ErrorReceived;
            PinChanged += MySerialPort_PinChanged;
        }
        private byte ParityData(byte[] buffer)
        {
            byte parity = 0;
            foreach (var item in buffer)
                parity ^= item;

            switch (Parity)
            {
                case Parity.Even:
                    if ((parity & (byte)255) % 2 != 0)
                        parity |= 128;
                    break;
                case Parity.Mark:
                    parity |= 128;
                    break;
                case Parity.Odd:
                    if ((parity & (byte)255) % 2 != 1)
                        parity |= 128;
                    break;
                case Parity.Space:
                    parity <<= 1;
                    parity >>= 1;
                    break;
            }
            return parity;
        }
        private void MySerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MySerialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            switch (e.EventType)
            {
                case SerialPinChange.Break:
                    break;
                case SerialPinChange.CDChanged:
                case SerialPinChange.CtsChanged:
                case SerialPinChange.DsrChanged:
                    State = (this.CDHolding && this.CtsHolding) ? SerialConnectionState.Opened : SerialConnectionState.NoTXD;
                    break;
                case SerialPinChange.Ring:
                    break;
            }

            MySerialPort a = this;
        }
    }


    public class SerialDataReceivedCompleteEventArgs : System.EventArgs
    {
        public byte[] Data { get; set; }
        public SerialData EventType { get; set; }
    }
}
