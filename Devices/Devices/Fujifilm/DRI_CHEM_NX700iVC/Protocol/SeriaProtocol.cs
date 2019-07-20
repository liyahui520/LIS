using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerialPort;
namespace Devices.Fujifilm.DRI_CHEM_NX700iVCProtocol
{
    public class SeriaProtocol : IProtocol
    {
        private MySerialPort Serial;
        private List<byte> buffer;
        private DRI_CHEM_NX700iVC nx700;
        private Log log;
        private DRI_CHEM_NX700iVCConfig nx700config;

        public Exception Error { get; set; }

        public void Init(IDevices dirnx700)
        {
            nx700 = (DRI_CHEM_NX700iVC)dirnx700;
            log = new Log(nx700.Info.Name);
            nx700config = (DRI_CHEM_NX700iVCConfig)nx700.Config;
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
                SerialPortConfig serialPortConfig = nx700config.SerialPortConfig;
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
                SerialPortConfig serialPortConfig = nx700config.SerialPortConfig;
                string str = System.Text.Encoding.GetEncoding(serialPortConfig.EncodingName).GetString(buffer.ToArray(), start, end - start);
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
                    nx700.ResultComplete(result);
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
            List<Command> CMDS = nx700.CMDS;
            if (CMDS == null || CMDS.Count == 0)
                return "I,0,,,,,," + (char)0x3;

            //int count = CMDS.Count >= 4 ? 4 : CMDS.Count;
            int count = CMDS.Count;
            StringBuilder sb = new StringBuilder("I,");
            sb.Append(CMDS.Count);
            lock (CMDS)
            {
                for (int i = 0; i < count; i++)
                {
                    Command item = CMDS[i];
                    //检查号
                    if (i == 0)
                        sb.Append(",");
                    sb.Append(item.Id.PadRight(13, ' '));
                    //宠物ID
                    sb.Append("," + item.PetId.PadRight(13, ' '));

                    //宠物名
                    string name = Hz2Py.ChineseToPinYin(item.PetName).ToLower();
                    if (name.Length > 13)
                        name = name.Remove(12);

                    sb.Append("," + name.PadRight(13, ' '));

                    ////结果集编码
                    //sb.Append("," + item.Code);


                    //年龄
                    int age = 1;
                    if (int.TryParse(item.Age, out age))
                        age = age / 12;

                    //品种
                    int kindof = 14;
                    switch (item.KindOf)
                    {
                        case KindOfType.犬:
                            if (age < 1)
                                kindof = 21;
                            break;
                        case KindOfType.猫:
                            if (age < 1)
                                kindof = 22;
                            else
                                kindof = 15;
                            break;
                        case KindOfType.兔:
                            kindof = 16;
                            break;
                        case KindOfType.小鼠:
                        case KindOfType.大鼠:
                            kindof = 19;
                            break;
                    }


                    sb.Append("," + kindof.ToString().PadRight(2, ' '));
                    //性别 0雄 1雌
                    int Gender = 0;
                    if (item.Gender == GenderType.Male)
                        Gender = 1;
                    sb.Append("," + Gender);

                    //添加年龄
                    sb.Append("," + age.ToString().PadRight(3, ' '));
                    //添加区块传输结束符
                    sb.Append((char)0x17);
                }
            }
            sb.Remove(sb.Length - 1, 1);
            //添加结束符
            sb.Append((char)0x3);
            log.Msg("发送数据：" + sb.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// 组织设备查询单个检查的返回值 
        /// </summary>
        /// <returns></returns>
        private string FormatSelect(string id)
        {
            List<Command> CMDS = nx700.CMDS;
            if (!CMDS.Any(o => o.Id == id))
                return string.Format("W,{0},{1},{2},{3}{4}", id, null, null, 0, (char)3);
            Command cmd = CMDS.First(o => o.Id == id);
            int count = 0;
            string names = cmd.Tag as string;
            if (!string.IsNullOrEmpty(names))
                count = names.Count(c => c == ',');
            else
                names = "";
            string sendstr = string.Format("W,{0},{1},{2},{3}{4}{5}", id, cmd.PetId, Hz2Py.ChineseToPinYin(cmd.PetName).ToLower(), count, names, (char)3);
            log.Msg("发送数据：" + sendstr);
            return sendstr;
            //return string.Format("W,{0},{1},{2},{3}{4}{5}", id, cmd.PetId, cmd.PetName, count, names, (char)3);
        }

        private Result SeveResult(string inputString)
        {
            List<Command> CMDS = nx700.CMDS;
            #region 解析
            if (CMDS == null)
                return new Result { CMD = null, ResultDatas = null, Devices = nx700 };

            string[] strs = inputString.Split(',');
            string methodre = strs[1];
            DateTime startDatere = DateTime.Parse(strs[2] + " " + strs[3]);
            string id = strs[4].Trim();
            if (!CMDS.Any(o => o.Id == id))
                return new Result { CMD = null, ResultDatas = null, Devices = nx700 };

            Devices.Result result = new Result { Devices = nx700 };
            result.CMD = CMDS.LastOrDefault(o => o.Id == id);
            result.Source = inputString;
            ResultConfig rc = null;
            foreach (ResultConfig item in nx700.Config.ResultConfig)
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



    }
}
