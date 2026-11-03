using CommandShell.Core;
using CommandShell.Tests.Fakes;

namespace CommandShell.Tests;

[TestClass]
public class ColorCommandTests
{
    private FakeFileSystemService _fs = null!;
    private FakeConsoleService _console = null!;
    private CommandInvoker _invoker = null!;

    [TestInitialize]
    public void Setup()
    {
        _fs = new FakeFileSystemService();
        _fs.CurrentDirectory = "/";
        _console = new FakeConsoleService();
        _invoker = new CommandInvoker(new Parser(), _fs, _console);
    }

    [TestMethod]
    public void ExecuteCommand_ColorValid_SetsColors()
    {
        // Act: 0 = Hintergrund Schwarz, A = Text Hellgrün
        CommandResult result = _invoker.ExecuteCommand("color 0A");

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(ConsoleColor.Black, _console.Background);
        Assert.AreEqual(ConsoleColor.Green, _console.Foreground);
    }

    [TestMethod]
    public void ExecuteCommand_ColorLowercase_IsAccepted()
    {
        CommandResult result = _invoker.ExecuteCommand("color 1f");

        Assert.IsTrue(result.Success);
        Assert.AreEqual(ConsoleColor.DarkBlue, _console.Background);
        Assert.AreEqual(ConsoleColor.White, _console.Foreground);
    }

    [TestMethod]
    public void ExecuteCommand_ColorWithoutArgument_ResetsColors()
    {
        // Arrange
        _invoker.ExecuteCommand("color 1F");

        // Act
        CommandResult result = _invoker.ExecuteCommand("color");

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(ConsoleColor.Black, _console.Background);
        Assert.AreEqual(ConsoleColor.White, _console.Foreground);
    }

    [TestMethod]
    public void ExecuteCommand_ColorInvalidHex_ReturnsError()
    {
        CommandResult result = _invoker.ExecuteCommand("color ZZ");

        Assert.IsFalse(result.Success);
        Assert.Contains("Ungültiger Farbwert", result.OutputMessage);
    }

    [TestMethod]
    public void ExecuteCommand_ColorWrongLength_ReturnsError()
    {
        CommandResult result = _invoker.ExecuteCommand("color 0");

        Assert.IsFalse(result.Success);
    }

    [TestMethod]
    public void ExecuteCommand_ColorSameForegroundAndBackground_ReturnsError()
    {
        CommandResult result = _invoker.ExecuteCommand("color 00");

        Assert.IsFalse(result.Success);
    }

    [TestMethod]
    public void ExecuteCommand_ColorHelp_PrintsColorTable()
    {
        CommandResult result = _invoker.ExecuteCommand("color help");

        Assert.IsTrue(result.Success);
        Assert.IsTrue(_console.Output.Any(l => l.Contains("COLOR [xy]")));
    }
}