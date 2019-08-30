using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerialPort;
namespace Devices.Mindray.BC_2800Protocol
{
    public class SeriaProtocol : IProtocol
    {
        private MySerialPort Serial;
        private List<byte> buffer;
        private BC_2800 bc2800;
        private Log log;
        private BC_2800Config bc2800Config;
        private List<BC2800ResultDataItem> dataItems;
        public Exception Error { get; set; }

        public void Init(IDevices devices)
        {
            bc2800 = (BC_2800)devices;
            log = new Log(bc2800.Info.Name);
            bc2800Config = (BC_2800Config)bc2800.Config;
            buffer = new List<byte>();

            dataItems = new List<BC2800ResultDataItem>();
            dataItems.Add(new BC2800ResultDataItem { Code = "WBC", dataLength = 4, Multiple = 10 });
            dataItems.Add(new BC2800ResultDataItem { Code = "Lymph#", dataLength = 4, Multiple = 10 });
            dataItems.Add(new BC2800ResultDataItem { Code = "Mon#", dataLength = 4, Multiple = 10 });
            dataItems.Add(new BC2800ResultDataItem { Code = "Gran#", dataLength = 4, Multiple = 10 });

            dataItems.Add(new BC2800ResultDataItem { Code = "Lymph%", dataLength = 3, Multiple = 10 });
            dataItems.Add(new BC2800ResultDataItem { Code = "Mon%", dataLength = 3, Multiple = 10 });
            dataItems.Add(new BC2800ResultDataItem { Code = "Gran%", dataLength = 3, Multiple = 10 });

            dataItems.Add(new BC2800ResultDataItem { Code = "RBC%", dataLength = 4, Multiple = 100 });
            dataItems.Add(new BC2800ResultDataItem { Code = "HGB", dataLength = 3, Multiple = 1 });
            dataItems.Add(new BC2800ResultDataItem { Code = "MCHC", dataLength = 4, Multiple = 1 });
            dataItems.Add(new BC2800ResultDataItem { Code = "MCV", dataLength = 4, Multiple = 10 });
            dataItems.Add(new BC2800ResultDataItem { Code = "MCH", dataLength = 4, Multiple = 10 });
            dataItems.Add(new BC2800ResultDataItem { Code = "RDW", dataLength = 3, Multiple = 10 });

            dataItems.Add(new BC2800ResultDataItem { Code = "HCT", dataLength = 3, Multiple = 10 });

            dataItems.Add(new BC2800ResultDataItem { Code = "PLT", dataLength = 4, Multiple = 1 });
            dataItems.Add(new BC2800ResultDataItem { Code = "MPV", dataLength = 3, Multiple = 10 });
            dataItems.Add(new BC2800ResultDataItem { Code = "PDW", dataLength = 3, Multiple = 10 });
            dataItems.Add(new BC2800ResultDataItem { Code = "PCT", dataLength = 3, Multiple = 1000 });
        }

        public bool Close()
        {
            if (Serial != null)
            {
                Serial.Close();
                Serial.Dispose();
                Serial = null;
            }
            return true;
        }


        public bool Start()
        {
            if (Serial == null)
            {
                SerialPortConfig serialPortConfig = bc2800Config.SerialPortConfig;
                Serial = new MySerialPort(serialPortConfig.PortName, serialPortConfig.Baud, serialPortConfig.Parity, serialPortConfig.DataBits, serialPortConfig.StopBits);
                Serial.Encoding = Encoding.GetEncoding(serialPortConfig.EncodingName);
                Serial.SerialDataReceivedCompleteEventHandler += Serial_SerialDataReceivedCompleteEventHandler;
                Serial.DtrEnable = true;
                Serial.RtsEnable = true;
            }
            if (!Serial.IsOpen)
            {
                if (!Serial.Open())
                {
                    this.Error = Serial.Erroe;
                    return false;
                }
            }
            return true;
        }

        private void Serial_SerialDataReceivedCompleteEventHandler(object obj, SerialPort.SerialDataReceivedCompleteEventArgs arg)
        {
            Analysis(arg.Data);
        }

        /// <summary>
        /// 分析数据
        /// </summary>
        /// <param name="lst_Items"></param>
        /// <param name="data"></param>
        private void Analysis(byte[] data)
        {
            try
            {
                if (data != null && data.Length > 0)
                    buffer.AddRange(data);
                int end = buffer.IndexOf(26);
                if (end == -1)
                    return;

                end++;
                byte[] tmp = new byte[end];
                buffer.CopyTo(0, tmp, 0, end);
                buffer.RemoveRange(0, end);
                Paser(tmp);
                Analysis(null);
            }
            catch (Exception ex)
            {
                log.Write("分析数据出错:" + ex.StackTrace, "", LogType.Error);
            }
        }

        private void Paser(byte[] data)
        {
            Result result = SeveResult(data);
            bc2800.ResultComplete(result);
        }

        private Result SeveResult(byte[] tmp)
        {
            List<Command> CMDS = bc2800.CMDS;
            if (CMDS == null || CMDS.Count == 0)
                return new Result { CMD = null, ResultDatas = null, Devices = bc2800 };

            #region 解析
            ResultConfig rc = null;
            int index = 23;
            
            Encoding encoding = System.Text.Encoding.GetEncoding(bc2800Config.SerialPortConfig.EncodingName);

            Devices.Result result = new Result { Devices = bc2800 };
            result.CMD = CMDS[0];

            if (bc2800Config.ResultConfig.Any(o => o.InvokeFormula(result.CMD)))
                rc = bc2800Config.ResultConfig.First(o => o.InvokeFormula(result.CMD));

            result.ResultDatas = new List<ResultItem>();
            string dt = encoding.GetString(tmp, 0, tmp.Length);
            foreach (var item in dataItems)
            {
                string sd = encoding.GetString(tmp, index, item.dataLength);
                double value = 0;
                if (double.TryParse(sd, out value))
                {
                    ResultItem ri = new ResultItem { Code = item.Code, EnglishName = item.Code, Name = item.Code, Value = value / item.Multiple, Display = item.Code };
                    result.ResultDatas.Add(ri);
                    index += item.dataLength;
                }
            }
            return result;
            #endregion
        }

        private class BC2800ResultDataItem
        {
            public string Code { get; set; }
            public int dataLength { get; set; }
            public int Multiple { get; set; }
        }




    }
}
