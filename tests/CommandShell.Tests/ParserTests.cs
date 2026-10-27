using CommandShell.Core;

namespace CommandShell.Tests;

public class ParserTests
{
    private readonly Parser _parser = new();

    [Fact]
    public void Given_Dir_When_Parse_Then_NameIsDirWithoutArgumentsAndSwitches()
    {
        // Arrange
        string input = "dir";

        // Act
        CommandContext context = _parser.Parse(input);

        // Assert
        Assert.Equal("dir", context.CommandName);
        Assert.Empty(context.Arguments);
        Assert.Empty(context.Switches);
    }

    [Fact]
    public void Given_MoveWithSwitch_When_Parse_Then_OneSwitchAndTwoArguments()
    {
        // Arrange
        string input = @"move /y C:\Daten\a.txt C:\Archiv";

        // Act
        CommandContext context = _parser.Parse(input);

        // Assert
        Assert.Equal("move", context.CommandName);
        Assert.Equal(new[] { "/y" }, context.Switches);
        Assert.Equal(new[] { @"C:\Daten\a.txt", @"C:\Archiv" }, context.Arguments);
    }

    [Fact]
    public void Given_CdWithQuotes_When_Parse_Then_OneArgumentWithSpace()
    {
        // Arrange
        string input = "cd \"Meine Dateien\"";

        // Act
        CommandContext context = _parser.Parse(input);

        // Assert
        Assert.Equal("cd", context.CommandName);
        Assert.Equal(new[] { "Meine Dateien" }, context.Arguments);
    }

    [Fact]
    public void Given_ManySpaces_When_Parse_Then_SpacesAreIgnored()
    {
        // Arrange
        string input = "   cd    ..   ";

        // Act
        CommandContext context = _parser.Parse(input);

        // Assert
        Assert.Equal("cd", context.CommandName);
        Assert.Equal(new[] { ".." }, context.Arguments);
    }

    [Fact]
    public void Given_EmptyInput_When_Parse_Then_NameIsEmpty()
    {
        // Arrange
        string input = "";

        // Act
        CommandContext context = _parser.Parse(input);

        // Assert
        Assert.Equal(string.Empty, context.CommandName);
        Assert.Empty(context.Arguments);
        Assert.Empty(context.Switches);
    }
}
