using Hydrium;
using System.Diagnostics;
namespace ExecuteEngines
{
    public class Engines
    {
        public static void exe()
        {
            string appToStart = Environment.GetEnvironmentVariable("runApp");
            if(Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Process.Start($"{Environment.CurrentDirectory}\\{appToStart}");
                Engine.OutputSysNE("[EXE] Executing Started");
            }
            else
            {
                Engine.OutputSysWE("[EXE] Incompatible platform");
            }
        }
        public static void bat()
        {
            string scriptToExecute = Environment.GetEnvironmentVariable("runApp");
            if(Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Process.Start($"{Environment.CurrentDirectory}\\{scriptToExecute}");
                Engine.OutputSysNE("[BAT] Script executing started!");
            }
            else
            {
                Engine.OutputSysWE("[BAT] Incompatible platform");
            }
        }
        public static void sh()
        {
            Engine.OutputSysWW("[SH] Bash scripts executing is an experimental function");
            string scriptToExecute = Environment.GetEnvironmentVariable("runApp");
            if(Environment.OSVersion.Platform == PlatformID.Unix)
            {
                Process.Start("bash", $"{scriptToExecute}");
                Engine.OutputSysNE("[SH] Script executing started");
            }
            else
            {
                Engine.OutputSysWE("[SH] Incompatible platform");
            }
        } 
    }
}
