using CommandShell.Commands;
using CommandShell.Factory;
using CommandShell.Tests.Fakes;

[TestClass]
public class CommandFactoryTests
{
    [TestMethod]
    public void CreateCommand_UnknownCommand_ReturnsUnknownCommand()
    {
        var fileService = new FakeFileSystemService();
        var consoleService = new FakeConsoleService();
     
     
        var command = CommandFactory.Create("gibtsnicht", fileService, consoleService);
        
        Assert.IsInstanceOfType(command, typeof(UnknownCommand));
    }

}