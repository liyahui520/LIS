using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerialPort;
namespace Devices.Fujifilm.FF_6450Protocol
{
    public class SeriaProtocol : IProtocol
    {
        private MySerialPort Serial;
        private List<byte> buffer;
        private FF_6450 ff6450;
        private Log log;
        private FF_6450Config ff6450config;
        private List<FF_6450ResultDataItem> dataItems;
        public Exception Error { get; set; }

        public void Init(IDevices dirff6450)
        {
            ff6450 = (FF_6450)dirff6450;
            log = new Log(ff6450.Info.Name);
            ff6450config = (FF_6450Config)ff6450.Config;
            buffer = new List<byte>();
            dataItems = new List<FF_6450ResultDataItem>();
            dataItems.Add(new FF_6450ResultDataItem { Code = "WBC", dataLength = 4, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "EO%", dataLength = 5, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "EO#", dataLength = 2, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "RBC", dataLength = 1, Multiple = 10 });

            dataItems.Add(new FF_6450ResultDataItem { Code = "HGB", dataLength = 1, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "HCT", dataLength = 1, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "MCV", dataLength = 1, Multiple = 10 });

            dataItems.Add(new FF_6450ResultDataItem { Code = "MCH", dataLength = 1, Multiple = 100 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "MCHC", dataLength = 1, Multiple = 1 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "RDW", dataLength = 1, Multiple = 1 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "PLT", dataLength = 1, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "PCT", dataLength = 1, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "MPV", dataLength = 1, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "PDW", dataLength = 1, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "LY%", dataLength = 1, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "MO%", dataLength = 1, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "GR%", dataLength = 1, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "LY#", dataLength = 1, Multiple = 1000 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "MO#", dataLength = 1, Multiple = 10 });
            dataItems.Add(new FF_6450ResultDataItem { Code = "GR#", dataLength = 1, Multiple = 1000 });
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
                SerialPortConfig serialPortConfig = ff6450config.SerialPortConfig;
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
        void Serial_SerialDataReceivedCompleteEventHandler(object obj, SerialPort.SerialDataReceivedCompleteEventArgs arg)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in arg.Data)
                sb.Append(item.ToString("X") + " ");
            log.Write("接收到数据:" + sb.ToString());

            communication_RecevieResultEvent(arg.Data);
        }

        private void communication_RecevieResultEvent(byte[] data)
        {
            try
            {
                if (data != null && data.Length > 0)
                    buffer.AddRange(data);
                int start = buffer.IndexOf(2);
                int end = buffer.IndexOf(3);
                if (start == -1 || end == -1)
                    return;
                start += 1;
                SerialPortConfig serialPortConfig = ff6450config.SerialPortConfig;
                string str = Encoding.GetEncoding(serialPortConfig.EncodingName).GetString(buffer.ToArray(), start, end - start);
                buffer.RemoveRange(0, end + 1);
                log.Write("接收到数据:" + str);
                Result result = Paser(str);
                ff6450.ResultComplete(result);
                communication_RecevieResultEvent(null);
            }
            catch (Exception ex)
            {
                log.Write("分析数据出错:" + ex.Message + "\r\n" + ex.StackTrace, "", LogType.Error);
            }
        }

        public Result Paser(string str)
        {
            List<Command> CMDS = ff6450.CMDS;
            #region 解析
            if (CMDS == null)
                return new Result { CMD = null, ResultDatas = null, Devices = ff6450 };

            string[] inputData = str.Split(Environment.NewLine.ToCharArray());

            string id = inputData[17].Trim();
            Devices.Result result = new Result { Devices = ff6450 };

            if (!CMDS.Any(o => o.Id == id))
                result.CMD = CMDS[0];
            else
                result.CMD = CMDS.LastOrDefault(o => o.Id == id);

            DateTime startDatere = DateTime.Parse(inputData[10].Trim() + "-" + inputData[11].Trim() + "-" + inputData[12].Trim() + " " + inputData[14].Trim() + ":" + inputData[15].Trim() + ":" + inputData[16].Trim());

            result.Source = str;
            ResultConfig rc = null;
            foreach (ResultConfig item in ff6450config.ResultConfig)
            {
                if (item.InvokeFormula(result.CMD))
                {
                    rc = item;
                    break;
                }
            }

            result.ResultDatas = new List<ResultItem>();

            int index = 18;
            foreach (var item in dataItems)
            {
                ResultItem ri = new ResultItem { Code = item.Code, EnglishName = item.Code, Name = item.Code, Value = inputData[index], Display = item.Code };
                ResultItemConfig itemconfig = null;
                if (rc != null && rc.Items != null && rc.Items.Any(o => o.Code == item.Code))
                    itemconfig = rc.Items.First(o => o.Code == item.Code);
                if (itemconfig != null)
                {
                    ri.Name = itemconfig.Name;
                    ri.Max = itemconfig.Max;
                    ri.Min = itemconfig.Min;
                    ri.Display = itemconfig.Display;
                    ri.Unit = itemconfig.Unit;
                }
                result.ResultDatas.Add(ri);
                index += item.dataLength;
            }


            if (result.ResultDatas.Count > 0 && rc != null && rc.Items.Count > 0)
            {
                for (int i = rc.Items.Count - 1; i >= 0; i--)
                {
                    ResultItem ri = result.ResultDatas.FirstOrDefault(o => o.Code == rc.Items[i].Code);
                    if (ri != null)
                    {
                        result.ResultDatas.Remove(ri);
                        result.ResultDatas.Insert(0, ri);
                    }
                }
            }


            return result;
            #endregion
        }

        private class FF_6450ResultDataItem
        {
            public string Code { get; set; }
            public int dataLength { get; set; }
            public int Multiple { get; set; }
        }

    }
}
