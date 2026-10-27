using CommandShell.Core;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

public class ShellTests
{
    [Fact]
    public void Run_UnknownCommand_PrintsErrorAndKeepsRunning()
    {
        // "abc" ist unbekannt, danach "xyz" – die Shell muss beide verarbeiten
        var console = new FakeConsoleService("abc", "xyz");
        var fileSystem = new FakeFileSystemService();
        var shell = new Shell(new CommandInvoker(new Parser(), fileSystem, console), console, fileSystem);

        shell.Run();

        Assert.Contains(console.Output, line => line.Contains("abc"));
        Assert.Contains(console.Output, line => line.Contains("xyz"));
    }
}
