using Hydrium;

namespace OperManager
{
    public class Operture
    {
        protected Delegate[] commands;
        protected Action[] services;
        protected Delegate[] executingEngines;
        public Operture(Delegate[] commands, Action[] services, Delegate[] executingEngines)
        {
            this.services = services;
            this.commands = commands;
            this.executingEngines = executingEngines;
        }


        public async Task FashCmd(string cmd)
        {
            for(int cm = 0; cm < cmd.Split("\n").Length; cm++)
            {
                for (int del = 0; del < commands.Length; del++)
                {
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
                        await FashCmd(File.ReadAllText($"{Environment.CurrentDirectory}\\{getData.Split()[1]}"));
                    }
                    else
                    {
                        Engine.OutputSysWE("[FASH] Script file not found");
                    }
                }
                if (getData.Split()[0] == "run")
                {
                    try
                    {
                        if (File.Exists($"{Environment.CurrentDirectory}\\{getData.Split()[1]}"))
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
                        Engine.OutputSysWE($"[OPERTURE] {ex}");
                    }
                }
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
                    Engine.OutputSysWE($"[INVOKING] Exception {ex}");
                }
            }
            
        }
        // It`s useless lol
        private async void StartService(int activateService, string arguments)
        {
            if (activateService > services.Length || activateService < 0 || services.Length == 0)
            {
                Engine.OutputSysWE($"[INVOKING] Delegate cant be found!");
            }
            else
            {
                try
                {
                    Engine.OutputSysNE($"[INVOKING] Service {services[activateService].Method.Name} started");
                    Environment.SetEnvironmentVariable($"arguments{services[activateService].Method.Name}", arguments);
                    services[activateService].Method.Invoke(null, null);
                }
                catch (Exception ex)
                {
                    Engine.OutputSysWE($"[INVOKING] Exception {ex}");
                }
            }
        }
        
    }



}
