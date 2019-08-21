using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Devices.Print;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Devices.Print
{
    public partial class LabReportA4Data : DevExpress.XtraReports.UI.XtraReport
    {
        public LabReportA4Data(Devices.Result result)
        {
            InitializeComponent();
            if (result.ResultDatas != null && result.ResultDatas.Count > 0)
            {
                int range = 0;
                foreach (Devices.ResultItem item in result.ResultDatas)
                {
                    DataRow newrow = reportDataSet1.TestingResults.NewRow();
                    var reg = new Regex(@"-?\d+(\.\d+)?");
                    string resultValue = item.Value.ToString().GetNum().ToString();
                    float r = 0;
                    float.TryParse(item.Value.ToString(), out r);
                    newrow["ID"] = item.Code;
                    newrow["Image"] = BitmapTobytes(GetImg(resultValue, Convert.ToInt32(item.Max.ToString().GetNum()), Convert.ToInt32(item.Min.ToString().GetNum()), out range));
                    newrow["IntRange"] = range;
                    newrow["ItemName"] = item.Display;
                    newrow["ResultValue"] = item.Value.ToString().GetNum();
                    newrow["ResultSymbol"] = item.Code;
                    newrow["Max"] = item.Max == null ? 0 : Convert.ToInt32(item.Max.ToString().GetNum());
                    //newrow["Range"] = TextImg(result, range);
                    newrow["Min"] = item.Min == null ? 0 : Convert.ToInt32(item.Min.ToString().GetNum());
                    newrow["Unit"] = item.Unit;
                    newrow["RangeText"] = GetRangeText(item.Max.ToString().GetNum(), item.Min.ToString().GetNum());
                    //newrow["Remark"] =item.Name;
                    reportDataSet1.TestingResults.Rows.Add(newrow);
                }
            }
        }

        #region 5期弃用-by hzhao

        #endregion

        private Bitmap GetImg(string value, decimal max, decimal min, out int range)
        {
            decimal tvalue = string.IsNullOrEmpty(value) ? 0 : Convert.ToDecimal(value);
            range = 0;

            int rectangleWidth = 50;
            int rectangleHeight = 12;

            Bitmap barRange = new Bitmap(155, 15);
            Graphics g = Graphics.FromImage(barRange);
            g.Clear(Color.White);
            g.DrawRectangle(new Pen(Color.Gray), new Rectangle(0, 2, rectangleWidth, rectangleHeight));
            g.DrawRectangle(new Pen(Color.Gray), new Rectangle(rectangleWidth, 2, rectangleWidth, rectangleHeight));
            g.DrawRectangle(new Pen(Color.Gray), new Rectangle(rectangleWidth * 2, 2, rectangleWidth, rectangleHeight));

            int n = 0;
            if (max == min)
            {
                if (tvalue < min)
                {
                    n = 0;
                }
                else if (tvalue > max)
                {
                    n = rectangleWidth * 3;
                }
                else
                {
                    n = rectangleWidth * 3 / 2;
                }
            }
            else
            {
                n = rectangleWidth + (int)Math.Round(rectangleWidth / (max - min) * (tvalue - min), 0);
            }
            if (n < 0)
                n = 0;
            if (n > rectangleWidth * 3 - 4)
                n = rectangleWidth * 3 - 4;
            if (!string.IsNullOrEmpty(value))
            {
                Color drawColor;
                if (tvalue < min)
                {
                    range = -1;
                    drawColor = Color.Green;
                }
                else if (tvalue > max)
                {
                    range = 1;
                    drawColor = Color.Red;
                }
                else
                {
                    range = 0;
                    drawColor = Color.Black;
                }
                g.FillRectangle(new SolidBrush(drawColor), new Rectangle(n, 2, 4, rectangleHeight));
            }
            return barRange;
        }
        private byte[] BitmapTobytes(Bitmap b)
        {
            if (b == null)
                return null;
            MemoryStream ms = new MemoryStream();
            b.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bytes = ms.GetBuffer();  //byte[]   bytes=   ms.ToArray(); 这两句都可以，至于区别么，下面有解释
            ms.Close();
            return bytes;
        }


        private string GetRangeText(double max, double min)
        {
            string ret = "";
            if (max < 100)
            {
                ret = string.Format("{0:F2}", min) + "-" + string.Format("{0:F2}", max);

            }
            else
            {
                ret = string.Format("{0:F1}", min) + "-" + string.Format("{0:F1}", max);
            }
            return ret;
        }

        private void Range_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRControl control = sender as XRControl;
            int range = Convert.ToInt32(control.Tag);


            Color foreColor = Color.Black;
            Font font = new Font("微软雅黑", 9f);
            if (range == -1)
            {
                foreColor = Color.Green;
            }
            else if (range == 1)
            {
                foreColor = Color.Red;
            }
            control.ForeColor = foreColor;
        }

    }
}
