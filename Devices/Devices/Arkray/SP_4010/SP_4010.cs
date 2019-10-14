using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

using Devices.Fujifilm;
using System.Windows.Forms;

namespace Devices.Arkray
{
    public class SP_4010 : DevicesBase<FF_6450Config>
    {
        public SP_4010(string fileName)
            : base(fileName)
        {
            //Protocol =null;
            //Protocol.Init(this);
        }

        protected override DevicesInformation DefaultInfo()
        {
            DevicesInformation info = new DevicesInformation
            {
                Num =11,
                Brand = "爱科来(Arkray)",
                Model = "PocketChem UA PU-4010",
                Name = "小型尿液分析仪PocketChem UA PU-4010",
                Remarks = "小型尿液分析仪",
                Code = "PocketChem_UA_PU_4010",
                Url = "http://www.arkray.cn/chinese/products/laboratory/analyzers/pu-4010.html",
               
            };
            info.ImagePath = info.Code + ".jpg";
            return info;
        }
        public override DialogResult ShowConfigForm()
        {
            return DialogResult.OK;
        }

        protected override Config DefaultConfig()
        {
            FF_6450Config config = new FF_6450Config();
            config.SerialPortConfig = new SerialPort.SerialPortConfig
            {
                Baud = 9600,
                PortName = "COM1",
                EncodingName = "utf-8",
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One
            };
            config.ResultConfig = new List<ResultConfig>();
            //犬
            ResultConfig dog = new ResultConfig { Name = "犬", Code = "1000", Formula = "o=>o.KindOf==KindOfType.犬" };
            dog.Items = new List<ResultItemConfig>();
            dog.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "18", Min = "12", Unit = "g/dL", Name = "血红蛋白", Id = 9 });
            dog.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "55", Min = "37", Unit = "%", Name = "红细胞压积", Id = 14 });
            dog.Items.Add(new ResultItemConfig { Code = "RBC", EnglishName = "RBC", Format = "%C(%E)", Max = "8.5", Min = "5.5", Unit = "10^6/μL", Name = "红细胞数目", Id = 8 });
            dog.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "77", Min = "60", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            dog.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "24.5", Min = "19.4", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            dog.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "36", Min = "32", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            dog.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "16", Min = "12", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            dog.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "17", Min = "6", Unit = "10^3/μL", Name = "白细胞数目", Id = 1 });
            dog.Items.Add(new ResultItemConfig { Code = "LY%", EnglishName = "LY%", Format = "%C(%E)", Max = "30", Min = "12", Unit = "%", Name = "淋巴细胞百分比", Id = 5 });
            dog.Items.Add(new ResultItemConfig { Code = "MO%", EnglishName = "MO%", Format = "%C(%E)", Max = "10", Min = "3", Unit = "%", Name = "单核细胞百分比", Id = 6 });
            dog.Items.Add(new ResultItemConfig { Code = "EO%", EnglishName = "EO%", Format = "%C(%E)", Max = "10", Min = "2", Unit = "%", Name = "嗜酸性细胞百分率", Id = 6 });
            dog.Items.Add(new ResultItemConfig { Code = "GR%", EnglishName = "GR%", Format = "%C(%E)", Max = "80", Min = "60", Unit = "%", Name = "中性粒细胞百分比", Id = 6 });
            dog.Items.Add(new ResultItemConfig { Code = "LY#", EnglishName = "LY#", Format = "%C(%E)", Max = "4.8", Min = "1", Unit = "10^3/μL", Name = "淋巴细胞数目", Id = 6 });
            dog.Items.Add(new ResultItemConfig { Code = "MO#", EnglishName = "MO#", Format = "%C(%E)", Max = "1.4", Min = "0.2", Unit = "10^3/μL", Name = "单核细胞数目", Id = 3 });
            dog.Items.Add(new ResultItemConfig { Code = "EO#", EnglishName = "EO#", Format = "%C(%E)", Max = "1.3", Min = "0.1", Unit = "10^3/μL", Name = "嗜酸性细胞数目", Id = 6 });
            dog.Items.Add(new ResultItemConfig { Code = "GR#", EnglishName = "GR#", Format = "%C(%E)", Max = "11.8", Min = "3", Unit = "X10^9/L", Name = "中性粒细胞数目", Id = 4 });
            dog.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "500", Min = "200", Unit = "10^3/μL", Name = "血小板数目", Id = 15 });
            dog.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "2.9", Min = "0", Unit = "%", Name = "血小板压积", Id = 18 });
            dog.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "11.1", Min = "6.7", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            dog.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "50", Min = "0", Unit = "%", Name = "血小板分布宽度", Id = 17 });
            config.ResultConfig.Add(dog);

            ResultConfig cat = new ResultConfig { Name = "猫", Code = "1000", Formula = "o=>o.KindOf==KindOfType.猫" };
            cat.Items = new List<ResultItemConfig>();
            cat.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "18", Min = "12", Unit = "g/dL", Name = "血红蛋白", Id = 9 });
            cat.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "55", Min = "37", Unit = "%", Name = "红细胞压积", Id = 14 });
            cat.Items.Add(new ResultItemConfig { Code = "RBC", EnglishName = "RBC", Format = "%C(%E)", Max = "8.5", Min = "5.5", Unit = "10^6/μL", Name = "红细胞数目", Id = 8 });
            cat.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "77", Min = "60", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            cat.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "24.5", Min = "19.4", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            cat.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "36", Min = "32", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            cat.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "16", Min = "12", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            cat.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "17", Min = "6", Unit = "10^3/μL", Name = "白细胞数目", Id = 1 });
            cat.Items.Add(new ResultItemConfig { Code = "LY%", EnglishName = "LY%", Format = "%C(%E)", Max = "30", Min = "12", Unit = "%", Name = "淋巴细胞百分比", Id = 5 });
            cat.Items.Add(new ResultItemConfig { Code = "MO%", EnglishName = "MO%", Format = "%C(%E)", Max = "10", Min = "3", Unit = "%", Name = "单核细胞百分比", Id = 6 });
            cat.Items.Add(new ResultItemConfig { Code = "EO%", EnglishName = "EO%", Format = "%C(%E)", Max = "10", Min = "2", Unit = "%", Name = "嗜酸性细胞百分率", Id = 6 });
            cat.Items.Add(new ResultItemConfig { Code = "GR%", EnglishName = "GR%", Format = "%C(%E)", Max = "80", Min = "60", Unit = "%", Name = "中性粒细胞百分比", Id = 6 });
            cat.Items.Add(new ResultItemConfig { Code = "LY#", EnglishName = "LY#", Format = "%C(%E)", Max = "4.8", Min = "1", Unit = "10^3/μL", Name = "淋巴细胞数目", Id = 6 });
            cat.Items.Add(new ResultItemConfig { Code = "MO#", EnglishName = "MO#", Format = "%C(%E)", Max = "1.4", Min = "0.2", Unit = "10^3/μL", Name = "单核细胞数目", Id = 3 });
            cat.Items.Add(new ResultItemConfig { Code = "EO#", EnglishName = "EO#", Format = "%C(%E)", Max = "1.3", Min = "0.1", Unit = "10^3/μL", Name = "嗜酸性细胞数目", Id = 6 });
            cat.Items.Add(new ResultItemConfig { Code = "GR#", EnglishName = "GR#", Format = "%C(%E)", Max = "11.8", Min = "3", Unit = "X10^9/L", Name = "中性粒细胞数目", Id = 4 });
            cat.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "500", Min = "200", Unit = "10^3/μL", Name = "血小板数目", Id = 15 });
            cat.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "2.9", Min = "0", Unit = "%", Name = "血小板压积", Id = 18 });
            cat.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "11.1", Min = "6.7", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            cat.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "50", Min = "0", Unit = "%", Name = "血小板分布宽度", Id = 17 });
            config.ResultConfig.Add(cat);


            ResultConfig cow = new ResultConfig { Name = "牛", Code = "1000", Formula = "o=>o.KindOf==KindOfType.奶牛" };
            cow.Items = new List<ResultItemConfig>();
            cow.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "18", Min = "12", Unit = "g/dL", Name = "血红蛋白", Id = 9 });
            cow.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "55", Min = "37", Unit = "%", Name = "红细胞压积", Id = 14 });
            cow.Items.Add(new ResultItemConfig { Code = "RBC", EnglishName = "RBC", Format = "%C(%E)", Max = "8.5", Min = "5.5", Unit = "10^6/μL", Name = "红细胞数目", Id = 8 });
            cow.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "77", Min = "60", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            cow.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "24.5", Min = "19.4", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            cow.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "36", Min = "32", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            cow.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "16", Min = "12", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            cow.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "17", Min = "6", Unit = "10^3/μL", Name = "白细胞数目", Id = 1 });
            cow.Items.Add(new ResultItemConfig { Code = "LY%", EnglishName = "LY%", Format = "%C(%E)", Max = "30", Min = "12", Unit = "%", Name = "淋巴细胞百分比", Id = 5 });
            cow.Items.Add(new ResultItemConfig { Code = "MO%", EnglishName = "MO%", Format = "%C(%E)", Max = "10", Min = "3", Unit = "%", Name = "单核细胞百分比", Id = 6 });
            cow.Items.Add(new ResultItemConfig { Code = "EO%", EnglishName = "EO%", Format = "%C(%E)", Max = "10", Min = "2", Unit = "%", Name = "嗜酸性细胞百分率", Id = 6 });
            cow.Items.Add(new ResultItemConfig { Code = "GR%", EnglishName = "GR%", Format = "%C(%E)", Max = "80", Min = "60", Unit = "%", Name = "中性粒细胞百分比", Id = 6 });
            cow.Items.Add(new ResultItemConfig { Code = "LY#", EnglishName = "LY#", Format = "%C(%E)", Max = "4.8", Min = "1", Unit = "10^3/μL", Name = "淋巴细胞数目", Id = 6 });
            cow.Items.Add(new ResultItemConfig { Code = "MO#", EnglishName = "MO#", Format = "%C(%E)", Max = "1.4", Min = "0.2", Unit = "10^3/μL", Name = "单核细胞数目", Id = 3 });
            cow.Items.Add(new ResultItemConfig { Code = "EO#", EnglishName = "EO#", Format = "%C(%E)", Max = "1.3", Min = "0.1", Unit = "10^3/μL", Name = "嗜酸性细胞数目", Id = 6 });
            cow.Items.Add(new ResultItemConfig { Code = "GR#", EnglishName = "GR#", Format = "%C(%E)", Max = "11.8", Min = "3", Unit = "X10^9/L", Name = "中性粒细胞数目", Id = 4 });
            cow.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "500", Min = "200", Unit = "10^3/μL", Name = "血小板数目", Id = 15 });
            cow.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "2.9", Min = "0", Unit = "%", Name = "血小板压积", Id = 18 });
            cow.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "11.1", Min = "6.7", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            cow.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "50", Min = "0", Unit = "%", Name = "血小板分布宽度", Id = 17 });
            config.ResultConfig.Add(cow);


            ResultConfig rat = new ResultConfig { Name = "大鼠", Code = "1000", Formula = "o=>o.KindOf==KindOfType.大鼠" };
            rat.Items = new List<ResultItemConfig>();
            rat.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "18", Min = "12", Unit = "g/dL", Name = "血红蛋白", Id = 9 });
            rat.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "55", Min = "37", Unit = "%", Name = "红细胞压积", Id = 14 });
            rat.Items.Add(new ResultItemConfig { Code = "RBC", EnglishName = "RBC", Format = "%C(%E)", Max = "8.5", Min = "5.5", Unit = "10^6/μL", Name = "红细胞数目", Id = 8 });
            rat.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "77", Min = "60", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            rat.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "24.5", Min = "19.4", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            rat.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "36", Min = "32", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            rat.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "16", Min = "12", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            rat.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "17", Min = "6", Unit = "10^3/μL", Name = "白细胞数目", Id = 1 });
            rat.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "500", Min = "200", Unit = "10^3/μL", Name = "血小板数目", Id = 15 });
            rat.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "2.9", Min = "0", Unit = "%", Name = "血小板压积", Id = 18 });
            rat.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "11.1", Min = "6.7", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            rat.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "50", Min = "0", Unit = "%", Name = "血小板分布宽度", Id = 17 });
            config.ResultConfig.Add(rat);


            ResultConfig mouse = new ResultConfig { Name = "小鼠", Code = "1000", Formula = "o=>o.KindOf==KindOfType.小鼠" };
            mouse.Items = new List<ResultItemConfig>();
            mouse.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "18", Min = "12", Unit = "g/dL", Name = "血红蛋白", Id = 9 });
            mouse.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "55", Min = "37", Unit = "%", Name = "红细胞压积", Id = 14 });
            mouse.Items.Add(new ResultItemConfig { Code = "RBC", EnglishName = "RBC", Format = "%C(%E)", Max = "8.5", Min = "5.5", Unit = "10^6/μL", Name = "红细胞数目", Id = 8 });
            mouse.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "77", Min = "60", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            mouse.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "24.5", Min = "19.4", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            mouse.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "36", Min = "32", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            mouse.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "16", Min = "12", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            mouse.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "17", Min = "6", Unit = "10^3/μL", Name = "白细胞数目", Id = 1 });
            mouse.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "500", Min = "200", Unit = "10^3/μL", Name = "血小板数目", Id = 15 });
            mouse.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "2.9", Min = "0", Unit = "%", Name = "血小板压积", Id = 18 });
            mouse.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "11.1", Min = "6.7", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            mouse.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "50", Min = "0", Unit = "%", Name = "血小板分布宽度", Id = 17 });
            config.ResultConfig.Add(mouse);


            ResultConfig horse = new ResultConfig { Name = "马", Code = "1000", Formula = "o=>o.KindOf==KindOfType.马" };
            horse.Items = new List<ResultItemConfig>();
            horse.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Format = "%C(%E)", Max = "18", Min = "12", Unit = "g/dL", Name = "血红蛋白", Id = 9 });
            horse.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Format = "%C(%E)", Max = "55", Min = "37", Unit = "%", Name = "红细胞压积", Id = 14 });
            horse.Items.Add(new ResultItemConfig { Code = "RBC", EnglishName = "RBC", Format = "%C(%E)", Max = "8.5", Min = "5.5", Unit = "10^6/μL", Name = "红细胞数目", Id = 8 });
            horse.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Format = "%C(%E)", Max = "77", Min = "60", Unit = "fL", Name = "平均红细胞休积", Id = 11 });
            horse.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Format = "%C(%E)", Max = "24.5", Min = "19.4", Unit = "Pg", Name = "平均血红细胞蛋白含量 ", Id = 12 });
            horse.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Format = "%C(%E)", Max = "36", Min = "32", Unit = "g/L", Name = "平均红细胞血红蛋白浓度", Id = 10 });
            horse.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Format = "%C(%E)", Max = "16", Min = "12", Unit = "%", Name = "红细胞分布宽度变异系数", Id = 13 });
            horse.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Format = "%C(%E)", Max = "17", Min = "6", Unit = "10^3/μL", Name = "白细胞数目", Id = 1 });
            horse.Items.Add(new ResultItemConfig { Code = "LY%", EnglishName = "LY%", Format = "%C(%E)", Max = "30", Min = "12", Unit = "%", Name = "淋巴细胞百分比", Id = 5 });
            horse.Items.Add(new ResultItemConfig { Code = "MO%", EnglishName = "MO%", Format = "%C(%E)", Max = "10", Min = "3", Unit = "%", Name = "单核细胞百分比", Id = 6 });
            horse.Items.Add(new ResultItemConfig { Code = "EO%", EnglishName = "EO%", Format = "%C(%E)", Max = "10", Min = "2", Unit = "%", Name = "嗜酸性细胞百分率", Id = 6 });
            horse.Items.Add(new ResultItemConfig { Code = "GR%", EnglishName = "GR%", Format = "%C(%E)", Max = "80", Min = "60", Unit = "%", Name = "中性粒细胞百分比", Id = 6 });
            horse.Items.Add(new ResultItemConfig { Code = "LY#", EnglishName = "LY#", Format = "%C(%E)", Max = "4.8", Min = "1", Unit = "10^3/μL", Name = "淋巴细胞数目", Id = 6 });
            horse.Items.Add(new ResultItemConfig { Code = "MO#", EnglishName = "MO#", Format = "%C(%E)", Max = "1.4", Min = "0.2", Unit = "10^3/μL", Name = "单核细胞数目", Id = 3 });
            horse.Items.Add(new ResultItemConfig { Code = "EO#", EnglishName = "EO#", Format = "%C(%E)", Max = "1.3", Min = "0.1", Unit = "10^3/μL", Name = "嗜酸性细胞数目", Id = 6 });
            horse.Items.Add(new ResultItemConfig { Code = "GR#", EnglishName = "GR#", Format = "%C(%E)", Max = "11.8", Min = "3", Unit = "X10^9/L", Name = "中性粒细胞数目", Id = 4 });
            horse.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Format = "%C(%E)", Max = "500", Min = "200", Unit = "10^3/μL", Name = "血小板数目", Id = 15 });
            horse.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Format = "%C(%E)", Max = "2.9", Min = "0", Unit = "%", Name = "血小板压积", Id = 18 });
            horse.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Format = "%C(%E)", Max = "11.1", Min = "6.7", Unit = "fL", Name = "平均血小板体积", Id = 16 });
            horse.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Format = "%C(%E)", Max = "50", Min = "0", Unit = "%", Name = "血小板分布宽度", Id = 17 });
            config.ResultConfig.Add(horse);
            return config;
        }


    }
}
