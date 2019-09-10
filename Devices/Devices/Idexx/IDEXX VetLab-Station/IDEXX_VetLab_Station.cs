using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Devices.IDEXX
{
    [Serializable]
    public class IDEXX_VetLab_Station : DevicesBase<IDEXX_VetLab_StationConfig>
    {
        IDEXX_VetLab_StationProtocol.FileProtocol fileProtocol;
        public IDEXX_VetLab_Station(string fileName)
            : base(fileName)
        {
            fileProtocol = new IDEXX_VetLab_StationProtocol.FileProtocol();
            Protocol = fileProtocol;
            Protocol.Init(this);
        }

        public override System.Windows.Forms.DialogResult ShowConfigForm()
        {
            UCIDEXX_VetLab_StationConfig uc = new UCIDEXX_VetLab_StationConfig(this);
            return uc.ShowForm().ShowDialog();
        }

        public override bool AddCommand(Command cmd)
        {
            fileProtocol.SendCMD(cmd, true);
            return base.AddCommand(cmd);
        }



        #region private


        /// <summary>
        /// 设备信息
        /// </summary>
        protected override DevicesInformation DefaultInfo()
        {
            DevicesInformation info = new DevicesInformation
            {
                Num = 1,
                Brand = "IDEXX",
                Model = "IDEXX VetLab-Station",
                Name = "IDEXX 工作站",
                Remarks = "IDEXX 工作站",
                Code = "IDEXX_VetLab_Station",
                Url = "https://www.idexx.de/de/veterinary/analyzers/idexx-vet-lab-station/",
                ImagePath = "vetlab-station.jpg",
            };
            return info;
        }


        #endregion

        protected override Config DefaultConfig()
        {
            IDEXX_VetLab_StationConfig config = new IDEXX_VetLab_StationConfig();
            config.PDFPath = "D:\\idxpdf\\";
            config.ResultPath = "D:\\idxdata\\";
            config.RequestPath = "D:\\idx\\";
            config.DateFormat = "MM/dd/yyyy HH:mm:ss";

            config.ResultConfig = new List<ResultConfig>();
            //所有
            ResultConfig dog = new ResultConfig { Name = "所有", Code = "1700", Formula = "o=>true" };
            dog.Items = new List<ResultItemConfig>();
            dog.Items.Add(new ResultItemConfig { Code = "RBC", EnglishName = "RBC", Name = "红细胞计数", Format = "%C(%E)", Id = 1 });
            dog.Items.Add(new ResultItemConfig { Code = "HCT", EnglishName = "HCT", Name = "红细胞压积", Format = "%C(%E)", Id = 2 });
            dog.Items.Add(new ResultItemConfig { Code = "HGB", EnglishName = "HGB", Name = "血红蛋白", Format = "%C(%E)", Id = 3 });
            dog.Items.Add(new ResultItemConfig { Code = "MCV", EnglishName = "MCV", Name = "红细胞平均体积", Format = "%C(%E)", Id = 4 });
            dog.Items.Add(new ResultItemConfig { Code = "MCH", EnglishName = "MCH", Name = "平均红细胞血红蛋白含量", Format = "%C(%E)", Id = 5 });
            dog.Items.Add(new ResultItemConfig { Code = "MCHC", EnglishName = "MCHC", Name = "平均红细胞血红蛋白浓度", Format = "%C(%E)", Id = 6 });
            dog.Items.Add(new ResultItemConfig { Code = "RDW", EnglishName = "RDW", Name = "红细胞分布宽度", Format = "%C(%E)", Id = 7 });
            dog.Items.Add(new ResultItemConfig { Code = "RETIC", EnglishName = "RETIC", Name = "网织红细胞计数", Format = "%C(%E)", Id = 8 });
            dog.Items.Add(new ResultItemConfig { Code = "%RETIC", EnglishName = "%RETIC", Name = "网织红细胞百分比", Format = "%C(%E)", Id = 9 });
            dog.Items.Add(new ResultItemConfig { Code = "nRBC", EnglishName = "nRBC", Name = "有核红细胞", Format = "%C(%E)", Id = 10 });
            dog.Items.Add(new ResultItemConfig { Code = "RETIC-HGB", EnglishName = "RETIC-Hgb", Name = "网织红细胞血红蛋白", Format = "%C(%E)", Id = 11 });
            dog.Items.Add(new ResultItemConfig { Code = "WBC", EnglishName = "WBC", Name = "白细胞计数", Format = "%C(%E)", Id = 1 });
            dog.Items.Add(new ResultItemConfig { Code = "NEU", EnglishName = "NEU", Name = "嗜中性粒细胞计数", Format = "%C(%E)", Id = 2 });
            dog.Items.Add(new ResultItemConfig { Code = "%NEU", EnglishName = "%NEU", Name = "嗜中性粒细胞百分比", Format = "%C(%E)", Id = 3 });
            dog.Items.Add(new ResultItemConfig { Code = "LYM", EnglishName = "LYM", Name = "淋巴细胞计数", Format = "%C(%E)", Id = 4 });
            dog.Items.Add(new ResultItemConfig { Code = "%LYM", EnglishName = "%LYM", Name = "淋巴细胞百分比", Format = "%C(%E)", Id = 5 });
            dog.Items.Add(new ResultItemConfig { Code = "MONO", EnglishName = "MONO", Name = "单核细胞计数", Format = "%C(%E)", Id = 6 });
            dog.Items.Add(new ResultItemConfig { Code = "%MONO", EnglishName = "%MONO", Name = "单核细胞百分比", Format = "%C(%E)", Id = 7 });
            dog.Items.Add(new ResultItemConfig { Code = "EOS", EnglishName = "EOS", Name = "嗜酸性粒细胞计数", Format = "%C(%E)", Id = 8 });
            dog.Items.Add(new ResultItemConfig { Code = "%EOS", EnglishName = "%EOS", Name = "嗜酸性粒细胞百分比", Format = "%C(%E)", Id = 9 });
            dog.Items.Add(new ResultItemConfig { Code = "BASO", EnglishName = "BASO", Name = "嗜碱性粒细胞计数", Format = "%C(%E)", Id = 10 });
            dog.Items.Add(new ResultItemConfig { Code = "%BASO", EnglishName = "%BASO", Name = "嗜碱性粒细胞百分比", Format = "%C(%E)", Id = 11 });
            dog.Items.Add(new ResultItemConfig { Code = "BAND", EnglishName = "BAND", Name = "杆状嗜中性粒细胞", Format = "%C(%E)", Id = 12 });

            dog.Items.Add(new ResultItemConfig { Code = "PLT", EnglishName = "PLT", Name = "血小板计数", Format = "%C(%E)", Id = 9 });
            dog.Items.Add(new ResultItemConfig { Code = "MPV", EnglishName = "MPV", Name = "平均血小板体积", Format = "%C(%E)", Id = 10 });
            dog.Items.Add(new ResultItemConfig { Code = "PDW", EnglishName = "PDW", Name = "血小板分布宽度", Format = "%C(%E)", Id = 11 });
            dog.Items.Add(new ResultItemConfig { Code = "PCT", EnglishName = "PCT", Name = "血小板压积", Format = "%C(%E)", Id = 12 });

            config.ResultConfig.Add(dog);

            return config;
        }
    }

    public enum CmdType
    {
        work_request_20,
        census_20
    }
}
