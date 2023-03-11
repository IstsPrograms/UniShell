using System.Reflection;
using Hydrium;

namespace UniPluginBase
{
    class Plugin
    {
        public string Name { get; set; }
        public Assembly assembly;
        public Plugin(string Name, string File)
        {
            this.Name = Name.Split('.')[0];

            if(Name.Contains(".dll"))
            {
                try
                {
                    Engine.OutputSysWW($"[Plugin] <{this.Name}> : loading plugin");
                    assembly = Assembly.LoadFrom(File);
                }
                catch
                {
                    Engine.OutputSysWE($"[Plugin] <{this.Name}> : non UniShell plugin");
                }
            }

        }
        public void StartUp()
        {
            try
            {
                Type type = assembly.GetType("Plugin.PluginClass");
                object instance = Activator.CreateInstance(type);
                MethodInfo method = instance.GetType().GetMethod("StartUp");
                method.Invoke(instance, null);
            }
            catch 
            {
                Engine.OutputSysWE($"[Plugin] <{Name}> : non UniShell plugin");
            }
        }
    }
}
