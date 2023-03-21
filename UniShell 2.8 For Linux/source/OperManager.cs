using Hydrium;
using Logs;
using System.Text.Json;
using UniPluginBase;

namespace OperManager
{

    public class Operture
    {
        public Delegate[]? commands;
        protected Action[]? services;
        protected Delegate[]? executingEngines;
        protected Macros macros;
        Plugin[] plugins;
        Logger log;
        public Operture(Delegate[]? commands, Action[]? services, Delegate[]? executingEngines, Logger log)
        {
            this.services = services;
            this.commands = commands;
            this.executingEngines = executingEngines;
            this.log = log;
            macros = new Macros();
            plugins = new Plugin[Directory.GetFiles($"/UniPlugins/").Length];
            string[] plugs = Directory.GetFiles($"/UniPlugins/");
            int countOfLoaded = 0;
            for (int i = 0; i < plugs.Length; i++)
            {
                if (plugs[i].Contains(".dll"))
                {
                    string Name = plugs[i].Split("/")[plugs[i].Split("/").Length - 1];
                    plugins[i] = new Plugin(Name, plugs[i]);
                    countOfLoaded++;
                }
            }
            Engine.OutputSysNE($"[OPERTURE] Plugins loaded! ({countOfLoaded})");


        }
        public async Task MacroManager(MacroOperationType type, string args)
        {
            try
            {
                switch (type)
                {
                    case MacroOperationType.Create:
                        if (File.Exists(Environment.CurrentDirectory + "/" + args))
                        {
                            bool resultOf = macros.macro.TryAdd(args.Split('.')[0], File.ReadAllText(Environment.CurrentDirectory + "/" + args));
                            if (resultOf)
                            {
                                Engine.OutputSysNE($"[MACROMGR] Macro with name {args.Split('.')[0]} has been created");
                            }
                            else
                            {
                                Engine.OutputSysWE("[MACROMGR] Unknown error");
                            }
                            return;
                        }
                        else
                        {
                            Engine.OutputSysWE($"[MACROMGR] File does not exists, macro can't be created");
                            return;
                        }
                    case MacroOperationType.Delete:
                        bool result = macros.macro.Remove(args);
                        if (result)
                        {
                            Engine.OutputSysNE($"[MACROMGR] Macro with name {args} has been deleted");
                        }
                        else
                        {
                            Engine.OutputSysWE("[MACROMGR] Unknown error");
                        }
                        return;
                    case MacroOperationType.Use:
                        string uselessVariable = "";
                        bool tryGetValue = macros.macro.TryGetValue(args, out uselessVariable);
                        if (tryGetValue)
                        {
                            await FashCmd(uselessVariable);
                        }
                        else
                        {
                            Engine.OutputSysWE("[MACROMGR] Unknown error");
                        }
                        return;
                    case MacroOperationType.Save:
                        var stream = new StreamWriter($"/uniShellMacro.json");
                        JsonSerializer.Serialize<Dictionary<string, string>>(stream.BaseStream, macros.macro);
                        stream.Close();
                        Engine.OutputSysNE("[MACROMGR] All macro has been saved");
                        return;
                    case MacroOperationType.Load:
                        var streamLoad = new StreamReader($"/uniShellMacro.json");
                        macros.macro = JsonSerializer.Deserialize<Dictionary<string, string>>(streamLoad.BaseStream);
                        streamLoad.Close();
                        Engine.OutputSysNE("[MACROMGR] All macro has been loaded");
                        return;
                }
            }
            catch
            {
                Engine.OutputSysWE("[MACROMGR] Error 23");
            }
            
            
        }
        public async Task FashCmd(string cmd)
        {

            for(int cm = 0; cm < cmd.Split("\n").Length; cm++)
            {
                for (int del = 0; del < commands.Length; del++)
                {
                    log.Log($"{cmd.Split("\n")[cm]}");
                    if (commands[del].Method.Name == (cmd.Split("\n")[cm]).Split()[0])
                    {
                        await Invoker(del, (cmd.Split("\n")[cm]).Replace(commands[del].Method.Name, ""));
                    }
                }
            }
            Engine.OutputSysNE("[FASH] Script executing has ended");
        }


