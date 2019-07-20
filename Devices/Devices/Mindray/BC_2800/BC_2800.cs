using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
namespace Devices.Mindray
{
    [Serializable]
    public class BC_2800 : DevicesBase<BC_2800Config>
    {
        public BC_2800(string fileName)
            : base(fileName)
        {
            Protocol = new BC_2800Protocol.SeriaProtocol();
            Protocol.Init(this);
        }

        /// <summary>
        /// 设备信息
        /// </summary>
        protected override DevicesInformation DefaultInfo()
        {
            DevicesInformation info = new DevicesInformation
            {
                Num = 2,
                Brand = "Mindray",
                Model = "BC-2800",
                Name = "Mindray 三分群血液细胞分析仪 BC-2800",
                Remarks = "Mindray 血液细胞分析仪",
                Code = "Mindray 三分群血液细胞分析仪 BC-2800",
                Url = "http://www.mindray.com/cn/product/ba17524d-5c96-4fda-bb50-33a599e68460.html",
                ImagePath = "BC_2800.png",
            };
            return info;
        }

        public override System.Windows.Forms.DialogResult ShowConfigForm()
        {
            UCBC_2800Config uc = new UCBC_2800Config(this);
            return uc.ShowForm().ShowDialog();
        }

        protected override Config DefaultConfig()
        {
            BC_2800Config config = new BC_2800Config();
            Config = config;
            config.SerialPortConfig = new SerialPort.SerialPortConfig
            {
                Baud = 9600,
                PortName = "COM3",
                EncodingName = "gb2312",
                Parity = Parity.None,
                DataBits = 7,
                StopBits = StopBits.One
            };
            config.ResultConfig = new List<ResultConfig>();
            //犬
            ResultConfig dog = new ResultConfig { Name = "犬", Code = "1000", Formula = "o=>o.KindOf==KindOfType.犬" };
            dog.Items = new List<ResultItemConfig>();
            dog.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "17", Min = "6", Unit = "X10^9/L", Name = "白细胞数目", Id = 1 });
            dog.Items.Add(new ResultItemConfig { Code = "Lymph#", EnglishName = "Lymph#", Format = "%C(%E)", Max = "5.1", Min = "0.8", Unit = "X10^9/L", Name = "淋巴细胞数目", Id = 2 });
            dog.Items.Add(new ResultItemConfig { Code = "Mon#", EnglishName = "Mon#", Format = "%C(%E)", Max = "1.8", Min = "0", Unit = "X10^9/L", Name = "单核细胞数", Id = 3 });
            dog.Items.Add(new ResultItemConfig { Code = "Gran#", EnglishName = "Gran#", Format = "%C(%E)", Max = "12.6", Min = "4.0", Unit = "X10^9/L", Name = "中性粒细胞数目", Id = 4 });
            dog.Items.Add(new ResultItemConfig { Code = "Lymph%", EnglishName = "Lymph%", Format = "%C(%E)", Max = "30", Min = "12", Unit = "%", Name = "淋巴细胞百分比", Id = 5 });
            dog.Items.Add(new ResultItemConfig { Code = "Mon%", EnglishName = "Mon%", Format = "%C(%E)", Max = "9.0", Min = "2.0", Unit = "%", Name = "单核细胞百分比", Id = 6 });
            dog.Items.Add(new ResultItemConfig { Code = "Gran%", EnglishName = "Gran%", Format = "%C(%E)", Max = "83", Min = "60", Unit = "%", Name = "中性粒细胞百分比", Id = 7 });
            dog.Items.Add(new ResultItemConfig { Code = "RBC%", EnglishName = "RBC", Format = "%C(%E)", Max = "8.5", Min = "5.5", Unit = "X10^12/L", Name = "红细胞数目", Id = 8 });
            dog.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "190", Min = "110", Unit = "%", Name = "血红蛋白", Id = 9 });
            dog.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "56", Min = "39", Unit = "%", Name = "红细胞压积", Id = 14 });
            dog.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "72", Min = "62", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            dog.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "25", Min = "20", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            dog.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "380", Min = "300", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            dog.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "15.5", Min = "11", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            dog.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "460", Min = "117", Unit = "X10^9/L", Name = "血小板数目", Id = 15 });
            dog.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "12.9", Min = "7", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            dog.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板分布宽度", Id = 17 });
            dog.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板压积", Id = 18 });

            config.ResultConfig.Add(dog);

            //mao
            ResultConfig cat = new ResultConfig { Name = "猫", Code = "1001", Formula = "o=>o.KindOf==KindOfType.猫" };
            cat.Items = new List<ResultItemConfig>();
            cat.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "19.5", Min = "5.5", Unit = "X10^9/L", Name = "白细胞数目", Id = 1 });
            cat.Items.Add(new ResultItemConfig { Code = "Lymph#", EnglishName = "Lymph#", Format = "%C(%E)", Max = "7.0", Min = "0.8", Unit = "X10^9/L", Name = "淋巴细胞数目", Id = 2 });
            cat.Items.Add(new ResultItemConfig { Code = "Mon#", EnglishName = "Mon#", Format = "%C(%E)", Max = "1.9", Min = "0", Unit = "X10^9/L", Name = "单核细胞数", Id = 3 });
            cat.Items.Add(new ResultItemConfig { Code = "Gran#", EnglishName = "Gran#", Format = "%C(%E)", Max = "15", Min = "2.1", Unit = "X10^9/L", Name = "中性粒细胞数目", Id = 4 });
            cat.Items.Add(new ResultItemConfig { Code = "Lymph%", EnglishName = "Lymph%", Format = "%C(%E)", Max = "45", Min = "12", Unit = "%", Name = "淋巴细胞百分比", Id = 5 });
            cat.Items.Add(new ResultItemConfig { Code = "Mon%", EnglishName = "Mon%", Format = "%C(%E)", Max = "9.0", Min = "2.0", Unit = "%", Name = "单核细胞百分比", Id = 6 });
            cat.Items.Add(new ResultItemConfig { Code = "Gran%", EnglishName = "Gran%", Format = "%C(%E)", Max = "85", Min = "35", Unit = "%", Name = "中性粒细胞百分比", Id = 7 });
            cat.Items.Add(new ResultItemConfig { Code = "RBC%", EnglishName = "RBC", Format = "%C(%E)", Max = "10", Min = "4.6", Unit = "X10^12/L", Name = "红细胞数目", Id = 8 });
            cat.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "153", Min = "93", Unit = "%", Name = "血红蛋白", Id = 9 });
            cat.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "49", Min = "28", Unit = "%", Name = "红细胞压积", Id = 14 });
            cat.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "52", Min = "39", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            cat.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "21", Min = "12", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            cat.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "380", Min = "300", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            cat.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "18", Min = "14", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            cat.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "514", Min = "100", Unit = "X10^9/L", Name = "血小板数目", Id = 15 });
            cat.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "11.8", Min = "5", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            cat.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板分布宽度", Id = 17 });
            cat.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板压积", Id = 18 });
            config.ResultConfig.Add(cat);


            ResultConfig horse = new ResultConfig { Name = "马", Code = "1002", Formula = "o=>o.KindOf==KindOfType.马" };
            horse.Items = new List<ResultItemConfig>();
            horse.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "19.5", Min = "5.5", Unit = "X10^9/L", Name = "白细胞数目", Id = 1 });
            horse.Items.Add(new ResultItemConfig { Code = "Lymph#", EnglishName = "Lymph#", Format = "%C(%E)", Max = "7.0", Min = "0.8", Unit = "X10^9/L", Name = "淋巴细胞数目", Id = 2 });
            horse.Items.Add(new ResultItemConfig { Code = "Mon#", EnglishName = "Mon#", Format = "%C(%E)", Max = "1.9", Min = "0", Unit = "X10^9/L", Name = "单核细胞数", Id = 3 });
            horse.Items.Add(new ResultItemConfig { Code = "Gran#", EnglishName = "Gran#", Format = "%C(%E)", Max = "15", Min = "2.1", Unit = "X10^9/L", Name = "中性粒细胞数目", Id = 4 });
            horse.Items.Add(new ResultItemConfig { Code = "Lymph%", EnglishName = "Lymph%", Format = "%C(%E)", Max = "45", Min = "12", Unit = "%", Name = "淋巴细胞百分比", Id = 5 });
            horse.Items.Add(new ResultItemConfig { Code = "Mon%", EnglishName = "Mon%", Format = "%C(%E)", Max = "9.0", Min = "2.0", Unit = "%", Name = "单核细胞百分比", Id = 6 });
            horse.Items.Add(new ResultItemConfig { Code = "Gran%", EnglishName = "Gran%", Format = "%C(%E)", Max = "85", Min = "35", Unit = "%", Name = "中性粒细胞百分比", Id = 7 });
            horse.Items.Add(new ResultItemConfig { Code = "RBC%", EnglishName = "RBC", Format = "%C(%E)", Max = "10", Min = "4.6", Unit = "X10^12/L", Name = "红细胞数目", Id = 8 });
            horse.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "153", Min = "93", Unit = "%", Name = "血红蛋白", Id = 9 });
            horse.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "49", Min = "28", Unit = "%", Name = "红细胞压积", Id = 14 });
            horse.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "52", Min = "39", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            horse.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "21", Min = "12", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            horse.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "380", Min = "300", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            horse.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "18", Min = "14", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            horse.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "514", Min = "100", Unit = "X10^9/L", Name = "血小板数目", Id = 15 });
            horse.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "11.8", Min = "5", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            horse.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板分布宽度", Id = 17 });
            horse.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板压积", Id = 18 });
            config.ResultConfig.Add(horse);


            ResultConfig Rat = new ResultConfig { Name = "大鼠", Code = "1003", Formula = "o=>o.KindOf==KindOfType.大鼠" };
            Rat.Items = new List<ResultItemConfig>();
            Rat.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "15.3", Min = "2.9", Unit = "X10^9/L", Name = "白细胞数目", Id = 1 });
            Rat.Items.Add(new ResultItemConfig { Code = "Lymph#", EnglishName = "Lymph#", Format = "%C(%E)", Max = "13.5", Min = "2.6", Unit = "X10^9/L", Name = "淋巴细胞数目", Id = 2 });
            Rat.Items.Add(new ResultItemConfig { Code = "Mon#", EnglishName = "Mon#", Format = "%C(%E)", Max = "0.5", Min = "0", Unit = "X10^9/L", Name = "单核细胞数", Id = 3 });
            Rat.Items.Add(new ResultItemConfig { Code = "Gran#", EnglishName = "Gran#", Format = "%C(%E)", Max = "3.2", Min = "0.4", Unit = "X10^9/L", Name = "中性粒细胞数目", Id = 4 });
            Rat.Items.Add(new ResultItemConfig { Code = "Lymph%", EnglishName = "Lymph%", Format = "%C(%E)", Max = "90.1", Min = "63.7", Unit = "%", Name = "淋巴细胞百分比", Id = 5 });
            Rat.Items.Add(new ResultItemConfig { Code = "Mon%", EnglishName = "Mon%", Format = "%C(%E)", Max = "4.5", Min = "1.5", Unit = "%", Name = "单核细胞百分比", Id = 6 });
            Rat.Items.Add(new ResultItemConfig { Code = "Gran%", EnglishName = "Gran%", Format = "%C(%E)", Max = "30.1", Min = "7.3", Unit = "%", Name = "中性粒细胞百分比", Id = 7 });
            Rat.Items.Add(new ResultItemConfig { Code = "RBC%", EnglishName = "RBC", Format = "%C(%E)", Max = "7.89", Min = "5.6", Unit = "X10^12/L", Name = "红细胞数目", Id = 8 });
            Rat.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "150", Min = "120", Unit = "%", Name = "血红蛋白", Id = 9 });
            Rat.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "46", Min = "36", Unit = "%", Name = "红细胞压积", Id = 14 });
            Rat.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "68.8", Min = "53", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            Rat.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "23.1", Min = "16", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            Rat.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "341", Min = "300", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            Rat.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "15.5", Min = "11", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            Rat.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "1610", Min = "100", Unit = "X10^9/L", Name = "血小板数目", Id = 15 });
            Rat.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "6.2", Min = "3.8", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            Rat.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板分布宽度", Id = 17 });
            Rat.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板压积", Id = 18 });
            config.ResultConfig.Add(Rat);

            ResultConfig Mouse = new ResultConfig { Name = "小鼠", Code = "1004", Formula = "o=>o.KindOf==KindOfType.小鼠" };
            Mouse.Items = new List<ResultItemConfig>();
            Mouse.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "16.8", Min = "0.8", Unit = "X10^9/L", Name = "白细胞数目", Id = 1 });
            Mouse.Items.Add(new ResultItemConfig { Code = "Lymph#", EnglishName = "Lymph#", Format = "%C(%E)", Max = "5.7", Min = "0.7", Unit = "X10^9/L", Name = "淋巴细胞数目", Id = 2 });
            Mouse.Items.Add(new ResultItemConfig { Code = "Mon#", EnglishName = "Mon#", Format = "%C(%E)", Max = "0.3", Min = "0", Unit = "X10^9/L", Name = "单核细胞数", Id = 3 });
            Mouse.Items.Add(new ResultItemConfig { Code = "Gran#", EnglishName = "Gran#", Format = "%C(%E)", Max = "1.8", Min = "0.1", Unit = "X10^9/L", Name = "中性粒细胞数目", Id = 4 });
            Mouse.Items.Add(new ResultItemConfig { Code = "Lymph%", EnglishName = "Lymph%", Format = "%C(%E)", Max = "90.6", Min = "55.8", Unit = "%", Name = "淋巴细胞百分比", Id = 5 });
            Mouse.Items.Add(new ResultItemConfig { Code = "Mon%", EnglishName = "Mon%", Format = "%C(%E)", Max = "6.0", Min = "1.8", Unit = "%", Name = "单核细胞百分比", Id = 6 });
            Mouse.Items.Add(new ResultItemConfig { Code = "Gran%", EnglishName = "Gran%", Format = "%C(%E)", Max = "38.9", Min = "8.6", Unit = "%", Name = "中性粒细胞百分比", Id = 7 });
            Mouse.Items.Add(new ResultItemConfig { Code = "RBC%", EnglishName = "RBC", Format = "%C(%E)", Max = "9.42", Min = "6.36", Unit = "X10^12/L", Name = "红细胞数目", Id = 8 });
            Mouse.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "143", Min = "110", Unit = "%", Name = "血红蛋白", Id = 9 });
            Mouse.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "44.6", Min = "34.6", Unit = "%", Name = "红细胞压积", Id = 14 });
            Mouse.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "58.3", Min = "48.2", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            Mouse.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "19", Min = "15.8", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            Mouse.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "353", Min = "302", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            Mouse.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "17", Min = "13", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            Mouse.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "1590", Min = "450", Unit = "X10^9/L", Name = "血小板数目", Id = 15 });
            Mouse.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "6", Min = "3.8", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            Mouse.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板分布宽度", Id = 17 });
            Mouse.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板压积", Id = 18 });
            config.ResultConfig.Add(Mouse);


            ResultConfig Rabbit = new ResultConfig { Name = "兔", Code = "1005", Formula = "o=>o.KindOf==KindOfType.兔" };
            Rabbit.Items = new List<ResultItemConfig>();
            Rabbit.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "13.5", Min = "5.2", Unit = "X10^9/L", Name = "白细胞数目", Id = 1 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "Lymph#", EnglishName = "Lymph#", Format = "%C(%E)", Max = "9", Min = "3.2", Unit = "X10^9/L", Name = "淋巴细胞数目", Id = 2 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "Mon#", EnglishName = "Mon#", Format = "%C(%E)", Max = "0.6", Min = "0.1", Unit = "X10^9/L", Name = "单核细胞数", Id = 3 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "Gran#", EnglishName = "Gran#", Format = "%C(%E)", Max = "7.5", Min = "2", Unit = "X10^9/L", Name = "中性粒细胞数目", Id = 4 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "Lymph%", EnglishName = "Lymph%", Format = "%C(%E)", Max = "75.6", Min = "35.2", Unit = "%", Name = "淋巴细胞百分比", Id = 5 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "Mon%", EnglishName = "Mon%", Format = "%C(%E)", Max = "6.0", Min = "2.5", Unit = "%", Name = "单核细胞百分比", Id = 6 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "Gran%", EnglishName = "Gran%", Format = "%C(%E)", Max = "59.3", Min = "20.2", Unit = "%", Name = "中性粒细胞百分比", Id = 7 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "RBC%", EnglishName = "RBC", Format = "%C(%E)", Max = "7.6", Min = "2.5", Unit = "X10^12/L", Name = "红细胞数目", Id = 8 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "170", Min = "105", Unit = "%", Name = "血红蛋白", Id = 9 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "46", Min = "31", Unit = "%", Name = "红细胞压积", Id = 14 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "66.5", Min = "56.8", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "25.1", Min = "20.1", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "370", Min = "320", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "18.5", Min = "13", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "712", Min = "100", Unit = "X10^9/L", Name = "血小板数目", Id = 15 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "6.8", Min = "3.8", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板分布宽度", Id = 17 });
            Rabbit.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板压积", Id = 18 });
            config.ResultConfig.Add(Rabbit);

            ResultConfig Monkey = new ResultConfig { Name = "猴", Code = "1006", Formula = "o=>o.KindOf==KindOfType.猴" };
            Monkey.Items = new List<ResultItemConfig>();
            Monkey.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "15.8", Min = "6.1", Unit = "X10^9/L", Name = "白细胞数目", Id = 1 });
            Monkey.Items.Add(new ResultItemConfig { Code = "Lymph#", EnglishName = "Lymph#", Format = "%C(%E)", Max = "7.6", Min = "1.9", Unit = "X10^9/L", Name = "淋巴细胞数目", Id = 2 });
            Monkey.Items.Add(new ResultItemConfig { Code = "Mon#", EnglishName = "Mon#", Format = "%C(%E)", Max = "1.5", Min = "0.4", Unit = "X10^9/L", Name = "单核细胞数", Id = 3 });
            Monkey.Items.Add(new ResultItemConfig { Code = "Gran#", EnglishName = "Gran#", Format = "%C(%E)", Max = "9.6", Min = "3.1", Unit = "X10^9/L", Name = "中性粒细胞数目", Id = 4 });
            Monkey.Items.Add(new ResultItemConfig { Code = "Lymph%", EnglishName = "Lymph%", Format = "%C(%E)", Max = "52", Min = "25", Unit = "%", Name = "淋巴细胞百分比", Id = 5 });
            Monkey.Items.Add(new ResultItemConfig { Code = "Mon%", EnglishName = "Mon%", Format = "%C(%E)", Max = "12", Min = "4", Unit = "%", Name = "单核细胞百分比", Id = 6 });
            Monkey.Items.Add(new ResultItemConfig { Code = "Gran%", EnglishName = "Gran%", Format = "%C(%E)", Max = "65", Min = "32", Unit = "%", Name = "中性粒细胞百分比", Id = 7 });
            Monkey.Items.Add(new ResultItemConfig { Code = "RBC%", EnglishName = "RBC", Format = "%C(%E)", Max = "6.8", Min = "4.3", Unit = "X10^12/L", Name = "红细胞数目", Id = 8 });
            Monkey.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "160", Min = "100", Unit = "%", Name = "血红蛋白", Id = 9 });
            Monkey.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "48", Min = "31", Unit = "%", Name = "红细胞压积", Id = 14 });
            Monkey.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "82", Min = "68", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            Monkey.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "28.3", Min = "21.1", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            Monkey.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "360", Min = "300", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            Monkey.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "16.2", Min = "11", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            Monkey.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "480", Min = "130", Unit = "X10^9/L", Name = "血小板数目", Id = 15 });
            Monkey.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "8.9", Min = "6.3", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            Monkey.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板分布宽度", Id = 17 });
            Monkey.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板压积", Id = 18 });
            config.ResultConfig.Add(Monkey);


            ResultConfig Cow = new ResultConfig { Name = "奶牛", Code = "1006", Formula = "o=>o.KindOf==KindOfType.奶牛" };
            Cow.Items = new List<ResultItemConfig>();
            Cow.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "15.8", Min = "6.1", Unit = "X10^9/L", Name = "白细胞数目", Id = 1 });
            Cow.Items.Add(new ResultItemConfig { Code = "Lymph#", EnglishName = "Lymph#", Format = "%C(%E)", Max = "7.6", Min = "1.9", Unit = "X10^9/L", Name = "淋巴细胞数目", Id = 2 });
            Cow.Items.Add(new ResultItemConfig { Code = "Mon#", EnglishName = "Mon#", Format = "%C(%E)", Max = "1.5", Min = "0.4", Unit = "X10^9/L", Name = "单核细胞数", Id = 3 });
            Cow.Items.Add(new ResultItemConfig { Code = "Gran#", EnglishName = "Gran#", Format = "%C(%E)", Max = "9.6", Min = "3.1", Unit = "X10^9/L", Name = "中性粒细胞数目", Id = 4 });
            Cow.Items.Add(new ResultItemConfig { Code = "Lymph%", EnglishName = "Lymph%", Format = "%C(%E)", Max = "52", Min = "25", Unit = "%", Name = "淋巴细胞百分比", Id = 5 });
            Cow.Items.Add(new ResultItemConfig { Code = "Mon%", EnglishName = "Mon%", Format = "%C(%E)", Max = "12", Min = "4", Unit = "%", Name = "单核细胞百分比", Id = 6 });
            Cow.Items.Add(new ResultItemConfig { Code = "Gran%", EnglishName = "Gran%", Format = "%C(%E)", Max = "65", Min = "32", Unit = "%", Name = "中性粒细胞百分比", Id = 7 });
            Cow.Items.Add(new ResultItemConfig { Code = "RBC%", EnglishName = "RBC", Format = "%C(%E)", Max = "6.8", Min = "4.3", Unit = "X10^12/L", Name = "红细胞数目", Id = 8 });
            Cow.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "160", Min = "100", Unit = "%", Name = "血红蛋白", Id = 9 });
            Cow.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "48", Min = "31", Unit = "%", Name = "红细胞压积", Id = 14 });
            Cow.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "82", Min = "68", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            Cow.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "28.3", Min = "21.1", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            Cow.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "360", Min = "300", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            Cow.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "16.2", Min = "11", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            Cow.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "480", Min = "130", Unit = "X10^9/L", Name = "血小板数目", Id = 15 });
            Cow.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "8.9", Min = "6.3", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            Cow.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板分布宽度", Id = 17 });
            Cow.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "", Min = "", Unit = "", Name = "血小板压积", Id = 18 });
            config.ResultConfig.Add(Cow);

            return config;

        }
    }
}
