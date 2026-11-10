using CommandShell.Core;
using CommandShell.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandShell.Tests;

[TestClass]
public class RenCommandTests
{
    private FakeFileSystemService _fileService = null!;
    private CommandInvoker _invoker = null!;

    [TestInitialize]
    public void Setup()
    {
        _fileService = new FakeFileSystemService();

        _invoker = new CommandInvoker(
            new Parser(),
            _fileService,
            new FakeConsoleService());
    }

    [TestMethod]
    public void Ren_DateiUmbenennen_InhaltBleibtGleich()
    {
        _fileService.WriteAllText("alt.txt", "Hallo Welt");

        var result = _invoker.ExecuteCommand("ren alt.txt neu.txt");

        Assert.IsTrue(result.Success, result.OutputMessage);
        Assert.IsFalse(_fileService.FileExists("alt.txt"));
        Assert.IsTrue(_fileService.FileExists("neu.txt"));
        Assert.AreEqual(
            "Hallo Welt",
            _fileService.ReadAllText("neu.txt"));
    }

    [TestMethod]
    public void Ren_OrdnerUmbenennen_Funktioniert()
    {
        _fileService.CreateDirectory("Test");

        var result = _invoker.ExecuteCommand("ren Test Archiv");

        Assert.IsTrue(result.Success, result.OutputMessage);
        Assert.IsFalse(_fileService.DirectoryExists("Test"));
        Assert.IsTrue(_fileService.DirectoryExists("Archiv"));
    }

    [TestMethod]
    public void Ren_ZielExistiert_GibtFehler()
    {
        _fileService.WriteAllText("alt.txt", "Alter Inhalt");
        _fileService.WriteAllText("neu.txt", "Neuer Inhalt");

        var result = _invoker.ExecuteCommand("ren alt.txt neu.txt");

        Assert.IsFalse(result.Success);
        Assert.IsTrue(_fileService.FileExists("alt.txt"));
        Assert.AreEqual(
            "Alter Inhalt",
            _fileService.ReadAllText("alt.txt"));
        Assert.AreEqual(
            "Neuer Inhalt",
            _fileService.ReadAllText("neu.txt"));
    }

    [TestMethod]
    public void Ren_QuelleFehlt_GibtFehler()
    {
        var result = _invoker.ExecuteCommand("ren fehlt.txt neu.txt");

        Assert.IsFalse(result.Success);
        Assert.IsFalse(_fileService.FileExists("neu.txt"));
    }

    [TestMethod]
    public void Ren_OhneZweiArgumente_GibtSyntaxfehler()
    {
        var result = _invoker.ExecuteCommand("ren alt.txt");

        Assert.IsFalse(result.Success);
        StringAssert.Contains(
            result.OutputMessage, "Syntax: ren");
    }

    [TestMethod]
    public void Rename_Alias_Funktioniert()
    {
        _fileService.WriteAllText("alt.txt", "Test");

        var result = _invoker.ExecuteCommand("rename alt.txt neu.txt");

        Assert.IsTrue(result.Success, result.OutputMessage);
        Assert.IsTrue(_fileService.FileExists("neu.txt"));
    }
}
