using Hydrium;
namespace OperManager
{
    public class InvokingManagerRealtime
    {
        protected Delegate[] commands;
        protected Action[] services;
        public InvokingManagerRealtime(Delegate[] commands, Action[] services)
        {
            this.services = services;
            this.commands = commands;
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
