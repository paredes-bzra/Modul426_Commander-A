using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class RenCommand : Command
{
    public RenCommand(
        IFileSystemService fileService,
        IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "ren";

    public override string Description =>
        "Benennt eine Datei oder einen Ordner um.";

    public override CommandResult Execute(CommandContext context)
    {
        if (context.Arguments.Count != 2 || context.Switches.Count > 0)
        {
            return CommandResult.Error(
                "Syntax: ren <AlterName> <NeuerName>");
        }

        string alterName = context.Arguments[0];
        string neuerName = context.Arguments[1];

        try
        {
            FileService.Rename(alterName, neuerName);
            return CommandResult.Ok();
        }
        catch (Exception ex)
        {
            return CommandResult.Error(
                $"Umbenennen fehlgeschlagen: {ex.Message}");
        }
    }
}
