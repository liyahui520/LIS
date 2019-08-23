using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    public interface IPrint
    {
        void Print(Result result);

        void Preview(Result result);

        void Print(Result result,string PrinterName);
        
        String PrintClassName { get; }
    }
}
