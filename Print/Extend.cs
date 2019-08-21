using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace Devices.Print
{
    public static class Extend
    {
        public static double GetNum(this string str)
        {
            if (string.IsNullOrEmpty(str.Trim()))
                return 0;
            string newstr = Regex.Replace(str, @"[^\d.\d]", "");
            double num = 0;
            double.TryParse(newstr,out num);
            return num;
        }
    }
}
