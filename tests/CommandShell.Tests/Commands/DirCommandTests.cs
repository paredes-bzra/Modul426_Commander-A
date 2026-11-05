using CommandShell.Core;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class DirCommandTests
{
    private FakeFileSystemService _fs = null!;
    private FakeConsoleService _console = null!;
    private CommandInvoker _invoker = null!;

    [TestInitialize]
    public void Setup()
    {
        _fs = new FakeFileSystemService();
        _fs.CurrentDirectory = "/";
        _console = new FakeConsoleService();
        _invoker = new CommandInvoker(new Parser(), _fs, _console);
    }

    [TestMethod]
    public void ExecuteCommand_DirEmpty_ReturnsNoFilesMessage()
    {
        CommandResult result = _invoker.ExecuteCommand("dir");

        Assert.IsTrue(result.Success);
        Assert.Contains("Keine Dateien gefunden.", result.OutputMessage);
    }

    [TestMethod]
    public void ExecuteCommand_Dir_ListsFilesAndDirectories()
    {
        // Arrange
        _fs.WriteAllText("/a.txt", "abc");
        _fs.CreateDirectory("/docs");

        // Act
        CommandResult result = _invoker.ExecuteCommand("dir");

        // Assert
        Assert.IsTrue(result.Success);
        Assert.Contains("a.txt", result.OutputMessage);
        Assert.Contains("<DIR>  docs", result.OutputMessage);
    }

    [TestMethod]
    public void ExecuteCommand_Dir_ShowsFileAndDirectoryCount()
    {
        _fs.WriteAllText("/a.txt", "abc");
        _fs.WriteAllText("/b.txt", "de");
        _fs.CreateDirectory("/docs");

        CommandResult result = _invoker.ExecuteCommand("dir");

        Assert.Contains("2 Datei(en), 1 Ordner", result.OutputMessage);
        Assert.Contains("Gesamtgrösse:", result.OutputMessage);
    }
}