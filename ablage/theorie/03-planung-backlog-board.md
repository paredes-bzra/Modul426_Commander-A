# 03 – Agile Planung, Backlog und Project Board

## Backlogs

| Backlog | Inhalt | Wer pflegt es |
|---|---|---|
| **Product Backlog** | alle Anforderungen und Wünsche für das ganze Produkt, nach Wichtigkeit sortiert | Product Owner |
| **Sprint Backlog** | eine kleine, feste Auswahl aus dem Product Backlog für **einen** Sprint | Team |

## Planning Poker

**Zweck:** Backlog Items schätzen. Mehrere Personen schätzen unabhängig voneinander. Geschätzt wird **relativ**, also im Vergleich zu anderen Items und nicht in Stunden.

**Karten:**
- Fibonacci: `0, ½, 1, 2, 3, 5, 8, 13, 21, 34, 55, ?`
- vereinfacht (oft verwendet): `1, 2, 3, 5, 8, 13, 20, 40, 100, ?`
- T-Shirt-Grössen: `S, M, L, XL, XXL` (wenn Zahlen zu schwierig sind)
- `?` bedeutet: Das Item kann nicht geschätzt werden

Fibonacci wird verwendet, weil die Unsicherheit bei grösseren Aufgaben wächst.

**Ablauf** (PO und Entwicklungsteam):
1. User Story und Akzeptanzkriterien verstehen
2. Jeder schätzt für sich (ca. 20 Sek.)
3. Alle decken **gleichzeitig** auf
4. Die Personen mit der höchsten und der tiefsten Schätzung erklären ihre Begründung
5. Bei Bedarf wiederholen, bis ein Konsens erreicht ist
6. Die Zahl nehmen, die am häufigsten gezeigt wurde
7. Weiter zur nächsten Story

## Release- und Sprintplanung

```
Release Planung        ............ Rel. 0.1 ................ Rel. 0.2
Sprint Planning   [1]       [2]        [3]        [4]        [5]
               Vorbereitung │ Sprint 1 │ Sprint 2 │ Sprint 3 │ Sprint 4 │
                               ▼ Increment (time-boxed)
```

**Resultate des Sprint Planning Meetings:**
- Die Sprint-Kapazität ist berechnet
- Die Sprintziele sind definiert
- Das Sprint Backlog ist priorisiert, geschätzt und gefüllt
- Die Anforderungen sind klar und die **Akzeptanzkriterien definiert**
- Ein Burndown- oder Burnup-Chart ist aufgesetzt

**Kapazität berechnen:** Man rechnet die verfügbaren Personentage aller Teammitglieder zusammen und zieht Ferien und Abwesenheiten ab. Wie viele Story Points ins Sprint Backlog passen, schätzt man aus den **früheren Sprints** (Velocity). Ein Beispiel: Die letzten Sprints ergaben 28, 24 und 25 SP, also plant man für den nächsten Sprint ca. 24 SP.

## Project Board

Ein Board **visualisiert** die geplanten Arbeitspakete (Stories, Tasks), deren Priorität, wer zuständig ist, den Fortschritt (Work in Progress), was erledigt ist und aktuelle Probleme. So ist die Planung **transparent und für alle zugänglich**.

- **Physisches Board:** fördert die Interaktion im Team
- **Digitales Board** (z. B. Jira): ideal für verteilte Teams, braucht aber einen grossen Bildschirm oder Beamer für das Daily

### Scrum Board

```
┌──────────┬─────────────┬─────────────┬──────────┐
│ Stories  │ To Do       │ In Progress │ Done     │
├──────────┼─────────────┼─────────────┼──────────┤
│ Story #1 │ Task A, B   │ Task C      │          │  ← eine Zeile pro Story
│ Story #5 │ Task C      │ Task D      │ Task A,B │
└──────────┴─────────────┴─────────────┴──────────┘
```

### Kanban Board (Alternative)

Das Wort kommt aus der japanischen Autoindustrie: Kan = visualisieren, Ban = Karte.
Die Spalten sind z. B. **Backlog → In Progress → In Test → Done** plus **Blocked**. Für dringende Fehler aus der Produktion gibt es zusätzlich eine **Fast Lane**.

### Board-Karte

```
┌──────────────────────────────────────────┐
│ ① CR-0012  ② Produkt Bilder anzeigen  ⑤ Marco │
│ ③ Beschreibung: Als Kunde möchte ich …    │
│ ④ Akzeptanzkriterien: - … - … - …         │
└──────────────────────────────────────────┘
① ID  ② Titel  ③ Kurzbeschreibung  ④ Akzeptanzkriterien  ⑤ Verantwortlich
```

Quelle: Unterricht M426, Folien 03, 04, 05
