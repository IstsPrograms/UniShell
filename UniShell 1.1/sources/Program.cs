using System.Net.NetworkInformation;
using System.IO.Compression;

StartUp();

void StartUp()
{
    UniTools.UniTools tool = new UniTools.UniTools();
    Console.Title = "UniShell (linux arm64)";
    Main("/", tool);
}

void FileConsole(string file)
{
    try
    {
        while (true)
        {
            Console.Write($"[FC <{file}>] - ");
            string action = Console.ReadLine();
            switch (action.Split()[0])
            {
                case "read":
                    Console.WriteLine(System.IO.File.ReadAllText(file));
                    break;
                case "write":
                    string text = "";
                    for (int i = 1; i < action.Split().Length; i++)
                    {
                        if (i + 1 == action.Split().Length)
                        {
                            text += action.Split()[i];
                        }
                        else
                        {
                            text += action.Split()[i];
                            text += " ";
                        }
                    }
                    System.IO.File.WriteAllText(file, text);
                    break;
                case "appd":
                    string _text = " ";
                    for (int i = 1; i < action.Split().Length; i++)
                    {
                        if (i + 1 == action.Split().Length)
                        {
                            _text += action.Split()[i];
                        }
                        else
                        {
                            _text += action.Split()[i];
                            _text += " ";
                        }
                    }

                    System.IO.File.AppendAllText(file, _text);
                    break;
                case "exit":
                    return;
                case "erase":
                    System.IO.File.WriteAllText(file, "");
                    break;
                case "exec":
                    System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                    newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(file);
                    newProcess.Start();
                    break;
                case "appdl":
                    string __text = " ";
                    for (int i = 1; i < action.Split().Length; i++)
                    {
                        if (i + 1 == action.Split().Length)
                        {
                            __text += action.Split()[i];
                            __text += "\n";
                        }
                        else
                        {
                            __text += action.Split()[i];
                            __text += " ";
                        }

                    }

                    System.IO.File.AppendAllText(file, __text);
                    break;
                


            }
        }
    }
    catch
    {

    }
    
}
void Main(string path, UniTools.UniTools ut)
{
    Sys.Sys ss = new Sys.Sys("UniShell", "Public 1.0.0", "UniShellCore 1.1.0", "No updates");
    string npath = path;
    Console.WriteLine("UniShell Started!");
    while(true)
    {
        Console.Write($"{npath} > ");
        string action = Console.ReadLine();
        switch (action.Split()[0])
        {
            case "sys":
                ss.See();
                break;
            case "shutdown":
                return;
            case "cd":
                try
                {
                    if (action.Split()[1] == "..")
                    {
                        npath = ut.UniPathCreate(npath, path);
                        break;
                    }
                    else if (System.IO.Directory.Exists(npath + action.Split()[1]))
                    {
                        npath += action.Split()[1] + "/";
                        break;
                    }
                    else if (System.IO.File.Exists(npath + action.Split()[1]))
                    {
                        try
                        {
                            FileConsole(npath + action.Split()[1]);
                            break;
                        }
                        catch
                        {

                        }
                    }

                    string checkPath = "";
                    for (int i = 1; i < action.Split().Length; i++)
                    {
                        if (i + 1 == action.Split().Length)
                        {
                            checkPath += action.Split()[i];
                        }
                        else
                        {
                            checkPath += action.Split()[i];
                            checkPath += " ";
                        }
                    }
                    if (System.IO.Directory.Exists(npath + checkPath))
                    {
                        npath += checkPath + "/";
                        break;
                    }
                    else if (System.IO.File.Exists(npath + checkPath))
                    {
                        FileConsole(npath + checkPath);
                        break;
                    }
                }
                catch
                {

                }

                break;
            case "dir":
                string[] files = System.IO.Directory.GetFiles(npath);
                string[] dirs = System.IO.Directory.GetDirectories(npath);
                for (int i = 0; i < dirs.Length; i++)
                {
                    Console.WriteLine($"{dirs[i]}     DIR");
                }
                
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"{files[i]}     FILE");
                }
                break;
            case "mkdir":
                System.IO.Directory.CreateDirectory(npath + action.Split()[1]);
                break;
            case "mkfile":
                System.IO.File.Create(npath + action.Split()[1]);
                break;
            case "cls":
                Console.Clear();
                break;
            case "fget":
                Console.WriteLine("Downloading...");
                System.Net.WebClient wc = new System.Net.WebClient();
                try
                {
                    wc.DownloadFile(action.Split()[1], npath + action.Split()[2]);
                    Console.WriteLine("Downloaded");
                }
                catch
                {
                    Console.WriteLine("Failed!");
                }
                break;
            case "htmlget":
                try
                {
                    System.Net.WebClient wc2 = new System.Net.WebClient();
                    System.IO.File.WriteAllText(npath + action.Split()[2], wc2.DownloadString(action.Split()[1]));
                    Console.WriteLine("HTML saved!");
                }
                catch
                {
                    Console.WriteLine("Failed!");
                }
                break;
            case "rm":
                string deletingFormat = action.Split()[1];
                if(deletingFormat == "-dir" && System.IO.Directory.Exists(npath + action.Split()[2]))
                {
                    System.IO.Directory.Delete(npath + action.Split()[2]);
                }
                if (deletingFormat == "-file" && System.IO.File.Exists(npath + action.Split()[2]))
                {
                    System.IO.File.Delete(npath + action.Split()[2]);
                }
                break;
            case "ping":
                try
                {
                    Ping sender = new Ping();
                    PingOptions opt = new PingOptions();
                    opt.DontFragment = true;
                    byte[] b = System.Text.Encoding.ASCII.GetBytes("UniShell Message! Core ver. - 1.2.0");

                    for (int i = 0; i < 5; i++)
                    {
                        PingReply rp = sender.Send(action.Split()[1], 120, b, opt);
                        if (rp.Status == IPStatus.Success)
                        {
                            Console.WriteLine($"[ITERATION {i}] {rp.Address} {rp.RoundtripTime}ms");
                        }
                        else
                        {
                            Console.WriteLine($"STATUS: {rp.Status.ToString()}");
                        }
                    }
                }
                catch
                {

                }
                
                break;
            case "unzip":
                
                try
                {
                    if(File.Exists(npath + action.Split()[1]))
                    {
                        if(!Directory.Exists(npath + action.Split()[2]))
                        {
                            Directory.CreateDirectory(npath + action.Split()[2]);
                        }
                        ZipFile.ExtractToDirectory(npath + action.Split()[1], npath + $"{action.Split()[2]}/", true);
                    }
                    else
                    {
                        Console.WriteLine("File does not exists!");
                    }
                }
                catch
                {
                    
                }
                
                break;
            case "to":
                if (Directory.Exists(action.Split()[1]))
                {
                    try
                    {
                        if (action.Split()[1][action.Split().Length] == '/')
                        {
                            npath = action.Split()[1];
                        }
                    }
                    catch
                    {

                    }
                }
                break;
            case "ld":
                for (int i = 0; i < System.Environment.GetLogicalDrives().Length; i++)
                {
                    Console.WriteLine($"[{i}] {System.Environment.GetLogicalDrives()[i]}");
                }
                break;
            case "help":
                Console.WriteLine("ld Отобразить все логические диски \ncd <Директория/Файл> перейти в директорию/файл\ncd .. вернуться в предыдущую директорию");
                Console.WriteLine("unzip <Файл.zip> <Директория в которую надо распаковать> распаковка zip файла\nping <домен> проверить своё соединение");
                Console.WriteLine("rm <-file/-dir> <файл/директория> удалить файл/директорию\nhtmlget <сайт для взятия html кода> <выходной файл> сохранить html код");
                Console.WriteLine("fget <ссылка на файл> <выходной файл> загрузить файл \ncls отчистить консоль\nmkfile <название файла> создать файл\nmkdir <название директории> создать директорию");
                Console.WriteLine("dir вывод всего содержимого текущей директории\nshutdown выключение\nsys узнать версию ядра и самой UniShell");
                Console.WriteLine("mkzip <папка для создания zip> <выходной файл> <включать ли исходную директорию: да/нет (true/false)> создать архив");
                break;
            case "mkzip":
                try
                {
                    if (Directory.Exists(npath + action.Split()[1]))
                    {
                        bool includeDirectory = action.Split()[3] == "true";
                        ZipFile.CreateFromDirectory(npath + action.Split()[1], npath + action.Split()[2], CompressionLevel.SmallestSize, includeDirectory);
                    }
                }
                catch
                {

                }
                
                break;
        }
    }
}