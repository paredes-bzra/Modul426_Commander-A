using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell;

public static class Program
{
    public static void Main(string[] args)
    {
        IConsoleService console = new ConsoleService();
        string savePath = Path.Combine(AppContext.BaseDirectory, "filesystem.json");
        IFileSystemStorage storage = new JsonFileSystemStorage(savePath);
        var fileSystem = new VirtualFileSystemService(storage);

        if (fileSystem.LoadWarning != null)
        {
            console.WriteLine(fileSystem.LoadWarning);
        }

        var invoker = new CommandInvoker(new Parser(), fileSystem, console);
        var shell = new Shell(invoker, console, fileSystem);

        shell.Run();
    }
}