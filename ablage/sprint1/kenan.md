# Kenan – Sprint 1

Rolle: Product Owner (fertige Stories ausprobieren und abnehmen, Sprint Review präsentieren) · Review-Partner: **Sami**

## 1. Grundgerüst: CommandContext, CommandResult, Parser

Warten, bis Daniel «Solution ist drin» meldet, dann:

```
git checkout main
git pull
git checkout -b feature/SCRUM-20-core
```

| Datei | Inhalt |
|---|---|
| `src/CommandShell/Core/CommandContext.cs` | siehe schnittstellen.md |
| `src/CommandShell/Core/CommandResult.cs` | siehe schnittstellen.md |
| `src/CommandShell/Core/Parser.cs` | siehe schnittstellen.md |
| `tests/CommandShell.Tests/ParserTests.cs` | siehe unten |

**Bitte zuerst CommandContext und CommandResult** fertig machen und pushen (Pull Request). Samuel und Daniel brauchen sie. Danach den Parser.

Testfälle für den Parser:
- `dir` → Name `dir`, keine Argumente, keine Switches
- `move /y C:\Daten\a.txt C:\Archiv` → Switch `/y`, zwei Argumente
- `cd "Meine Dateien"` → ein Argument `Meine Dateien`
- `   cd    ..   ` → Name `cd`, Argument `..`
- leere Eingabe → Name ist `""`

Hintergrund zum Parser: `ablage/theorie/08-windows-befehlssyntax.md`

### Prompt für deine KI

```
Ich arbeite im Team an einer Windows Command Shell in C# (.NET 10, Konsolenanwendung,
xUnit-Tests). Architektur: Command Pattern + Simple Factory. Im Anhang ist
schnittstellen.md, das ist verbindlich: Namen, Namespaces und Signaturen genau so übernehmen.

Meine Aufgabe sind nur diese Dateien:
- src/CommandShell/Core/CommandContext.cs
- src/CommandShell/Core/CommandResult.cs (privater Konstruktor, statische Methoden Ok, Error, Exit)
- src/CommandShell/Core/Parser.cs: zerlegt eine Eingabezeile. Erstes Wort = CommandName,
  Wörter mit / am Anfang = Switches, Rest = Arguments, Text in Anführungszeichen bleibt
  ein Argument, mehrere Leerzeichen werden ignoriert. Nicht einfach Split(' ') verwenden.
- tests/CommandShell.Tests/ParserTests.cs mit diesen Fällen: "dir", "move /y C:\Daten\a.txt
  C:\Archiv", "cd \"Meine Dateien\"", "   cd    ..   ", leere Eingabe.

Regeln: Einfacher Code auf Lehrlingsniveau, keine zusätzlichen Klassen oder Bibliotheken,
kurze Kommentare nur wo nötig. Tests nach dem AAA-Muster (Arrange, Act, Assert).
```

## 2. Stories

### SCRUM-14 VER (1 SP)
Branch: `feature/SCRUM-14-ver` · Neu: `Commands/VerCommand.cs` · Anpassen: `Factory/CommandFactory.cs`
- Given die Shell läuft, when ich `ver` eingebe, then erscheint die Versionsnummer des Betriebssystems, z. B. `Microsoft Windows [Version 10.0.26100]`.

### SCRUM-16 CLS (3 SP)
Branch: `feature/SCRUM-16-cls` · Neu: `Commands/ClsCommand.cs` · Anpassen: `Factory/CommandFactory.cs`
- Given in der Konsole steht Text, when ich `cls` eingebe, then ist die Konsole leer und oben steht nur noch der Prompt.

### Prompt für deine KI (Stories)

```
Gleiches Projekt wie vorher (schnittstellen.md im Anhang). Jetzt die Story <SCRUM-Nr>:
<Story-Text und Akzeptanzkriterien hier einfügen>
Neue Klasse in src/CommandShell/Commands/, erbt von Command, Konstruktor wie in
schnittstellen.md. In CommandFactory nur eine Zeile ins Dictionary eintragen.
Dazu Tests in tests/CommandShell.Tests/<Name>Tests.cs: ein Normalfall, ein Fehlerfall,
mit den Fakes. Nichts anderes ändern.
```

## 3. Als Product Owner

Wenn ein Pull Request gemergt ist: Shell starten, die Akzeptanzkriterien der Story von Hand durchspielen, dann die Karte in Jira auf **Done** schieben. Wenn etwas nicht stimmt, zurück an die Person.
