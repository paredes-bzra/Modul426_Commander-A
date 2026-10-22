# Team und Tools

## Rollen

| Rolle | Person |
|---|---|
| Scrum Master | Daniel |
| Product Owner | Kenan |
| Entwicklungsteam | Daniel, Kenan, Samuel, Sami |
| Stakeholder | Lehrperson |

## Tools

| Bereich | Tool |
|---|---|
| Sprache | C# / .NET 10 (Konsolen-App) |
| IDE | JetBrains Rider |
| Tests | xUnit |
| Code | GitHub (dieses Repo) |
| Backlog und Sprints | Jira, Projekt SCRUM |
| Architektur | Miro-Board «Modul 426» |

## So arbeiten wir parallel

```
main ──●────────────●──────────●──────────►
        \          /  \        /
         feature/SCRUM-11-cd    feature/SCRUM-8-dir
```

1. Story in Jira auf «In Arbeit» setzen
2. Branch erstellen: `feature/SCRUM-<Nr>-<befehl>`
3. Klasse + Tests schreiben (siehe Abschnitt «Umsetzung» in der Story)
4. Pull Request erstellen, Karte auf «Review» schieben
5. Ein anderes Teammitglied macht das Review und gibt frei
6. Merge in `main`, PO testet, Karte auf «Done»

`main` ist geschützt: Änderungen kommen nur per Pull Request mit 1 Review hinein.
