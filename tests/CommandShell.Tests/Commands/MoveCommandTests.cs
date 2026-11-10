using CommandShell.Core;
using CommandShell.Services;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class MoveCommandTests
{
    private VirtualFileSystemService _fs = null!;
    private CommandInvoker _invoker = null!;

    [TestInitialize]
    public void Setup()
    {
        _fs = new VirtualFileSystemService();
        _invoker = new CommandInvoker(new Parser(), _fs, new FakeConsoleService());
    }

    [TestMethod]
    public void Execute_FileIntoFolder_FileIsMoved()
    {
        _fs.CreateDirectory("Archiv");
        _fs.WriteAllText("notiz.txt", "Hallo");

        CommandResult result = _invoker.ExecuteCommand("move notiz.txt Archiv");

        Assert.IsTrue(result.Success);
        Assert.IsFalse(_fs.FileExists("notiz.txt"));
        Assert.AreEqual("Hallo", _fs.ReadAllText(@"Archiv\notiz.txt"));
    }

    [TestMethod]
    public void Execute_WithNewName_FileIsMovedAndRenamed()
    {
        _fs.CreateDirectory("Archiv");
        _fs.WriteAllText("alt.txt", "Hallo");

        CommandResult result = _invoker.ExecuteCommand(@"move alt.txt Archiv\neu.txt");

        Assert.IsTrue(result.Success);
        Assert.IsTrue(_fs.FileExists(@"Archiv\neu.txt"));
        Assert.IsFalse(_fs.FileExists("alt.txt"));
    }

    [TestMethod]
    public void Execute_TargetExists_ReturnsErrorAndKeepsFile()
    {
        _fs.CreateDirectory("Archiv");
        _fs.WriteAllText("notiz.txt", "Neu");
        _fs.WriteAllText(@"Archiv\notiz.txt", "Alt");

        CommandResult result = _invoker.ExecuteCommand("move notiz.txt Archiv");

        Assert.IsFalse(result.Success);
        Assert.IsTrue(_fs.FileExists("notiz.txt"));
        Assert.AreEqual("Alt", _fs.ReadAllText(@"Archiv\notiz.txt"));
    }

    [TestMethod]
    public void Execute_NoArguments_ReturnsSyntaxHint()
    {
        CommandResult result = _invoker.ExecuteCommand("move");

        Assert.IsFalse(result.Success);
        Assert.Contains("Syntax", result.OutputMessage);
    }
}
