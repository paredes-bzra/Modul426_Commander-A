using CommandShell.Commands;
using CommandShell.Services;

namespace CommandShell.Factory;

public static class CommandFactory
{
    // Neuer Befehl: eine Zeile eintragen { "name", (f, c) => new XyCommand(f, c) }
    private static readonly Dictionary<string, Func<IFileSystemService, IConsoleService, Command>> Commands =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "exit", (f, c) => new ExitCommand(f, c) },
            { "mkdir", (f, c) => new MkdirCommand(f, c) },
            { "md", (f, c) => new MkdirCommand(f, c) },
        };

    public static Command Create(string name, IFileSystemService fileService, IConsoleService consoleService)
    {
        if (Commands.TryGetValue(name, out var create))
        {
            return create(fileService, consoleService);
        }

        return new UnknownCommand(name, fileService, consoleService);
    }

    public static IReadOnlyCollection<string> GetCommands()
    {
        return Commands.Keys;
    }
}
