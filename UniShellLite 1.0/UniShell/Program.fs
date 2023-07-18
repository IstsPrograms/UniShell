module UniFs
open System
open System.IO
open System.Diagnostics;
open System.Collections.Generic
open System.Globalization
open System.Net
open System.Net.Http
open System.Reflection
open System.Net.NetworkInformation
open CmdAttrLib;
let getMonthName n =
    DateTimeFormatInfo.CurrentInfo.MonthNames[n-1]
// All types

type Command = { Description: string; Execute: string -> int; }
type ExecuteDelegate = delegate of string -> int
let cmds = Dictionary<string, Command>()
// All commands
let cd_r (args: string) =
    if Directory.Exists args then
        Environment.CurrentDirectory <- args
    else
        printfn "Directory does not exist"
    0
let dir_r(args: string) =
    for i in Directory.GetDirectories(Environment.CurrentDirectory) do
        printfn $"{DirectoryInfo(i).Name}  --  DIR"
    for i in Directory.GetFiles(Environment.CurrentDirectory) do
        printfn $"{FileInfo(i).Name}  --  FILE"
    0
let clear_r(args: string) =
    Console.Clear()
    0
let exit_r(args: string) =
    exit(0)
    0
let echo_r(args:string) =
    printfn $"{args}"
    0
let date_r(args:string) =
    let mutable minutes = $"{DateTime.Now.Minute}"
    if minutes.Length = 1 then
        minutes <- $"0{minutes}"
    printfn $"{DateTime.Now.Day} {getMonthName DateTime.Now.Month}, {DateTime.Now.Hour}:{minutes}"
    0
let touch_r(args:string) =
    if File.Exists args then
        printfn "File is already exist"
    else
        File.Create(args)
        printfn "File created"
    0
let mkdir_r(args:string) =
    if Directory.Exists args then
        printfn "Directory is already exist"
    else
        Directory.CreateDirectory(args)
        printfn "Directory created"
    0
let rm_r(args:string) =
  try
    if File.GetAttributes(args).HasFlag(FileAttributes.Directory) then
      Directory.Delete args
    else
      File.Delete args
  with
  | ex -> printfn "%s" ex.Message
  0
let getd_r(args:string) =
    let drives = Environment.GetLogicalDrives()
    printfn "All devices/mounted drives:"
    for drive in drives do
        printfn $"| {drive}"
    0
let ping_r(args: string) =
    let ip = args.Split()[0]
    let iterations = int (args.Split()[1])
    let pingSender = new Ping()

    let mutable totalTime = 0L
    for i in 1 .. iterations do
        let reply = pingSender.Send(ip)
        if reply.Status = IPStatus.Success then
            totalTime <- totalTime + int64 reply.RoundtripTime

    let averageTime = totalTime / int64 iterations
    printfn "Ping result for %s: Average time = %d ms" ip averageTime
    0
let read_r(args: string) =
    if File.Exists args then
        printfn "File Content"
        printfn $"{File.ReadAllText args}"
    else
        printfn "File does not exist"
    0
let write_r(args: string) =
    File.WriteAllText(args.Split()[0], args.Replace($"{args.Split()[0]} ", ""))
    printfn "Successfully written"
    0
let execute_r(args: string) =
    if File.Exists args then
        try
            Process.Start(args) |> ignore
        with
        | _ -> printfn "File is not executable"
    else
        printfn "File does not exist"
    0
let search_r(args: string) =
    printfn "Results of searching: "
    for file in Directory.GetFiles(Environment.CurrentDirectory) do
        if FileInfo(file).Name.ToLowerInvariant().Contains(args.ToLowerInvariant()) then
            printfn $"| {FileInfo(file).Name}"
    0
let fget_r(args: string) =
    printfn "Downloading started"
    let fileName = args.Replace($"{args.Split()[0]} ", "")
    WebClient().DownloadFile(args.Split()[0], fileName)
    printfn $"Downloaded as {fileName}"
    0
let http_get_r(args: string) =
    try
        let client = HttpClient()
        let response = client.GetAsync(args).Result.Content.ReadAsStringAsync().Result;
        printfn $"Response: \n {response}"
    with
    | _ -> printfn "Not found(?)"
    0
let addcmd_r(args:string) =
    if File.Exists $"{Environment.CurrentDirectory}/{args}" then
        try 
            let makeCommand (t: ICommand) =
                let name = t.Name
                let description = t.Description;
                let execute = t.Execute
                (name, { Description = string(description); Execute = execute;})
            
            for t: Type in Assembly.LoadFile($"{Environment.CurrentDirectory}/{args}").GetTypes() do
                try
                    let instance = Activator.CreateInstance(t)
                    (makeCommand(downcast instance) |> cmds.Add) |> ignore
                with
                | x -> ()
        with
        | x -> ()
    else
        printfn "File does not exist"
    0
