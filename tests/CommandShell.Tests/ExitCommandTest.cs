using CommandShell.Core;
using CommandShell.Tests.Fakes;


namespace CommandShell.Tests;

public class ExitCommandTest
{
    private readonly CommandInvoker _invoker =
        new(new Parser(), new FakeFileSystemService(), new FakeConsoleService());
    
    [TestMethod]
    public void ExecuteCommand_ExitCommand_ReturnsSuccess()
    {
        CommandResult result = _invoker.ExecuteCommand("exit");
        Assert.IsTrue(result.ShouldExit);
    }
}