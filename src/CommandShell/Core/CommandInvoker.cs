using CommandShell.Commands;
using CommandShell.Factory;
using CommandShell.Services;

namespace CommandShell.Core;

/// <summary>
/// Invoker aus dem Command Pattern: holt das passende Command
/// bei der CommandFactory und führt es aus.
/// </summary>
public class CommandInvoker
{
    private readonly Parser _parser;
    private readonly IFileSystemService _fileSystem;
    private readonly IConsoleService _console;

    public CommandInvoker(Parser parser, IFileSystemService fileSystem, IConsoleService console)
    {
        _parser = parser;
        _fileSystem = fileSystem;
        _console = console;
    }

    public CommandResult ExecuteCommand(string input)
    {
        CommandContext context = _parser.Parse(input);

        if (string.IsNullOrEmpty(context.CommandName))
        {
            // Leere Eingabe: nichts tun, einfach neuer Prompt
            return CommandResult.Ok();
        }

        Command command = CommandFactory.Create(context.CommandName, _fileSystem, _console);
        return command.Execute(context);
    }
}
