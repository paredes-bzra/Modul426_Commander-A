using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class VerCommand : Command
{
    public VerCommand(IFileSystemService fileService, IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "ver";

    public override string Description => "Zeigt die Version des Betriebssystems.";

    public override CommandResult Execute(CommandContext context)
    {
        var version = Environment.OSVersion.Version;
        return CommandResult.Ok($"Microsoft Windows [Version {version}]");
    }
}