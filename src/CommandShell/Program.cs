using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell;

public static class Program
{
    public static void Main(string[] args)
    {
        IConsoleService console = new ConsoleService();
        IFileSystemService fileSystem = new FileSystemService();

        var invoker = new CommandInvoker(new Parser(), fileSystem, console);
        var shell = new Shell(invoker, console, fileSystem);

        shell.Run();
    }
}