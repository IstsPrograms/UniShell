namespace CmdAttrLib;

public interface ICommand
{
    public string Name { get; set; }
    public string Description { get; set; }

    public int Execute(string args);
}