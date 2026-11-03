using CommandShell.Core;

namespace CommandShell.Tests;

[TestClass]
public class ParserTests
{
    private readonly Parser _parser = new();

    [TestMethod]
    public void Given_Dir_When_Parse_Then_NameIsDirWithoutArgumentsAndSwitches()
    {
        // Arrange
        string input = "dir";

        // Act
        CommandContext context = _parser.Parse(input);

        // Assert
        Assert.AreEqual("dir", context.CommandName);
        Assert.AreEqual(0, context.Arguments.Count);
        Assert.AreEqual(0, context.Switches.Count);
    }

    [TestMethod]
    public void Given_MoveWithSwitch_When_Parse_Then_OneSwitchAndTwoArguments()
    {
        // Arrange
        string input = @"move /y C:\Daten\a.txt C:\Archiv";

        // Act
        CommandContext context = _parser.Parse(input);

        // Assert
        Assert.AreEqual("move", context.CommandName);
        CollectionAssert.AreEqual(new[] { "/y" }, context.Switches.ToList());
        CollectionAssert.AreEqual(new[] { @"C:\Daten\a.txt", @"C:\Archiv" }, context.Arguments.ToList());
    }

    [TestMethod]
    public void Given_CdWithQuotes_When_Parse_Then_OneArgumentWithSpace()
    {
        // Arrange
        string input = "cd \"Meine Dateien\"";

        // Act
        CommandContext context = _parser.Parse(input);

        // Assert
        Assert.AreEqual("cd", context.CommandName);
        CollectionAssert.AreEqual(new[] { "Meine Dateien" }, context.Arguments.ToList());
    }

    [TestMethod]
    public void Given_ManySpaces_When_Parse_Then_SpacesAreIgnored()
    {
        // Arrange
        string input = "   cd    ..   ";

        // Act
        CommandContext context = _parser.Parse(input);

        // Assert
        Assert.AreEqual("cd", context.CommandName);
        CollectionAssert.AreEqual(new[] { ".." }, context.Arguments.ToList());
    }

    [TestMethod]
    public void Given_EmptyInput_When_Parse_Then_NameIsEmpty()
    {
        // Arrange
        string input = "";

        // Act
        CommandContext context = _parser.Parse(input);

        // Assert
        Assert.AreEqual(string.Empty, context.CommandName);
        Assert.AreEqual(0, context.Arguments.Count);
        Assert.AreEqual(0, context.Switches.Count);
    }
}
