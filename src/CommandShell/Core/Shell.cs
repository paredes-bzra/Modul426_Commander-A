using CommandShell.Services;

namespace CommandShell.Core;

/// <summary>
/// Hauptschleife: Prompt anzeigen, Eingabe lesen, an den Invoker weitergeben.
/// </summary>
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
            DisplayPrompt();

            string? input = _console.ReadLine();
            if (input == null)
            {
                // Eingabe wurde geschlossen (z. B. Ctrl+Z)
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
        // TODO SCRUM-15: aktuelles Verzeichnis anzeigen, z. B. "C:\Users\Daniel>"
        // (_fileSystem.CurrentDirectory)
        _console.Write("> ");
    }
}
