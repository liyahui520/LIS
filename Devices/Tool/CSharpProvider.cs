using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Linq.Expressions;

namespace Devices
{
    internal class CSharpProvider
    {
        internal static Devices.IFormula CreateFormula(string lambda)
        {
            string nameSpace = "Tool";
            string className = "CSharpTool";
            CSharpCodeProvider compiler = new CSharpCodeProvider();
         
            CompilerParameters paras = new CompilerParameters();
            paras.ReferencedAssemblies.Add(AppDomain.CurrentDomain.BaseDirectory+"Devices.dll");
            paras.GenerateExecutable = false;
            paras.GenerateInMemory = true;

            StringBuilder sb = new StringBuilder();
            sb.Append(" using  System;" + Environment.NewLine);
            sb.Append(" using  Devices;" + Environment.NewLine);
            sb.Append(" namespace " + nameSpace + " { " + Environment.NewLine);
            sb.Append("public  class  " + className + ": Devices.IFormula { " + Environment.NewLine);
            sb.Append("public bool InvokeFormula(Devices.Command cmd){ return new Func<Devices.Command, bool>(");
            sb.Append(lambda);
            sb.Append(").Invoke(cmd);}");
            sb.Append("}" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            string source = sb.ToString();
            CompilerResults result = compiler.CompileAssemblyFromSource(paras, source);
            CompilerErrorCollection error = result.Errors;
            if (error.HasErrors)
            {
                return null;
            }


            Assembly assembly = result.CompiledAssembly;
            object eval = assembly.CreateInstance(nameSpace + "." + className);
            return (Devices.IFormula)eval;
        }
    }
}
