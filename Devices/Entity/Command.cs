using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    [Serializable]
    public class Command
    {
        /// <summary>
        /// 检查ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 检查ID
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 查检名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 宠物ID
        /// </summary>
        public string PetId { get; set; }
        /// <summary>
        /// 宠物名
        /// </summary>
        public string PetName { get; set; }

        /// <summary>
        /// 种类
        /// </summary>
        public KindOfType KindOf { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public GenderType Gender { get; set; }

        /// <summary>
        /// 年龄(月)
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 顾客
        /// </summary>
        public string Customer { get; set; }
        public string CustomerId { get; set; }

        /// <summary>
        /// 体重(KG)
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 医生
        /// </summary>
        public string Doctor { get; set; }

        public string DoctorId { get; set; }

        public DateTime Date { get; set; }

        public object Tag { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            System.Reflection.PropertyInfo [] pinfos= typeof(Command).GetProperties();
            if (pinfos != null && pinfos.Length > 0)
                pinfos.ToList().ForEach(o=>sb.Append(string.Format("\r\n{0}:{1}",o.Name,o.GetValue(this,null))));
            return sb.ToString();
            //return string.Format("Id:{0},Name:{1},PetName:{2},PetId:{3},Age{4}",Id,Name,PetName,PetId,Age);
        }
    }

    public enum GenderType
    {
        /// <summary>
        /// 雄
        /// </summary>
        Female = 1,
        /// <summary>
        /// 雌
        /// </summary>
        Male = 2
    }

    public enum KindOfType
    {
        犬 = 1,
        猫 = 2,
        马 = 3,
        大鼠 = 4,
        小鼠 = 5,
        兔 = 6,
        猴 = 7,
        奶牛 = 8,
        猪 = 9,
        水牛 = 10,
        骆驼 = 11,
        绵羊 = 12,
        山羊 = 13,
        龟 = 14,
        蜥蜴 = 15,
        其它 = 100
    }
}
