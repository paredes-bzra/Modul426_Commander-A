
using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class MoreCommand : Command
{
    private const int PageSize = 20;

    public MoreCommand(
        IFileSystemService fileService,
        IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "more";

    public override string Description =>
        "Zeigt den Inhalt einer Textdatei seitenweise an.";

    public override CommandResult Execute(CommandContext context)
    {
        if (context.Arguments.Count != 1 ||
            context.Switches.Count > 0)
        {
            return CommandResult.Error(
                "Syntax: more <Dateiname>");
        }

        string path = context.Arguments[0];

        try
        {
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

            string content = FileService.ReadAllText(path);

            using var reader = new StringReader(content);

            int lineCount = 0;
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                ConsoleService.WriteLine(line);
                lineCount++;

                if (lineCount == PageSize && reader.Peek() != -1)
                {
                    while (true)
                    {
                        ConsoleService.Write("-- Mehr --");

                        string? input = ConsoleService.ReadLine();

                        if (input == null ||
                            input.Trim().Equals(
                                "q",
                                StringComparison.OrdinalIgnoreCase))
                        {
                            return CommandResult.Ok();
                        }

                        if (string.IsNullOrWhiteSpace(input))
                        {
                            break;
                        }
                    }

                    lineCount = 0;
                }
            }

            return CommandResult.Ok();
        }
        catch (Exception ex)
        {
            return CommandResult.Error(
                $"Datei konnte nicht gelesen werden: {ex.Message}");
        }
    }
}
