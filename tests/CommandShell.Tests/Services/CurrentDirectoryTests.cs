using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Tests;

[TestClass]
public class CurrentDirectoryTests
{
    [TestMethod]
    public void Restart_AfterCdDaten_StartsInDaten()
    {
        string file = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".json");
        var storage = new JsonShellContextStorage(file);
        var fs = new VirtualFileSystemService(storage.Load(), storage);
        fs.CreateDirectory("Daten");
        fs.CurrentDirectory = "Daten";

        var restarted = new VirtualFileSystemService(storage.Load(), storage);

        Assert.AreEqual(@"C:\Daten", restarted.CurrentDirectory);
        File.Delete(file);
    }

    [TestMethod]
    public void Restart_SavedFolderMissing_StartsAtRoot()
    {
        var context = ShellContext.CreateDefault();
        context.CurrentDirectory = @"C:\Gibtsnicht";

        var fs = new VirtualFileSystemService(context);

        Assert.AreEqual(@"C:\", fs.CurrentDirectory);
    }
}
