# 08 – Aufbau einer Windows-Befehlszeile (Auftrag 2)

## Elemente einer Eingabezeile

```
 move   /y   C:\Daten\*.txt   "C:\Mein Archiv"
 └─┬┘   └┬┘  └──────┬──────┘  └──────┬───────┘
Befehl Switch   Argument 1       Argument 2
(Name) (Option) <source>         <target>
```

| Element | Erkennung | Beispiele |
|---|---|---|
| **Befehl** | erstes Wort, Gross-/Kleinschreibung egal | `dir`, `CD`, `Move` |
| **Switch / Option** | beginnt mit `/` (teilweise `-`), evtl. mit Wert | `/y`, `/s`, `/a:d`, `/-y`, `/?` |
| **Argument / Parameter** | alles andere, die Reihenfolge zählt | `Projekte`, `a.txt` |
| **Pfad** (Art von Argument) | absolut oder relativ | `C:\Users`, `..`, `.\docs` |
| **Platzhalter** | `*` beliebig viele Zeichen, `?` genau ein Zeichen | `*.txt`, `bild?.png` |
| **Anführungszeichen** | fassen ein Argument mit Leerzeichen zusammen | `cd "Meine Dateien"` |

## Notation in der Microsoft-Referenz

| Schreibweise | Bedeutung | Beispiel |
|---|---|---|
| Text ohne Klammern | genau so eintippen | `move` |
| `<Text>` | Platzhalter, Wert einsetzen | `<source>` |
| `[Text]` | optional | `[/y]` |
| `{a \| b}` | Pflicht, **genau eine** Variante wählen | `{/y \| -y}` |
| `…` | darf mehrfach vorkommen | `<file> …` |

**Beispiel aus der Referenz:**
```
move [{/y|-y}] [<source>] [<target>]
```

## Syntax unserer Befehle (vereinfacht)

| Befehl | Syntax | Story |
|---|---|---|
| EXIT | `exit` | SCRUM-7 |
| DIR | `dir` | SCRUM-8 |
| MKDIR | `mkdir <ordner>` (auch `md`) | SCRUM-9 |
| TYPE | `type <datei>` | SCRUM-10 |
| CD | `cd [<pfad>]` | SCRUM-11 |
| MORE | `more <datei>` | SCRUM-12 |
| REN | `ren <quelle> <neuer_name>` | SCRUM-13 |
| VER | `ver` | SCRUM-14 |
| CLS | `cls` | SCRUM-16 |
| MOVE | `move [{/y\|-y}] <source> <target>` | SCRUM-17 |
| COLOR | `color <xy>` (x = Hintergrund, y = Text) | SCRUM-18 |
| DEL | `del <datei>` | SCRUM-19 |

## Was heisst das für den Parser?

1. **Nicht** einfach `Split(' ')` verwenden, weil Anführungszeichen beachtet werden müssen.
2. Das erste Token ist der **Befehl**.
3. Tokens mit `/` am Anfang sind **Switches**, alle anderen sind **Argumente**.
4. Das Ergebnis ist ein `CommandContext` mit `CommandName`, `Arguments`, `Switches` und `RawInput`.

## Links

- Windows Commands (Übersicht): https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/windows-commands
- Syntax-Notation: https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/command-line-syntax-key
- Referenz als PDF: https://download.microsoft.com/download/5/8/9/58911986-D4AD-4695-BF63-F734CD4DF8F2/ws-commands.pdf
