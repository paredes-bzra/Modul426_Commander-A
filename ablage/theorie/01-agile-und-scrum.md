# 01 – Agile Entwicklung und Scrum

## Klassisch vs. agil

**Wasserfall:** Die Phasen laufen nacheinander ab (Anforderungen → Entwurf → Implementierung → Überprüfung → Wartung). Eine Phase startet erst, wenn die vorherige fertig ist. Spätere Änderungen sind teuer.

**Agil:** Die Software entsteht **iterativ und inkrementell**, also in kurzen Zyklen, die jeweils etwas Lauffähiges liefern. Anforderungen dürfen sich ändern, auch spät im Projekt.

| Kernpunkt | Bedeutung |
|---|---|
| Iterativ und inkrementell | kleine, lauffähige Versionen, die schrittweise wachsen |
| Kunden- und Nutzerfokus | Anforderungen gemeinsam mit dem Kunden, laufend angepasst |
| Kurze Zyklen (Sprints) | regelmässig neue Versionen zum Testen und Bewerten |
| Enge Zusammenarbeit | Entwickler, Auftraggeber und Nutzer arbeiten eng zusammen |
| Reagieren statt starr planen | Änderungen sind willkommen |

Beispiele für agile Methoden: **Scrum, Kanban, Extreme Programming (XP)**

## Agiles Manifest

| Wichtiger … | … als |
|---|---|
| **Individuen und Interaktionen** | Prozesse und Werkzeuge |
| **Funktionsfähige Software** | umfassende Dokumentation |
| **Zusammenarbeit mit dem Kunden** | Vertragsverhandlungen |
| **Auf Änderungen eingehen** | einen Plan strikt verfolgen |

Die Werte rechts sind nicht wertlos, die links sind aber wichtiger. Mehr dazu: https://agilemanifesto.org/iso/de/manifesto.html

## Scrum auf einen Blick

```
Product      Planning     Sprint        2–4 Wochen Sprint           Product
Backlog  ──► Meeting  ──► Backlog  ──►  ┌──── Daily Scrum ────┐ ──► Increment
(PO)                                    └──────────────────────┘        │
                                                                         ▼
                                                    Sprint Review ──► Retrospektive
```

- Agiler, iterativer Entwicklungsprozess
- Anforderungen werden als **User Stories** im **Product Backlog** verwaltet und priorisiert
- Ein Sprint dauert **2–4 Wochen** und wird mit dem **Sprint Backlog** geplant
- Drei Rollen: **Product Owner, Scrum Master, Entwicklungsteam**
- Das Team organisiert sich selbst
- Nach jedem Sprint wird ein **Product Increment** geliefert

## Scrum-Elemente und agile Praktiken

```
          Pair Programming · Refactoring · Burndowns · Informative Workspace
                                   ▼
User Stories ─► Planning ─► [ Sprint (time-boxed) mit Daily Scrum ] ─► Review
Planning Poker    Meeting                                          ─► Retrospektive
                                   ▲
          Test Driven Development · Release Planning · Continuous Integration
```

**Scrum** gibt den Rahmen vor (Sprint, Meetings, Rollen). Die **agilen Praktiken** wie TDD oder Pair Programming sind Techniken, die man in diesen Rahmen einbaut.

## Deming-Kreis (PDCA): kontinuierliche Verbesserung

| Schritt | Bedeutung | In Scrum |
|---|---|---|
| **Plan** | planen | Sprint Planning |
| **Do** | umsetzen | Sprint |
| **Check** | überprüfen | Sprint Review und Retrospektive |
| **Act** | nachbearbeiten und verbessern | Massnahmen für den nächsten Sprint |

## Sprintplanung über das ganze Projekt

Das Ziel verschiebt sich während des Projekts, weil sich Bedürfnisse und Anforderungen ändern und Feedback neue Erkenntnisse bringt. Deshalb fliessen Änderungen, Feedback und Fehlerkorrekturen jeweils in die **nächste** Sprintplanung ein. Am Ende steht so die **nützliche** Lösung und nicht die ursprünglich geplante.

Quelle: Unterricht M426, Folien 02 «SW Entwicklungsprozesse und Scrum»
