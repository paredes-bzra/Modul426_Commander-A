using CommandShell.Core;
using CommandShell.Services;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class CdCommandTests
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
    public void Execute_ExistingFolder_ChangesDirectory()
    {
        _fs.CreateDirectory("Projekte");

        _invoker.ExecuteCommand("cd Projekte");

        Assert.AreEqual(@"C:\Projekte", _fs.CurrentDirectory);
    }

    [TestMethod]
    public void Execute_DotDot_GoesUp()
    {
        _fs.CreateDirectory(@"Projekte\2026");
        _fs.CurrentDirectory = @"C:\Projekte\2026";

        _invoker.ExecuteCommand("cd ..");

        Assert.AreEqual(@"C:\Projekte", _fs.CurrentDirectory);
    }

    [TestMethod]
    public void Execute_UnknownFolder_ReturnsErrorAndStays()
    {
        CommandResult result = _invoker.ExecuteCommand("cd Gibtsnicht");

        Assert.IsFalse(result.Success);
        Assert.AreEqual(@"C:\", _fs.CurrentDirectory);
    }
}
