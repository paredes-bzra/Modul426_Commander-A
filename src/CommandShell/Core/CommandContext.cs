namespace CommandShell.Core;

public class CommandContext
{
    public string CommandName { get; }
    public IReadOnlyList<string> Arguments { get; }
    public IReadOnlyList<string> Switches { get; }

    public CommandContext(string commandName, IReadOnlyList<string> arguments,
        IReadOnlyList<string> switches)
    {
        CommandName = commandName;
        Arguments = arguments;
        Switches = switches;
    }
}
