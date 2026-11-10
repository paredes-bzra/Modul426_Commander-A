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
            if (context.Arguments.Count == 0)
            {
                return CommandResult.Ok(FileService.CurrentDirectory);
            }

            string path = string.Join(" ", context.Arguments);

            if (!FileService.DirectoryExists(path))
            {
                return CommandResult.Error("Das System kann den angegebenen Pfad nicht finden.");
            }

            FileService.CurrentDirectory = path;
            return CommandResult.Ok();
        }
    }
}