using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class ClsCommand : Command
{
    public ClsCommand(
        IFileSystemService fileService,
        IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "cls";

    public override string Description => "Leert die Konsole.";

    public override CommandResult Execute(CommandContext context)
    {
        if (context.Arguments.Count > 0 ||
            context.Switches.Count > 0)
        {
            return CommandResult.Error("Syntax: cls");
        }

        ConsoleService.Clear();
        return CommandResult.Ok();
    }
}