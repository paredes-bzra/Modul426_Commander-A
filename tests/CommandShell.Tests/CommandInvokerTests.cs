using CommandShell.Core;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class CommandInvokerTests
{
    private readonly CommandInvoker _invoker =
        new(new Parser(), new FakeFileSystemService(), new FakeConsoleService());

    [TestMethod]
    public void ExecuteCommand_UnknownCommand_ReturnsError()
    {
   
    }

    [TestMethod]
    public void ExecuteCommand_EmptyInput_DoesNothing()
    {
   
    }
}
