using CommandShell.Core;
using CommandShell.Tests.Fakes;
namespace CommandShell.Tests;

[TestClass]
public class CdCommandTests
{
    private CommandInvoker _invoker = null!;
    private FakeFileSystemService _fileService = null!;
    [TestInitialize]
    public void Setup()
    {
        _fileService = new FakeFileSystemService();
        var consoleService = new FakeConsoleService();
        _invoker = new CommandInvoker(new Parser(), _fileService,consoleService);
    }

    [TestMethod]
    public void Execute_ExistingFolder_ChangesDirectory()
    {
        //Arrange
        _fileService.CurrentDirectory = @"C:\Temp";
        _fileService.CreateDirectory(@"C:\Temp\Projekte");
        //Act
        _invoker.ExecuteCommand("cd Projekte");
        //Assert
        Assert.AreEqual(@"C:\Temp\Projekte", _fileService.CurrentDirectory);
        
        
    }
}