using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DopIns
{
    public class DopInstruments
    {
        public static string ChangeDirectory(string inPath)
        {
            string outPath = string.Empty;
            for (int dLe = 0; dLe < inPath.Split("/").Length-1; dLe++)
            {
                if(dLe+1==inPath.Split("/").Length-1)
                {
                    outPath += inPath.Split("/")[dLe];
                }
                else
                {
                    outPath += inPath.Split("/")[dLe] + "/";
                }
            }
            return outPath;
        }
        public static string DeleteSpace()
        {
            string oldArgs = Environment.GetEnvironmentVariable("arguments");
            string newArgs = string.Empty;
            for (int symb = 1; symb < oldArgs.Length; symb++)
            {
                newArgs += oldArgs[symb];
            }
            return newArgs;
        }
    }
}
