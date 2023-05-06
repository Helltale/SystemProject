using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    public class AnalyzerMain
    {
        private static string Analizator(string cycle)
        {
            int indexCycle = cycle.IndexOf("do");
            if (indexCycle == -1) throw new Exception("Ошибка. Нет цикла do::while;");

            cycle = cycle.Insert(0, "int counterIterator = 0; \n");

            int indexStart = cycle.IndexOf('{', indexCycle) + 1;
            cycle = cycle.Insert(indexStart, "\n counterIterator++;\n");
            cycle = cycle.Insert(cycle.Length, "\nConsole.WriteLine(counterIterator);\n");

            return string.Format("using System; namespace Cycle\n {{ class CycleClass{{static void Main(string[] args){{{0}}}}}}}", cycle);
        }

        public static string Analyz(string cycle)
        {
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
            CompilerResults results = codeProvider.CompileAssemblyFromSource(new CompilerParameters { GenerateExecutable = true, OutputAssembly = "Out.exe" }, Analizator(cycle));

            if (results.Errors.Count > 0) return results.Errors[0].ErrorText;
            else
            {
                Process process = new Process();
                process.StartInfo.FileName = "Out.exe";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = false;

                var sb = new StringBuilder(string.Empty);

                process.Start();
                while (!process.StandardOutput.EndOfStream)
                    sb.Append(process.StandardOutput.ReadLine());
                process.Close();

                return sb.ToString();
            }
        }

    }
}
