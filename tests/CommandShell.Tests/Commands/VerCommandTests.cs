using CommandShell.Tests.Fakes;
using CommandShell.Core;
using CommandShell.Factory;

namespace CommandShell.Tests;

[TestClass]
public class VerCommandTests
{
    private CommandInvoker _invoker = null!;

    [TestInitialize]
    public void Setup()
    {
        var fileService = new FakeFileSystemService();
        var consoleService = new FakeConsoleService();
        _invoker = new CommandInvoker(new Parser(), fileService,consoleService );
    }

    [TestMethod]
    public void ExecuteCommand_Ver_ReturnSuccess()
    {
        CommandResult result = _invoker.ExecuteCommand("ver");
        
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public void ExecuteCommand_Ver_OutputContainsVersion()
    {
        CommandResult result = _invoker.ExecuteCommand("ver");
        
        Assert.StartsWith("Microsoft Windows [Version", result.OutputMessage);
    }
}