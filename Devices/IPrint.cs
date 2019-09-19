using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    public interface IPrint
    {
        void Print(List<Result> result);

        void Preview(List<Result> result);

        void Print(List<Result> result,string PrinterName);
        void Preview(List<Result> result,string PrinterName);

        PrintInfo PrintInfo { get;}
    }



}
