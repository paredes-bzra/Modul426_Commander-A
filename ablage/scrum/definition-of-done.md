# Definition of Done – Team Commander A

Eine Story ist bei uns fertig, wenn:

**Code**
- Der Befehl ist eine eigene Klasse, die von `Command` erbt, und ist in der `CommandFactory` eingetragen.
- `Console`, `File` und `Directory` werden nur im `ConsoleService` und im `FileSystemService` benutzt, sonst nirgends.

**Tests**
- Es gibt einen Test für den normalen Fall und einen für einen Fehlerfall, z. B. «Datei gibt es nicht».
- Alle Tests sind grün.

**Review**
- Der Pull Request ist offen, und die Karte liegt in der Spalte Review.
- Einer aus dem Team hat den Code angeschaut und den Pull Request freigegeben.

**Abschluss**
- Der Pull Request ist in `main` gemergt, und das Issue ist geschlossen.
- Die Akzeptanzkriterien der Story sind erfüllt, und der Product Owner hat es ausprobiert.
- Die Karte ist auf dem Board in der Spalte Done.

*Ausnahme:* SCRUM-15 (Prompt) ist kein Befehl und braucht deshalb keine eigene Klasse.
