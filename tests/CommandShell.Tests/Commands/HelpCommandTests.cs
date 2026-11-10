
using CommandShell.Core;
using CommandShell.Factory;
using CommandShell.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandShell.Tests;

[TestClass]
public class HelpCommandTests
{
    private FakeFileSystemService _fileSystem = null!;
    private FakeConsoleService _console = null!;
    private CommandInvoker _invoker = null!;

    [TestInitialize]
    public void Setup()
    {
        _fileSystem = new FakeFileSystemService();
        _console = new FakeConsoleService();

        _invoker = new CommandInvoker(
            new Parser(),
            _fileSystem,
            _console);
    }

    [TestMethod]
    public void Help_ZeigtAlleBefehle()
    {
        var result = _invoker.ExecuteCommand("help");

        Assert.IsTrue(result.Success);

        var lines = result.OutputMessage.Split(
            Environment.NewLine);

        foreach (string name in CommandFactory.GetCommands())
        {
            Assert.IsTrue(
                lines.Any(line =>
                    line.StartsWith(
                        $"{name,-12} ",
                        StringComparison.OrdinalIgnoreCase)),
                $"Befehl fehlt: {name}");
        }
    }

    [TestMethod]
    public void Help_ZeigtBeschreibungen()
    {
        var result = _invoker.ExecuteCommand("help");

        Assert.IsTrue(result.Success);

        foreach (string name in CommandFactory.GetCommands())
        {
            var command = CommandFactory.Create(
                name,
                _fileSystem,
                _console);

            StringAssert.Contains(
                result.OutputMessage,
                $"{name,-12} {command.Description}");
        }
    }

    [TestMethod]
    public void Help_ListetSichSelbstAuf()
    {
        var result = _invoker.ExecuteCommand("help");

        Assert.IsTrue(result.Success);

        StringAssert.Contains(
            result.OutputMessage,
            "help");

        StringAssert.Contains(
            result.OutputMessage,
            "Zeigt alle verfügbaren Befehle an.");
    }

    [TestMethod]
    public void Help_OhneRegistrierungVonEinzelbefehlen()
    {
        var result = _invoker.ExecuteCommand("help");

        Assert.IsTrue(result.Success);

        var lines = result.OutputMessage.Split(
            Environment.NewLine);

        Assert.AreEqual(
            CommandFactory.GetCommands().Count,
            lines.Length - 2);
    }

    [TestMethod]
    public void Help_MitArgument_GibtSyntaxfehler()
    {
        var result = _invoker.ExecuteCommand(
            "help irgendwas");

        Assert.IsFalse(result.Success);

        StringAssert.Contains(
            result.OutputMessage,
            "Syntax: help");
    }
}
