using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell;

public static class Program
{
    public static void Main(string[] args)
    {
        IConsoleService console = new ConsoleService();

        string savePath = Path.Combine(AppContext.BaseDirectory, "shellcontext.json");
        IShellContextStorage storage = new JsonShellContextStorage(savePath);

        ShellContext context;
        try
        {
            context = storage.Load();
        }
        catch (InvalidDataException)
        {
            console.WriteLine("Die Speicherdatei ist beschädigt. Die Shell startet mit leerem C:\\");
            context = ShellContext.CreateDefault();
        }

        var fileSystem = new VirtualFileSystemService(context, storage);

        var invoker = new CommandInvoker(new Parser(), fileSystem, console);
        var shell = new Shell(invoker, console, fileSystem);

        shell.Run();
    }
}
