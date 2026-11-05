using CommandShell.Services;

namespace CommandShell.Tests;

[TestClass]
public class VirtualFileSystemServiceTests
{
    private VirtualFileSystemService _fs = null!;

    [TestInitialize]
    public void Setup()
    {
        _fs = new VirtualFileSystemService();
    }

    [TestMethod]
    public void CurrentDirectory_AtStart_IsRoot()
    {
        // Act
        string current = _fs.CurrentDirectory;

        // Assert
        Assert.AreEqual(@"C:\", current);
    }

    [TestMethod]
    public void CreateDirectory_NewFolder_DirectoryExists()
    {
        // Act
        _fs.CreateDirectory("Daten");

        // Assert
        Assert.IsTrue(_fs.DirectoryExists("Daten"));
        Assert.IsTrue(_fs.DirectoryExists(@"C:\Daten"));
    }

    [TestMethod]
    public void CreateDirectory_TwoLevels_CreatesBoth()
    {
        // Act
        _fs.CreateDirectory(@"Projekte\2026");

        // Assert
        Assert.IsTrue(_fs.DirectoryExists("Projekte"));
        Assert.IsTrue(_fs.DirectoryExists(@"C:\Projekte\2026"));
    }

    [TestMethod]
    public void CurrentDirectory_SetSubfolder_ChangesPath()
    {
        // Arrange
        _fs.CreateDirectory("Daten");

        // Act
        _fs.CurrentDirectory = "Daten";

        // Assert
        Assert.AreEqual(@"C:\Daten", _fs.CurrentDirectory);
    }

    [TestMethod]
    public void CurrentDirectory_SetDotDot_GoesUp()
    {
        // Arrange
        _fs.CreateDirectory(@"Daten\Archiv");
        _fs.CurrentDirectory = @"C:\Daten\Archiv";

        // Act
        _fs.CurrentDirectory = "..";

        // Assert
        Assert.AreEqual(@"C:\Daten", _fs.CurrentDirectory);
    }

    [TestMethod]
    public void CurrentDirectory_UnknownFolder_ThrowsAndStays()
    {
        // Act + Assert
        Assert.ThrowsExactly<DirectoryNotFoundException>(() => _fs.CurrentDirectory = "Gibtsnicht");
        Assert.AreEqual(@"C:\", _fs.CurrentDirectory);
    }

    [TestMethod]
    public void WriteAllText_NewFile_CanBeRead()
    {
        // Arrange
        _fs.CreateDirectory("Daten");

        // Act
        _fs.WriteAllText(@"Daten\notiz.txt", "Hallo");

        // Assert
        Assert.IsTrue(_fs.FileExists(@"C:\Daten\notiz.txt"));
        Assert.AreEqual("Hallo", _fs.ReadAllText(@"Daten\notiz.txt"));
    }

    [TestMethod]
    public void WriteAllText_ExistingFile_ReplacesContent()
    {
        // Arrange
        _fs.WriteAllText("notiz.txt", "Alt");

        // Act
        _fs.WriteAllText("notiz.txt", "Neu");

        // Assert
        Assert.AreEqual("Neu", _fs.ReadAllText("notiz.txt"));
    }

    [TestMethod]
    public void ReadAllText_UnknownFile_ThrowsFileNotFound()
    {
        // Act + Assert
        Assert.ThrowsExactly<FileNotFoundException>(() => _fs.ReadAllText("gibtsnicht.txt"));
    }

    [TestMethod]
    public void GetEntries_FolderAndFile_ReturnsBothWithSize()
    {
        // Arrange
        _fs.CreateDirectory("Daten");
        _fs.WriteAllText("notiz.txt", "Hallo");

        // Act
        var entries = _fs.GetEntries(@"C:\").ToList();

        // Assert
        Assert.AreEqual(2, entries.Count);
        Assert.IsTrue(entries.Any(e => e.Name == "Daten" && e.IsDirectory));
        Assert.IsTrue(entries.Any(e => e.Name == "notiz.txt" && !e.IsDirectory && e.Size == 5));
    }

    [TestMethod]
    public void DeleteFile_ExistingFile_IsGone()
    {
        // Arrange
        _fs.WriteAllText("notiz.txt", "Hallo");

        // Act
        _fs.DeleteFile("notiz.txt");

        // Assert
        Assert.IsFalse(_fs.FileExists("notiz.txt"));
    }

    [TestMethod]
    public void DeleteDirectory_CurrentFolder_GoesBackToParent()
    {
        // Arrange
        _fs.CreateDirectory("Daten");
        _fs.CurrentDirectory = "Daten";

        // Act
        _fs.DeleteDirectory(@"C:\Daten");

        // Assert
        Assert.IsFalse(_fs.DirectoryExists(@"C:\Daten"));
        Assert.AreEqual(@"C:\", _fs.CurrentDirectory);
    }
}
