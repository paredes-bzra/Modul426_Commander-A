# Sprint 1 – wer macht was

Sprintziel: Lauffähige Shell mit Prompt, Navigation und ersten Befehlen
Sprint: 27.10. – 30.10.2026 · Jira: https://commander24a.atlassian.net/jira/software/projects/SCRUM/boards/1

| Person | Datei | Vormittag 27.10. (Grundgerüst SCRUM-20) | Danach (Stories) |
|---|---|---|---|
| Daniel (SM) | [daniel.md](daniel.md) | Solution anlegen, Program, Shell, CommandInvoker | SCRUM-15 Prompt, SCRUM-7 EXIT |
| Kenan (PO) | [kenan.md](kenan.md) | CommandContext, CommandResult, Parser | SCRUM-14 VER, SCRUM-16 CLS, Abnahme aller Stories |
| Samuel | [samuel.md](samuel.md) | Command, UnknownCommand, CommandFactory | SCRUM-11 CD, SCRUM-8 DIR |
| Sami | [sami.md](sami.md) | Services, DirectoryEntry, Fakes | SCRUM-9 MKDIR |

**Für alle gilt:** [schnittstellen.md](schnittstellen.md) – dort stehen alle Klassennamen und Methoden.

## Ablauf am 27.10.

```
08:00  Daily (Daniel)
08:15  Daniel: Solution anlegen und pushen  ──► alle: git pull
08:30  alle parallel am Grundgerüst, jeder auf eigenem Branch
       Reihenfolge der Merges:
         1. Kenan (Core-Daten + Parser)   ┐ zuerst, die anderen brauchen das
         2. Sami  (Services + Fakes)      ┘
         3. Samuel (Command + Factory)
         4. Daniel (Invoker + Shell)
~11:30 alles in main → gemeinsam: dotnet build, dotnet test, Shell starten, "abc" eingeben
13:00  Stories, jeder seine eigenen
```

## Reviews

```
Daniel ⇄ Samuel        Kenan ⇄ Sami
```

## So arbeitest du mit deiner KI

1. Öffne deine Datei (z. B. `samuel.md`)
2. Kopiere den Abschnitt **«Prompt für deine KI»** in deine KI
3. Gib ihr dazu `schnittstellen.md` (Datei anhängen oder Inhalt einfügen)
4. Prüfe den Code, bevor du committest: Stimmen Namen und Signaturen mit `schnittstellen.md`?
5. `dotnet build` und `dotnet test` müssen grün sein, dann Pull Request
