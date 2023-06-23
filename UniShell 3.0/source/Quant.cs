using Hydrium;

namespace Quant.Cs {
    class QuantCore
    {
        public List<IQuantCommand> commands;
        public string pathFormat = "/";
        public User currentUser;
        public QuantCore(List<IQuantCommand> commands, User? currentUser = null)
        {
            
            this.commands = commands;
            this.currentUser = new User("Root", "Root", QuantPermissions.Root);
        }

        public void FindAndExecuteCommand(string argument)
        {
            foreach (var item in commands)
            {
                if (item.Name.ToLowerInvariant() == argument.Split()[0].ToLowerInvariant())
                {
                    if (item.permissionLevel <= currentUser.Permissions)
                    {
                        string[] argumentList = { argument.Replace($"{argument.Split()[0]} ", ""), pathFormat };
                        item.Execute(argumentList); // If command name equals first word of input and user has permission - execute command
                    }
                    else
                    {
                        Engine.OutputSysWE($"<Quant.cs> You have no permission to execute this command. Command required permission '{item.permissionLevel}', you have: '{currentUser.Permissions}'");
                    }
                    
                    return;
                }
            }

            try
            {
                switch (argument.Split()[0])
                {
                    case "exscript":
                        if (File.Exists(argument.Split()[1]))
                        {
                            List<string> script = File.ReadAllText(argument.Split()[1]).Split("\n").ToList();
                            Engine.OutputSysNE("<Quant.cs> Script execution started");
                            foreach (var script_element in script)
                            {
                                FindAndExecuteCommand(script_element);
                            }

                            Engine.OutputSysNE("<Quant.cs> Script execution ended");
                        }
                        else
                        {
                            Engine.OutputSysWE($"<Quant.cs> File with name '{argument.Split()[1]}'does not exist");
                        }
                        break;
                    case "cmdinfo":
                        List<string> namesAndDescriptions = new List<string>() { "All IQuantCommands:" };
                        foreach (var cmd in commands)
                        {
                            namesAndDescriptions.Add($"| {cmd.Name}: {cmd.Description}");
                        }
                        Engine.OutputList(namesAndDescriptions);
                        break;
                }
            }
            catch {}
        }
        public void Launch()
        {
            
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                pathFormat = "/";
                Environment.CurrentDirectory = $"/home/{Environment.UserName}/"; // If system is Linux, set current directory to home directory
            }
            else
            {
                Environment.CurrentDirectory = $"C:\\Users\\{Environment.UserName}\\"; // If system is windows, set current directory to user's directory
            }
            Engine.OutputSysNE("<Quant.cs> Launched!");
            while (true)
            {
                Engine.InputTextSysOutput($"{Environment.UserName}@{Environment.CurrentDirectory} > ");
                string argument = Console.ReadLine();
                try
                {
                    FindAndExecuteCommand(argument);
                }
                catch(Exception exception)
                {
                    Engine.OutputSysWE($"<Quant.cs> Error: {exception}");
                }
            }
        }
    }
    class User
    {
        public string Name;
        public string Password;
        public QuantPermissions Permissions;
        public User(string Name, string Password, QuantPermissions Permissions) 
        {
            this.Name = Name;
            this.Password = Password;
            this.Permissions = Permissions;
        }
    }
    public enum QuantPermissions
    {
        Root = 2,
        Guest = 1,
        None = 0
    }
    interface IQuantCommand
    {
        public QuantPermissions permissionLevel { get; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Execute(params string[] arg);
    }

}