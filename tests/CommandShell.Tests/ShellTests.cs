using CommandShell.Core;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class ShellTests
{
    private readonly Shell _shell =
        new(new CommandInvoker(new Parser(), new FakeFileSystemService(), new FakeConsoleService()), new FakeConsoleService(), new FakeFileSystemService());
    
    [TestMethod]
    public void Run_ShowsCurrentDirectoryInPrompt()
    {
        
    }
}
