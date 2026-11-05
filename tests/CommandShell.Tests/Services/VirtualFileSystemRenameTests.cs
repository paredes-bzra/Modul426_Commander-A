using CommandShell.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandShell.Tests;

[TestClass]
public class VirtualFileSystemRenameTests
{
    [TestMethod]
    public void Rename_Datei_InhaltBleibtErhalten()
    {
        var fs = new VirtualFileSystemService();
        fs.WriteAllText("alt.txt", "Hallo Welt");

        fs.Rename("alt.txt", "neu.txt");

        Assert.IsFalse(fs.FileExists("alt.txt"));
        Assert.AreEqual("Hallo Welt", fs.ReadAllText("neu.txt"));
    }

    [TestMethod]
    public void Rename_Ordner_InhaltBleibtErhalten()
    {
        var fs = new VirtualFileSystemService();
        fs.CreateDirectory("Test");
        fs.WriteAllText(@"Test\notiz.txt", "Inhalt");

        fs.Rename("Test", "Archiv");

        Assert.IsFalse(fs.DirectoryExists("Test"));
        Assert.IsTrue(fs.DirectoryExists("Archiv"));
        Assert.AreEqual(
            "Inhalt",
            fs.ReadAllText(@"Archiv\notiz.txt"));
    }

    [TestMethod]
    public void Rename_ZielExistiert_GibtFehler()
    {
        var fs = new VirtualFileSystemService();
        fs.WriteAllText("alt.txt", "Alt");
        fs.WriteAllText("neu.txt", "Neu");

        Assert.ThrowsExactly<IOException>(
            () => fs.Rename("alt.txt", "neu.txt"));

        Assert.AreEqual("Alt", fs.ReadAllText("alt.txt"));
        Assert.AreEqual("Neu", fs.ReadAllText("neu.txt"));
    }

    [TestMethod]
    public void Rename_QuelleFehlt_GibtFehler()
    {
        var fs = new VirtualFileSystemService();

        Assert.ThrowsExactly<FileNotFoundException>(
            () => fs.Rename("fehlt.txt", "neu.txt"));
    }
}
