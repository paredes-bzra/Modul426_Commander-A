using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands
{
    public class CdCommand : Command
    {
        public CdCommand(IFileSystemService fileService, IConsoleService consoleService)
            : base(fileService, consoleService)
        {
        }

        public override string Name => "cd";

        public override string Description => "Wechselt in einen Ordner.";

        public override CommandResult Execute(CommandContext context)
        {
            return CommandResult.Ok();
        }
    }
}