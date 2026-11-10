
using CommandShell.Core;
using CommandShell.Services;
using CommandShell.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandShell.Tests;

[TestClass]
public class MoreCommandTests
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
    public void More_KurzeDatei_ZeigtAlleZeilen()
    {
        _fileSystem.WriteAllText(
            "notiz.txt", "Hallo\nWelt");

        var result = _invoker.ExecuteCommand(
            "more notiz.txt");

        Assert.IsTrue(result.Success);
        Assert.AreEqual(2, _console.Output.Count);
        Assert.AreEqual("Hallo", _console.Output[0]);
        Assert.AreEqual("Welt", _console.Output[1]);
    }

    [TestMethod]
    public void More_LangeDatei_Enter_ZeigtNaechsteSeite()
    {
        string content = string.Join(
            "\n",
            Enumerable.Range(1, 25)
                .Select(i => $"Zeile {i}"));

        _fileSystem.WriteAllText("datei.txt", content);
        _console.Input.Enqueue("");

        var result = _invoker.ExecuteCommand(
            "more datei.txt");

        Assert.IsTrue(result.Success);
        Assert.AreEqual(26, _console.Output.Count);
        Assert.AreEqual("Zeile 20", _console.Output[19]);
        Assert.AreEqual("-- Mehr --", _console.Output[20]);
        Assert.AreEqual("Zeile 21", _console.Output[21]);
        Assert.AreEqual("Zeile 25", _console.Output[25]);
    }

    [TestMethod]
    public void More_LangeDatei_Q_BrichtAb()
    {
        string content = string.Join(
            "\n",
            Enumerable.Range(1, 30)
                .Select(i => $"Zeile {i}"));

        _fileSystem.WriteAllText("datei.txt", content);
        _console.Input.Enqueue("q");

        var result = _invoker.ExecuteCommand(
            "more datei.txt");

        Assert.IsTrue(result.Success);
        Assert.AreEqual(21, _console.Output.Count);
        Assert.AreEqual("-- Mehr --", _console.Output[20]);
        Assert.IsFalse(_console.Output.Contains("Zeile 21"));
    }

    [TestMethod]
    public void More_DateiFehlt_GibtFehler()
    {
        var result = _invoker.ExecuteCommand(
            "more fehlt.txt");

        Assert.IsFalse(result.Success);
        StringAssert.Contains(
            result.OutputMessage,
            "Datei nicht gefunden");
    }

    [TestMethod]
    public void More_Ordner_GibtFehler()
    {
        _fileSystem.CreateDirectory("Test");

        var result = _invoker.ExecuteCommand("more Test");

        Assert.IsFalse(result.Success);
        StringAssert.Contains(
            result.OutputMessage,
            "ist ein Ordner");
    }

    [TestMethod]
    public void More_OhneArgument_GibtSyntaxfehler()
    {
        var result = _invoker.ExecuteCommand("more");

        Assert.IsFalse(result.Success);
        StringAssert.Contains(
            result.OutputMessage,
            "Syntax: more");
    }

    [TestMethod]
    public void More_Genau20Zeilen_KeineWeitereSeite()
    {
        string content = string.Join(
            "\n",
            Enumerable.Range(1, 20)
                .Select(i => $"Zeile {i}"));

        _fileSystem.WriteAllText("datei.txt", content);

        var result = _invoker.ExecuteCommand(
            "more datei.txt");

        Assert.IsTrue(result.Success);
        Assert.AreEqual(20, _console.Output.Count);
        Assert.IsFalse(_console.Output.Contains("-- Mehr --"));
    }

    [TestMethod]
    public void More_VirtuellesDateisystem_Funktioniert()
    {
        var fileSystem = new VirtualFileSystemService();
        var console = new FakeConsoleService();

        fileSystem.WriteAllText(
            "notiz.txt", "Hallo\nWelt");

        var invoker = new CommandInvoker(
            new Parser(),
            fileSystem,
            console);

        var result = invoker.ExecuteCommand(
            "more notiz.txt");

        Assert.IsTrue(result.Success);
        Assert.AreEqual("Hallo", console.Output[0]);
        Assert.AreEqual("Welt", console.Output[1]);
    }
}
