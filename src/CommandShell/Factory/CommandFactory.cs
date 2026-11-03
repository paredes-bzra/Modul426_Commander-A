using CommandShell.Commands;
using CommandShell.Services;

namespace CommandShell.Factory;

public static class CommandFactory
{
    private static readonly Dictionary<string, Func<IFileSystemService, IConsoleService, Command>> Commands =
        new(StringComparer.OrdinalIgnoreCase)
        {
            //für jeden neuen Command das hier machen
            { "exit", (f, c) => new ExitCommand(f, c) },
            { "mkdir", (f, c) => new MkdirCommand(f, c) },
            { "md", (f, c) => new MkdirCommand(f, c) },
            { "cls", (f, c) => new ClsCommand(f, c) },
            { "cd", (fs, console) => new CdCommand(fs, console) },
            {"ver", (fs, console) => new VerCommand(fs, console) },
            {"dir", (fs, console) => new DirCommand(fs, console) },
            {"set color", (f, c) => new ColorCommand(f, c) },
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
