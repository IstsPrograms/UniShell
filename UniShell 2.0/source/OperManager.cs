using Hydrium;
namespace OperManager
{
    public class InvokingManagerRealtime
    {
        protected Delegate[] commands;
        public InvokingManagerRealtime(Delegate[] commands)
        {
            
            this.commands = commands;
        }
        public async Task MainCmd()
        {
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
        
    }



}
