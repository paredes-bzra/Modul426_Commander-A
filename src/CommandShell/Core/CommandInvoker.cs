using CommandShell.Commands;
using CommandShell.Factory;
using CommandShell.Services;

namespace CommandShell.Core;

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
        if (!context.IsNullOrEmpty== true)
        {
            return CommandResult.Ok();
        }
        Command command = CommandFactory.Create(context, _console, _fileSystem);
        return command.Execute(context);
    }
}
