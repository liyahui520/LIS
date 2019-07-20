using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    [Serializable]
    public class ResultItemConfig
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 编号或简写 如WBC
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public string Min { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public string Max { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 显示
        /// </summary>
        public string Display
        {
            get
            {
                if (!string.IsNullOrEmpty(Format))
                    return Format.Replace("%C", Name).Replace("%A", Code).Replace("%U", Unit).Replace("%E",EnglishName);
                return Format;
            }
        }
        /// <summary>
        /// 显示格式串
        /// </summary>
        public string Format { get; set; }
    }
}
