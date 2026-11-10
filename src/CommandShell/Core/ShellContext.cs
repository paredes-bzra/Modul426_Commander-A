using CommandShell.FileSystem;

namespace CommandShell.Core;

public class ShellContext
{
    public VirtualDirectory Root { get; set; } = null!;
    public ConsoleColor ForegroundColor { get; set; }
    public ConsoleColor BackgroundColor { get; set; }

    public static ShellContext CreateDefault()
    {
        return new ShellContext
        {
            Root = new VirtualDirectory { Name = "C:" },
            ForegroundColor = ConsoleColor.Gray,
            BackgroundColor = ConsoleColor.Black
        };
    }
}
