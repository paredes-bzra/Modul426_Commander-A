using CommandShell.Core;
using CommandShell.Services;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class DelCommandTests
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
    public void Execute_ExistingFile_IsDeleted()
    {
        _fs.WriteAllText("notiz.txt", "Hallo");

        CommandResult result = _invoker.ExecuteCommand("del notiz.txt");

        Assert.IsTrue(result.Success);
        Assert.IsFalse(_fs.FileExists("notiz.txt"));
    }

    [TestMethod]
    public void Execute_MissingFile_ReturnsError()
    {
        CommandResult result = _invoker.ExecuteCommand("del fehlt.txt");

        Assert.IsFalse(result.Success);
    }

    [TestMethod]
    public void Execute_NoArgument_ReturnsError()
    {
        CommandResult result = _invoker.ExecuteCommand("del");

        Assert.IsFalse(result.Success);
    }
}
