using CommandShell.Commands;
using CommandShell.Factory;
using CommandShell.Services;

namespace CommandShell.Core;

public class CommandInvoker
{
    private readonly Parser _parser;
    private readonly IFileSystemService _fileSystem;
    private readonly IConsoleService _console;

    public CommandInvoker(Parser parser, IFileSystemService fileSystem, IConsoleService console)
    {
        _parser = parser;
        _fileSystem = fileSystem;
        _console = console;
       
    }

    public CommandResult ExecuteCommand(string input)
    {
        
    }
}
