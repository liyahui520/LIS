using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    /// <summary>
    /// 设备信息
    /// </summary>
   [Serializable]
    public class DevicesInformation
    {
       public int Num { get; set; }
       public string Id { get; set; }
       /// <summary>
       /// 名称
       /// </summary>
       public string Name { get; set; }

       /// <summary>
       /// 型号
       /// </summary>
       public string Model { get; set; }

       /// <summary>
       /// 品牌
       /// </summary>
       public string Brand { get; set; }

       /// <summary>
       /// 编号
       /// </summary>
       public string Code { get; set; }

       /// <summary>
       /// 备注
       /// </summary>
       public string Remarks { get; set; }

       /// <summary>
       /// 官网
       /// </summary>
       public string Url { get; set; }

       public string ImagePath
       {
           get;
           set;
       }
       public System.Drawing.Image GetImage()
       {
           if (string.IsNullOrEmpty(ImagePath))
               return null;
           string path= AppDomain.CurrentDomain.BaseDirectory+"Devices\\img\\"+ImagePath;
           if (!System.IO.File.Exists(path))
               return null;
           return System.Drawing.Image.FromFile(path);
       }
       /// <summary>
       /// 
       /// </summary>
       public string ClassInfo { get; set; }
    }
}
