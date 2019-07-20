using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerialPort;
namespace Devices.Fujifilm.DRI_CHEM_NX500iVCProtocol
{
    public class SeriaProtocol : IProtocol
    {
        private MySerialPort Serial;
        private List<byte> buffer;
        private DRI_CHEM_NX500iVC nx500;
        private Log log;
        private DRI_CHEM_NX500iVCConfig nx500config;

        public Exception Error { get; set; }

        public void Init(IDevices dirnx500)
        {
            nx500 = (DRI_CHEM_NX500iVC)dirnx500;
            log = new Log(nx500.Info.Name);
            nx500config = (DRI_CHEM_NX500iVCConfig)nx500.Config;
            buffer = new List<byte>();
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
                SerialPortConfig serialPortConfig = nx500config.SerialPortConfig;
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
                int start = buffer.IndexOf(2);
                int end = buffer.IndexOf(3);
                if (start == -1 || end == -1)
                    return;
                start += 1;
                SerialPortConfig serialPortConfig = ((DRI_CHEM_NX500iVCConfig)nx500.Config).SerialPortConfig;
                string str = Encoding.GetEncoding(serialPortConfig.EncodingName).GetString(buffer.ToArray(), start, end - start);
                buffer.RemoveRange(0, end + 1);
                log.Write("接收到数据:" + str);
                Paser(str);
                Analysis(null);
            }
            catch (Exception ex)
            {
                log.Write("分析数据出错:" + ex.StackTrace, "", LogType.Error);
            }
        }

        private void Paser(string data)
        {
            char cmd = data[0];
            switch (cmd)
            {
                case 'I'://请求队列
                    Serial.Send(FormatSelectList());
                    break;
                case 'W'://请求查看样本信息
                    string[] datas = data.Split(',');
                    Serial.Send(FormatSelect(datas[1]));
                    break;
                case 'R': //结果 
                    Result result = SeveResult(data);
                    nx500.ResultComplete(result);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 组织设备查询当前队例的返回值
        /// </summary>
        /// <returns></returns>
        private string FormatSelectList()
        {
            List<Command> CMDS = nx500.CMDS;
            if (CMDS == null || CMDS.Count == 0)
                return "I,0,,,,,," + (char)3;

            int count = CMDS.Count >= 4 ? 4 : CMDS.Count;
            StringBuilder sb = new StringBuilder("I,");
            sb.Append(CMDS.Count);
            lock (CMDS)
            {
                for (int i = 0; i < count; i++)
                {
                    Command item = CMDS[i];
                    //检查号
                    sb.Append("," + item.Id);
                    //宠物ID
                    sb.Append("," + item.PetId);
                    //宠物名
                    sb.Append("," + item.PetName);
                    //结果集编码
                    sb.Append("," + item.Code);
                    //性别 0雄 1雌
                    sb.Append("," + (item.Gender - 1));
                    //年龄
                    sb.Append("," + item.Age);
                    //添加区块传输结束符
                    sb.Append((char)23);
                }
            }
            sb.Remove(sb.Length - 1, 1);
            //添加结束符
            sb.Append((char)3);
            return sb.ToString();
        }

        /// <summary>
        /// 组织设备查询单个检查的返回值 
        /// </summary>
        /// <returns></returns>
        private string FormatSelect(string id)
        {
            List<Command> CMDS = nx500.CMDS;
            if (!CMDS.Any(o => o.Id == id))
                return string.Format("W,{0},{1},{2},{3}{4}", id, null, null, 0, (char)3);
            Command cmd = CMDS.First(o => o.Id == id);
            return string.Format("W,{0},{1},{2},2,{3},{4}{5}", id, cmd.PetId, cmd.PetName, id, cmd.PetName, (char)3);
            //W,2000000507,33669717,33669717,2,NO:2000000507,Pet:33669717
        }

        private Result SeveResult(string inputString)
        {
            List<Command> CMDS = nx500.CMDS;
            #region 解析
            if (CMDS == null)
                return new Result { CMD = null, ResultDatas = null, Devices = nx500 };

            string[] strs = inputString.Split(',');
            string methodre = strs[1];
            DateTime startDatere = DateTime.Parse(strs[2] + " " + strs[3]);
            string id = strs[4].Trim();
            //string petId = strs[5];
            //string petName = strs[6];
            //string kindof = strs[7];
            //string geinder = strs[8];
            //string age = strs[9];
            //string resultCount = strs[11];
            if (!CMDS.Any(o => o.Id == id))
                return new Result { CMD = null, ResultDatas = null, Devices = nx500 };

            Devices.Result result = new Result { Devices = nx500 };
            result.CMD = CMDS.LastOrDefault(o => o.Id == id);
            result.Source = inputString;
            ResultConfig rc = null;
            foreach (ResultConfig item in nx500config.ResultConfig)
            {
                if (item.InvokeFormula(result.CMD))
                {
                    rc = item;
                    break;
                }
            }

            result.ResultDatas = new List<ResultItem>();

            for (int i = 12; i < strs.Length; i += 7)
            {
                ResultItem item = new ResultItem();
                //读取数据
                item.Code = strs[i].Replace("-PS", "").Trim();
                item.EnglishName = item.Code;
                string[] cb = strs[i + 2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                item.Value = cb[0].Trim();
                if (cb.Length > 1)
                    item.Unit = cb[1].Trim();
                item.Min = strs[i + 4].Trim();
                item.Max = strs[i + 5].Trim();

                item.Name = item.Code.Trim();
                item.Display = item.Code.Trim();

                //设置显示
                ResultItemConfig itemconfig = null;
                if (rc != null && rc.Items != null && rc.Items.Any(o => o.Code == item.Code))
                    itemconfig = rc.Items.First(o => o.Code == item.Code);
                if (itemconfig != null)
                {
                    item.EnglishName = itemconfig.EnglishName;
                    item.Min = itemconfig.Min;
                    item.Max = itemconfig.Max;
                    item.Unit = itemconfig.Unit;
                    item.Name = itemconfig.Name;
                    item.Display = itemconfig.Display;
                }
                result.ResultDatas.Add(item);
            }
            return result;
            #endregion
        }




    }
}
