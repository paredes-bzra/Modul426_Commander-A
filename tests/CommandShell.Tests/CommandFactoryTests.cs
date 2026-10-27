using CommandShell.Commands;
using CommandShell.Factory;

namespace CommandShell.Tests;

public class CommandFactoryTests
{
    [Fact]
    public void CreateCommand_UnknownCommand_ReturnsUnknownCommand()
    {
        var fileService = new FakeFileSystemService();
        var consoleService = new FakeConsoleService();
     
     
        var command = CommandFactory.Create("gibtsnicht", fileService, consoleService);
        
        Assert.IsType<UnknownCommand>(command);
    }

}