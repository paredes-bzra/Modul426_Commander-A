using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class MoveCommand : Command
{
    public MoveCommand(IFileSystemService fileService, IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "move";

    public override string Description => "Verschiebt eine Datei oder einen Ordner.";

    public override CommandResult Execute(CommandContext context)
    {
        if (context.Arguments.Count != 2)
        {
            return CommandResult.Error("Syntax: move <Quelle> <Ziel>");
        }

        try
        {
            FileService.Move(context.Arguments[0], context.Arguments[1]);
            return CommandResult.Ok();
        }
        catch (Exception ex)
        {
            return CommandResult.Error($"Verschieben fehlgeschlagen: {ex.Message}");
        }
    }
}
