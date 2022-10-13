using System.Net.NetworkInformation;
using Hydrium;
using DopIns;
using System.IO.Compression;

namespace Cmds
{
    public class CommandsR
    {
        public static void cd()
        {
            string getArgs = DopInstruments.DeleteSpace();
            if (getArgs != null)
            {
                if (getArgs == "..")
                {
                    Environment.CurrentDirectory = DopInstruments.ChangeDirectory(Environment.CurrentDirectory);
                }
                else
                {
                    if(Directory.Exists(Environment.CurrentDirectory + $"\\{getArgs.Replace(" ", "")}"))
                    {
                        Environment.CurrentDirectory += $"\\{getArgs.Replace(" ", "")}";
                    }
                    else if (File.Exists($"{Environment.CurrentDirectory}\\{getArgs}"))
                    {
                        Engine.OutputNSys(File.ReadAllText($"{Environment.CurrentDirectory}\\{getArgs}"));
                    }
                    else
                    {
                        Engine.OutputSysWE("[CD] Error: directory/file does not exists");
                    }
                }
            }
        }

        public static void dir()
        {
            string getArgs = Environment.CurrentDirectory;
            for (int dirs = 0; dirs < Directory.GetDirectories(getArgs).Length; dirs++)
            {
                Engine.OutputSysNE($"[DIR] {Directory.GetDirectories(getArgs)[dirs]}");
            }
            for (int files = 0; files < Directory.GetFiles(getArgs).Length; files++)
            {
                Engine.OutputSysNE($"[FILE] {Directory.GetFiles(getArgs)[files]}");
            }
        }

        public static void cls()
        {
            Console.Clear();
        }

