using Hydrium;
using Quant.Commands;
using Quant.Cs;
Engine.OutputSysNE("<Program.cs> Powered by Quant.cs");
var qu = new QuantCore(new List<IQuantCommand>()
{
    new Cd(),
    new Rm(),
    new Dir(),
    new Help(),
    new Echo(),
    new Exit(),
    new Date(),
    new Getd(),
    new Read(),
    new Fget(),
    new Touch(),
    new Mkdir(),
    new Clear(),
    new Mkzip(),
    new Unzip(),
    new Write(),
    new Search(),
    new ColorEcho(),
    new SystemInfo(),
    new ExecuteFile(),
    new PingCommand()
});
qu.Launch();