using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class UnknownCommand : Command
{
    private readonly string _name;

    public UnknownCommand(string name, IFileSystemService fileService, IConsoleService consoleService)
        : base(fileService, consoleService)
    {
        _name = name;
    }

    public override string Name => _name;

    public override string Description => "Unbekannter Befehl.";

    public override CommandResult Execute(CommandContext context)
    {
        return CommandResult.Error($"Der Befehl \"{_name}\" ist entweder falsch geschrieben oder konnte nicht gefunden werden.");
    }
}
