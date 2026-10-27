# Daniel – Sprint 1

Rolle: Scrum Master (Daily leiten, Retro moderieren) · Review-Partner: **Samuel**

## 1. Solution anlegen (zuerst, ca. 15 Min.)

Im Terminal im Repo-Hauptordner:

```
git checkout main
git pull
dotnet new sln -n CommandShell --format sln
dotnet new console -n CommandShell -o src/CommandShell -f net10.0 --use-program-main
dotnet new xunit -n CommandShell.Tests -o tests/CommandShell.Tests -f net10.0
dotnet sln add src/CommandShell/CommandShell.csproj tests/CommandShell.Tests/CommandShell.Tests.csproj
dotnet add tests/CommandShell.Tests reference src/CommandShell
dotnet build
```

Danach:
- `tests/CommandShell.Tests/UnitTest1.cs` löschen
- In `src/CommandShell/CommandShell.csproj` prüfen: `<Nullable>enable</Nullable>`
- `.gitkeep`-Dateien in `src/` und `tests/` dürfen bleiben, bis echte Dateien drin sind
- committen und pushen: `git add . && git commit -m "SCRUM-20: Solution und Projekte angelegt" && git push`
- dem Team sagen: **«Solution ist drin, git pull»**

Danach `CommandShell.sln` in Rider öffnen.

## 2. Grundgerüst: Program, Shell, CommandInvoker

Branch: `feature/SCRUM-20-shell`

| Datei | Inhalt |
|---|---|
| `src/CommandShell/Program.cs` | Services, Parser, CommandInvoker und Shell erzeugen, `shell.Run()` |
| `src/CommandShell/Core/CommandInvoker.cs` | siehe schnittstellen.md |
| `src/CommandShell/Core/Shell.cs` | siehe schnittstellen.md, Prompt vorerst `"> "` |
| `tests/CommandShell.Tests/CommandInvokerTests.cs` | unbekannter Befehl → Error, leere Eingabe → Ok |
| `tests/CommandShell.Tests/ShellTests.cs` | `"abc"` dann Ende → Fehlermeldung ausgegeben |

Du brauchst die Klassen von Kenan, Sami und Samuel. Schreib deinen Code schon nach `schnittstellen.md`, gemergt wird bei dir **zuletzt**.

### Prompt für deine KI

```
Ich arbeite im Team an einer Windows Command Shell in C# (.NET 10, Konsolenanwendung,
xUnit-Tests). Architektur: Command Pattern + Simple Factory. Im Anhang ist
schnittstellen.md, das ist verbindlich: Namen, Namespaces und Signaturen genau so übernehmen.

Meine Aufgabe sind nur diese Dateien:
- src/CommandShell/Program.cs (Klasse Program mit static void Main, erzeugt ConsoleService,
  FileSystemService, Parser, CommandInvoker, Shell und ruft Run auf)
- src/CommandShell/Core/CommandInvoker.cs
- src/CommandShell/Core/Shell.cs (Prompt vorerst "> ", das aktuelle Verzeichnis kommt später)
- tests/CommandShell.Tests/CommandInvokerTests.cs und ShellTests.cs mit FakeConsoleService
  und FakeFileSystemService aus CommandShell.Tests.Fakes

Die anderen Klassen (CommandContext, CommandResult, Parser, Command, CommandFactory,
Services, Fakes) schreiben meine Teamkollegen. Nicht neu erfinden, nur so verwenden,
wie sie in schnittstellen.md stehen.

Regeln: Console nur über IConsoleService. Einfacher Code auf Lehrlingsniveau,
keine zusätzlichen Klassen oder Bibliotheken, kurze Kommentare nur wo nötig,
Meldungen auf Deutsch. Tests nach dem AAA-Muster (Arrange, Act, Assert).
```

## 3. Stories

### SCRUM-7 EXIT (1 SP)
Branch: `feature/SCRUM-7-exit` · Neu: `Commands/ExitCommand.cs` · Anpassen: `Factory/CommandFactory.cs`
- Given die Shell läuft, when ich `exit` eingebe, then wird die Shell beendet.
- Given die Shell läuft, when ich `EXIT` oder `Exit` eingebe, then wird sie ebenfalls beendet.

### SCRUM-15 Prompt (5 SP)
Branch: `feature/SCRUM-15-prompt` · Anpassen: `Core/Shell.cs` (Methode für den Prompt)
- Given die Shell startet, then zeigt der Prompt das Startverzeichnis, z. B. `C:\Users\Daniel>`.
- Given ich bin in `C:\Users\Daniel`, when ich `cd Projekte` eingebe, then zeigt der Prompt `C:\Users\Daniel\Projekte>`.
- Given ein Befehl schlägt fehl, then bleibt der Prompt unverändert.

Der Test mit `cd` geht erst, wenn Samuel CD gemergt hat.

### Prompt für deine KI (Stories)

```
Gleiches Projekt wie vorher (schnittstellen.md im Anhang). Jetzt die Story <SCRUM-Nr>:
<Story-Text und Akzeptanzkriterien hier einfügen>
Neue Klasse in src/CommandShell/Commands/, erbt von Command, Konstruktor wie in
schnittstellen.md. In CommandFactory nur eine Zeile ins Dictionary eintragen.
Dazu Tests in tests/CommandShell.Tests/<Name>Tests.cs: ein Normalfall, ein Fehlerfall,
mit den Fakes. Nichts anderes ändern.
```
