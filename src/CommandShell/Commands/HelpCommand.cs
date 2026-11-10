using CommandShell.Core;
using CommandShell.Factory;
using CommandShell.Services;

namespace CommandShell.Commands;

public class HelpCommand : Command
{
    public HelpCommand(
        IFileSystemService fileService,
        IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "help";

    public override string Description =>
        "Zeigt alle verfügbaren Befehle an.";

    public override CommandResult Execute(CommandContext context)
    {
        if (context.Arguments.Count > 0 ||
            context.Switches.Count > 0)
        {
            return CommandResult.Error("Syntax: help");
        }

        var lines = new List<string>
        {
            "Verfügbare Befehle:",
            ""
        };

        var commandNames = CommandFactory.GetCommands()
            .OrderBy(name => name, StringComparer.OrdinalIgnoreCase);

        foreach (string name in commandNames)
        {
            Command command = CommandFactory.Create(
                name,
                FileService,
                ConsoleService);

            lines.Add($"{name,-12} {command.Description}");
        }

        return CommandResult.Ok(
            string.Join(Environment.NewLine, lines));
    }
}