using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerialPort;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
namespace Devices.IDEXX.IDEXX_VetLab_StationProtocol
{
    public class FileProtocol : IProtocol
    {
        private IDEXX_VetLab_Station idexx;
        private Log log;
        private IDEXX_VetLab_StationConfig idexxConfig;
        private object LockObj = null;

        private FileSystemWatcher watcher;
        public Exception Error { get; set; }

        public void Init(IDevices diridexx)
        {
            LockObj = "lock";
            idexx = (IDEXX_VetLab_Station)diridexx;
            log = new Log(idexx.Info.Name);
            idexxConfig = (IDEXX_VetLab_StationConfig)idexx.Config;
        }

        public bool Close()
        {
            if (watcher != null)
            {
                watcher.Changed -= Watcher_Changed;
                watcher.Changed -= Watcher_Changed;
            }
            return true;
        }

        public bool Start()
        {
            if (!Directory.Exists(idexxConfig.ResultPath))
                Directory.CreateDirectory(idexxConfig.ResultPath);
            string[] xmls = Directory.GetFiles(idexxConfig.ResultPath, "*.xml");

            if (xmls != null || xmls.Length > 0)
                foreach (var item in xmls)
                    Watcher_Changed(null, new FileSystemEventArgs(WatcherChangeTypes.Created, idexxConfig.ResultPath, item.Substring(item.LastIndexOf("\\") + 1)));

            if (watcher == null)
            {
                watcher = new FileSystemWatcher(idexxConfig.ResultPath);
                watcher.Filter = "*.xml";
                watcher.EnableRaisingEvents = true;
            }
            watcher.Created += Watcher_Changed;
            watcher.Changed += Watcher_Changed;
            return true;
        }





        public void SendCMD(Command cmd, bool isNew)
        {
            
            string noticeFile = null;
            string requestFile = null;
            if (isNew)
            {
                noticeFile = string.Format("{0}_NI_10001_{1}_Notice.xml", DateTime.Now.ToString("yyMMddHHmmssfff"), cmd.PetId);
                requestFile = string.Format("{0}_NI_10001_{1}_{2}_Request.xml", DateTime.Now.ToString("yyMMddHHmmssfff"), cmd.PetId, cmd.Id);
            }
            else
            {
                noticeFile = string.Format("{0}_NO_10001_{1}_Notice.xml", DateTime.Now.ToString("yyMMddHHmmssfff"), cmd.PetId);
                requestFile = string.Format("{0}_NC_10001_{1}_{2}_Request.xml", DateTime.Now.ToString("yyMMddHHmmssfff"), cmd.PetId, cmd.Id);
            }
            //Tool.ObjectSaveToXML(GetIdexxMessage(CmdType.census_20, cmd, isNew), config.RequestPath + noticeFile);
            //Tool.ObjectSaveToXML(GetIdexxMessage(CmdType.work_request_20, cmd, isNew), config.RequestPath + requestFile);
            Serialize(GetIdexxMessage(CmdType.census_20, cmd, isNew), idexxConfig.RequestPath + noticeFile);
            Serialize(GetIdexxMessage(CmdType.work_request_20, cmd, isNew), idexxConfig.RequestPath + requestFile);

        }

