using CommandShell.FileSystem;

namespace CommandShell.Tests;

[TestClass]
public class FileSystemModelTests
{
    private VirtualDirectory _root = null!;
    private VirtualDirectory _daten = null!;

    [TestInitialize]
    public void Setup()
    {
        _root = new VirtualDirectory { Name = "C:" };
        _daten = new VirtualDirectory { Name = "Daten" };
        _root.AddChild(_daten);
    }

    [TestMethod]
    public void GetFullPath_FileInSubfolder_ReturnsFullPath()
    {
        // Arrange
        var notiz = new TextFile("notiz.txt", _daten, DateTime.Now, "Hallo");
        _daten.AddChild(notiz);

        // Act
        string path = notiz.GetFullPath();

        // Assert
        Assert.AreEqual(@"C:\Daten\notiz.txt", path);
    }

    [TestMethod]
    public void GetFullPath_Root_ReturnsDriveName()
    {
        // Act
        string path = _root.GetFullPath();

        // Assert
        Assert.AreEqual("C:", path);
    }

    [TestMethod]
    public void AddChild_NewItem_SetsParent()
    {
        // Arrange
        var notiz = new TextFile("notiz.txt", _daten, DateTime.Now, "Hallo");

        // Act
        _daten.AddChild(notiz);

        // Assert
        Assert.AreSame(_daten, notiz.Parent);
        Assert.AreEqual(1, _daten.Children.Count);
    }

    [TestMethod]
    public void AddChild_DuplicateName_ThrowsIOException()
    {
        // Arrange
        _daten.AddChild(new TextFile("notiz.txt", _daten, DateTime.Now, "Hallo"));
        var duplicate = new TextFile("NOTIZ.TXT", _daten, DateTime.Now, "Doppelt");

        // Act + Assert
        Assert.ThrowsExactly<IOException>(() => _daten.AddChild(duplicate));
        Assert.AreEqual(1, _daten.Children.Count);
    }

    [TestMethod]
    public void GetChild_DifferentCase_FindsItem()
    {
        // Act
        FileSystemItem? found = _root.GetChild("DATEN");

        // Assert
        Assert.AreSame(_daten, found);
    }

    [TestMethod]
    public void GetChild_UnknownName_ReturnsNull()
    {
        // Act
        FileSystemItem? found = _root.GetChild("Gibtsnicht");

        // Assert
        Assert.IsNull(found);
    }

    [TestMethod]
    public void RemoveChild_ExistingItem_RemovesIt()
    {
        // Arrange
        var notiz = new TextFile("notiz.txt", _daten, DateTime.Now, "Hallo");
        _daten.AddChild(notiz);

        // Act
        _daten.RemoveChild(notiz);

        // Assert
        Assert.IsNull(_daten.GetChild("notiz.txt"));
    }

    [TestMethod]
    public void GetSize_TextFile_ReturnsBytesOfContent()
    {
        // Arrange
        var notiz = new TextFile("notiz.txt", _daten, DateTime.Now, "Hallo");

        // Act
        long size = notiz.GetSize();

        // Assert
        Assert.AreEqual(5, size);
    }

    [TestMethod]
    public void GetSize_DirectoryWithSubfolder_ReturnsSumOfAllFiles()
    {
        // Arrange
        _daten.AddChild(new TextFile("a.txt", _daten, DateTime.Now, "Hallo"));
        _daten.AddChild(new TextFile("b.txt", _daten, DateTime.Now, "Welt!"));
        _root.AddChild(new TextFile("c.txt", _root, DateTime.Now, "123"));

        // Act
        long size = _root.GetSize();

        // Assert
        Assert.AreEqual(13, size);
    }

    [TestMethod]
    public void GetSize_EmptyDirectory_ReturnsZero()
    {
        // Act
        long size = _daten.GetSize();

        // Assert
        Assert.AreEqual(0, size);
    }
}
