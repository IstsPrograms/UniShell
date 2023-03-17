using OperManager;
using Cmds;
using Hydrium;
using Logs;
using ExecuteEngines;

if(!Directory.Exists($"C:\\Users\\{Environment.UserName}\\UniPlugins"))
{
    Directory.CreateDirectory($"C:\\Users\\{Environment.UserName}\\UniPlugins");
}


var logger = new Logger();
logger.Log("Log Created, 7str");
Engine.OutputSysNE("[OPERTURE] Logger created");
string cPath = $"C:/Users/{Environment.UserName}/";
logger.Log("Configuring path");
Environment.CurrentDirectory = cPath;
Engine.OutputSysNE("[PCONF] Path Configured");
logger.Log("Creating CommandsR's commands array");
Delegate[] cmds = { CommandsR.cd, CommandsR.dir, CommandsR.cls, CommandsR.ping, CommandsR.shutdown, CommandsR.fget, CommandsR.sys, CommandsR.echo, CommandsR.getd, CommandsR.to, CommandsR.mkzip, CommandsR.unzip, CommandsR.rm, CommandsR.write, CommandsR.mkdir, CommandsR.mkfile, CommandsR.help, CommandsR.ls, CommandsR.clear, CommandsR.about, CommandsR.date, CommandsR.gc_collect, CommandsR.mono, CommandsR.fire, CommandsR.rgb,CommandsR.sea,CommandsR.rainbow, CommandsR.find, CommandsR.read, CommandsR.http_get};
Action[] services = { };
Delegate[] engines = { Engines.exe, Engines.bat};
logger.Log("Creating Operture");
Operture invokeMgr = new Operture(cmds, services, engines, logger);
logger.Log("Launching MainCmd");
await invokeMgr.MainCmd();
Console.ReadKey();