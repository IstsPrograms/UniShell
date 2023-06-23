using System.Diagnostics;
using System.IO.Compression;
using System.Net.NetworkInformation;
using Hydrium;
using System.Net;
namespace Quant.Commands;
using Quant.Cs;

public class Cd : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "cd";
    public string Description { get; set; } = "Directory navigation command";
    public void Execute(params string[] arg)
    {
        if (Directory.Exists(arg[0]))
        {
            Environment.CurrentDirectory = arg[0];
        }
        else
        {
            Engine.OutputSysWE("<Cd> Directory does not exist");
        }
    }
}

public class Dir : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "dir";
    public string Description { get; set; } = "View directory contents";
    public void Execute(params string[] arg)
    {

        List<string> content = new List<string>() {"Directory content: "};
        foreach (var dir in Directory.GetDirectories(Environment.CurrentDirectory).ToList())
        {
            content.Add($"| [DIR] {new DirectoryInfo(dir).Name}");
        }
        foreach (var file in Directory.GetFiles(Environment.CurrentDirectory).ToList())
        {
            content.Add($"| [FILE] {new FileInfo(file).Name}");
        }

        Engine.OutputList(content);
    }
}

public class Clear : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "clear";
    public string Description { get; set; } = "Clear console";
    public void Execute(params string[] arg)
    {
        Console.Clear();
    }
}

public class Exit : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "exit";
    public string Description { get; set; } = "Exit";
    public void Execute(params string[] arg)
    {
        Environment.Exit(0);
    }
}

public class Echo : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "echo";

    public string Description { get; set; } = "Displays the entered text";
    public void Execute(params string[] arg)
    {
        Engine.OutputSysNE($"<Echo> {arg[0]}");
    }
}

public enum Months
{
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10,
    November = 11,
    December = 12
}

public class Date : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "date";
    public string Description { get; set; } = "Gets the current date";
    public void Execute(params string[] arg)
    {
        string minutes = $"{DateTime.Now.Minute}";
        if (minutes.Length == 1)
        {
            minutes = $"0{minutes}";
        }
        Engine.OutputSysNE($"<Date> {DateTime.Now.Day} {(Months)DateTime.Now.Month} {DateTime.Now.Year}, {DateTime.Now.Hour}:{minutes}");
    }
}

public class Touch : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.Guest;
    public string Name { get; set; } = "touch";
    public string Description { get; set; } = "Creates file";
    public void Execute(params string[] arg)
    {
        if (!File.Exists(arg[0]))
        {
            File.Create(arg[0]);
            Engine.OutputSysNE($"<Touch> Created file with name '{arg[0]}'");
        }
        else
        {
            Engine.OutputSysWE($"<Touch> File with name '{arg[0]}' is already exist");
        }
    }
}

public class Mkdir : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.Guest;
    public string Name { get; set; } = "mkdir";
    public string Description { get; set; } = "Creates directory";
    public void Execute(params string[] arg)
    {
        if (!Directory.Exists(arg[0]))
        {
            Directory.CreateDirectory(arg[0]);
            Engine.OutputSysNE($"<Mkdir> Created directory with name '{arg[0]}'");
        }
        else
        {
            Engine.OutputSysWE($"<Mkdir> Directory with name '{arg[0]}' is already exist");
        }
    }
}

public class Rm : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.Guest;
    public string Name { get; set; } = "rm";
    public string Description { get; set; } = "Removes file";
    public void Execute(params string[] arg)
    {
        if (File.Exists(arg[0]))
        {
            File.Delete(arg[0]);
            Engine.OutputSysNE($"<Rm> File with name '{arg[0]}' deleted");
        }
        else
        {
            Engine.OutputSysWE($"<Rm> File with name '{arg[0]}' not exist");
        }
    }
}

