using CommandShell.Core;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class ShellTests
{
    private readonly FakeFileSystemService _fakeFileSystemService = new FakeFileSystemService();
    private readonly FakeConsoleService _consoleService = new FakeConsoleService();
    private Shell _shell = null!;

    [TestInitialize]
    public void Setup()
    {
        var invoker = new CommandInvoker(new Parser(), _fakeFileSystemService, _consoleService);
        _shell = new Shell(invoker, _consoleService, _fakeFileSystemService, ShellContext.CreateDefault());
    }

    [TestMethod]
    public void Run_ShowsCurrentDirectoryInPrompt()
    {
        //Arrange
        _fakeFileSystemService.CurrentDirectory = @"C:\Temp";

        //Act
        _shell.Run();
        //Assert
        Assert.AreEqual(@"C:\Temp> ", _consoleService.Output[0]);

    }
}
