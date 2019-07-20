using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
     [Serializable]
    public class ResultConfig 
    {
      private IFormula iformula;
      public string Name { get; set; }
      public string Code { get; set; }
      /// <summary>
      /// 条件,表达式
      /// </summary>
      public string Formula { get; set; }
      public List<ResultItemConfig> Items { get; set; }

      public bool InvokeFormula(Command cmd)
      {
          if (string.IsNullOrEmpty(Formula))
              return false;
          if (iformula == null)
              iformula = GetFormula(Formula);
          if (iformula != null)
             return iformula.InvokeFormula(cmd);
          return false;
      }
      public static IFormula GetFormula(string formulaStr)
      {
          return CSharpProvider.CreateFormula(formulaStr);
      }
    }

     public interface IFormula
     {
         bool InvokeFormula(Devices.Command cmd);
     }
}
