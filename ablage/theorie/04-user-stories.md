# 04 – User Stories

## Format

> **Als** [Rolle] **möchte ich** [Ziel/Funktion], **damit** [Nutzen].

| Teil | Englisch | Frage |
|---|---|---|
| Rolle | As a … *Person / Role* | Wer braucht es? |
| Ziel | I want … *Goal* | Was möchte die Person tun? |
| Nutzen | So that … *Business Value* | Warum ist es wichtig? |

Danach folgen die **Akzeptanzkriterien**, also weitere Anforderungen.

## Akzeptanzkriterien

- Der Entwickler versteht besser, **was er umsetzen muss und was geprüft wird**
- Sie sind fürs **Testen** sehr wichtig
- Sie dienen der **Abnahme** der Umsetzung (Sprint Review)

### Given – When – Then

| | Bedeutung | Beispiel (CD) |
|---|---|---|
| **Given** | Ausgangslage | Ich bin in `C:\Temp` |
| **When** | Aktion | Ich gebe `cd Projekte` ein |
| **Then** | erwartetes Ergebnis | Ich bin in `C:\Temp\Projekte` |

## Gutes und schlechtes Beispiel

**Schlecht:** «Passwort zurücksetzen implementieren.»
- ❌ keine Rolle, kein Bedürfnis, kein Nutzen
- ❌ zu technisch: «implementieren» beschreibt das Wie
- ❌ keine Akzeptanzkriterien, Sonderfälle fehlen

**Gut:**
> Als registrierter Nutzer möchte ich mein vergessenes Passwort zurücksetzen können, damit ich wieder Zugriff auf mein Konto erhalte.
> - Ich kann auf «Passwort vergessen?» klicken
> - Bei einer gültigen E-Mail kommt ein Link
> - Der Link ist nur begrenzt gültig
> - Mit dem neuen Passwort kann ich mich anmelden

**Typischer Fehler:** Der Nutzen wiederholt nur das Ziel, z. B. «… möchte ich das Verzeichnis wechseln, *damit ich das Verzeichnis wechseln kann*». Ein echter Nutzen wäre: «… damit ich mich in der Ordnerstruktur bewegen kann».

## Checkliste

- [ ] **Rolle** definiert: Ist klar, wer die Funktion braucht?
- [ ] **Bedürfnis** beschrieben: Was möchte der Nutzer tun?
- [ ] **Nutzen** genannt: Warum möchte er das?
- [ ] **Verständlich**, ohne Fachjargon
- [ ] **Konkretes Ziel**
- [ ] **Nicht zu gross**, in kurzer Zeit umsetzbar
- [ ] **Unabhängig** von anderen Stories
- [ ] **Testbar**: Es ist eindeutig, wann die Story erfüllt ist
- [ ] **Akzeptanzkriterien** vorhanden
- [ ] **Keine technische Lösung** vorgegeben: Was und Warum, nicht Wie
- [ ] **Eindeutig**, nicht widersprüchlich
- [ ] **Mehrwert** für den Nutzer

## Stories organisieren

```
Epic ──┬── User Story ──┬── Task
       │                ├── Task
       │                └── Task
       └── User Story ──┬── Task
                        └── Task
```

- **Epics** gruppieren User Stories
- Zu grosse User Stories teilt man in **Tasks** auf

Quelle: Unterricht M426, Folien 06 «User Stories» und «Tipps für User Stories»
