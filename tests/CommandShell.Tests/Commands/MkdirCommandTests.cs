using CommandShell.Core;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class MkdirCommandTests
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
    public void ExecuteCommand_Mkdir_CreatesDirectory()
    {
        // Act
        CommandResult result = _invoker.ExecuteCommand("mkdir neu");

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsTrue(_fs.DirectoryExists("neu"));
    }

    [TestMethod]
    public void ExecuteCommand_Md_Alias_CreatesDirectory()
    {
        CommandResult result = _invoker.ExecuteCommand("md neu");

        Assert.IsTrue(result.Success);
        Assert.IsTrue(_fs.DirectoryExists("neu"));
    }

    [TestMethod]
    public void ExecuteCommand_MkdirWithoutArgument_ReturnsSyntaxError()
    {
        CommandResult result = _invoker.ExecuteCommand("mkdir");

        Assert.IsFalse(result.Success);
        Assert.Contains("Syntax: mkdir", result.OutputMessage);
    }

    [TestMethod]
    public void ExecuteCommand_MkdirExistingDirectory_ReturnsError()
    {
        // Arrange
        _fs.CreateDirectory("neu");

        // Act
        CommandResult result = _invoker.ExecuteCommand("mkdir neu");

        // Assert
        Assert.IsFalse(result.Success);
        Assert.Contains("neu", result.OutputMessage);
    }
}