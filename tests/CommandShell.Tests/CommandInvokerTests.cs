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
        CommandResult result = _invoker.ExecuteCommand("abc");

        Assert.False(result.Success);
        Assert.False(result.ShouldExit);
        Assert.Contains("abc", result.OutputMessage);
    }

    [Fact]
    public void ExecuteCommand_EmptyInput_DoesNothing()
    {
        CommandResult result = _invoker.ExecuteCommand("");

        Assert.True(result.Success);
        Assert.Equal(string.Empty, result.OutputMessage);
    }
}
