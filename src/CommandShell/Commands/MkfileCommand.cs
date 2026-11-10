using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class MkfileCommand : Command
{
    public MkfileCommand(
        IFileSystemService fileService,
        IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "mkfile";

    public override string Description => "Erstellt eine Textdatei mit Inhalt";

    public override CommandResult Execute(CommandContext context)
    {
        if (context.Arguments.Count == 0)
            return CommandResult.Error("Verwendung: mkfile <Dateiname> <Inhalt>");
        
        string path = context.Arguments[0];
        
        if (FileService.FileExists(path))
            return CommandResult.Error("Die Datei existiert bereits.");
        
        FileService.WriteAllText(path, String.Join(" ", context.Arguments.Skip(1)));
        return CommandResult.Ok();
    }
}
   