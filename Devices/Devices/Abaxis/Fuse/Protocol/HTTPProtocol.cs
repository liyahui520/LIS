using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Timers;
using Devices.Abaxis;
namespace Devices.Abaxis.FuseProtocol
{

    // ----------   这台设备同时只能发送一个请求吗？？？？ 看你的代码貌似只能同时检查一个
    //我现在是按可以同时做多个写的代码
    public class HTTPProtocol : IProtocol
    {
        private Abaxis_Fuse abaxisDevice;
        private Log log;
        private FuseConfig fuseConfig;
        private Timer timer;

        public Exception Error { get; set; }

        public void Init(IDevices info)
        {
            timer = new Timer(10000);
            timer.Elapsed += Timer_Elapsed;
            abaxisDevice = (Abaxis_Fuse)info;
            log = new Log(abaxisDevice.Info.Name);
            fuseConfig = (FuseConfig)abaxisDevice.Config;
        }



        public bool Close()
        {
            if (timer.Enabled)
                timer.Stop();
            return true;
        }


        public bool Start()
        {
            try
            {
                //获取当前所有设备信息
                HttpResult<string> result = WebLogic.GetHttpResult<string>(CreateHttpItem(@"vetsync/v1/devices"));
                if (result.Success)
                {
                    string ncResultXML = result.Result;
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(ncResultXML);
                    XmlNode DataDictionaryUpdateInfo = xml.SelectSingleNode("Devices");
                    foreach (XmlNode node in DataDictionaryUpdateInfo.ChildNodes)
                    {
                        if (node["Type"] != null && node["Type"].InnerText == abaxisDevice.FuseCode)
                        {
                            abaxisDevice.FuseID = node["Id"].InnerText;
                            abaxisDevice.CommandsChanged += Vs2_CommandsChanged;
                            timer.Start();
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }

            return false;
        }


        #region 私有


        private void Vs2_CommandsChanged(IDevices dev, Command cmd)
        {
            //新添加的命令
            if (dev.CMDS.Contains(cmd))
                SendCommand(cmd);
            //取消了一个检查命令 
            else
                CancelCommand(cmd);
        }


        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="cmd"></param>
        private void SendCommand(Command cmd)
        {
            //------------------
            LabRequest request = new LabRequest();
            request.DeviceID = abaxisDevice.FuseID;
            request.TestCode = abaxisDevice.RequestCode;// "HEM"; //----------这是什么?
            LabRequests labRequests = new LabRequests();
            labRequests.LabRequest = new List<LabRequest>();
            labRequests.LabRequest.Add(request);
            //宠物信息
            AnimalDetails deatails = new AnimalDetails();
            deatails.AnimalID = cmd.PetId;
            deatails.AnimalName = cmd.PetName;
            deatails.Breed = cmd.KindOf.ToString(); //----------品种 这里是可以随意填还是有固定值？
            deatails.Gender = cmd.Gender.ToString();// "female";//性别
            deatails.Species = cmd.KindOf.ToString();// "DOG";//----------种类 这里是可以随意填还是有固定值？
            int age = Convert.ToInt32(cmd.Age);
            deatails.Age = string.Format("{0}Y{1}M0D", age / 12,age%12);// "1Y3M25D";
            deatails.DateOfBirth = DateTime.Now.AddMonths(-age).ToString("yyyy-MM");// "2018-08-11";
            deatails.AbbreviatedHistory = "";
            //主人信息
            Identification identifiObj = new Identification();
            identifiObj.ReportType = "Request";
            identifiObj.PracticeID = cmd.Id;    // ----------这两个字段是做什么的？
            identifiObj.PracticeRef = cmd.Code; // ----------这两个字段是做什么的?
            identifiObj.LaboratoryID = cmd.Id;
            identifiObj.PIMSName = "";           //----------这个是什么
            identifiObj.OwnerID = cmd.CustomerId;
            identifiObj.OwnerName = cmd.Customer;
            identifiObj.VetID = cmd.DoctorId;// "8888";//医生ID
            identifiObj.VetName = cmd.Doctor;// //医生姓名

            LabReport report = new LabReport();
            report.LabRequests = labRequests;
            report.AnimalDetails = deatails;
            report.Identification = identifiObj;
            string xmlStr = string.Empty;
            string xmlmessage = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                MemoryStream ms = new MemoryStream();
                XmlTextWriter textwriter = new XmlTextWriter(ms, Encoding.GetEncoding("UTF-8"));
                XmlSerializer xz = new XmlSerializer(typeof(LabReport));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                xz.Serialize(textwriter, report, ns);
                xmlmessage = Encoding.UTF8.GetString(ms.GetBuffer());
                //xz.Serialize(sw, report, ns);
                xmlStr = xmlmessage;
            }

            HttpItem httpItem = CreateHttpItem(@"vetsync/v1/orders");
            httpItem.Method = HttpMethod.Post;
            httpItem.Content = Encoding.UTF8.GetBytes(xmlStr);


            HttpResult<string> result = WebLogic.GetHttpResult<string>(httpItem);
            if (result.Success)
            {
                //result.Result   //----------此字段为返回的数据！ 返回什么表示成功？？？？
            }
        }

        /// <summary>
        /// 取消请求
        /// </summary>
        /// <param name="cmd"></param>
        private void CancelCommand(Command cmd)
        {
            //----------http://192.168.0.104:8080/vetsync/v1/orders/7777 ????  7777是表示查检号吗？
            //按practiceref字段搜索

            HttpItem httpItem = CreateHttpItem(@"vetsync/v1/orders/"+cmd.Code);
            httpItem.Method = HttpMethod.Delete;
            HttpResult<string> result = WebLogic.GetHttpResult<string>(httpItem);
            if (result.Success)
            {
                //result.Result   //----------此字段为返回的数据！ 返回什么表示成功？？？？
            }

        }

        /// <summary>
        /// 读取结果
        /// </summary>
        /// <param name="cmd"></param>
        private void ReadResult(Command cmd)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(commonURLs + "orders/6666/results");  ----------666是什么意思 

            HttpItem httpItem = CreateHttpItem(string.Format("vetsync/v1/orders/{0}/results",cmd.Code));
            HttpResult<string> result = WebLogic.GetHttpResult<string>(httpItem);
            if (result.Success)
            {
                Devices.Result cmdResult = new Result();
                cmdResult.CMD = cmd;
                cmdResult.Devices = abaxisDevice;
                cmdResult.Date = DateTime.Now;
                cmdResult.Source = result.Result;


                XmlDocument xml = new XmlDocument();
                xml.LoadXml(result.Result);
                XmlNode DataDictionaryUpdateInfo = xml.SelectSingleNode("LabReport");

                XmlNode nodelist = xml.SelectSingleNode("LabReport/LabResults/LabResult/LabResultItems");
                if (nodelist == null)
                    return;
                cmdResult.ResultDatas = new List<ResultItem>();
                foreach (XmlNode node in nodelist.ChildNodes)
                {
                    ResultItem item = new ResultItem();
                    item.Code = node["AnalyteCode"].InnerText;
                    item.Code = node["AnalyteCode"].InnerText;
                    item.Value = node["Result"].InnerText;
                    item.Unit = node["Units"].InnerText;
                    item.Min = node["LowRange"]?.InnerText;  // --------------此字段有可能为空吗?
                    item.Max = node["HighRange"]?.InnerText;  //--------------此字段有可能为空吗?
                    cmdResult.ResultDatas.Add(item);
                }
                abaxisDevice.ResultComplete(cmdResult);
            }
        }

        /// <summary>
        /// 查询查一个检查是否完成
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private bool GetStatus(Command cmd)
        {
            //HttpItem httpItem = CreateHttpItem(@"vetsync/v1/orders/UNREQ-1708/status?rel=status");

            HttpItem httpItem = CreateHttpItem(string.Format(@"vetsync/v1/orders/{0}/status?rel=status",cmd.Code));
            HttpResult<string> result = WebLogic.GetHttpResult<string>(httpItem);
            if (result.Success)
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(result.Result);
                XmlNode node = xml.SelectSingleNode("order");
                if (node["status"] != null && node["status"].InnerText == "Done")
                    return true;
            }
            return false;
        }


