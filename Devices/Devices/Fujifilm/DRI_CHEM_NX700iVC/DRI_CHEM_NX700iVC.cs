using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Devices.Fujifilm
{
    [Serializable]
    public class DRI_CHEM_NX700iVC : DevicesBase<DRI_CHEM_NX700iVCConfig>
    {

        public DRI_CHEM_NX700iVC(string fileName)
            : base(fileName)
        {
            Protocol = new DRI_CHEM_NX700iVCProtocol.SeriaProtocol();
            Protocol.Init(this);
        }


        /// <summary>
        /// 设备信息
        /// </summary>
        protected override DevicesInformation DefaultInfo()
        {
            DevicesInformation info = new DevicesInformation
            {
                Num = 4,
                Brand = "fujifilm",
                Model = "FUJI DRI-CHEM NX700iVC",
                Name = "FUJI DRI-CHEM NX700iVC干式生化仪",
                Remarks = "富士生化",
                Code = "FUJI_DRI_CHEM_NX700iVC",
                Url = "http://www.fujifilm.com.cn/products/medical/animal/",
                ImagePath = "nx700ivc.png",
            };
            return info;
        }


        public override System.Windows.Forms.DialogResult ShowConfigForm()
        {
            UCDRI_CHEM_NX700iVCConfig uc = new UCDRI_CHEM_NX700iVCConfig(this);
            return uc.ShowForm().ShowDialog();
        }

        private static void Sost(List<ResultItemConfig> s, List<ResultItemConfig> d)
        {
            s.ForEach(o =>
            {
                int index = s.IndexOf(o);
                ResultItemConfig ric = d.FirstOrDefault(o1 => o1.Code == o.Code);
                if (ric != null)
                {
                    d.Remove(ric);
                    d.Insert(index, ric);
                }
            });
        }

        protected override Config DefaultConfig()
        {
            DRI_CHEM_NX700iVCConfig config = new DRI_CHEM_NX700iVCConfig();
            config.SerialPortConfig = new SerialPort.SerialPortConfig
            {
                Baud = 19200,
                PortName = "COM1",
                EncodingName = "utf-8",
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One
            };
            config.ResultConfig = new List<ResultConfig>();

            //犬<1
            ResultConfig dog = new ResultConfig { Name = "犬<1岁", Code = "1700", Formula = "o=>o.KindOf==KindOfType.犬 && int.Parse(o.Age)<=12" };
            dog.Items = new List<ResultItemConfig>();
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "GLU", EnglishName = "GLU", Name = "血糖", Format = "%C(%E)", Unit = "mmol/L", Min = "4.2", Max = "7.1", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN", EnglishName = "BUN", Name = "尿素氮", Format = "%C(%E)", Unit = "mmol/L", Min = "3.28", Max = "10.42", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "CRE", EnglishName = "CRE", Name = "肌酐", Format = "%C(%E)", Unit = "mmol/L", Min = "35", Max = "124", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN/CRE", EnglishName = "BUN/CRE", Name = "尿素氮肌酐比", Format = "%C(%E)", Unit = "", Min = "12.5", Max = "31.8", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "IP", EnglishName = "IP", Name = "无机磷", Format = "%C(%E)", Unit = "mmol/L", Min = "0.61", Max = "1.61", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "Ca", EnglishName = "Ca", Name = "钙离子", Format = "%C(%E)", Unit = "mmol/L", Min = "2.33", Max = "3.03", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB", EnglishName = "ALB", Name = "白蛋白", Format = "%C(%E)", Unit = "g/L", Min = "26", Max = "40", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "TP", EnglishName = "TP", Name = "总蛋白", Format = "%C(%E)", Unit = "mmol/L", Min = "50", Max = "72", });

            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB/GLB", EnglishName = "ALB/GLB", Name = "白蛋白球蛋白比", Format = "%C(%E)", Unit = "", Min = "0.7", Max = "1.9", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "ALT", EnglishName = "ALT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "17", Max = "78", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "GPT", EnglishName = "GPT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "17", Max = "78", });

            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "ALP", EnglishName = "ALP", Name = "碱性磷酸酶", Format = "%C(%E)", Unit = "U/L", Min = "20", Max = "109.5", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "GGT", EnglishName = "GGT", Name = "谷氨酰转移酶", Format = "%C(%E)", Unit = "U/L", Min = "5", Max = "14", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "TBIL", EnglishName = "TBIL", Name = "总胆红素", Format = "%C(%E)", Unit = "mmol/L", Min = "2", Max = "9", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "NH3", EnglishName = "NH3", Name = "氨", Format = "%C(%E)", Unit = "mmol/L", Min = "11", Max = "54", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "TCHO", EnglishName = "TCHO", Name = "总胆固醇", Format = "%C(%E)", Unit = "mmol/L", Min = "2.87", Max = "8.07", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "TG", EnglishName = "TG", Name = "甘油三酯", Format = "%C(%E)", Unit = "mmol/L", Min = "0.34", Max = "1.5", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "Na", EnglishName = "Na", Name = "钠离子", Format = "%C(%E)", Unit = "mg/dl", Min = "141", Max = "152", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "K", EnglishName = "K", Name = "钾离子", Format = "%C(%E)", Unit = "mmol/L", Min = "3.8", Max = "5", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "Cl", EnglishName = "Cl", Name = "氯", Format = "%C(%E)", Unit = "mmol/L", Min = "102", Max = "117", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "Na/K", EnglishName = "Na/K", Name = "纳钾比", Format = "%C(%E)", Unit = "", Min = "29.9", Max = "39.2", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "TCO2", EnglishName = "TCO2", Name = "总二氧化碳", Format = "%C(%E)", Unit = "mmol/L", Min = "18", Max = "31", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "Anion Gap", EnglishName = "Anion Gap", Name = "阴离子间隙", Format = "%C(%E)", Unit = "mmol/L", Min = "8", Max = "16", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "Mg", EnglishName = "Mg", Name = "镁", Format = "%C(%E)", Unit = "mmol/L", Min = "0.79", Max = "1.06", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "vAMY", EnglishName = "vAMY", Name = "淀粉酶", Format = "%C(%E)", Unit = "U/L", Min = "200", Max = "1400", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "vLIP", EnglishName = "vLIP", Name = "特异性脂肪酶", Format = "%C(%E)", Unit = "U/L", Min = "10", Max = "160", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "CPK", EnglishName = "CPK", Name = "肌酸磷酸激酶", Format = "%C(%E)", Unit = "U/L", Min = "49", Max = "166", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "LDH", EnglishName = "LDH", Name = "乳酸脱氢酶", Format = "%C(%E)", Unit = "U/L", Min = "20", Max = "109", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "GOT/AST", EnglishName = "GOT/AST", Name = "天冬氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "17", Max = "44", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "g/L", Min = "0", Max = "0.6", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "GLOB", EnglishName = "GLOB", Name = "球蛋白", Format = "%C(%E)", Unit = "g/l", Min = "16", Max = "37", });
            dog.Items.Add(new ResultItemConfig { Id = 1, Code = "CRP", EnglishName = "CRP", Name = "反应蛋白", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0.7", });
            config.ResultConfig.Add(dog);

            //犬>1
            ResultConfig dog1 = new ResultConfig { Name = "犬>1岁", Code = "1700", Formula = "o=>o.KindOf==KindOfType.犬 && int.Parse(o.Age)>12" };
            dog1.Items = new List<ResultItemConfig>();
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "ALP", EnglishName = "ALP", Name = "碱性磷酸酶", Format = "%C(%E)", Unit = "U/L", Min = "12.5", Max = "82.71", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "vAMY", EnglishName = "vAMY", Name = "淀粉酶", Format = "%C(%E)", Unit = "U/L", Min = "200", Max = "1400", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "CPK", EnglishName = "CPK", Name = "肌酸磷酸激酶", Format = "%C(%E)", Unit = "U/L", Min = "49", Max = "166", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "GGT", EnglishName = "GGT", Name = "谷氨酰转移酶", Format = "%C(%E)", Unit = "U/L", Min = "5", Max = "14", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "GOT/AST", EnglishName = "GOT/AST", Name = "天冬氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "17", Max = "44", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "ALT", EnglishName = "ALT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "17", Max = "78", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "GPT", EnglishName = "GPT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "17", Max = "78", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "LDH", EnglishName = "LDH", Name = "乳酸脱氢酶", Format = "%C(%E)", Unit = "U/L", Min = "20", Max = "109", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "vLIP", EnglishName = "vLIP", Name = "特异性脂肪酶", Format = "%C(%E)", Unit = "U/L", Min = "10", Max = "160", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB", EnglishName = "ALB", Name = "白蛋白", Format = "%C(%E)", Unit = "g/L", Min = "26", Max = "40", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN", EnglishName = "BUN", Name = "尿素氮", Format = "%C(%E)", Unit = "mmol/L", Min = "3.28", Max = "10.42", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "Ca", EnglishName = "Ca", Name = "钙离子", Format = "%C(%E)", Unit = "mmol/L", Min = "2.33", Max = "3.03", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "CRE", EnglishName = "CRE", Name = "肌酐", Format = "%C(%E)", Unit = "mmol/L", Min = "35", Max = "124", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "GLU", EnglishName = "GLU", Name = "血糖", Format = "%C(%E)", Unit = "mmol/L", Min = "4.2", Max = "7.1", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "IP", EnglishName = "IP", Name = "无机磷", Format = "%C(%E)", Unit = "mmol/L", Min = "0.61", Max = "1.61", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "NH3", EnglishName = "NH3", Name = "氨", Format = "%C(%E)", Unit = "mmol/L", Min = "11", Max = "54", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "TBIL", EnglishName = "TBIL", Name = "总胆红素", Format = "%C(%E)", Unit = "mmol/L", Min = "2", Max = "9", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "TCHO", EnglishName = "TCHO", Name = "总胆固醇", Format = "%C(%E)", Unit = "mmol/L", Min = "2.87", Max = "8.07", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "TG", EnglishName = "TG", Name = "甘油三酯", Format = "%C(%E)", Unit = "mmol/L", Min = "0.34", Max = "1.5", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "TP", EnglishName = "TP", Name = "总蛋白", Format = "%C(%E)", Unit = "mmol/L", Min = "50", Max = "72", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "g/L", Min = "0", Max = "0.6", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "Na", EnglishName = "Na", Name = "钠离子", Format = "%C(%E)", Unit = "mg/dl", Min = "141", Max = "152", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "K", EnglishName = "K", Name = "钾离子", Format = "%C(%E)", Unit = "mmol/L", Min = "3.8", Max = "5", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "Na/K", EnglishName = "Na/K", Name = "纳钾比", Format = "%C(%E)", Unit = "", Min = "29.9", Max = "39.2", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "Cl", EnglishName = "Cl", Name = "氯", Format = "%C(%E)", Unit = "mmol/L", Min = "102", Max = "117", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "Mg", EnglishName = "Mg", Name = "镁", Format = "%C(%E)", Unit = "mmol/L", Min = "0.79", Max = "1.06", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "TCO2", EnglishName = "TCO2", Name = "总二氧化碳", Format = "%C(%E)", Unit = "mmol/L", Min = "18", Max = "31", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0.6", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "GLOB", EnglishName = "GLOB", Name = "球蛋白", Format = "%C(%E)", Unit = "g/l", Min = "16", Max = "37", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB/GLB", EnglishName = "ALB/GLB", Name = "白蛋白球蛋白比", Format = "%C(%E)", Unit = "", Min = "0.7", Max = "1.9", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN/CRE", EnglishName = "BUN/CRE", Name = "尿素氮肌酐比", Format = "%C(%E)", Unit = "", Min = "12.5", Max = "31.8", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "Anion Gap", EnglishName = "Anion Gap", Name = "阴离子间隙", Format = "%C(%E)", Unit = "mmol/L", Min = "8", Max = "16", });
            dog1.Items.Add(new ResultItemConfig { Id = 1, Code = "CRP", EnglishName = "CRP", Name = "反应蛋白", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0.7", });
            Sost(dog.Items, dog1.Items);
            config.ResultConfig.Add(dog1);

            //猫<1
            ResultConfig cat = new ResultConfig { Name = "猫<1岁", Code = "1700", Formula = "o=>o.KindOf==KindOfType.猫 && int.Parse(o.Age)<=12" };
            cat.Items = new List<ResultItemConfig>();
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "ALP", EnglishName = "ALP", Name = "碱性磷酸酶", Format = "%C(%E)", Unit = "U/L", Min = "22.7", Max = "118", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "vAMY", EnglishName = "vAMY", Name = "淀粉酶", Format = "%C(%E)", Unit = "U/L", Min = "200", Max = "1900", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "CPK", EnglishName = "CPK", Name = "肌酸磷酸激酶", Format = "%C(%E)", Unit = "U/L", Min = "87", Max = "309", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "GGT", EnglishName = "GGT", Name = "谷氨酰转移酶", Format = "%C(%E)", Unit = "U/L", Min = "1", Max = "10", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "GOT/AST", EnglishName = "GOT/AST", Name = "天冬氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "18", Max = "51", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "ALT", EnglishName = "ALT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "17", Max = "78", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "GPT", EnglishName = "GPT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "17", Max = "78", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "LDH", EnglishName = "LDH", Name = "乳酸脱氢酶", Format = "%C(%E)", Unit = "U/L", Min = "35", Max = "187", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "vLIP", EnglishName = "vLIP", Name = "特异性脂肪酶", Format = "%C(%E)", Unit = "U/L", Min = "0", Max = "30", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB", EnglishName = "ALB", Name = "白蛋白", Format = "%C(%E)", Unit = "g/L", Min = "23", Max = "35", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN", EnglishName = "BUN", Name = "尿素氮", Format = "%C(%E)", Unit = "mmol/L", Min = "6.28", Max = "11.7", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "Ca", EnglishName = "Ca", Name = "钙离子", Format = "%C(%E)", Unit = "mmol/L", Min = "2.2", Max = "2.98", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "CRE", EnglishName = "CRE", Name = "肌酐", Format = "%C(%E)", Unit = "mmol/L", Min = "71", Max = "159", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "GLU", EnglishName = "GLU", Name = "血糖", Format = "%C(%E)", Unit = "mmol/L", Min = "3.9", Max = "8.2", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "IP", EnglishName = "IP", Name = "无机磷", Format = "%C(%E)", Unit = "mmol/L", Min = "0.84", Max = "1.94", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "NH3", EnglishName = "NH3", Name = "氨", Format = "%C(%E)", Unit = "mmol/L", Min = "16", Max = "56", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "TBIL", EnglishName = "TBIL", Name = "总胆红素", Format = "%C(%E)", Unit = "mmol/L", Min = "2", Max = "7", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "TCHO", EnglishName = "TCHO", Name = "总胆固醇", Format = "%C(%E)", Unit = "mmol/L", Min = "2.3", Max = "4.55", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "TG", EnglishName = "TG", Name = "甘油三酯", Format = "%C(%E)", Unit = "mmol/L", Min = "0.19", Max = "1.17", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "TP", EnglishName = "TP", Name = "总蛋白", Format = "%C(%E)", Unit = "mmol/L", Min = "57", Max = "728", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "g/L", Min = "0", Max = "0.2", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "Na", EnglishName = "Na", Name = "钠离子", Format = "%C(%E)", Unit = "mg/dl", Min = "147", Max = "156", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "K", EnglishName = "K", Name = "钾离子", Format = "%C(%E)", Unit = "mmol/L", Min = "3.4", Max = "4.6", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "Na/K", EnglishName = "Na/K", Name = "纳钾比", Format = "%C(%E)", Unit = "", Min = "33.6", Max = "44.2", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "Cl", EnglishName = "Cl", Name = "氯", Format = "%C(%E)", Unit = "mmol/L", Min = "107", Max = "120", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "Mg", EnglishName = "Mg", Name = "镁", Format = "%C(%E)", Unit = "mmol/L", Min = "0.62", Max = "1.03", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "TCO2", EnglishName = "TCO2", Name = "总二氧化碳", Format = "%C(%E)", Unit = "mmol/L", Min = "20", Max = "30", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0.2", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "GLOB", EnglishName = "GLOB", Name = "球蛋白", Format = "%C(%E)", Unit = "g/l", Min = "27", Max = "52", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB/GLB", EnglishName = "ALB/GLB", Name = "白蛋白球蛋白比", Format = "%C(%E)", Unit = "", Min = "0.4", Max = "1.1", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN/CRE", EnglishName = "BUN/CRE", Name = "尿素氮肌酐比", Format = "%C(%E)", Unit = "", Min = "17.5", Max = "21.9", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "Anion Gap", EnglishName = "Anion Gap", Name = "阴离子间隙", Format = "%C(%E)", Unit = "mmol/L", Min = "8", Max = "16", });
            cat.Items.Add(new ResultItemConfig { Id = 1, Code = "CRP", EnglishName = "CRP", Name = "反应蛋白", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0", });
            Sost(dog.Items, cat.Items);
            config.ResultConfig.Add(cat);


            //猫>1
            ResultConfig cat1 = new ResultConfig { Name = "猫>1岁", Code = "1700", Formula = "o=>o.KindOf==KindOfType.猫 && int.Parse(o.Age)>12" };
            cat1.Items = new List<ResultItemConfig>();
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "ALP", EnglishName = "ALP", Name = "碱性磷酸酶", Format = "%C(%E)", Unit = "U/L", Min = "9.49", Max = "52.5", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "vAMY", EnglishName = "vAMY", Name = "淀粉酶", Format = "%C(%E)", Unit = "U/L", Min = "200", Max = "1900", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "CPK", EnglishName = "CPK", Name = "肌酸磷酸激酶", Format = "%C(%E)", Unit = "U/L", Min = "87", Max = "309", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "GGT", EnglishName = "GGT", Name = "谷氨酰转移酶", Format = "%C(%E)", Unit = "U/L", Min = "1", Max = "10", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "GOT/AST", EnglishName = "GOT/AST", Name = "天冬氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "18", Max = "51", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "ALT", EnglishName = "ALT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "17", Max = "78", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "GPT", EnglishName = "GPT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "17", Max = "78", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "LDH", EnglishName = "LDH", Name = "乳酸脱氢酶", Format = "%C(%E)", Unit = "U/L", Min = "35", Max = "187", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "vLIP", EnglishName = "vLIP", Name = "特异性脂肪酶", Format = "%C(%E)", Unit = "U/L", Min = "0", Max = "30", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB", EnglishName = "ALB", Name = "白蛋白", Format = "%C(%E)", Unit = "g/L", Min = "23", Max = "35", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN", EnglishName = "BUN", Name = "尿素氮", Format = "%C(%E)", Unit = "mmol/L", Min = "6.28", Max = "11.7", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "Ca", EnglishName = "Ca", Name = "钙离子", Format = "%C(%E)", Unit = "mmol/L", Min = "2.2", Max = "2.98", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "CRE", EnglishName = "CRE", Name = "肌酐", Format = "%C(%E)", Unit = "mmol/L", Min = "71", Max = "159", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "GLU", EnglishName = "GLU", Name = "血糖", Format = "%C(%E)", Unit = "mmol/L", Min = "3.9", Max = "8.2", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "IP", EnglishName = "IP", Name = "无机磷", Format = "%C(%E)", Unit = "mmol/L", Min = "0.84", Max = "1.94", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "NH3", EnglishName = "NH3", Name = "氨", Format = "%C(%E)", Unit = "mmol/L", Min = "16", Max = "56", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "TBIL", EnglishName = "TBIL", Name = "总胆红素", Format = "%C(%E)", Unit = "mmol/L", Min = "2", Max = "7", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "TCHO", EnglishName = "TCHO", Name = "总胆固醇", Format = "%C(%E)", Unit = "mmol/L", Min = "2.3", Max = "4.55", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "TG", EnglishName = "TG", Name = "甘油三酯", Format = "%C(%E)", Unit = "mmol/L", Min = "0.19", Max = "1.17", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "TP", EnglishName = "TP", Name = "总蛋白", Format = "%C(%E)", Unit = "mmol/L", Min = "57", Max = "728", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "g/L", Min = "0", Max = "0.2", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "Na", EnglishName = "Na", Name = "钠离子", Format = "%C(%E)", Unit = "mg/dl", Min = "147", Max = "156", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "K", EnglishName = "K", Name = "钾离子", Format = "%C(%E)", Unit = "mmol/L", Min = "3.4", Max = "4.6", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "Na/K", EnglishName = "Na/K", Name = "纳钾比", Format = "%C(%E)", Unit = "", Min = "33.6", Max = "44.2", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "Cl", EnglishName = "Cl", Name = "氯", Format = "%C(%E)", Unit = "mmol/L", Min = "107", Max = "120", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "Mg", EnglishName = "Mg", Name = "镁", Format = "%C(%E)", Unit = "mmol/L", Min = "0.62", Max = "1.03", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "TCO2", EnglishName = "TCO2", Name = "总二氧化碳", Format = "%C(%E)", Unit = "mmol/L", Min = "20", Max = "30", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0.2", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "GLOB", EnglishName = "GLOB", Name = "球蛋白", Format = "%C(%E)", Unit = "g/l", Min = "27", Max = "52", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB/GLB", EnglishName = "ALB/GLB", Name = "白蛋白球蛋白比", Format = "%C(%E)", Unit = "", Min = "0.4", Max = "1.1", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN/CRE", EnglishName = "BUN/CRE", Name = "尿素氮肌酐比", Format = "%C(%E)", Unit = "", Min = "17.5", Max = "21.9", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "Anion Gap", EnglishName = "Anion Gap", Name = "阴离子间隙", Format = "%C(%E)", Unit = "mmol/L", Min = "8", Max = "16", });
            cat1.Items.Add(new ResultItemConfig { Id = 1, Code = "CRP", EnglishName = "CRP", Name = "反应蛋白", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0", });
            Sost(dog.Items, cat1.Items);
            config.ResultConfig.Add(cat1);

            //以下信息未必准确
            //兔子<1
            ResultConfig rabbit = new ResultConfig { Name = "兔子<1岁", Code = "1700", Formula = "o=>o.KindOf==KindOfType.兔 && int.Parse(o.Age)<=12" };
            rabbit.Items = new List<ResultItemConfig>();
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "ALP", EnglishName = "ALP", Name = "碱性磷酸酶", Format = "%C(%E)", Unit = "U/L", Min = "26.8", Max = "178", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "vAMY", EnglishName = "vAMY", Name = "淀粉酶", Format = "%C(%E)", Unit = "U/L", Min = "200", Max = "1900", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "CPK", EnglishName = "CPK", Name = "肌酸磷酸激酶", Format = "%C(%E)", Unit = "U/L", Min = "87", Max = "309", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "GGT", EnglishName = "GGT", Name = "谷氨酰转移酶", Format = "%C(%E)", Unit = "U/L", Min = "1", Max = "10", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "GOT/AST", EnglishName = "GOT/AST", Name = "天冬氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "18", Max = "51", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "ALT", EnglishName = "ALT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "17", Max = "78", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "GPT", EnglishName = "GPT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "17", Max = "78", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "LDH", EnglishName = "LDH", Name = "乳酸脱氢酶", Format = "%C(%E)", Unit = "U/L", Min = "35", Max = "187", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "vLIP", EnglishName = "vLIP", Name = "特异性脂肪酶", Format = "%C(%E)", Unit = "U/L", Min = "0", Max = "30", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB", EnglishName = "ALB", Name = "白蛋白", Format = "%C(%E)", Unit = "g/L", Min = "23", Max = "35", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN", EnglishName = "BUN", Name = "尿素氮", Format = "%C(%E)", Unit = "mmol/L", Min = "6.28", Max = "11.7", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "Ca", EnglishName = "Ca", Name = "钙离子", Format = "%C(%E)", Unit = "mmol/L", Min = "2.2", Max = "2.98", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "CRE", EnglishName = "CRE", Name = "肌酐", Format = "%C(%E)", Unit = "mmol/L", Min = "71", Max = "159", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "GLU", EnglishName = "GLU", Name = "血糖", Format = "%C(%E)", Unit = "mmol/L", Min = "3.9", Max = "8.2", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "IP", EnglishName = "IP", Name = "无机磷", Format = "%C(%E)", Unit = "mmol/L", Min = "0.84", Max = "1.94", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "NH3", EnglishName = "NH3", Name = "氨", Format = "%C(%E)", Unit = "mmol/L", Min = "16", Max = "56", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "TBIL", EnglishName = "TBIL", Name = "总胆红素", Format = "%C(%E)", Unit = "mmol/L", Min = "2", Max = "7", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "TCHO", EnglishName = "TCHO", Name = "总胆固醇", Format = "%C(%E)", Unit = "mmol/L", Min = "2.3", Max = "4.55", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "TG", EnglishName = "TG", Name = "甘油三酯", Format = "%C(%E)", Unit = "mmol/L", Min = "0.19", Max = "1.17", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "TP", EnglishName = "TP", Name = "总蛋白", Format = "%C(%E)", Unit = "mmol/L", Min = "57", Max = "728", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "g/L", Min = "0", Max = "0.2", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "Na", EnglishName = "Na", Name = "钠离子", Format = "%C(%E)", Unit = "mg/dl", Min = "147", Max = "156", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "K", EnglishName = "K", Name = "钾离子", Format = "%C(%E)", Unit = "mmol/L", Min = "3.4", Max = "4.6", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "Na/K", EnglishName = "Na/K", Name = "纳钾比", Format = "%C(%E)", Unit = "", Min = "33.6", Max = "44.2", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "Cl", EnglishName = "Cl", Name = "氯", Format = "%C(%E)", Unit = "mmol/L", Min = "107", Max = "120", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "Mg", EnglishName = "Mg", Name = "镁", Format = "%C(%E)", Unit = "mmol/L", Min = "0.62", Max = "1.03", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "TCO2", EnglishName = "TCO2", Name = "总二氧化碳", Format = "%C(%E)", Unit = "mmol/L", Min = "20", Max = "30", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0.2", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "GLOB", EnglishName = "GLOB", Name = "球蛋白", Format = "%C(%E)", Unit = "g/l", Min = "27", Max = "52", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB/GLB", EnglishName = "ALB/GLB", Name = "白蛋白球蛋白比", Format = "%C(%E)", Unit = "", Min = "0.4", Max = "1.1", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN/CRE", EnglishName = "BUN/CRE", Name = "尿素氮肌酐比", Format = "%C(%E)", Unit = "", Min = "17.5", Max = "21.9", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "Anion Gap", EnglishName = "Anion Gap", Name = "阴离子间隙", Format = "%C(%E)", Unit = "mmol/L", Min = "8", Max = "16", });
            rabbit.Items.Add(new ResultItemConfig { Id = 1, Code = "CRP", EnglishName = "CRP", Name = "反应蛋白", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0", });
            Sost(dog.Items, rabbit.Items);
            config.ResultConfig.Add(rabbit);


            //兔子>1
            ResultConfig rabbit1 = new ResultConfig { Name = "兔子>1岁", Code = "1700", Formula = "o=>o.KindOf==KindOfType.兔 && int.Parse(o.Age)>12" };
            rabbit1.Items = new List<ResultItemConfig>();
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "ALP", EnglishName = "ALP", Name = "碱性磷酸酶", Format = "%C(%E)", Unit = "U/L", Min = "26.8", Max = "178", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "vAMY", EnglishName = "vAMY", Name = "淀粉酶", Format = "%C(%E)", Unit = "U/L", Min = "200", Max = "1900", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "CPK", EnglishName = "CPK", Name = "肌酸磷酸激酶", Format = "%C(%E)", Unit = "U/L", Min = "87", Max = "309", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "GGT", EnglishName = "GGT", Name = "谷氨酰转移酶", Format = "%C(%E)", Unit = "U/L", Min = "1", Max = "10", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "GOT/AST", EnglishName = "GOT/AST", Name = "天冬氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "18", Max = "51", });

            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "ALT", EnglishName = "ALT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "22", Max = "84", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "LDH", EnglishName = "LDH", Name = "乳酸脱氢酶", Format = "%C(%E)", Unit = "U/L", Min = "35", Max = "187", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "vLIP", EnglishName = "vLIP", Name = "特异性脂肪酶", Format = "%C(%E)", Unit = "U/L", Min = "0", Max = "30", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB", EnglishName = "ALB", Name = "白蛋白", Format = "%C(%E)", Unit = "g/L", Min = "23", Max = "35", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN", EnglishName = "BUN", Name = "尿素氮", Format = "%C(%E)", Unit = "mmol/L", Min = "6.28", Max = "11.7", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "Ca", EnglishName = "Ca", Name = "钙离子", Format = "%C(%E)", Unit = "mmol/L", Min = "2.2", Max = "2.98", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "CRE", EnglishName = "CRE", Name = "肌酐", Format = "%C(%E)", Unit = "mmol/L", Min = "71", Max = "159", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "GLU", EnglishName = "GLU", Name = "血糖", Format = "%C(%E)", Unit = "mmol/L", Min = "3.9", Max = "8.2", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "IP", EnglishName = "IP", Name = "无机磷", Format = "%C(%E)", Unit = "mmol/L", Min = "0.84", Max = "1.94", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "NH3", EnglishName = "NH3", Name = "氨", Format = "%C(%E)", Unit = "mmol/L", Min = "16", Max = "56", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "TBIL", EnglishName = "TBIL", Name = "总胆红素", Format = "%C(%E)", Unit = "mmol/L", Min = "2", Max = "7", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "TCHO", EnglishName = "TCHO", Name = "总胆固醇", Format = "%C(%E)", Unit = "mmol/L", Min = "2.3", Max = "4.55", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "TG", EnglishName = "TG", Name = "甘油三酯", Format = "%C(%E)", Unit = "mmol/L", Min = "0.19", Max = "1.17", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "TP", EnglishName = "TP", Name = "总蛋白", Format = "%C(%E)", Unit = "mmol/L", Min = "57", Max = "728", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "g/L", Min = "0", Max = "0.2", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "Na", EnglishName = "Na", Name = "钠离子", Format = "%C(%E)", Unit = "mg/dl", Min = "147", Max = "156", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "K", EnglishName = "K", Name = "钾离子", Format = "%C(%E)", Unit = "mmol/L", Min = "3.4", Max = "4.6", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "Na/K", EnglishName = "Na/K", Name = "纳钾比", Format = "%C(%E)", Unit = "", Min = "33.6", Max = "44.2", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "Cl", EnglishName = "Cl", Name = "氯", Format = "%C(%E)", Unit = "mmol/L", Min = "107", Max = "120", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "Mg", EnglishName = "Mg", Name = "镁", Format = "%C(%E)", Unit = "mmol/L", Min = "0.62", Max = "1.03", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "TCO2", EnglishName = "TCO2", Name = "总二氧化碳", Format = "%C(%E)", Unit = "mmol/L", Min = "20", Max = "30", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0.2", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "GLOB", EnglishName = "GLOB", Name = "球蛋白", Format = "%C(%E)", Unit = "g/l", Min = "27", Max = "52", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB/GLB", EnglishName = "ALB/GLB", Name = "白蛋白球蛋白比", Format = "%C(%E)", Unit = "", Min = "0.4", Max = "1.1", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN/CRE", EnglishName = "BUN/CRE", Name = "尿素氮肌酐比", Format = "%C(%E)", Unit = "", Min = "17.5", Max = "21.9", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "Anion Gap", EnglishName = "Anion Gap", Name = "阴离子间隙", Format = "%C(%E)", Unit = "mmol/L", Min = "8", Max = "16", });
            rabbit1.Items.Add(new ResultItemConfig { Id = 1, Code = "CRP", EnglishName = "CRP", Name = "反应蛋白", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0", });
            Sost(dog.Items, rabbit1.Items);
            config.ResultConfig.Add(rabbit1);

            //小鼠
            ResultConfig mouse = new ResultConfig { Name = "小型鼠", Code = "1700", Formula = "o=>o.KindOf==KindOfType.小鼠" };
            mouse.Items = new List<ResultItemConfig>();
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "ALP", EnglishName = "ALP", Name = "碱性磷酸酶", Format = "%C(%E)", Unit = "U/L", Min = "26.8", Max = "178", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "vAMY", EnglishName = "vAMY", Name = "淀粉酶", Format = "%C(%E)", Unit = "U/L", Min = "200", Max = "1900", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "CPK", EnglishName = "CPK", Name = "肌酸磷酸激酶", Format = "%C(%E)", Unit = "U/L", Min = "87", Max = "309", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "GGT", EnglishName = "GGT", Name = "谷氨酰转移酶", Format = "%C(%E)", Unit = "U/L", Min = "1", Max = "10", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "GOT/AST", EnglishName = "GOT/AST", Name = "天冬氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "18", Max = "51", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "ALT", EnglishName = "ALT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "22", Max = "84", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "LDH", EnglishName = "LDH", Name = "乳酸脱氢酶", Format = "%C(%E)", Unit = "U/L", Min = "35", Max = "187", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "vLIP", EnglishName = "vLIP", Name = "特异性脂肪酶", Format = "%C(%E)", Unit = "U/L", Min = "0", Max = "30", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB", EnglishName = "ALB", Name = "白蛋白", Format = "%C(%E)", Unit = "g/L", Min = "23", Max = "35", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN", EnglishName = "BUN", Name = "尿素氮", Format = "%C(%E)", Unit = "mmol/L", Min = "6.28", Max = "11.7", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "Ca", EnglishName = "Ca", Name = "钙离子", Format = "%C(%E)", Unit = "mmol/L", Min = "2.2", Max = "2.98", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "CRE", EnglishName = "CRE", Name = "肌酐", Format = "%C(%E)", Unit = "mmol/L", Min = "71", Max = "159", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "GLU", EnglishName = "GLU", Name = "血糖", Format = "%C(%E)", Unit = "mmol/L", Min = "3.9", Max = "8.2", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "IP", EnglishName = "IP", Name = "无机磷", Format = "%C(%E)", Unit = "mmol/L", Min = "0.84", Max = "1.94", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "NH3", EnglishName = "NH3", Name = "氨", Format = "%C(%E)", Unit = "mmol/L", Min = "16", Max = "56", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "TBIL", EnglishName = "TBIL", Name = "总胆红素", Format = "%C(%E)", Unit = "mmol/L", Min = "2", Max = "7", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "TCHO", EnglishName = "TCHO", Name = "总胆固醇", Format = "%C(%E)", Unit = "mmol/L", Min = "2.3", Max = "4.55", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "TG", EnglishName = "TG", Name = "甘油三酯", Format = "%C(%E)", Unit = "mmol/L", Min = "0.19", Max = "1.17", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "TP", EnglishName = "TP", Name = "总蛋白", Format = "%C(%E)", Unit = "mmol/L", Min = "57", Max = "728", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "g/L", Min = "0", Max = "0.2", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "Na", EnglishName = "Na", Name = "钠离子", Format = "%C(%E)", Unit = "mg/dl", Min = "147", Max = "156", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "K", EnglishName = "K", Name = "钾离子", Format = "%C(%E)", Unit = "mmol/L", Min = "3.4", Max = "4.6", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "Na/K", EnglishName = "Na/K", Name = "纳钾比", Format = "%C(%E)", Unit = "", Min = "33.6", Max = "44.2", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "Cl", EnglishName = "Cl", Name = "氯", Format = "%C(%E)", Unit = "mmol/L", Min = "107", Max = "120", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "Mg", EnglishName = "Mg", Name = "镁", Format = "%C(%E)", Unit = "mmol/L", Min = "0.62", Max = "1.03", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "TCO2", EnglishName = "TCO2", Name = "总二氧化碳", Format = "%C(%E)", Unit = "mmol/L", Min = "20", Max = "30", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0.2", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "GLOB", EnglishName = "GLOB", Name = "球蛋白", Format = "%C(%E)", Unit = "g/l", Min = "27", Max = "52", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB/GLB", EnglishName = "ALB/GLB", Name = "白蛋白球蛋白比", Format = "%C(%E)", Unit = "", Min = "0.4", Max = "1.1", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN/CRE", EnglishName = "BUN/CRE", Name = "尿素氮肌酐比", Format = "%C(%E)", Unit = "", Min = "17.5", Max = "21.9", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "Anion Gap", EnglishName = "Anion Gap", Name = "阴离子间隙", Format = "%C(%E)", Unit = "mmol/L", Min = "8", Max = "16", });
            mouse.Items.Add(new ResultItemConfig { Id = 1, Code = "CRP", EnglishName = "CRP", Name = "反应蛋白", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0", });
            Sost(dog.Items, mouse.Items);
            config.ResultConfig.Add(mouse);

            //奶牛
            ResultConfig cow = new ResultConfig { Name = "奶牛", Code = "1700", Formula = "o=>o.KindOf==KindOfType.奶牛" };
            cow.Items = new List<ResultItemConfig>();
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "ALP", EnglishName = "ALP", Name = "碱性磷酸酶", Format = "%C(%E)", Unit = "U/L", Min = "26.8", Max = "178", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "vAMY", EnglishName = "vAMY", Name = "淀粉酶", Format = "%C(%E)", Unit = "U/L", Min = "200", Max = "1900", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "CPK", EnglishName = "CPK", Name = "肌酸磷酸激酶", Format = "%C(%E)", Unit = "U/L", Min = "87", Max = "309", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "GGT", EnglishName = "GGT", Name = "谷氨酰转移酶", Format = "%C(%E)", Unit = "U/L", Min = "1", Max = "10", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "GOT/AST", EnglishName = "GOT/AST", Name = "天冬氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "18", Max = "51", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "ALT", EnglishName = "ALT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "22", Max = "84", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "LDH", EnglishName = "LDH", Name = "乳酸脱氢酶", Format = "%C(%E)", Unit = "U/L", Min = "35", Max = "187", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "vLIP", EnglishName = "vLIP", Name = "特异性脂肪酶", Format = "%C(%E)", Unit = "U/L", Min = "0", Max = "30", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB", EnglishName = "ALB", Name = "白蛋白", Format = "%C(%E)", Unit = "g/L", Min = "23", Max = "35", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN", EnglishName = "BUN", Name = "尿素氮", Format = "%C(%E)", Unit = "mmol/L", Min = "6.28", Max = "11.7", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "Ca", EnglishName = "Ca", Name = "钙离子", Format = "%C(%E)", Unit = "mmol/L", Min = "2.2", Max = "2.98", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "CRE", EnglishName = "CRE", Name = "肌酐", Format = "%C(%E)", Unit = "mmol/L", Min = "71", Max = "159", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "GLU", EnglishName = "GLU", Name = "血糖", Format = "%C(%E)", Unit = "mmol/L", Min = "3.9", Max = "8.2", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "IP", EnglishName = "IP", Name = "无机磷", Format = "%C(%E)", Unit = "mmol/L", Min = "0.84", Max = "1.94", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "NH3", EnglishName = "NH3", Name = "氨", Format = "%C(%E)", Unit = "mmol/L", Min = "16", Max = "56", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "TBIL", EnglishName = "TBIL", Name = "总胆红素", Format = "%C(%E)", Unit = "mmol/L", Min = "2", Max = "7", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "TCHO", EnglishName = "TCHO", Name = "总胆固醇", Format = "%C(%E)", Unit = "mmol/L", Min = "2.3", Max = "4.55", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "TG", EnglishName = "TG", Name = "甘油三酯", Format = "%C(%E)", Unit = "mmol/L", Min = "0.19", Max = "1.17", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "TP", EnglishName = "TP", Name = "总蛋白", Format = "%C(%E)", Unit = "mmol/L", Min = "57", Max = "728", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "g/L", Min = "0", Max = "0.2", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "Na", EnglishName = "Na", Name = "钠离子", Format = "%C(%E)", Unit = "mg/dl", Min = "147", Max = "156", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "K", EnglishName = "K", Name = "钾离子", Format = "%C(%E)", Unit = "mmol/L", Min = "3.4", Max = "4.6", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "Na/K", EnglishName = "Na/K", Name = "纳钾比", Format = "%C(%E)", Unit = "", Min = "33.6", Max = "44.2", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "Cl", EnglishName = "Cl", Name = "氯", Format = "%C(%E)", Unit = "mmol/L", Min = "107", Max = "120", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "Mg", EnglishName = "Mg", Name = "镁", Format = "%C(%E)", Unit = "mmol/L", Min = "0.62", Max = "1.03", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "TCO2", EnglishName = "TCO2", Name = "总二氧化碳", Format = "%C(%E)", Unit = "mmol/L", Min = "20", Max = "30", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0.2", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "GLOB", EnglishName = "GLOB", Name = "球蛋白", Format = "%C(%E)", Unit = "g/l", Min = "27", Max = "52", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB/GLB", EnglishName = "ALB/GLB", Name = "白蛋白球蛋白比", Format = "%C(%E)", Unit = "", Min = "0.4", Max = "1.1", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN/CRE", EnglishName = "BUN/CRE", Name = "尿素氮肌酐比", Format = "%C(%E)", Unit = "", Min = "17.5", Max = "21.9", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "Anion Gap", EnglishName = "Anion Gap", Name = "阴离子间隙", Format = "%C(%E)", Unit = "mmol/L", Min = "8", Max = "16", });
            cow.Items.Add(new ResultItemConfig { Id = 1, Code = "CRP", EnglishName = "CRP", Name = "反应蛋白", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0", });
            Sost(dog.Items, cow.Items);
            config.ResultConfig.Add(cow);

            //马
            ResultConfig horse = new ResultConfig { Name = "马", Code = "1700", Formula = "o=>o.KindOf==KindOfType.马" };
            horse.Items = new List<ResultItemConfig>();
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "ALP", EnglishName = "ALP", Name = "碱性磷酸酶", Format = "%C(%E)", Unit = "U/L", Min = "26.8", Max = "178", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "vAMY", EnglishName = "vAMY", Name = "淀粉酶", Format = "%C(%E)", Unit = "U/L", Min = "200", Max = "1900", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "CPK", EnglishName = "CPK", Name = "肌酸磷酸激酶", Format = "%C(%E)", Unit = "U/L", Min = "87", Max = "309", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "GGT", EnglishName = "GGT", Name = "谷氨酰转移酶", Format = "%C(%E)", Unit = "U/L", Min = "1", Max = "10", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "GOT/AST", EnglishName = "GOT/AST", Name = "天冬氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "18", Max = "51", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "ALT", EnglishName = "ALT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "22", Max = "84", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "LDH", EnglishName = "LDH", Name = "乳酸脱氢酶", Format = "%C(%E)", Unit = "U/L", Min = "35", Max = "187", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "vLIP", EnglishName = "vLIP", Name = "特异性脂肪酶", Format = "%C(%E)", Unit = "U/L", Min = "0", Max = "30", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB", EnglishName = "ALB", Name = "白蛋白", Format = "%C(%E)", Unit = "g/L", Min = "23", Max = "35", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN", EnglishName = "BUN", Name = "尿素氮", Format = "%C(%E)", Unit = "mmol/L", Min = "6.28", Max = "11.7", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "Ca", EnglishName = "Ca", Name = "钙离子", Format = "%C(%E)", Unit = "mmol/L", Min = "2.2", Max = "2.98", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "CRE", EnglishName = "CRE", Name = "肌酐", Format = "%C(%E)", Unit = "mmol/L", Min = "71", Max = "159", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "GLU", EnglishName = "GLU", Name = "血糖", Format = "%C(%E)", Unit = "mmol/L", Min = "3.9", Max = "8.2", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "IP", EnglishName = "IP", Name = "无机磷", Format = "%C(%E)", Unit = "mmol/L", Min = "0.84", Max = "1.94", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "NH3", EnglishName = "NH3", Name = "氨", Format = "%C(%E)", Unit = "mmol/L", Min = "16", Max = "56", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "TBIL", EnglishName = "TBIL", Name = "总胆红素", Format = "%C(%E)", Unit = "mmol/L", Min = "2", Max = "7", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "TCHO", EnglishName = "TCHO", Name = "总胆固醇", Format = "%C(%E)", Unit = "mmol/L", Min = "2.3", Max = "4.55", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "TG", EnglishName = "TG", Name = "甘油三酯", Format = "%C(%E)", Unit = "mmol/L", Min = "0.19", Max = "1.17", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "TP", EnglishName = "TP", Name = "总蛋白", Format = "%C(%E)", Unit = "mmol/L", Min = "57", Max = "728", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "g/L", Min = "0", Max = "0.2", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "Na", EnglishName = "Na", Name = "钠离子", Format = "%C(%E)", Unit = "mg/dl", Min = "147", Max = "156", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "K", EnglishName = "K", Name = "钾离子", Format = "%C(%E)", Unit = "mmol/L", Min = "3.4", Max = "4.6", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "Na/K", EnglishName = "Na/K", Name = "纳钾比", Format = "%C(%E)", Unit = "", Min = "33.6", Max = "44.2", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "Cl", EnglishName = "Cl", Name = "氯", Format = "%C(%E)", Unit = "mmol/L", Min = "107", Max = "120", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "Mg", EnglishName = "Mg", Name = "镁", Format = "%C(%E)", Unit = "mmol/L", Min = "0.62", Max = "1.03", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "TCO2", EnglishName = "TCO2", Name = "总二氧化碳", Format = "%C(%E)", Unit = "mmol/L", Min = "20", Max = "30", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0.2", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "GLOB", EnglishName = "GLOB", Name = "球蛋白", Format = "%C(%E)", Unit = "g/l", Min = "27", Max = "52", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB/GLB", EnglishName = "ALB/GLB", Name = "白蛋白球蛋白比", Format = "%C(%E)", Unit = "", Min = "0.4", Max = "1.1", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN/CRE", EnglishName = "BUN/CRE", Name = "尿素氮肌酐比", Format = "%C(%E)", Unit = "", Min = "17.5", Max = "21.9", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "Anion Gap", EnglishName = "Anion Gap", Name = "阴离子间隙", Format = "%C(%E)", Unit = "mmol/L", Min = "8", Max = "16", });
            horse.Items.Add(new ResultItemConfig { Id = 1, Code = "CRP", EnglishName = "CRP", Name = "反应蛋白", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0", });
            Sost(dog.Items, horse.Items);
            config.ResultConfig.Add(horse);

            //马
            ResultConfig pig = new ResultConfig { Name = "猪", Code = "1700", Formula = "o=>o.KindOf==KindOfType.猪" };
            pig.Items = new List<ResultItemConfig>();
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "ALP", EnglishName = "ALP", Name = "碱性磷酸酶", Format = "%C(%E)", Unit = "U/L", Min = "26.8", Max = "178", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "vAMY", EnglishName = "vAMY", Name = "淀粉酶", Format = "%C(%E)", Unit = "U/L", Min = "200", Max = "1900", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "CPK", EnglishName = "CPK", Name = "肌酸磷酸激酶", Format = "%C(%E)", Unit = "U/L", Min = "87", Max = "309", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "GGT", EnglishName = "GGT", Name = "谷氨酰转移酶", Format = "%C(%E)", Unit = "U/L", Min = "1", Max = "10", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "GOT/AST", EnglishName = "GOT/AST", Name = "天冬氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "18", Max = "51", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "ALT", EnglishName = "ALT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "22", Max = "84", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "LDH", EnglishName = "LDH", Name = "乳酸脱氢酶", Format = "%C(%E)", Unit = "U/L", Min = "35", Max = "187", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "vLIP", EnglishName = "vLIP", Name = "特异性脂肪酶", Format = "%C(%E)", Unit = "U/L", Min = "0", Max = "30", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB", EnglishName = "ALB", Name = "白蛋白", Format = "%C(%E)", Unit = "g/L", Min = "23", Max = "35", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN", EnglishName = "BUN", Name = "尿素氮", Format = "%C(%E)", Unit = "mmol/L", Min = "6.28", Max = "11.7", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "Ca", EnglishName = "Ca", Name = "钙离子", Format = "%C(%E)", Unit = "mmol/L", Min = "2.2", Max = "2.98", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "CRE", EnglishName = "CRE", Name = "肌酐", Format = "%C(%E)", Unit = "mmol/L", Min = "71", Max = "159", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "GLU", EnglishName = "GLU", Name = "血糖", Format = "%C(%E)", Unit = "mmol/L", Min = "3.9", Max = "8.2", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "IP", EnglishName = "IP", Name = "无机磷", Format = "%C(%E)", Unit = "mmol/L", Min = "0.84", Max = "1.94", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "NH3", EnglishName = "NH3", Name = "氨", Format = "%C(%E)", Unit = "mmol/L", Min = "16", Max = "56", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "TBIL", EnglishName = "TBIL", Name = "总胆红素", Format = "%C(%E)", Unit = "mmol/L", Min = "2", Max = "7", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "TCHO", EnglishName = "TCHO", Name = "总胆固醇", Format = "%C(%E)", Unit = "mmol/L", Min = "2.3", Max = "4.55", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "TG", EnglishName = "TG", Name = "甘油三酯", Format = "%C(%E)", Unit = "mmol/L", Min = "0.19", Max = "1.17", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "TP", EnglishName = "TP", Name = "总蛋白", Format = "%C(%E)", Unit = "mmol/L", Min = "57", Max = "728", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "g/L", Min = "0", Max = "0.2", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "Na", EnglishName = "Na", Name = "钠离子", Format = "%C(%E)", Unit = "mg/dl", Min = "147", Max = "156", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "K", EnglishName = "K", Name = "钾离子", Format = "%C(%E)", Unit = "mmol/L", Min = "3.4", Max = "4.6", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "Na/K", EnglishName = "Na/K", Name = "纳钾比", Format = "%C(%E)", Unit = "", Min = "33.6", Max = "44.2", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "Cl", EnglishName = "Cl", Name = "氯", Format = "%C(%E)", Unit = "mmol/L", Min = "107", Max = "120", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "Mg", EnglishName = "Mg", Name = "镁", Format = "%C(%E)", Unit = "mmol/L", Min = "0.62", Max = "1.03", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "TCO2", EnglishName = "TCO2", Name = "总二氧化碳", Format = "%C(%E)", Unit = "mmol/L", Min = "20", Max = "30", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0.2", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "GLOB", EnglishName = "GLOB", Name = "球蛋白", Format = "%C(%E)", Unit = "g/l", Min = "27", Max = "52", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB/GLB", EnglishName = "ALB/GLB", Name = "白蛋白球蛋白比", Format = "%C(%E)", Unit = "", Min = "0.4", Max = "1.1", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN/CRE", EnglishName = "BUN/CRE", Name = "尿素氮肌酐比", Format = "%C(%E)", Unit = "", Min = "17.5", Max = "21.9", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "Anion Gap", EnglishName = "Anion Gap", Name = "阴离子间隙", Format = "%C(%E)", Unit = "mmol/L", Min = "8", Max = "16", });
            pig.Items.Add(new ResultItemConfig { Id = 1, Code = "CRP", EnglishName = "CRP", Name = "反应蛋白", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0", });
            Sost(dog.Items, pig.Items);
            config.ResultConfig.Add(pig);


            //绵羊
            ResultConfig sheep = new ResultConfig { Name = "绵羊", Code = "1700", Formula = "o=>o.KindOf==KindOfType.绵羊" };
            sheep.Items = new List<ResultItemConfig>();
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "ALP", EnglishName = "ALP", Name = "碱性磷酸酶", Format = "%C(%E)", Unit = "U/L", Min = "26.8", Max = "178", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "vAMY", EnglishName = "vAMY", Name = "淀粉酶", Format = "%C(%E)", Unit = "U/L", Min = "200", Max = "1900", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "CPK", EnglishName = "CPK", Name = "肌酸磷酸激酶", Format = "%C(%E)", Unit = "U/L", Min = "87", Max = "309", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "GGT", EnglishName = "GGT", Name = "谷氨酰转移酶", Format = "%C(%E)", Unit = "U/L", Min = "1", Max = "10", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "GOT/AST", EnglishName = "GOT/AST", Name = "天冬氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "18", Max = "51", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "ALT", EnglishName = "ALT", Name = "丙氨酸转氨酶", Format = "%C(%E)", Unit = "U/L", Min = "22", Max = "84", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "LDH", EnglishName = "LDH", Name = "乳酸脱氢酶", Format = "%C(%E)", Unit = "U/L", Min = "35", Max = "187", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "vLIP", EnglishName = "vLIP", Name = "特异性脂肪酶", Format = "%C(%E)", Unit = "U/L", Min = "0", Max = "30", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB", EnglishName = "ALB", Name = "白蛋白", Format = "%C(%E)", Unit = "g/L", Min = "23", Max = "35", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN", EnglishName = "BUN", Name = "尿素氮", Format = "%C(%E)", Unit = "mmol/L", Min = "6.28", Max = "11.7", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "Ca", EnglishName = "Ca", Name = "钙离子", Format = "%C(%E)", Unit = "mmol/L", Min = "2.2", Max = "2.98", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "CRE", EnglishName = "CRE", Name = "肌酐", Format = "%C(%E)", Unit = "mmol/L", Min = "71", Max = "159", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "GLU", EnglishName = "GLU", Name = "血糖", Format = "%C(%E)", Unit = "mmol/L", Min = "3.9", Max = "8.2", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "IP", EnglishName = "IP", Name = "无机磷", Format = "%C(%E)", Unit = "mmol/L", Min = "0.84", Max = "1.94", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "NH3", EnglishName = "NH3", Name = "氨", Format = "%C(%E)", Unit = "mmol/L", Min = "16", Max = "56", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "TBIL", EnglishName = "TBIL", Name = "总胆红素", Format = "%C(%E)", Unit = "mmol/L", Min = "2", Max = "7", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "TCHO", EnglishName = "TCHO", Name = "总胆固醇", Format = "%C(%E)", Unit = "mmol/L", Min = "2.3", Max = "4.55", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "TG", EnglishName = "TG", Name = "甘油三酯", Format = "%C(%E)", Unit = "mmol/L", Min = "0.19", Max = "1.17", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "TP", EnglishName = "TP", Name = "总蛋白", Format = "%C(%E)", Unit = "mmol/L", Min = "57", Max = "728", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "g/L", Min = "0", Max = "0.2", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "Na", EnglishName = "Na", Name = "钠离子", Format = "%C(%E)", Unit = "mg/dl", Min = "147", Max = "156", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "K", EnglishName = "K", Name = "钾离子", Format = "%C(%E)", Unit = "mmol/L", Min = "3.4", Max = "4.6", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "Na/K", EnglishName = "Na/K", Name = "纳钾比", Format = "%C(%E)", Unit = "", Min = "33.6", Max = "44.2", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "Cl", EnglishName = "Cl", Name = "氯", Format = "%C(%E)", Unit = "mmol/L", Min = "107", Max = "120", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "Mg", EnglishName = "Mg", Name = "镁", Format = "%C(%E)", Unit = "mmol/L", Min = "0.62", Max = "1.03", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "TCO2", EnglishName = "TCO2", Name = "总二氧化碳", Format = "%C(%E)", Unit = "mmol/L", Min = "20", Max = "30", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "UA", EnglishName = "UA", Name = "尿酸", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0.2", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "GLOB", EnglishName = "GLOB", Name = "球蛋白", Format = "%C(%E)", Unit = "g/l", Min = "27", Max = "52", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "ALB/GLB", EnglishName = "ALB/GLB", Name = "白蛋白球蛋白比", Format = "%C(%E)", Unit = "", Min = "0.4", Max = "1.1", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "BUN/CRE", EnglishName = "BUN/CRE", Name = "尿素氮肌酐比", Format = "%C(%E)", Unit = "", Min = "17.5", Max = "21.9", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "Anion Gap", EnglishName = "Anion Gap", Name = "阴离子间隙", Format = "%C(%E)", Unit = "mmol/L", Min = "8", Max = "16", });
            sheep.Items.Add(new ResultItemConfig { Id = 1, Code = "CRP", EnglishName = "CRP", Name = "反应蛋白", Format = "%C(%E)", Unit = "mg/dl", Min = "0", Max = "0", });
            Sost(dog.Items, sheep.Items);
            config.ResultConfig.Add(sheep);

            return config;
        }
    }
}
