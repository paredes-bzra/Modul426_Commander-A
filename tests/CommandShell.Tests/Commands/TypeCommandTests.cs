
using CommandShell.Core;
using CommandShell.Services;
using CommandShell.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandShell.Tests;

[TestClass]
public class TypeCommandTests
{
    private FakeFileSystemService _fileSystem = null!;
    private CommandInvoker _invoker = null!;

    [TestInitialize]
    public void Setup()
    {
        _fileSystem = new FakeFileSystemService();

        _invoker = new CommandInvoker(
            new Parser(),
            _fileSystem,
            new FakeConsoleService());
    }

    [TestMethod]
    public void Type_DateiExistiert_ZeigtInhalt()
    {
        _fileSystem.WriteAllText("notiz.txt", "Hallo");

        var result = _invoker.ExecuteCommand("type notiz.txt");

        Assert.IsTrue(result.Success);
        Assert.AreEqual("Hallo", result.OutputMessage);
    }

    [TestMethod]
    public void Type_DateiExistiertNicht_GibtFehler()
    {
        var result = _invoker.ExecuteCommand(
            "type gibtsnicht.txt");

        Assert.IsFalse(result.Success);
        StringAssert.Contains(
            result.OutputMessage, "Datei nicht gefunden");
    }

    [TestMethod]
    public void Type_IstOrdner_GibtFehler()
    {
        _fileSystem.CreateDirectory("Test");

        var result = _invoker.ExecuteCommand("type Test");

        Assert.IsFalse(result.Success);
        StringAssert.Contains(
            result.OutputMessage, "ist ein Ordner");
    }

    [TestMethod]
    public void Type_OhneArgument_GibtSyntaxfehler()
    {
        var result = _invoker.ExecuteCommand("type");

        Assert.IsFalse(result.Success);
        StringAssert.Contains(
            result.OutputMessage, "Syntax: type");
    }

    [TestMethod]
    public void Type_ZuVieleArgumente_GibtSyntaxfehler()
    {
        var result = _invoker.ExecuteCommand(
            "type notiz.txt andere.txt");

        Assert.IsFalse(result.Success);
        StringAssert.Contains(
            result.OutputMessage, "Syntax: type");
    }

    [TestMethod]
    public void Type_VirtuellesDateisystem_ZeigtInhalt()
    {
        var fileSystem = new VirtualFileSystemService();

        fileSystem.WriteAllText("notiz.txt", "Hallo");

        var invoker = new CommandInvoker(
            new Parser(),
            fileSystem,
            new FakeConsoleService());

        var result = invoker.ExecuteCommand(
            "type notiz.txt");

        Assert.IsTrue(result.Success);
        Assert.AreEqual("Hallo", result.OutputMessage);
    }
}