public class Getd : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "getd";
    public string Description { get; set; } = "Get all logical drives/mounted drives";
    public void Execute(params string[] arg)
    {
        List<string> drives = new List<string>() { "All mounted drives: " };
        foreach (var drive in Environment.GetLogicalDrives())
        {
            drives.Add($"| {drive}");
        }
        Engine.OutputList(drives);
    }
}
public class PingCommand : IQuantCommand
{
    public string Name { get; set; } = "ping";
    public string Description { get; set; } = "Ping a website or IP address";
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public void Execute(params string[] arg)
    {
        if (arg[0].Split().Length <= 1)
        {
            Engine.OutputSysWE("<Ping> Not enough arguments.");
            return;
        }

        string host = arg[0].Split()[0];
        int count = 4;
        if (int.TryParse(arg[0].Split()[1], out int parsedCount))
        {
            count = parsedCount;
        }

        Ping pingSender = new Ping();
        PingOptions options = new PingOptions()
        {
            DontFragment = true
        };

        for (int i = 0; i < count; i++)
        {
            PingReply reply = pingSender.Send(host);

            if (reply.Status == IPStatus.Success)
            {
                Engine.OutputSysWW($"<Ping> Reply from {reply.Address}: bytes={reply.Buffer.Length} time={reply.RoundtripTime}");
            }
            else
            {
                Engine.OutputSysWE($"<Ping> Status: {reply.Status}");
            }
        }
    }
}

public class Read : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.Guest;
    public string Name { get; set; } = "read";
    public string Description { get; set; } = "Reads file's content";
    public void Execute(params string[] arg)
    {
        if (File.Exists(arg[0]))
        {
            Engine.OutputNSys(File.ReadAllText(arg[0]));
        }
        else
        {
            Engine.OutputSysWE($"<Read> File with name {arg[0]} does not exist");
        }
    }
}

public class Write : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.Root;
    public string Name { get; set; } = "write";
    public string Description { get; set; } = "Writes content to file";
    public void Execute(params string[] arg)
    {
        if (File.Exists(arg[0].Split()[0]))
        {
            string content = arg[0].Replace("write ", "");
            File.WriteAllText(arg[0].Split()[0], content);
            Engine.OutputSysNE("<Write> Successfully written");
        }
        else
        {
            Engine.OutputSysWE($"<Write> File with name '{arg[0]}' does not exist");
        }
    }
}

public class SystemInfo : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "sysinfo";
    public string Description { get; set; } = "System info";
    public void Execute(params string[] arg)
    {
        Engine.OutputList(new List<string>()
        {
            "System info:",
            $"| OS: {Environment.OSVersion}",
            $"| Current User: {Environment.UserName}",
            $"| Processor count: {Environment.ProcessorCount}",
            $"| PC Name: {Environment.MachineName}",
            "UniShell info:",
            "| UniShell Version: 3.0",
            "| HydriumLite Version: 1.3",
            "| Quant.cs Version: Modified 1.0 Pre-release"
        });
    }
}

public class ExecuteFile : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.Root;
    public string Name { get; set; } = "execute";
    public string Description { get; set; } = "Executes a file";
    public void Execute(params string[] arg)
    {
        if (File.Exists(arg[0]))
        {
            try
            {
                Engine.OutputSysWW("<Execute> Starting...");
                Process.Start(arg[0]);
            }
            catch
            {
                Engine.OutputSysWE($"<Execute> File is not executable or does not have executable permission");
            }
        }
        else
        {
            Engine.OutputSysWE($"<Execute> File with name '{arg[0]}' does not exist");
        }
    }
}

public class Search : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "search";
    public string Description { get; set; } = "Search file by name";
    public void Execute(params string[] arg)
    {
        List<string> results = new List<string>() { "Searching results:" };
        foreach (var file in Directory.GetFiles(Environment.CurrentDirectory))
        {
            if (new FileInfo(file).Name.ToLowerInvariant().Contains(arg[0].ToLowerInvariant()))
            {
                results.Add($"| {new FileInfo(file).Name}");
            }
        }
        Engine.OutputList(results);
    }
}

