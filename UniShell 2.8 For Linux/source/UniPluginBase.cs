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
                catch(Exception ex)
                {
                    Engine.OutputSysWE($"[Plugin] <{this.Name}> : non UniShell plugin, 23");
                }
            }

        }
        public void StartUp()
        {
            try
            {
                var t = assembly.GetTypes();
                int attempts = 0;
                foreach( var t2 in t)
                {
                    try
                    {
                        object instance = Activator.CreateInstance(assembly.GetType($"{t2.Namespace}.PluginClass"));
                        MethodInfo method = instance.GetType().GetMethod("StartUp");
                        method.Invoke(instance, null);
                        return;
                    }
                    catch
                    {
                        Engine.OutputSysWW($"[Plugin.StartUp()] <{Name}> : {attempts} attempt");
                        attempts++;
                    }
                }
                Engine.OutputSysWE($"[Plugin] <{Name}> : non UniShell plugin, 49");

            }
            catch (Exception ex)
            {
                Engine.OutputSysWE($"[Plugin] <{Name}> : non UniShell plugin, 54");
            }
        }
    }
}
