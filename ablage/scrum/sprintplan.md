# Sprintplan

Jira: [aktiver Sprint](https://commander24a.atlassian.net/jira/software/projects/SCRUM/boards/1) · [Backlog](https://commander24a.atlassian.net/jira/software/projects/SCRUM/boards/1/backlog)

## Termine

| Sprint | Termine | Lektionen | Review + Retro |
|---|---|---|---|
| 1 | 27.10.2026 (GT) · 30.10.2026 (NM) | 9 + 4 = 13 | 30.10.2026 |
| 2 | 03.11.2026 (GT) · 06.11.2026 (NM) | 9 + 4 = 13 | 06.11.2026 |
| 3 | 10.11.2026 (GT) | 9 | 10.11.2026 |

GT = ganzer Tag, NM = Nachmittag

## Kapazität Sprint 1

| | |
|---|---|
| 4 Personen × 13 Lektionen × 45 Min. | ≈ 39 h |
| minus Planning, Daily, Review, Retro (~20 %) | ≈ 31 h netto |
| Richtwert: 81 SP auf 3 Sprints | ≈ 27 SP pro Sprint |
| **Geplant Sprint 1** (wegen Grundgerüst etwas weniger) | **25 SP** |

## Sprint 1 – Backlog

**Sprintziel:** Lauffähige Shell mit Prompt, Navigation und ersten Befehlen

| # | Jira | Story | SP |
|---|---|---|---|
| 0 | SCRUM-20 | Technik: Grundgerüst (.NET 10) | – |
| 1 | SCRUM-7 | EXIT | 1 |
| 2 | SCRUM-15 | Prompt zeigt aktuelles Verzeichnis | 5 |
| 3 | SCRUM-11 | CD | 2 |
| 4 | SCRUM-8 | DIR | 5 |
| 5 | SCRUM-9 | MKDIR | 8 |
| 6 | SCRUM-14 | VER | 1 |
| 7 | SCRUM-16 | CLS | 3 |
| | | **Total** | **25** |

```
SCRUM-20 Grundgerüst ──┬──► EXIT, VER, CLS, MKDIR, DIR   (parallel)
   (zuerst mergen)     └──► CD ──► Prompt (letzter Test erst nach CD)
```

## Vorschlag Sprint 2 und 3

| Sprint | Stories | SP |
|---|---|---|
| 2 | TYPE 13, DEL 2, REN 5, SET COLOR 3 | 23 |
| 3 | MORE 13, MOVE 20 | 33 ⚠️ zu viel für 9 Lektionen |

→ Entweder MORE in Sprint 2 nehmen oder MOVE aufteilen (eine Datei / mehrere Dateien).
