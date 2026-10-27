# Sami – Sprint 1

Review-Partner: **Kenan**

## 1. Grundgerüst: Services und Fakes

Warten, bis Daniel «Solution ist drin» meldet, dann:

```
git checkout main
git pull
git checkout -b feature/SCRUM-20-services
```

| Datei | Inhalt |
|---|---|
| `src/CommandShell/Services/IConsoleService.cs` | Interface, siehe schnittstellen.md |
| `src/CommandShell/Services/ConsoleService.cs` | der **einzige** Ort mit `Console.…` |
| `src/CommandShell/Services/IFileSystemService.cs` | Interface |
| `src/CommandShell/Services/FileSystemService.cs` | der **einzige** Ort mit `File.…` und `Directory.…` |
| `src/CommandShell/Services/DirectoryEntry.cs` | record |
| `tests/CommandShell.Tests/Fakes/FakeConsoleService.cs` | Konsole im Speicher für Tests |
| `tests/CommandShell.Tests/Fakes/FakeFileSystemService.cs` | Dateisystem im Speicher für Tests |

**Bitte zuerst die drei Interface-Dateien + DirectoryEntry** pushen (Pull Request). Samuel und Daniel brauchen sie. Danach die Implementierungen und die Fakes.

Deine Fakes nutzen später **alle** für ihre Tests. Sie müssen sich genau wie der echte Service verhalten, also auch dieselben Exceptions werfen.

### Prompt für deine KI

```
Ich arbeite im Team an einer Windows Command Shell in C# (.NET 10, Konsolenanwendung,
xUnit-Tests). Im Anhang ist schnittstellen.md, das ist verbindlich: Namen, Namespaces
und Signaturen genau so übernehmen.

Meine Aufgabe sind nur diese Dateien:
- src/CommandShell/Services/IConsoleService.cs und ConsoleService.cs
  (einziger Ort, wo System.Console verwendet wird; GetOSVersion gibt
  "Microsoft Windows [Version x.y.z]" mit Environment.OSVersion.Version zurück)
- src/CommandShell/Services/IFileSystemService.cs und FileSystemService.cs
  (einziger Ort mit File und Directory. Merkt sich CurrentDirectory selbst, Start mit
  Directory.GetCurrentDirectory(). Relative Pfade mit Path.GetFullPath(path, CurrentDirectory)
  auflösen. Fehler als Exception: DirectoryNotFoundException, FileNotFoundException,
  IOException wenn ein Ordner schon existiert. CreateDirectory erstellt Zwischenordner.
  MoveFileOrDirectory: ist das Ziel ein Ordner, behält die Datei ihren Namen.)
- src/CommandShell/Services/DirectoryEntry.cs (record)
- tests/CommandShell.Tests/Fakes/FakeConsoleService.cs: Konstruktor mit params string[]
  inputs, ReadLine gibt diese der Reihe nach zurück, danach null. Output-Liste,
  WasCleared, Foreground, Background, OSVersion (setzbar).
- tests/CommandShell.Tests/Fakes/FakeFileSystemService.cs: alles im Speicher
  (HashSet für Ordner, Dictionary für Dateien, Gross-/Kleinschreibung egal).
  Start: Ordner C:\, C:\Users, C:\Users\Test, CurrentDirectory = C:\Users\Test.
  Hilfsmethoden AddDirectory(path) und AddFile(path, content). Gleiche Exceptions
  wie der echte Service.

Regeln: Einfacher Code auf Lehrlingsniveau, keine zusätzlichen Klassen oder Bibliotheken,
kurze Kommentare nur wo nötig.
```

## 2. Story

### SCRUM-9 MKDIR (8 SP)
Branch: `feature/SCRUM-9-mkdir` · Neu: `Commands/MkDirCommand.cs` · Anpassen: `Factory/CommandFactory.cs` (`mkdir` und `md`)
- Given es gibt noch keinen Ordner `Test`, when ich `mkdir Test` eingebe, then wird der Ordner erstellt und `dir` zeigt ihn an.
- Given es gibt den Ordner `Projekte` nicht, when ich `mkdir Projekte\2026` eingebe, then werden beide Ordner erstellt.
- Given der Ordner `Test` existiert schon, when ich `mkdir Test` eingebe, then erscheint eine Fehlermeldung.
- Given ich gebe `mkdir` ohne Namen ein, then erscheint ein Hinweis, wie der Befehl verwendet wird.

### Prompt für deine KI (Story)

```
Gleiches Projekt wie vorher (schnittstellen.md im Anhang). Jetzt die Story SCRUM-9 MKDIR:
<Story-Text und Akzeptanzkriterien hier einfügen>
Neue Klasse src/CommandShell/Commands/MkDirCommand.cs, erbt von Command, Konstruktor wie
in schnittstellen.md. Ordner nur über FileService.CreateDirectory erstellen, IOException
wird zu CommandResult.Error. In CommandFactory zwei Zeilen eintragen: "mkdir" und "md".
Dazu Tests in tests/CommandShell.Tests/MkDirCommandTests.cs mit FakeFileSystemService:
Normalfall, Ordner existiert schon, kein Argument. Nichts anderes ändern.
```
