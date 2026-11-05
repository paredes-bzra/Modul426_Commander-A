using CommandShell.Services;
namespace CommandShell.Tests.Fakes;

public class FakeConsoleService : IConsoleService
{
    public List<string> Output {get;} = new();
	public Queue<string> Input {get;} = new();
	

	public void Write(string text) => Output.Add(text);
	public void WriteLine(string text) => Output.Add(text);
	public string? ReadLine() => Input.Count > 0 ? Input.Dequeue() : null;
	public void Clear() => Output.Clear();
	public ConsoleColor Background { get; private set; } = ConsoleColor.Black;
	public ConsoleColor Foreground { get; private set; } = ConsoleColor.Gray;
	

	public void SetColor(ConsoleColor background, ConsoleColor foreground)
	{
		Background = background;
		Foreground = foreground;
	}

	public void ResetColor() => SetColor(ConsoleColor.Black, ConsoleColor.White);
}
