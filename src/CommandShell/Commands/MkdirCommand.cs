using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class MkdirCommand : Command
{
    public MkdirCommand(IFileSystemService fileService, IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "mkdir";

    public override string Description => "Erstellt einen Ordner.";

    public override CommandResult Execute(CommandContext context)
    {
        if (context.Arguments.Count == 0)
        {
            return CommandResult.Error("Syntax: mkdir <Ordnername>");
        }

        string path = context.Arguments[0];

        if (FileService.DirectoryExists(path))
        {
            return CommandResult.Error($"Ein Unterverzeichnis oder eine Datei mit dem Namen \"{path}\" existiert bereits.");
        }

        FileService.CreateDirectory(path);
        return CommandResult.Ok();
    }
}
