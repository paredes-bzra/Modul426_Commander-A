using CommandShell.Commands;
using CommandShell.Services;

namespace CommandShell.Factory;

public class CommandFactory
{
    private static readonly Dictionary<string, Func<IFileSystemService, IConsoleService, Command>> _commands =
        new(StringComparer.OrdinalIgnoreCase)
        {
        };

    public static Command Create(string name, IFileSystemService fileService, IConsoleService consoleService)
    {
        if (_commands.TryGetValue(name, out var create))
        {
            return create(fileService, consoleService);
        }

        return new UnknownCommand(fileService, consoleService);
    }

    public static IEnumerable<Command> GetCommands(IFileSystemService fileService, IConsoleService consoleService)
    {
        return _commands.Values.Select(create => create(fileService, consoleService));
    }
}