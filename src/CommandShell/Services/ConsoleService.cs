using CommandShell.Core;

namespace CommandShell.Services;

public class ConsoleService : IConsoleService
{
    private readonly ShellContext _context;
    private readonly IShellContextStorage _storage;

    public ConsoleService(ShellContext context, IShellContextStorage storage)
    {
        _context = context;
        _storage = storage;
    }

    public void Write(string text) => Console.Write(text);
    public void WriteLine(string text) => Console.WriteLine(text);
    public string? ReadLine() => Console.ReadLine();
    public void Clear() => Console.Clear();

    public void SetColor(ConsoleColor background, ConsoleColor foreground)
    {
        _context.BackgroundColor = background;
        _context.ForegroundColor = foreground;
        _storage.Save(_context);

        Console.BackgroundColor = background;
        Console.ForegroundColor = foreground;
        Console.Clear();
    }

    public void ResetColor()
    {
        var defaults = ShellContext.CreateDefault();
        SetColor(defaults.BackgroundColor, defaults.ForegroundColor);
    }
}