        public async Task MainCmd()
        {
            for (int i = 0; i < services.Length; i++)
            {
                Thread e = new Thread(new ThreadStart(services[i]));
                e.Start();
                Engine.OutputSysNE($"[THREADING] Service {services[i].Method.Name} activated");
            }
            while(true)
            {
                string getData = Engine.RealtimeInput($"{Environment.UserName}@{Environment.MachineName}:{Environment.CurrentDirectory} > ");
                log.Log(getData);
                for (int del = 0; del < commands.Length; del++)
                {
                    if (commands[del].Method.Name == getData.Split()[0])
                    {
                        await Invoker(del, getData.Replace(commands[del].Method.Name, ""));
                        break;
                    }
                }
                if (getData.Split()[0] == "fash")
                {
                    if (File.Exists($"{Environment.CurrentDirectory}\\{getData.Split()[1]}"))
                    {
                        Engine.OutputSysNE("[FASH] Starting fash script...");
                        await FashCmd(File.ReadAllText($"{Environment.CurrentDirectory}/{getData.Split()[1]}"));
                    }
                    else
                    {
                        Engine.OutputSysWE("[FASH] Script file not found");
                    }
                }
                if (getData.Split()[0] == "use")
                {
                    foreach(var p in plugins)
                    {
                        if(p.Name == getData.Split()[1])
                        {
                            p.StartUp();
                        }
                    }
                }
                if (getData.Split()[0] == "run")
                {
                    try
                    {
                        if (File.Exists($"{Environment.CurrentDirectory}/{getData.Split()[1]}"))
                        {
                            string getExtension = getData.Split()[1].Split('.')[1];
                            for(int eng = 0; eng < executingEngines.Length; eng++)
                            {
                                if (executingEngines[eng].Method.Name == getExtension)
                                {
                                    Environment.SetEnvironmentVariable("runApp", getData.Split()[1]);
                                    executingEngines[eng].Method.Invoke(null, null);
                                    break;
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        log.Log($"{ex}");
                        Engine.OutputSysWE($"[OPERTURE] {ex}");
                    }
                }
                try
                {
                    if (getData.Split()[0][0] == '%')
                    {
                        string splittedGetData = getData.Split()[0].Replace("%", "");
                        try
                        {
                            switch (splittedGetData)
                            {
                                case "create":
                                    await MacroManager(MacroOperationType.Create, getData.Split()[1]);
                                    break;
                                case "use":
                                    await MacroManager(MacroOperationType.Use, getData.Split()[1]);
                                    break;
                                case "delete":
                                    await MacroManager(MacroOperationType.Delete, getData.Split()[1]);
                                    break;
                                case "save":
                                    await MacroManager(MacroOperationType.Save, "");
                                    break;
                                case "load":
                                    await MacroManager(MacroOperationType.Load, "");
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Log($"{ex}");
                            Engine.OutputSysWE("[MACROMGR] ");
                        }

                    }
                }
                catch
                {
                    
                }
                if (getData.Split()[0] == "save-log")
                {
                    Engine.OutputSysNE("[SAVE-LOG] Saving...");
                    log.WriteToFile();
                    Engine.OutputSysNE("[SAVE-LOG] Saved!");
                }
                //GC.Collect();
            }
        }

        public async Task Invoker(int activateCommand, string fullMsg)
        {
            if(activateCommand > commands.Length || activateCommand < 0 || commands.Length == 0)
            {
                Engine.OutputSysWE($"[INVOKING] Delegate cant be found!");
            }
            else
            {
                try
                {
                    Engine.OutputSysNE($"[INVOKING] Delegate {commands[activateCommand].Method.Name} invoked");
                    Environment.SetEnvironmentVariable("arguments", fullMsg);
                    commands[activateCommand].Method.Invoke(null, null);
                }
                catch(Exception ex)
                {
                    log.Log($"{ex}");
                    Engine.OutputSysWE($"[INVOKING] Exception {ex}");
                }
            }
            
        }

        
    }

    public enum MacroOperationType {
        Create,
        Use,
        Delete,
        Save,
        Load
    }
    public struct Macros
    {
        public Dictionary<string, string> macro;
        public Macros()
        {
            macro = new Dictionary<string, string>();
        }
    }

}
