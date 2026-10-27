namespace DefaultNamespace;

IFileSystem iFileSystem = new IFileSystem();

public class ExitCommand : Command
{
    private readonly IFileSystem _iFileSystem;
    private readonly IConsoleService _iConsoleService;

    public ExitCommand(IFileSystem iFileSystem, IConsoleService iConsoleService)
    {
    iFileSystem = _iFileSytem
    }
}