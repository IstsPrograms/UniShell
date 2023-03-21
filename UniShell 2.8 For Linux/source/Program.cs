using OperManager;
using Cmds;
using Hydrium;
using Logs;

if(!Directory.Exists($"/UniPlugins"))
{
    Directory.CreateDirectory($"/UniPlugins");
}


var logger = new Logger();
logger.Log("Log Created, 7str");
Engine.OutputSysNE("[OPERTURE] Logger created");
string cPath = $"/";
logger.Log("Configuring path");
Environment.CurrentDirectory = cPath;
Engine.OutputSysNE("[PCONF] Path Configured");
logger.Log("Creating CommandsR's commands array");
Delegate[] cmds = { CommandsR.cd, CommandsR.dir, CommandsR.cls, CommandsR.ping, CommandsR.shutdown, CommandsR.fget, CommandsR.sys, CommandsR.echo, CommandsR.getd, CommandsR.to, CommandsR.mkzip, CommandsR.unzip, CommandsR.rm, CommandsR.write, CommandsR.mkdir, CommandsR.mkfile, CommandsR.help, CommandsR.ls, CommandsR.clear, CommandsR.date, CommandsR.gc_collect, CommandsR.mono, CommandsR.fire, CommandsR.rgb,CommandsR.sea,CommandsR.rainbow, CommandsR.find, CommandsR.read, CommandsR.http_get};
Action[] services = { };
Delegate[] engines = { };
logger.Log("Creating Operture");
Operture invokeMgr = new Operture(cmds, services, engines, logger);
logger.Log("Launching MainCmd");
await invokeMgr.MainCmd();
Console.ReadKey();