public class Fget : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "fget";
    public string Description { get; set; } = "Downloads file from link";
    public void Execute(params string[] arg)
    {
        new Thread(new ThreadStart(DownloadProcess)).Start();
        Engine.OutputSysNE("<Fget> Downloading started");
        void DownloadProcess()
        {
            try
            {
                new WebClient().DownloadFile(arg[0].Split()[0], arg[0].Split()[1]);
                Engine.OutputSysNE($"\n<Fget> File downloaded as '{arg[0].Split()[1]}'");
                Engine.InputTextSysOutput($"{Environment.UserName}@{Environment.CurrentDirectory} > ");
            }
            catch
            {
                Engine.OutputSysWE($"\n<Fget> : DownloadProcess> Error while downloading");
                Engine.InputTextSysOutput($"{Environment.UserName}@{Environment.CurrentDirectory} > ");
            }
        }
    }
}

public class Mkzip : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.Guest;
    public string Name { get; set; } = "mkzip";
    public string Description { get; set; } = "Makes zip archive";
    public void Execute(params string[] arg)
    {
        try
        {
            if (Directory.Exists(arg[0].Split()[0]))
            {
                ZipFile.CreateFromDirectory(arg[0].Split()[0], arg[0].Split()[1], CompressionLevel.SmallestSize, true);
                Engine.OutputSysNE("<Mkzip> Successfully created zip");
            }
            else
            {
                Engine.OutputSysWE("<Mkzip> Directory does not exist");
            }
        }
        catch
        {
            Engine.OutputSysWE("<Mkzip> Unknown error");
        }
    }
}

public class Unzip : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "unzip";
    public string Description { get; set; } = "Unzips the file";
    public void Execute(params string[] arg)
    {
        try
        {
            if (File.Exists(arg[0].Split()[0]))
            {
                if (!Directory.Exists(arg[0].Split()[1]))
                {
                    Directory.CreateDirectory(arg[0].Split()[1]);
                }
                ZipFile.ExtractToDirectory(arg[0].Split()[0], $"{arg[0].Split()[1]}", true);
                Engine.OutputSysNE($"<Unzip> Successfully unzipped");
            }
            else
            {
                Engine.OutputSysWE("<Unzip> File does not exists");
            }
                
        }
        catch
        {
            Engine.OutputSysWE("<Unzip> Unknown error");
        }
    }
}

public class ColorEcho : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "cecho";
    public string Description { get; set; } = "Color echo";
    public void Execute(params string[] arg)
    {
        ColorOutputPattern pattern = ColorOutputPattern.Mono;
        switch (arg[0].Split()[0])
        {
            case "mono":
                pattern = ColorOutputPattern.Mono;
                break;
            case "fire":
                pattern = ColorOutputPattern.Fire;
                break;
            case "rgb":
                pattern = ColorOutputPattern.RGB;
                break;
            case "sea":
                pattern = ColorOutputPattern.Sea;
                break;
            case "rainbow":
                pattern = ColorOutputPattern.Rainbow;
                break;
            default:
                Engine.OutputSysWE("<Cecho> Pattern was not specified");
                return;
        }
        
        string content = arg[0].Replace($"{pattern.ToString().Split('.')[0].ToLowerInvariant()} ", "");
        Engine.ColorPatternOutput(content, pattern);
        Console.WriteLine();
    }
}

public class Help : IQuantCommand
{
    public QuantPermissions permissionLevel { get; } = QuantPermissions.None;
    public string Name { get; set; } = "help";
    public string Description { get; set; } = "Help command";
    public void Execute(params string[] arg)
    {
        Engine.OutputList(new List<string>()
        {
            "Commands usages:",
            "| help",
            "| cd (directory)",
            "| dir",
            "| clear",
            "| ping (domain) [iterations]",
            "| exit",
            "| fget (link to file)",
            "| sysinfo",
            "| echo (text)",
            "| getd",
            "| mkzip (source directory name) (output zip file name)",
            "| unzip (file name)",
            "| rm (file name)",
            "| read (file name)",
            "| write (file name)",
            "| mkdir (directory name)",
            "| touch (file name)",
            "| date",
            "| execute (file name)",
            "| cecho <mono/sea/rgb/fire/rainbow> (text)",
            "| exscript (file name with script)",
            "| cmdinfo - Get description about all IQuantCommands"
        });
    }
}