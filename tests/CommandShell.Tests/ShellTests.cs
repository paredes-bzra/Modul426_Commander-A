using CommandShell.Core;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class ShellTests
{
    private readonly Shell _shell =
        new(new CommandInvoker(new Parser(), new FakeFileSystemService(), new FakeConsoleService()), new FakeConsoleService(), new FakeFileSystemService());
    private readonly FakeFileSystemService _fakeFileSystemService = new FakeFileSystemService();
    private readonly FakeConsoleService _consoleService = new FakeConsoleService();

    [TestMethod]
    public void Run_ShowsCurrentDirectoryInPrompt()
    {
        //Arrange
        _fakeFileSystemService.CreateDirectory(@":\Temp");
        
        //Act
        _shell.Run();
        //Assert
        Assert.IsTrue(_consoleService.Output.Contains("Current Directory:"));
        
    }
}
