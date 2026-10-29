using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class ExitCommand : Command
{
    //Konstruktor
    public ExitCommand(IFileSystemService fileService, IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "exit";

    public override string Description => "Beendet die Shell.";

    public override CommandResult Execute(CommandContext context)
    {
        return CommandResult.Exit();
    }
}
