using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public abstract class Command
{
    protected IFileSystemService FileService { get; }
    protected IConsoleService ConsoleService { get; }

    protected Command(IFileSystemService fileService, IConsoleService consoleService)
    {
        FileService = fileService;
        ConsoleService = consoleService;
    }

    public abstract string Name { get; }

    public abstract string Description { get; }

    public abstract CommandResult Execute(CommandContext context);
}