let remcmd_r(args:string) =
    if cmds.ContainsKey(args.ToLowerInvariant()) then
        cmds.Remove(args.ToLowerInvariant()) |> ignore
    else
        printfn "Command does not exist"
    0
let cmdinfo_r(args:string) =
    for e in cmds do
        printfn $"{e.Key} : {e.Value.Description}"
    0
let help_r(args:string) =
    let help = [
      "Help";
    "| cd (dir)";
    "| dir";
    "| clear";
    "| exit";
    "| echo (text)";
    "| date";
    "| touch (file name)";
    "| mkdir (directory name)";
    "| rm (file/directory)";
    "| getd";
    "| ping (IP) (iterations)";
    "| read (file name)";
    "| write (file name) (text)";
    "| execute (file name)";
    "| search (text)";
    "| fget (url) (file name)";
    "| http_get (url)";
    "| addcmd (.dll file with custom command)";
    "| remcmd (command name)";
    "| exscript (file name)";
    "| cmdinfo"
    ]
    List.iter (printfn "%s") help
    0
let sysinfo_r(args:string) =
    let sysinf = [
        "System info:";
        $"| OS: {Environment.OSVersion}"
        $"| Current User: {Environment.UserName}"
        $"| Processor count: {Environment.ProcessorCount}"
        $"| PC Name: {Environment.MachineName}"
        $"| Uptime: {Environment.TickCount64/int64(1000)/int64(60)}m"
        "UniShell info:"
        "| UniShell Version: Lite 1.0"
        "| HydriumLite Version: None"
        "Other:"
        $"| .NET Version: {Environment.Version}"
    ]
    List.iter (printfn "%s") sysinf
    0
Environment.CurrentDirectory <- "/"

// Adding commands to dictionary
cmds.Add ("cd", {Description = "Changes current directory"; Execute = cd_r })
cmds.Add ("dir", {Description = "Shows current directory's content"; Execute = dir_r })
cmds.Add ("clear", {Description = "Clears console"; Execute = clear_r })
cmds.Add ("exit", {Description = "Exit"; Execute = exit_r })
cmds.Add ("echo", {Description = "Outputs the entered text"; Execute = echo_r })
cmds.Add ("date", {Description = "Shows current date"; Execute = date_r })
cmds.Add ("touch", {Description = "Creates file"; Execute = touch_r })
cmds.Add ("mkdir", {Description = "Creates directory"; Execute = mkdir_r })
cmds.Add ("rm", {Description = "Removes file/directory"; Execute = rm_r })
cmds.Add ("getd", {Description = "Gets all names of mounted devices/drives"; Execute = getd_r })
cmds.Add ("ping", {Description = "Makes a ping-request"; Execute = ping_r })
cmds.Add ("read", {Description = "Reads file content"; Execute = read_r })
cmds.Add ("write", {Description = "Writes text to file"; Execute = write_r })
cmds.Add ("execute", {Description = "Executes file"; Execute = execute_r })
cmds.Add ("search", {Description = "Search file by name"; Execute = search_r })
cmds.Add ("fget", {Description = "Downloads file"; Execute = fget_r })
cmds.Add ("http_get", {Description = "Sends http request"; Execute = http_get_r })
cmds.Add ("addcmd", {Description = "Adds custom command"; Execute = addcmd_r })
cmds.Add ("remcmd", {Description = "Removes any command"; Execute = remcmd_r })
cmds.Add ("cmdinfo", {Description = "Shows commands names and descriptions"; Execute = cmdinfo_r })
cmds.Add ("help", {Description = "Help command"; Execute = help_r })
cmds.Add ("sysinfo", {Description = "Shows system info"; Execute = sysinfo_r })
let rec executeCommand(line: string) =
    if cmds.ContainsKey(line.Split()[0]) then
        (cmds[line.Split()[0]].Execute (line.Replace($"{line.Split()[0]} ", ""))) |> ignore
    match line.Split()[0] with
    | "exscript" ->
        try
            File.ReadAllText(line.Replace($"{line.Split()[0]} ", "")).Split("\n") |> Seq.iter executeCommand
        with
        | ex -> printfn "%s" ex.Message
    | _ -> ()
   

if Directory.Exists "packages" then
    let listofdlls = List<string>()
    for e in Directory.GetFiles("./packages") do
        if FileInfo(e).Extension = ".dll" && not(FileInfo(e).Name = "CmdAttrLib.dll") then
            executeCommand($"addcmd packages/{FileInfo(e).Name}")
        else
            ignore()
    if File.Exists "packages/init.sh" then
        executeCommand($"exscript ./packages/init.sh")




while true do
    printf $"{Environment.UserName}@{Environment.CurrentDirectory} > "
    let input = Console.ReadLine()
    
    executeCommand input
    