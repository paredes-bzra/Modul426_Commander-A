using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class UnknownCommand : Command
{
    public UnknownCommand(IFileSystemService fileService, IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "unknown";

    public override string Description => "Wird verwendet, wenn der Befehl unbekannt ist.";

    public override CommandResult Execute(CommandContext context)
    {
        return CommandResult.Error($"\"{context.CommandName}\" wird nicht als interner oder externer Befehl erkannt.");
    }
}