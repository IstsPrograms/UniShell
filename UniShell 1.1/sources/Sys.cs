using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys
{
    public class Sys
    {
        protected string shell;
        protected string shellVersion;
        protected string coreVersion;
        protected string updatesThread;


        public Sys(string shell, string shellVersion,string coreVersion, string updatesThread)
        {
            this.shell = shell;
            this.shellVersion = shellVersion;
            this.coreVersion = coreVersion;
            this.updatesThread = updatesThread;
        }

        public void See()
        {
            Console.WriteLine($"##         ##       SH: {shell}");
            Console.WriteLine($"##         ##       SHV: {shellVersion}");
            Console.WriteLine($"##         ##       CV: {coreVersion}");
            Console.WriteLine($"##         ##       UT: {updatesThread}");
            Console.WriteLine($"##         ##");
            Console.WriteLine($"##         ##");
            Console.WriteLine($"\\#\\       /#/");
            Console.WriteLine($" \\#########/ ");
        }
    }
}