        private MessageBase GetIdexxMessage(CmdType cmdType, Command cmd, bool cancel)
        {
            
            MessageBase mess = new Message_request();
            if (cmdType == CmdType.census_20)
                mess = new Message_notice();
            mess.header = new header();
            mess.header.from_application_id = " ";
            mess.header.to_application_id = " ";
            mess.DOCTYPE = cmdType == CmdType.census_20 ? "census_20.dtd" : "work_request_20.dtd";
            mess.message_id = cmd.Id;
            mess.message_dt = cmd.Date.ToString(idexxConfig.DateFormat);
            mess.message_type = cmdType == CmdType.census_20 ? "Census_Notice" : "Work_Request";
            mess.message_dtd_version_number = "2.0";
            work work = null;
            switch (cmdType)
            {
                case CmdType.work_request_20:
                    mess.message_sub_type = cancel ? "New" : "Complete";
                    body_request rb = new body_request();
                    ((Message_request)mess).body = rb;
                    rb.work_request = new work_request { requisition_number = cmd.Id };
                    rb.work_request.service_add = new service_add { service_cd = cmd.Name };
                    work = rb.work_request;
                    break;
                case CmdType.census_20:
                    mess.message_sub_type = cancel ? "in" : "out";
                    body_Census_Notice bc = new body_Census_Notice();
                    ((Message_notice)mess).body = bc;
                    bc.census_notice = new work_census_notice { census_notice_reason = "inclinic" };
                    work = bc.census_notice;
                    break;
            }
            work.client = new client { client_id = cmd.CustomerId, first_name = cmd.Customer, last_name = " " };
            work.doctor = new doctor { first_name = cmd.Doctor, last_name = " " };
            work.patient = new patient
            {
                patient_breed = cmd.KindOf.ToString(),
                patient_id = cmd.PetId,
                patient_name = cmd.PetName,
                patient_weight = new patient_weight { weight = cmd.Weight.ToString(), patient_weight_uom = "lbs" },
                patient_birth_dt = string.IsNullOrEmpty(cmd.Age) ? cmd.Date.ToString(idexxConfig.DateFormat) : DateTime.Now.AddMonths(-int.Parse(cmd.Age)).ToString(idexxConfig.DateFormat),
                patient_gender = cmd.Gender == GenderType.Female ? "MALE_INTACT" : "FEMALE_INTACT",
                patient_species = GetSpecies(cmd.KindOf)
            };
            return mess;
        }

        private string GetSpecies(KindOfType type)
        {
            switch (type)
            {
                case KindOfType.犬:
                    return "CANINE";
                case KindOfType.猫:
                    return "FELINE";
                default:
                    return "OTHER";
            }
        }

        private void Serialize(MessageBase message, string xmlpath)
        {
            MemoryStream StreamRequest = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(message.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);
            using (XmlWriter xmlWriter = XmlWriter.Create(StreamRequest))
            {
                xmlWriter.WriteDocType("message", null, message.DOCTYPE, null);
                serializer.Serialize(xmlWriter, message, ns);
                byte[] buffer = StreamRequest.GetBuffer();
                File.AppendAllText(xmlpath, UTF8Encoding.UTF8.GetString(buffer));
            }
        }
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {

            IDEXXResult idexxResult = Tool.GetObjectByXML<IDEXXResult>(e.FullPath);
            if (idexxResult == null)
                return;
            List<Command> CMDS = idexx.CMDS;
            Command cmd = null;
            lock (LockObj)
            {
                if (CMDS == null || (cmd = CMDS.LastOrDefault(o => o.Id == idexxResult.body.result.requisition_number)) == null)
                {
                    log.Write("接收到数据文件:" + e.Name);
                    //File.Copy(e.FullPath, idexx.configFilePath + e.Name, true);
                    File.Delete(e.FullPath);
                    return;
                }

                Result result = new Result { CMD = cmd, Devices = idexx };
                result.ResultDatas = new List<ResultItem>();
                ResultConfig rc = idexxConfig.ResultConfig.LastOrDefault(o => o.InvokeFormula(cmd));
                foreach (var item in idexxResult.body.result.results)
                {
                    ResultItem ri = new ResultItem();
                    ri.Code = item.assay_name;
                    ri.Max = item.assay_reference_range.critical_high;
                    ri.Min = item.assay_reference_range.critical_low;
                    ri.Name = item.assay_name;
                    ri.EnglishName = item.assay_name;
                    ri.Unit = item.result_value_uom_cd;
                    ri.Display = item.assay_name;
                    ri.Value = item.result_value;
                    ResultItemConfig ric = rc.Items.LastOrDefault(o => o.Code == ri.Code);
                    if (ric != null)
                    {
                        ri.Name = ric.Name;
                        ri.Display = ric.Display;
                        ri.EnglishName = ric.EnglishName;
                        //ri.Max = ric.Max;
                        //ri.Min = ric.Min;
                        //ri.Unit = ric.Unit;
                    }
                    result.ResultDatas.Add(ri);
                }
                idexx.ResultComplete(result);
                File.Delete(e.FullPath);
            }
        }

    }


}
