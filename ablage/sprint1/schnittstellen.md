# Schnittstellen – verbindlich für alle

Diese Datei ist der Vertrag zwischen uns vier. Jeder baut seine Klassen **genau mit diesen Namen und Signaturen**. Dann passt der Code am Ende zusammen, auch wenn jeder mit einer anderen KI arbeitet.

Wenn du etwas ändern musst: zuerst im Team absprechen, dann diese Datei anpassen.

Klassendiagramm: https://miro.com/app/board/uXjVHwm6kzI=/?moveToWidget=3458764688101220187

## Projekt

| | |
|---|---|
| Solution | `CommandShell.sln` im Repo-Hauptordner |
| Programm | `src/CommandShell/CommandShell.csproj`, Console Application, `net10.0`, Nullable enable |
| Tests | `tests/CommandShell.Tests/CommandShell.Tests.csproj`, xUnit, verweist auf CommandShell |
| Namespace | `CommandShell.<Ordner>`, z. B. `CommandShell.Core` |

## Ordner und Namespaces

```
src/CommandShell/
├── Program.cs            namespace CommandShell
├── Core/                 namespace CommandShell.Core
├── Commands/             namespace CommandShell.Commands
├── Factory/              namespace CommandShell.Factory
└── Services/             namespace CommandShell.Services
tests/CommandShell.Tests/
├── Fakes/                namespace CommandShell.Tests.Fakes
└── *Tests.cs             namespace CommandShell.Tests
```

## Core

```csharp
public class CommandContext
{
    public CommandContext(string commandName, IReadOnlyList<string> arguments,
                          IReadOnlyList<string> switches, string rawInput);

    public string CommandName { get; }
    public IReadOnlyList<string> Arguments { get; }
    public IReadOnlyList<string> Switches { get; }
    public string RawInput { get; }

    public bool HasSwitch(string name);          // Gross-/Kleinschreibung egal
}

public class CommandResult
{
    public bool Success { get; }
    public string OutputMessage { get; }
    public bool ShouldExit { get; }

    public static CommandResult Ok(string outputMessage = "");
    public static CommandResult Error(string outputMessage);
    public static CommandResult Exit();
}

public class Parser
{
    public CommandContext Parse(string input);
    // erstes Wort = CommandName
    // Wörter mit / am Anfang = Switches, alles andere = Arguments
    // "Text in Anführungszeichen" = ein Argument
    // leere Eingabe → CommandName ist ""
}

public class CommandInvoker
{
    public CommandInvoker(Parser parser, IFileSystemService fileSystem, IConsoleService console);
    public CommandResult ExecuteCommand(string input);
    // leere Eingabe → CommandResult.Ok()
    // sonst: Parse → CommandFactory.Create → command.Execute
}

public class Shell
{
    public Shell(CommandInvoker invoker, IConsoleService console, IFileSystemService fileSystem);
    public void Run();
    // Schleife: Prompt anzeigen → ReadLine → ExecuteCommand
    //           → OutputMessage ausgeben (falls nicht leer) → bei ShouldExit beenden
    //           ReadLine gibt null → beenden
}
```

## Commands

```csharp
public abstract class Command
{
    protected Command(IFileSystemService fileService, IConsoleService consoleService);

    protected IFileSystemService FileService { get; }
    protected IConsoleService ConsoleService { get; }

    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract CommandResult Execute(CommandContext context);
}

public class UnknownCommand : Command
{
    public UnknownCommand(string name, IFileSystemService fileService, IConsoleService consoleService);
    // Execute → CommandResult.Error("Der Befehl \"<name>\" ist entweder falsch geschrieben oder konnte nicht gefunden werden.")
}
```

Jeder Befehl (ExitCommand, DirCommand, …) hat genau diesen Konstruktor:
```csharp
public XyzCommand(IFileSystemService fileService, IConsoleService consoleService)
    : base(fileService, consoleService) { }
```

## Factory

```csharp
public static class CommandFactory
{
    // Dictionary<string, Func<IFileSystemService, IConsoleService, Command>>
    // mit StringComparer.OrdinalIgnoreCase  → "DIR" und "dir" sind gleich

    public static Command Create(string name, IFileSystemService fileService, IConsoleService consoleService);
    // unbekannter Name → new UnknownCommand(name, ...)

    public static IList<string> GetCommands();   // alle Namen, alphabetisch
}
```

Neuer Befehl = **eine Zeile** im Dictionary:
```csharp
{ "cd", (fileService, consoleService) => new CdCommand(fileService, consoleService) },
```

## Services

```csharp
public interface IConsoleService
{
    string? ReadLine();
    void Write(string text);
    void WriteLine(string text);
    void Clear();
    void SetColor(ConsoleColor foreground, ConsoleColor background);
    string GetOSVersion();               // z. B. "Microsoft Windows [Version 10.0.26100]"
}

public interface IFileSystemService
{
    string CurrentDirectory { get; }
    IReadOnlyList<DirectoryEntry> GetDirectoryContents(string path);
    void CreateDirectory(string path);
    void DeleteFile(string path);
    string ReadFileText(string path);
    void ChangeDirectory(string path);
    void MoveFileOrDirectory(string source, string destination);
    void RenameFileOrDirectory(string source, string newName);
}

public record DirectoryEntry(string Name, bool IsDirectory, long Size, DateTime LastModified);
```

**Regeln für den FileSystemService:**
- Relative Pfade gelten ab `CurrentDirectory`: `Path.GetFullPath(path, CurrentDirectory)`
- `CurrentDirectory` startet mit `Directory.GetCurrentDirectory()` und wird nur über `ChangeDirectory` geändert
- Fehler werden als Exception geworfen: `DirectoryNotFoundException`, `FileNotFoundException`, `IOException` (z. B. Ordner existiert schon)
- `CreateDirectory` erstellt auch fehlende Zwischenordner (`Projekte\2026`)

## Fakes für Tests (tests/CommandShell.Tests/Fakes)

```csharp
public class FakeConsoleService : IConsoleService
{
    public FakeConsoleService(params string[] inputs);   // ReadLine gibt diese der Reihe nach zurück, danach null
    public List<string> Output { get; }                  // alles, was Write/WriteLine bekommen hat
    public bool WasCleared { get; }
    public ConsoleColor? Foreground { get; }
    public ConsoleColor? Background { get; }
    public string OSVersion { get; set; }
}

public class FakeFileSystemService : IFileSystemService
{
    public string CurrentDirectory { get; set; }         // Start: C:\Users\Test
    public void AddDirectory(string path);               // legt Ordner im Speicher an
    public void AddFile(string path, string content = "");
    // alle Interface-Methoden arbeiten nur im Speicher, gleiche Exceptions wie der echte Service
}
```

## Regeln für alle (auch für eure KI)

1. **Nur die eigenen Dateien** anlegen oder ändern. Fremde Dateien nur nach Absprache.
2. `Console`, `File` und `Directory` **nur** in `ConsoleService` und `FileSystemService`.
3. Eine Klasse pro Datei, Dateiname = Klassenname.
4. Kein `switch` und kein `if/else` über Befehlsnamen. Dafür ist die Factory da.
5. Meldungen an den Benutzer auf Deutsch.
6. Einfach halten: keine Extra-Klassen, keine Bibliotheken, keine langen Kommentare.
7. Zu jedem Befehl: ein Test für den Normalfall und einer für einen Fehlerfall (DoD).