        public static void ping()
        {
            string newArgs = DopInstruments.DeleteSpace();
            try
            {
                Ping sender = new Ping();
                PingOptions senderOptions = new PingOptions()
                {
                    DontFragment = true
                };
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes("OperTure Ping");
                for (int i = 0; i < Convert.ToInt32(newArgs.Split()[1]); i++)
                {
                    PingReply rp = sender.Send(newArgs.Split()[0], 120, bytes, senderOptions);
                    if (rp.Status == IPStatus.Success)
                    {
                        Engine.OutputSysNE($"[ITERATION {i}] {rp.Address} {rp.RoundtripTime}ms");
                    }
                    else
                    {
                        Engine.OutputSysWE($"[ITERATION {i}] {rp.Status}");
                    }
                }
            }
            catch
            {

            }
        }
        public static void shutdown()
        {
            Environment.Exit(0);
        }
        public static void fget()
        {
            string newArgs = DopInstruments.DeleteSpace();
            Engine.OutputSysNE("[FGET] Downloading");
            System.Net.WebClient wc = new System.Net.WebClient();
            try
            {
                wc.DownloadFile(newArgs.Split()[0], $"{Environment.CurrentDirectory}\\" + newArgs.Split()[1]);
                Engine.OutputSysNE("[FGET] Downloaded");
            }
            catch
            {
                Engine.OutputSysWE("[FGET] Failed");
            }
        }
        public static void sys()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"##             ##");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"     OSV: {Environment.OSVersion}");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"##             ##");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"     DMN: {Environment.MachineName}");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"##             ##");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"     ENG: OperTure 1.1");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"##             ##");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"     ENV: HydriumLite 1.0");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"##             ##");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"     SHV: UniShell 2.1 TEST1");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"##             ##");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("     CMD: CommandsR 1.0");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($" ##           ##");
            Console.WriteLine($"  ##         ## ");
            Console.WriteLine($"   ###########");
            Console.WriteLine($"     #######");
        }
        public static void echo()
        {
            Engine.OutputSysNE($"[ECHO] {DopInstruments.DeleteSpace()}");
        }
        public static void unzip()
        {
            string args = DopInstruments.DeleteSpace();
            try
            {
                if (File.Exists($"{Environment.CurrentDirectory}\\" + args.Split()[0]))
                {
                    if (!Directory.Exists($"{Environment.CurrentDirectory}\\" + args.Split()[1]))
                    {
                        Directory.CreateDirectory($"{Environment.CurrentDirectory}\\" + args.Split()[1]);
                    }
                    ZipFile.ExtractToDirectory($"{Environment.CurrentDirectory}\\" + args.Split()[0], $"{Environment.CurrentDirectory}\\" + $"{args.Split()[1]}/", true);
                    Engine.OutputSysNE($"[UNZIP] Succesfully unzipped");
                }
                else
                {
                    Engine.OutputSysWE("[UNZIP] File does not exists");
                }
                
            }
            catch
            {

            }
        }
        public static void getd()
        {
            for (int i = 0; i < Environment.GetLogicalDrives().Length; i++)
            {
                Engine.OutputSysNE($"[{i}] {Environment.GetLogicalDrives()[i]}");
            }
        }
        public static void to()
        {
            string getArgs = DopInstruments.DeleteSpace();
            if(Directory.Exists(getArgs))
            {
                Environment.CurrentDirectory = getArgs;
            }
        }
        public static void mkzip()
        {
            string getArgs = DopInstruments.DeleteSpace();
            try
            {
                if (Directory.Exists($"{Environment.CurrentDirectory}\\" + getArgs.Split()[0]))
                {
                    ZipFile.CreateFromDirectory($"{Environment.CurrentDirectory}\\" + getArgs.Split()[0], $"{Environment.CurrentDirectory}\\" + getArgs.Split()[1], CompressionLevel.SmallestSize, true);
                    Engine.OutputSysNE("[MKZIP] Succefsully created zip");
                }
            }
            catch
            {
            }
        }
        public static void rm()
        {
            string getArgs = DopInstruments.DeleteSpace();
            try
            {
                if (File.Exists($"{Environment.CurrentDirectory}\\{getArgs.Split()[0]}") || (Directory.Exists($"{Environment.CurrentDirectory}\\{getArgs.Split()[0]}") && Directory.GetFiles($"{Environment.CurrentDirectory}\\{getArgs.Split()[0]}").Length == 0))
                {
                    File.Delete($"{Environment.CurrentDirectory}\\{getArgs.Split()[0]}");
                    Engine.OutputSysNE("[RM] File deleted");
                }
                else
                {
                    Engine.OutputSysWE("[RM] File does not exists/Directory is not empty");
                }
            }
            catch
            {
                Engine.OutputSysWE("[RM] Access to this pass is denied");
            }
        }

        public static void read()
        {
            string getArgs = DopInstruments.DeleteSpace();
            if(File.Exists($"{Environment.CurrentDirectory}\\{getArgs}"))
            {
                Engine.OutputNSys(File.ReadAllText($"{Environment.CurrentDirectory}\\{getArgs}"));
            }
        }

        public static void write()
        {
            string getArgs = DopInstruments.DeleteSpace();
            
            try
            {
                string toFile = getArgs.Replace(getArgs.Split()[0], "");
                
                
                File.WriteAllText($"{Environment.CurrentDirectory}\\{getArgs.Split()[0]}", toFile);
                Engine.OutputSysNE("[WRITE] Succesfully writed!");
            }
            catch
            {
                Engine.OutputSysWE("[WRITE] Unknown error");
            }
        }

        

        public static void mkdir()
        {
            string getArgs = DopInstruments.DeleteSpace();
            try
            {
                if(Directory.Exists($"{Environment.CurrentDirectory}\\{getArgs}"))
                {
                    Engine.OutputSysWE("[MKDIR] Directory is already exists");
                }
                else
                {
                    Directory.CreateDirectory($"{Environment.CurrentDirectory}\\{getArgs}");
                }
            }
            catch
            {
                Engine.OutputSysWE("[MKDIR] Unknown error");
            }
        }

        public static void mkfile()
        {
            string getArgs = DopInstruments.DeleteSpace();
            if (File.Exists($"{Environment.CurrentDirectory}\\{getArgs}"))
            {
                Engine.OutputSysWE("[MKFILE] File already exists");
            }
            else
            {
                File.Create($"{Environment.CurrentDirectory}\\{getArgs}");
                Engine.OutputSysNE("[MKFILE] File successfully created");
            }
        }
        public static void help()
        {
            Engine.OutputSysNE("[HELP] cd <dir/..>");
            Engine.OutputSysNE("[HELP] dir");
            Engine.OutputSysNE("[HELP] cls");
            Engine.OutputSysNE("[HELP] ping <domain> <iterations>");
            Engine.OutputSysNE("[HELP] shutdown");
            Engine.OutputSysNE("[HELP] fget <link to file>");
            Engine.OutputSysNE("[HELP] sys");
            Engine.OutputSysNE("[HELP] echo <text>");
            Engine.OutputSysNE("[HELP] getd");
            Engine.OutputSysNE("[HELP] to <path>");
            Engine.OutputSysNE("[HELP] mkzip <directory for create zip> <output zip>");
            Engine.OutputSysNE("[HELP] rm <file/dir>");
            Engine.OutputSysNE("[HELP] read <file>");
            Engine.OutputSysNE("[HELP] write <file> <text>");
            Engine.OutputSysNE("[HELP] mkdir <dir name>");
            Engine.OutputSysNE("[HELP] mkfile <file name>");
            Engine.OutputSysNE("[HELP] unzip <zip name> <dir name>");
        }

    }
}
