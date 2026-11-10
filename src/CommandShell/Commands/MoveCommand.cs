using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class MoveCommand : Command
{
    public MoveCommand(IFileSystemService fileService, IConsoleService consoleService) : base(fileService, consoleService)
    {
        
    }

    public override string Name { get; }
    public override string Description { get; }
    public override CommandResult Execute(CommandContext context)
    {
        throw new NotImplementedException();
    }
}