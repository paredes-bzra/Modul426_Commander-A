using CommandShell.Core;
using CommandShell.Services;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class MkfileCommandTests
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
    public void Execute_NameAndText_CreatesFile()
    {
        CommandResult result = _invoker.ExecuteCommand("mkfile notiz.txt Hallo Welt");

        Assert.IsTrue(result.Success);
        Assert.AreEqual("Hallo Welt", _fs.ReadAllText("notiz.txt"));
    }

    [TestMethod]
    public void Execute_FileExists_ReturnsError()
    {
        _fs.WriteAllText("notiz.txt", "Alt");

        CommandResult result = _invoker.ExecuteCommand("mkfile notiz.txt Neu");

        Assert.IsFalse(result.Success);
        Assert.AreEqual("Alt", _fs.ReadAllText("notiz.txt"));
    }

    [TestMethod]
    public void Execute_NoArgument_ReturnsError()
    {
        CommandResult result = _invoker.ExecuteCommand("mkfile");

        Assert.IsFalse(result.Success);
    }
}
