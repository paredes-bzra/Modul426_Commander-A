namespace CommandShell.Core;

public class CommandContext
{
    public string CommandName { get; }
    public IReadOnlyList<string> Arguments { get; }
    public IReadOnlyList<string> Switches { get; }
    public string RawInput { get; }

    public CommandContext(string commandName, IReadOnlyList<string> arguments,
        IReadOnlyList<string> switches, string rawInput)
    {
        CommandName = commandName;
        Arguments = arguments;
        Switches = switches;
        RawInput = rawInput;
    }
}
