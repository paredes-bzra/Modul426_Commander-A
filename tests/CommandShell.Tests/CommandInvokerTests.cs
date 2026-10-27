using CommandShell.Core;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

public class CommandInvokerTests
{
    private readonly CommandInvoker _invoker =
        new(new Parser(), new FakeFileSystemService(), new FakeConsoleService());

    [Fact]
    public void ExecuteCommand_UnknownCommand_ReturnsError()
    {
   
    }

    [Fact]
    public void ExecuteCommand_EmptyInput_DoesNothing()
    {
   
    }
}
