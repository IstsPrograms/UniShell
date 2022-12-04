using OperManager;
using Cmds;
using Hydrium;
using Services;
using ExecuteEngines;
string cPath; 
if(Environment.OSVersion.Platform == PlatformID.Win32NT)
{
    cPath = $"C:/Users/{Environment.UserName}/";
}
else if(Environment.OSVersion.Platform == PlatformID.Unix)
{
    if(Environment.UserName == "root")
    {
        cPath = $"/{Environment.UserName}/";
    }
    else
    {
        cPath = $"/home/{Environment.UserName}";
    }
}
else
{
    cPath = "Crash: Other Platform";
}
Environment.CurrentDirectory = cPath;
Engine.OutputSysNE("[PCONF] Path Configured");
Delegate[] cmds = { CommandsR.cd, CommandsR.dir, CommandsR.cls, CommandsR.ping, CommandsR.shutdown, CommandsR.fget, CommandsR.sys, CommandsR.echo, CommandsR.getd, CommandsR.to, CommandsR.mkzip, CommandsR.unzip, CommandsR.rm, CommandsR.write, CommandsR.mkdir, CommandsR.mkfile, CommandsR.help, CommandsR.contacts, CommandsR.ls, CommandsR.clear, CommandsR.about};
Action[] services = { };
Delegate[] engines = { Engines.exe, Engines.bat};
InvokingManagerRealtime invokeMgr = new InvokingManagerRealtime(cmds, services, engines);
await invokeMgr.MainCmd();
Console.ReadKey();