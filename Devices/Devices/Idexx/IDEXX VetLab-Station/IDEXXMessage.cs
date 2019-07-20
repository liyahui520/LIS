using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
namespace Devices.IDEXX
{
    [Serializable]
    public class MessageBase
    {
        internal string DOCTYPE { get; set; }
        [XmlAttribute]
        public string message_id { get; set; }
        [XmlAttribute]
        public string message_dt { get; set; }
        [XmlAttribute]
        public string message_type { get; set; }

        [XmlAttribute]
        public string message_sub_type { get; set; }

        [XmlAttribute]
        public string message_dtd_version_number { get; set; }

        public header header { get; set; }
    }


    [Serializable]
    [XmlRoot(ElementName = "message")]
    public class Message_request : MessageBase
    {
        public body_request body { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "message")]
    public class Message_notice : MessageBase
    {
        public body_Census_Notice body { get; set; }
    }

    [Serializable]
    public class header
    {
        public string from_application_id { get; set; }
        public string to_application_id { get; set; }
    }
    [Serializable]
    public class body
    {

    }


    [XmlRoot(ElementName = "body")]
    public class body_request : body
    {
        public work_request work_request { get; set; }
    }
    [XmlRoot(ElementName = "body")]
    [XmlInclude(typeof(body_Census_Notice))]
    public class body_Census_Notice : body
    {
        public work_census_notice census_notice { get; set; }
    }

    [Serializable]
    public class work
    {
        /// <summary>
        /// 客户信息
        /// </summary>
        public client client { get; set; }
        /// <summary>
        /// 宠物信息
        /// </summary>
        public patient patient { get; set; }

        public doctor doctor { get; set; }
        public service_add service_add { get; set; }

        //private string number;
        protected CmdType _type;

        public void SetType(CmdType type)
        {
            _type = type;
        }
        public work()
        {
        }
    }



    [XmlRoot(ElementName = "census_notice")]
    [Serializable]
    public class work_census_notice : work
    {
        [XmlAttribute]
        public string census_notice_reason { get; set; }


    }



    [XmlRoot(ElementName = "work_request")]
    [Serializable]
    public class work_request : work
    {
     
        private string number = "inclinic";
       [XmlAttribute(AttributeName = "requisition_number")]
        public string requisition_number
        {
            get
            {
                return number;
            }
            set { number = value; }
        }

        
       public service_add service_add { get; set; }

    }

    /// <summary>
    /// 客户信息
    /// </summary>
    [Serializable]
    public class client
    {
        [XmlAttribute(AttributeName = "client_id")]
        public string client_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
    /// <summary>
    /// 宠物信息
    /// </summary>

    [Serializable]
    public class patient
    {
        [XmlAttribute]
        public string patient_id { get; set; }

        [XmlAttribute]
        public string patient_species { get; set; }

        [XmlAttribute]
        public string patient_gender { get; set; }

        public string patient_name { get; set; }
        public string patient_breed { get; set; }
        public string patient_birth_dt { get; set; }
        public patient_weight patient_weight { get; set; }
    }

    [Serializable]
    public class patient_weight
    {
        [XmlAttribute]
        public string patient_weight_uom { get; set; }
        public string weight { get; set; }
    }
    /// <summary>
    /// 医生
    /// </summary>

    [Serializable]
    public class doctor
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
    /// <summary>
    /// 检查项
    /// </summary>

    [XmlRoot("service_add")]
    [Serializable]
    public class service_add
    {
        public string service_cd { get; set; }
    }





    [Serializable]
    [XmlRoot(ElementName = "message")]
    public class IDEXXResult : MessageBase
    {
        public ResultBody body { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "body")]
    public class ResultBody
    {
        public result result { get; set; }
    }
    [Serializable]
    public class result : work
    {
        [XmlAttribute(AttributeName = "diagnostic_set_id")]
        public string diagnostic_set_id { get; set; }
        [XmlAttribute(AttributeName = "instrument")]
        public string instrument { get; set; }


        [XmlAttribute]
        public string requisition_number { get; set; }

        public string run_dt { get; set; }
        public List<assay_result> results { get; set; }
    }

    public class assay_result
    {
        [XmlAttribute(AttributeName = "assay_name")]
        public string assay_name { get; set; }
        public string result_value_uom_cd { get; set; }
        public renge assay_reference_range { get; set; }
        public string result_qualifier { get; set; }

        public string result_value { get; set; }
    }
    public class renge
    {
        public string critical_low { get; set; }
        public string low { get; set; }
        public string high { get; set; }
        public string critical_high { get; set; }
    }
}
