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

        FileService.Rename(context.Arguments[0], context.Arguments[1]);
        return CommandResult.Ok();
    }
}
