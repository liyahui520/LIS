using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices.Print
{
    public class UniversalPrint : IPrint
    {
        public string PrintClassName => "通用打印";

        public void Preview(Result result)
        {
            LabReportA4 labReportA4 = new LabReportA4(result);
            labReportA4.ShowPreviewDialog();
        }


        public void Print(Result result)
        {
            LabReportA4 labReportA4 = new LabReportA4(result);
            labReportA4.Print();
        }

        public void Print(Result result, string PrinterName)
        {
            LabReportA4 labReportA4 = new LabReportA4(result);
            labReportA4.Print(PrinterName);
        }
    }
}
