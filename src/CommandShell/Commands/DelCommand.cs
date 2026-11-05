using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class DelCommand : Command
{
    public DelCommand(IFileSystemService fileService, IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "del";

    public override String Description => "Löscht eine Datei";

    public override CommandResult Execute(CommandContext context)
    {
        if (context.Arguments.Count == 0)
        {
            return CommandResult.Error("Die Syntax des Befehls ist falsch. Verwendung: del <datei>");
        }

        string path = (context.Arguments[0]);
        if (!FileService.FileExists(path))
        {
            return CommandResult.Error("Keine Dateien gefunden.");
        }

        FileService.DeleteFile(path);
        return CommandResult.Ok();
    }
}
    
        
        
    