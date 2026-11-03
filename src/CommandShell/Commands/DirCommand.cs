using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class DirCommand : Command

{
    public DirCommand(IFileSystemService fileService, IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "dir";

    public override string Description => "Zeigt alle Dateien und Ordner im aktuellen Verzeichnis an.";

    public override CommandResult Execute(CommandContext context)
    {
        var entries = FileService.GetEntries(FileService.CurrentDirectory).ToList();

        if (entries.Count == 0)
        {
            return CommandResult.Ok("Keine Dateien gefunden.");
        }
        
        var lines = entries
            .Select(e => e.IsDirectory
                ? $"<DIR>  {e.Name}"
                : $"{e.Size,10} Bytes  {e.Name}")
            .ToList();

        var dirCount = entries.Count(e => e.IsDirectory);
        var fileCount = entries.Count - dirCount;
        lines.Add($"{fileCount} Datei(en), {dirCount} Ordner");

        return CommandResult.Ok(string.Join(Environment.NewLine, lines));
    }
}
