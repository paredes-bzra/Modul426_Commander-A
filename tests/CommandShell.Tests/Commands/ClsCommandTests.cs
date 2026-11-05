using CommandShell.Core;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class ClsCommandTests
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
    public void ExecuteCommand_Cls_ReturnsSuccess()
    {
        CommandResult result = _invoker.ExecuteCommand("cls");

        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public void ExecuteCommand_Cls_ClearsConsoleOutput()
    {
        // Arrange
        _console.WriteLine("alter Text");

        // Act
        _invoker.ExecuteCommand("cls");

        // Assert
        Assert.AreEqual(0, _console.Output.Count);
    }

    [TestMethod]
    public void ExecuteCommand_ClsWithArgument_ReturnsSyntaxError()
    {
        _console.WriteLine("bleibt");

        CommandResult result = _invoker.ExecuteCommand("cls abc");

        Assert.IsFalse(result.Success);
        Assert.Contains("Syntax: cls", result.OutputMessage);
        Assert.AreEqual(1, _console.Output.Count);
    }

    [TestMethod]
    public void ExecuteCommand_ClsWithSwitch_ReturnsSyntaxError()
    {
        CommandResult result = _invoker.ExecuteCommand("cls /x");

        Assert.IsFalse(result.Success);
        Assert.Contains("Syntax: cls", result.OutputMessage);
    }
}