using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class TypeCommand : Command
{
    public TypeCommand(
        IFileSystemService fileService,
        IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "type";

    public override string Description =>
        "Zeigt den Inhalt einer Textdatei an.";

    public override CommandResult Execute(CommandContext context)
    {
        if (context.Arguments.Count != 1 ||
            context.Switches.Count > 0)
        {
            return CommandResult.Error(
                "Syntax: type <Dateiname>");
        }

        string path = context.Arguments[0];

        if (FileService.DirectoryExists(path))
        {
            return CommandResult.Error(
                $"\"{path}\" ist ein Ordner.");
        }

        if (!FileService.FileExists(path))
        {
            return CommandResult.Error(
                $"Datei nicht gefunden: {path}");
        }

        return CommandResult.Ok(FileService.ReadAllText(path));
    }
}