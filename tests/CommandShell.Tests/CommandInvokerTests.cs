using CommandShell.Core;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class CommandInvokerTests
{
    private readonly CommandInvoker _invoker =
        new(new Parser(), new FakeFileSystemService(), new FakeConsoleService());
    private readonly FakeConsoleService _consoleService = new();

    [TestMethod]
    public void ExecuteCommand_UnknownCommand_ReturnsError()
    {
        CommandResult result =_invoker.ExecuteCommand("abc");
        Assert.IsFalse(result.Success);
        Assert.Contains("abc",result.OutputMessage);
    }

    [TestMethod]
    public void ExecuteCommand_EmptyInput_DoesNothing()
    {
        CommandResult result = _invoker.ExecuteCommand("");
        Assert.IsTrue(result.Success);
        Assert.Contains( "",result.OutputMessage);
    }
}
