using Quant.Cs;
using System.Reflection;
using Hydrium;

namespace UniShell;

public class PkgSystem
{
    public static List<IQuantCommand> GetIQCFromDll(string fileName)
    {
        List<IQuantCommand> IQC = new List<IQuantCommand>();
        try
        {
            
            Assembly file;
            try
            {
                file = Assembly.LoadFile($"{Environment.CurrentDirectory}/{fileName}");
            }
            catch
            {
                file = Assembly.LoadFile($"{fileName}");
            }
            var types = file.GetTypes();
            foreach (var t in types)
            {
                if (t.IsClass)
                {
                    try
                    {
                        var instance = Activator.CreateInstance(t);
                        var methods = instance.GetType().GetMethods();
                        string Description = "";
                        string Name = "";
                        foreach (var property in instance.GetType().GetProperties())
                        {
                            if (property.Name == "Description")
                            {
                                Description = property.GetValue(instance) as string;
                            }
                            if (property.Name == "Name")
                            {
                                Name = property.GetValue(instance) as string;
                            }
                        }
                        
                        foreach (var method in methods)
                        {
                            if (method.Name == "Execute")
                            {
                                IQC.Add(new ICustomQuantCommand()
                                {
                                    Description = Description,
                                    Name = Name,
                                    originalObject = instance,
                                    executeMethod = method
                                });
                                break;
                            }
                        }
                    }
                    catch
                    {
                        
                    }
                }
            }
        }
        catch(Exception exception)
        {
            Console.WriteLine(exception);
        }
        return IQC;
    }
}