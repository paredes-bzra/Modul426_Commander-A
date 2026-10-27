using CommandShell.Services;

namespace CommandShell.Core;

public class Shell
{
    private readonly CommandInvoker _invoker;
    private readonly IConsoleService _console;
    private readonly IFileSystemService _fileSystem;

    public Shell(CommandInvoker invoker, IConsoleService console, IFileSystemService fileSystem)
    {
        _invoker = invoker;
        _console = console;
        _fileSystem = fileSystem;
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
            DisplayPromt();
            string? input = _console.Readline();
            if (input == null)
            {
                break;
            }
            Commandresult result = _invoker.ExecuteCommand(input);
            _console.Writeline(result);
            running = !result.ShouldExit;
            
        }
        
    }

    private void DisplayPrompt()
    {
        _console.Write("> ");
    }
}
