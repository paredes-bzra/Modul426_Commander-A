namespace CommandShell.Services;

public interface IConsoleService
{
 	void Write(string text);
    void WriteLine(string text);
    string? ReadLine();
    void Clear();
    
    void SetColor(ConsoleColor background, ConsoleColor foreground);
    void ResetColor();
    
}