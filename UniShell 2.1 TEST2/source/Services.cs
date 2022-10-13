using System.Net;
using System.IO.Compression;
using Hydrium;
using System.Security.Cryptography.X509Certificates;
using DopIns;

namespace Services
{
    public class ServiceR
    {
        public static async void CheckSystem()
        {
            var date = DateTime.Now;
            Check();
            Engine.InputTextSysOutput($"{Environment.UserName}@{Environment.MachineName}:{Environment.CurrentDirectory} > ");
            while (true)
            {
                if(DateTime.Now.Minute == date.Minute+10)
                {
                    Check();
                    date = DateTime.Now;
                    Engine.InputTextSysOutput($"{Environment.UserName}@{Environment.MachineName}:{Environment.CurrentDirectory} > ");
                }
            }
            static void Check()
            {
                Engine.OutputSysNE("\n[CHECKING SERVICE] Checking...");
                string repoZip = $"{DopInstruments.ChangeDirectory(Environment.CommandLine).Replace('\\', '/')}/check.zip";
                Console.WriteLine(repoZip);
                string repoZipFolder = $"{DopInstruments.ChangeDirectory(Environment.CommandLine).Replace('\\', '/')}/check";
                string thisDll = $"{DopInstruments.ChangeDirectory(Environment.CommandLine).Replace('\\', '/')}/check/UniShell-main/UniShell 2.1 TEST2/publish/win-x86/Operture.dll";
                if (!File.Exists(repoZip))
                {
                    var downloadForCheck = new WebClient();
                    downloadForCheck.DownloadFile(new Uri("https://github.com/IstsPrograms/UniShell/archive/refs/heads/main.zip"), repoZip);
                }
                if (!Directory.Exists(repoZipFolder))
                {
                    Directory.CreateDirectory(repoZipFolder);
                }
                ZipFile.ExtractToDirectory(repoZip, repoZipFolder, true);
                if (!File.Exists(thisDll))
                {
                    var downloadForCheck = new WebClient();
                    downloadForCheck.DownloadFile(new Uri("https://github.com/IstsPrograms/UniShell/archive/refs/heads/main.zip"), repoZip);
                    ZipFile.ExtractToDirectory(repoZip, repoZipFolder, true);
                }
                var check = File.ReadAllBytes(Environment.CommandLine) == File.ReadAllBytes(thisDll);
                if (!check)
                {
                    Engine.OutputSysWE("[CHECKING SERVICE] Checking completed, bytes of your version do not match the bytes of the version from official repository");
                }
                else
                {
                    Engine.OutputSysNE("[CHECKING SERVICE] Checking completed, bytes of your version match the bytes of the version from official repository");
                }
                File.Delete(repoZipFolder);
            }
            
        }
    }
}
