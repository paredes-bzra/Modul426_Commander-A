using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell;

public static class Program
{
    public static void Main(string[] args)
    {
        string savePath = Path.Combine(AppContext.BaseDirectory, "shellcontext.json");
        IShellContextStorage storage = new JsonShellContextStorage(savePath);

        ShellContext context;
        string? loadWarning = null;
        try
        {
            context = storage.Load();
        }
        catch (InvalidDataException)
        {
            loadWarning = "Die Speicherdatei ist beschädigt. Die Shell startet mit leerem C:\\";
            context = ShellContext.CreateDefault();
        }

        IConsoleService console = new ConsoleService(context, storage);
        if (loadWarning != null)
        {
            console.WriteLine(loadWarning);
        }

        var fileSystem = new VirtualFileSystemService(context, storage);

        var invoker = new CommandInvoker(new Parser(), fileSystem, console);
        var shell = new Shell(invoker, console, fileSystem, context);

        shell.Run();
    }
}