        /// <summary>
        /// 每十秒检查一次是否完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (abaxisDevice.CMDS != null && abaxisDevice.CMDS.Count > 0)
            {
                Command cmd = null;
                lock (abaxisDevice.CMDS)
                {
                    foreach (var item in abaxisDevice.CMDS)
                    {
                        if (GetStatus(item))
                        {
                            cmd = item;
                            break;
                        }
                    }
                }
                ReadResult(cmd);
            }
        }

        private HttpItem CreateHttpItem(string apiAddress)
        {
            string url = @"http://192.168.0.104:8080/";
            url += apiAddress;
            List<string> headers = new List<string>();
            headers.Add("rel: devices");
            string code = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", fuseConfig.LoginName, fuseConfig.LoginPassword)));
            headers.Add("Authorization: " + "Basic " + code);
            return new HttpItem
            {
                Url = url,
                Method = HttpMethod.Get,
                ContentType = "application/xml",
                Headers = headers
            };
        }

        #endregion
    }



    #region
    [Serializable]
    [XmlRoot("LabReport")]
    public class LabReport
    {
        public LabReport() //XmlSerializer序列化要求一定要有无参数构造函数 
        {
            Identification = null;
            AnimalDetails = null;
            LabRequests = null;
        }
        //[XmlElement("Identification")]
        //[XmlAttribute("id")]
        public Identification Identification { get; set; }
        public AnimalDetails AnimalDetails { get; set; }

        public LabRequests LabRequests { get; set; }
    }
    [Serializable]
    public class Identification
    {
        public Identification() //XmlSerializer序列化要求一定要有无参数构造函数 
        {
            ReportType = string.Empty;
            PracticeID = string.Empty;
            PracticeRef = string.Empty;
            LaboratoryID = string.Empty;
            PIMSName = string.Empty;
            OwnerID = string.Empty;
            OwnerName = string.Empty;
            VetID = string.Empty;
            VetName = string.Empty;
        }
        public string ReportType { get; set; }
        public string PracticeID { get; set; }
        public string PracticeRef { get; set; }
        public string LaboratoryID { get; set; }
        public string PIMSName { get; set; }
        public string OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string VetID { get; set; }
        public string VetName { get; set; }
    }
    [Serializable]
    public class AnimalDetails
    {
        public AnimalDetails() //XmlSerializer序列化要求一定要有无参数构造函数 
        {
            AnimalID = string.Empty;
            AnimalName = string.Empty;
            Breed = string.Empty;
            Gender = string.Empty;
            Species = string.Empty;
            Age = string.Empty;
            DateOfBirth = string.Empty;
            AbbreviatedHistory = string.Empty;
        }
        public string AnimalID { get; set; }
        public string AnimalName { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public string Species { get; set; }
        public string Age { get; set; }
        public string DateOfBirth { get; set; }
        public string AbbreviatedHistory { get; set; }
    }
    [Serializable]
    public class LabRequests
    {
        public LabRequests() //XmlSerializer序列化要求一定要有无参数构造函数 
        {
            LabRequest = null;
        }
        //[XmlArray("LabRequest")]
        //[XmlArrayItem("LabRequest")]
        [XmlElement("LabRequest")]
        public List<LabRequest> LabRequest { get; set; }
    }
    [Serializable]
    public class LabRequest
    {
        public LabRequest() //XmlSerializer序列化要求一定要有无参数构造函数 
        {
            TestCode = string.Empty;
            DeviceID = string.Empty;
        }
        public string TestCode { get; set; }
        public string DeviceID { get; set; }
    }
    #endregion

}
