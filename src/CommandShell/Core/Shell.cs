using CommandShell.Services;

namespace CommandShell.Core;

public class Shell
{
    private readonly CommandInvoker _invoker;
    private readonly IConsoleService _console;
    private readonly IFileSystemService _fileSystem;
    private readonly ShellContext _context;

    public Shell(CommandInvoker invoker, IConsoleService console, IFileSystemService fileSystem, ShellContext context)
    {
        _invoker = invoker;
        _console = console;
        _fileSystem = fileSystem;
        _context = context;
    }

    public void Run()
    {
        _console.SetColor(_context.BackgroundColor, _context.ForegroundColor);

        bool running = true;
        while (running)
        {
            DisplayPrompt();
            string? input = _console.ReadLine();
            if (input == null)
            {
                break;
            }

            CommandResult result = _invoker.ExecuteCommand(input);
            if (!string.IsNullOrEmpty(result.OutputMessage))
            {
                _console.WriteLine(result.OutputMessage);
            }

            running = !result.ShouldExit;
        }
    }

    private void DisplayPrompt()
    {
        _console.Write(_fileSystem.CurrentDirectory + "> ");
    }
}