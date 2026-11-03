namespace CommandShell.Services;

public class ConsoleService : IConsoleService
{
    public void Write(string text) => Console.Write(text);
    public void WriteLine(string text) => Console.WriteLine(text);
    public string? ReadLine() => Console.ReadLine();
    public void Clear() => Console.Clear();

    public void SetColor(ConsoleColor background, ConsoleColor foreground)
    {
        Console.BackgroundColor = background;
        Console.ForegroundColor = foreground;
        Console.Clear();
    }


    public void ResetColor()
    {
        Console.ResetColor();
        Console.Clear();
    }
}