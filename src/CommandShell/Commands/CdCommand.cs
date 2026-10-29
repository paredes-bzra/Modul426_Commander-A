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

            string input = string.Join(" ", context.Arguments);
            string newPath = Path.GetFullPath(Path.Combine(FileService.CurrentDirectory, input));

            if (!FileService.DirectoryExists(newPath))
            {
                return CommandResult.Error("Das System kann den angegebenen Pfad nicht finden.");
            }

            FileService.CurrentDirectory = newPath;
            return CommandResult.Ok();
        }
    }
}