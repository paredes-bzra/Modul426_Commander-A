using CommandShell.Services;
namespace CommandShell.Core;

public class MkdirCommand : Command
{
    private readonly IFileSystemService _fileSystem;
    private readonly IConsoleService _console;
    
    public MkdirCommand(IFileSystemService fileSystem, IConsoleService console)
        {
        _fileSystem = fileSystem;
        _console = console;
        }

    public override CommandResult Execute(string[] args)
    {
        if (args.Length == 0)
        {
            _console.WriteLine("mkdir: Ordnername fehlt");
            return CommandResult.Error;
        }
        
        var path = args[0];

        if (_fileSystem.DirectoryExists(path))
        {
            _console.WriteLine($"mkdir: '{path}' existiert bereits");
            return CommandResult.Error;
        }
        _fileSystem.CreateDirectory(path);
        return CommandResult.Success;
    }
}