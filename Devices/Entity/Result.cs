using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    [Serializable]
    public class Result
    {
        /// <summary>
        /// 结果集
        /// </summary>
        public List<ResultItem> ResultDatas { get; set; }
        /// <summary>
        /// 对应的命令
        /// </summary>
        public Command CMD { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public IDevices Devices { get; set; }
        /// <summary>
        /// 源数据
        /// </summary>
        public object Source { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Date { get; set; }

        public Result()
        {
            Date = DateTime.Now;
        }
    }
    [Serializable]
    public class ResultItem
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string Display { get; set; }

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
        /// 结果
        /// </summary>
        public object Value { get; set; }
    }
}
