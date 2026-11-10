using CommandShell.Core;
using CommandShell.FileSystem;
using CommandShell.Services;

namespace CommandShell.Tests;

[TestClass]
public class JsonShellContextStorageTests
{
    private string _filePath = null!;
    private JsonShellContextStorage _storage = null!;

    [TestInitialize]
    public void Setup()
    {
        _filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".json");
        _storage = new JsonShellContextStorage(_filePath);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
        }
    }

    [TestMethod]
    public void Load_FileMissing_ReturnsDefault()
    {
        ShellContext context = _storage.Load();

        Assert.AreEqual("C:", context.Root.Name);
        Assert.AreEqual(0, context.Root.Children.Count);
        Assert.AreEqual(ConsoleColor.Black, context.BackgroundColor);
        Assert.AreEqual(ConsoleColor.Gray, context.ForegroundColor);
    }

    [TestMethod]
    public void Load_BrokenFile_ThrowsInvalidDataException()
    {
        File.WriteAllText(_filePath, "das ist kein json {");

        Assert.ThrowsExactly<InvalidDataException>(() => _storage.Load());
    }

    [TestMethod]
    public void Load_EmptyFile_ThrowsInvalidDataException()
    {
        File.WriteAllText(_filePath, "");

        Assert.ThrowsExactly<InvalidDataException>(() => _storage.Load());
    }

    [TestMethod]
    public void SaveAndLoad_FolderFileAndColors_AreRestored()
    {
        ShellContext original = ShellContext.CreateDefault();
        var daten = new VirtualDirectory { Name = "Daten" };
        original.Root.AddChild(daten);
        daten.AddChild(new TextFile("test.txt", daten, DateTime.Now, "Hallo"));
        original.BackgroundColor = ConsoleColor.Blue;
        original.ForegroundColor = ConsoleColor.Yellow;

        _storage.Save(original);
        ShellContext loaded = _storage.Load();

        var loadedDaten = loaded.Root.GetChild("Daten") as VirtualDirectory;
        Assert.IsNotNull(loadedDaten);
        var loadedNotiz = loadedDaten.GetChild("test.txt") as TextFile;
        Assert.IsNotNull(loadedNotiz);
        Assert.AreEqual("Hallo", loadedNotiz.Content);
        Assert.AreEqual(ConsoleColor.Blue, loaded.BackgroundColor);
        Assert.AreEqual(ConsoleColor.Yellow, loaded.ForegroundColor);
    }
}
