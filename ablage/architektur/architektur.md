# Architektur – Command Shell (Version 2)

Version 2 basiert auf der Musterlösung der Lehrperson ([musterloesung-v1.png](musterloesung-v1.png)) und übernimmt die Teile aus unserem ersten Entwurf, die sich bewährt haben.

## Klassendiagramm

```mermaid
classDiagram
    class Program {
        +Main(args: string[]) void
    }
    class Shell {
        -invoker: CommandInvoker
        -console: IConsoleService
        -fileSystem: IFileSystemService
        +Run() void
        -DisplayPrompt() void
    }
    class CommandInvoker {
        -parser: Parser
        +ExecuteCommand(input: string) CommandResult
    }
    class Parser {
        +Parse(input: string) CommandContext
    }
    class CommandContext {
        +CommandName: string
        +Arguments: IReadOnlyList~string~
        +Switches: IReadOnlyList~string~
        +RawInput: string
    }
    class CommandResult {
        +Success: bool
        +OutputMessage: string
        +ShouldExit: bool
    }
    class CommandFactory {
        <<static>>
        +Create(name, fileService, consoleService) Command
        +GetCommands() IList~string~
    }
    class Command {
        <<abstract>>
        #FileService: IFileSystemService
        #ConsoleService: IConsoleService
        +Name: string
        +Description: string
        +Execute(context: CommandContext) CommandResult
    }
    class IConsoleService {
        <<interface>>
        +ReadLine() string
        +Write(text) void
        +WriteLine(text) void
        +Clear() void
        +SetColor(fg, bg) void
        +GetOSVersion() string
    }
    class IFileSystemService {
        <<interface>>
        +CurrentDirectory: string
        +GetDirectoryContents(path) IReadOnlyList~DirectoryEntry~
        +CreateDirectory(path) void
        +DeleteFile(path) void
        +ReadFileText(path) string
        +ChangeDirectory(path) void
        +MoveFileOrDirectory(source, destination) void
        +RenameFileOrDirectory(source, newName) void
    }

    Program ..> Shell : use
    Shell o-- CommandInvoker
    Shell --> IConsoleService
    Shell --> IFileSystemService
    CommandInvoker --> Parser
    Parser ..> CommandContext : erzeugt
    CommandInvoker ..> CommandFactory : use
    CommandFactory ..> Command : erzeugt
    Command ..> CommandResult : liefert
    Command --> IFileSystemService
    Command --> IConsoleService
    Command <|-- ExitCommand : extends
    Command <|-- DirCommand : extends
    Command <|-- CdCommand : extends
    Command <|-- MkDirCommand : extends
    Command <|-- UnknownCommand : extends
    IConsoleService <|.. ConsoleService : implements
    IFileSystemService <|.. FileSystemService : implements
```

Die übrigen Befehle (Type, More, Ren, Ver, Cls, Move, SetColor, Del) erben genauso von `Command`. Sie fehlen im Diagramm nur, damit es übersichtlich bleibt.

## Ablauf einer Eingabe

```mermaid
sequenceDiagram
    participant U as Benutzer
    participant S as Shell
    participant I as CommandInvoker
    participant P as Parser
    participant F as CommandFactory
    participant C as Command

    S->>U: Prompt "C:\Users\Daniel>"
    U->>S: "cd Projekte"
    S->>I: ExecuteCommand("cd Projekte")
    I->>P: Parse(...)
    P-->>I: CommandContext
    I->>F: Create("cd", ...)
    F-->>I: CdCommand
    I->>C: Execute(context)
    C-->>I: CommandResult
    I-->>S: CommandResult
    S->>U: Ausgabe (falls vorhanden)
    Note over S: ShouldExit? → beenden, sonst neuer Prompt
```

## Rollen aus den Patterns

| Pattern | Rolle | Klasse |
|---|---|---|
| Command | Invoker | `CommandInvoker` |
| Command | Command (abstrakt) | `Command` |
| Command | ConcreteCommand | `DirCommand`, `CdCommand`, … |
| Command | Receiver | `FileSystemService`, `ConsoleService` |
| Simple Factory | Factory | `CommandFactory` |
| Simple Factory | Product | `Command` |

## Entscheide (Abweichungen von der Musterlösung)

| # | Musterlösung | Wir | Warum |
|---|---|---|---|
| 1 | `Execute(string param): void` | `Execute(CommandContext): CommandResult` | Ohne Rückgabe weiss die Shell nicht, wann EXIT kommt. Ausserdem gehen Switches und mehrere Argumente in einem String verloren. |
| 2 | keine Services | `IFileSystemService`, `IConsoleService` | DoD: `Console`/`File` nur in den Services. So sind Unit Tests mit Fakes möglich (Dependency Inversion). |
| 3 | `static Create(string)` | `static Create(name, fileService, consoleService)` | Die Commands brauchen die Services. Die Factory reicht sie weiter. |
| 4 | Parser gibt `string` zurück | Parser gibt `CommandContext` zurück | Befehl, Switches und Argumente sind getrennt (siehe Auftrag 2). |
| 5 | – | `UnknownCommand` | Bei unbekannten Befehlen muss niemand auf `null` prüfen. |
| 6 | `ICommand` + `AbstractCommand` (unser V1) | nur `abstract class Command` | wie «Command Pattern mit abstrakter Klasse» im Unterricht |

## Fehler in der Musterlösung (zur Diskussion)

- `ExitCommand.Execute(): void` hat keinen Parameter, die Oberklasse verlangt aber `Execute(string param)`. Das ist kein gültiger Override und verstösst gegen Liskov.
- Wie EXIT die Shell beendet, ist nicht modelliert.
- `ParseCommand(String): Sring` hat einen Tippfehler.

## Miro

- Klassendiagramm: https://miro.com/app/board/uXjVHwm6kzI=/?moveToWidget=3458764683570620898
- Ablaufdiagramm: https://miro.com/app/board/uXjVHwm6kzI=/?moveToWidget=3458764681694670076
