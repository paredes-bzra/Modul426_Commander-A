using CommandShell.Core;
using CommandShell.Services;

namespace CommandShell.Commands;

public class ColorCommand : Command
{
    
    public ColorCommand(IFileSystemService fileService, IConsoleService consoleService)
        : base(fileService, consoleService)
    {
    }

    public override string Name => "color";

    public override string Description => "Ändert die Farben der Konsole (color xy).";

    public override CommandResult Execute(CommandContext context)
    {

        if (context.Arguments.Count == 1 && (context.Arguments[0] == "help" || context.Arguments[0] == "/help"))
        {
            ConsoleService.WriteLine("COLOR [xy]  (x = Hintergrund, y = Text)");
            ConsoleService.WriteLine("");
            ConsoleService.WriteLine("0 = Schwarz      8 = Grau");
            ConsoleService.WriteLine("1 = Blau         9 = Hellblau");
            ConsoleService.WriteLine("2 = Grün         A = Hellgrün");
            ConsoleService.WriteLine("3 = Türkis       B = Helltürkis");
            ConsoleService.WriteLine("4 = Rot          C = Hellrot");
            ConsoleService.WriteLine("5 = Lila         D = Helllila");
            ConsoleService.WriteLine("6 = Gelb         E = Hellgelb");
            ConsoleService.WriteLine("7 = Hellgrau     F = Weiß");
            return CommandResult.Ok();
        }

        if (context.Arguments.Count == 0)
        {
            ConsoleService.ResetColor();
            return CommandResult.Ok();
        }

        string v = context.Arguments[0].ToUpper();

        if (v.Length != 2 || !Uri.IsHexDigit(v[0]) || !Uri.IsHexDigit(v[1]) || v[0] == v[1])
        {
            return CommandResult.Error("Ungültiger Farbwert. Verwendung: color xy (0-9, A-F, verschiedene Farben)");
        }

        ConsoleService.SetColor(
            (ConsoleColor)Convert.ToInt32(v[0].ToString(), 16),
            (ConsoleColor)Convert.ToInt32(v[1].ToString(), 16));

        return CommandResult.Ok();
    }
}