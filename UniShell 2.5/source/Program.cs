﻿using OperManager;
using Cmds;
using Hydrium;
using Services;
using ExecuteEngines;
string cPath = $"C:/Users/{Environment.UserName}/";
Environment.CurrentDirectory = cPath;
Engine.OutputSysNE("[PCONF] Path Configured");
Delegate[] cmds = { CommandsR.cd, CommandsR.dir, CommandsR.cls, CommandsR.ping, CommandsR.shutdown, CommandsR.fget, CommandsR.sys, CommandsR.echo, CommandsR.getd, CommandsR.to, CommandsR.mkzip, CommandsR.unzip, CommandsR.rm, CommandsR.write, CommandsR.mkdir, CommandsR.mkfile, CommandsR.help, CommandsR.contacts, CommandsR.ls, CommandsR.clear, CommandsR.about, CommandsR.date};
Action[] services = { };
Delegate[] engines = { Engines.exe, Engines.bat};
Operture invokeMgr = new Operture(cmds, services, engines);
await invokeMgr.MainCmd();
Console.ReadKey();