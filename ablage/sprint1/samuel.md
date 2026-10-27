# Samuel – Sprint 1

Review-Partner: **Daniel**

## 1. Grundgerüst: Command, UnknownCommand, CommandFactory

Warten, bis Daniel «Solution ist drin» meldet, dann:

```
git checkout main
git pull
git checkout -b feature/SCRUM-20-commands
```

| Datei | Inhalt |
|---|---|
| `src/CommandShell/Commands/Command.cs` | abstrakte Klasse, siehe schnittstellen.md |
| `src/CommandShell/Commands/UnknownCommand.cs` | Fehlermeldung bei unbekanntem Befehl |
| `src/CommandShell/Factory/CommandFactory.cs` | static, Dictionary, `Create` und `GetCommands` |
| `tests/CommandShell.Tests/CommandFactoryTests.cs` | unbekannter Name → `UnknownCommand` |

Du brauchst `CommandContext`, `CommandResult` (Kenan) und die Interfaces der Services (Sami). Schreib deinen Code schon nach `schnittstellen.md`. Sobald die beiden gemergt sind: `git merge main` in deinen Branch, dann baut es.

Hintergrund: `ablage/theorie/06-design-patterns.md` (Command Pattern, Simple Factory)

### Prompt für deine KI

```
Ich arbeite im Team an einer Windows Command Shell in C# (.NET 10, Konsolenanwendung,
xUnit-Tests). Architektur: Command Pattern (mit abstrakter Klasse) + Simple Factory.
Im Anhang ist schnittstellen.md, das ist verbindlich: Namen, Namespaces und Signaturen
genau so übernehmen.

Meine Aufgabe sind nur diese Dateien:
- src/CommandShell/Commands/Command.cs (abstrakte Oberklasse, geschützte Properties
  FileService und ConsoleService, abstrakt: Name, Description, Execute)
- src/CommandShell/Commands/UnknownCommand.cs
- src/CommandShell/Factory/CommandFactory.cs: static class mit einem Dictionary
  <string, Func<IFileSystemService, IConsoleService, Command>> und
  StringComparer.OrdinalIgnoreCase. Create gibt bei unbekanntem Namen ein UnknownCommand
  zurück. Kein switch, kein if/else über Namen. Das Dictionary ist vorerst leer, mit
  einem Kommentar, wie man einen Befehl einträgt.
- tests/CommandShell.Tests/CommandFactoryTests.cs

CommandContext, CommandResult, IFileSystemService, IConsoleService und die Fakes
schreiben meine Teamkollegen. Nicht neu erfinden, nur verwenden.

Regeln: Einfacher Code auf Lehrlingsniveau, keine zusätzlichen Klassen oder Bibliotheken,
kurze Kommentare nur wo nötig. Tests nach dem AAA-Muster.
```

## 2. Stories

### SCRUM-11 CD (2 SP)
Branch: `feature/SCRUM-11-cd` · Neu: `Commands/CdCommand.cs` · Anpassen: `Factory/CommandFactory.cs`
- Given ich bin in `C:\Temp`, when ich `cd Projekte` eingebe, then bin ich in `C:\Temp\Projekte`.
- Given ich bin in `C:\Temp\Projekte`, when ich `cd ..` eingebe, then bin ich wieder in `C:\Temp`.
- Given ich bin irgendwo, when ich `cd C:\Users` eingebe, then bin ich in `C:\Users`.
- Given der Ordner existiert nicht, when ich `cd Gibtsnicht` eingebe, then erscheint eine Fehlermeldung und das Verzeichnis bleibt gleich.
- Given ich gebe nur `cd` ein, then wird das aktuelle Verzeichnis angezeigt.

Bitte **CD zuerst** machen. Daniel braucht es für den Prompt-Test.

### SCRUM-8 DIR (5 SP)
Branch: `feature/SCRUM-8-dir` · Neu: `Commands/DirCommand.cs` · Anpassen: `Factory/CommandFactory.cs`
- Given im aktuellen Verzeichnis gibt es Dateien und Ordner, when ich `dir` eingebe, then werden alle aufgelistet und Ordner sind mit `<DIR>` markiert.
- Given die Liste wird angezeigt, then steht am Ende die Anzahl Dateien und Ordner.
- Given das Verzeichnis ist leer, when ich `dir` eingebe, then erscheint eine Meldung, dass keine Dateien gefunden wurden.

### Prompt für deine KI (Stories)

```
Gleiches Projekt wie vorher (schnittstellen.md im Anhang). Jetzt die Story <SCRUM-Nr>:
<Story-Text und Akzeptanzkriterien hier einfügen>
Neue Klasse in src/CommandShell/Commands/, erbt von Command, Konstruktor wie in
schnittstellen.md. Dateisystem nur über FileService (IFileSystemService), Fehler kommen
als Exception und werden zu CommandResult.Error. In CommandFactory nur eine Zeile ins
Dictionary eintragen. Dazu Tests in tests/CommandShell.Tests/<Name>Tests.cs: ein
Normalfall, ein Fehlerfall, mit FakeFileSystemService (AddDirectory, AddFile).
Nichts anderes ändern.
```
