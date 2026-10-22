# 05 – SOLID-Prinzipien

| | Prinzip | Kurz |
|---|---|---|
| **S** | Single Responsibility | Eine Klasse oder Methode hat **genau eine** Aufgabe. |
| **O** | Open-Closed | **Offen für Erweiterung, geschlossen für Änderung**: Neues kommt dazu, ohne bestehenden Code zu ändern. |
| **L** | Liskov Substitution | Eine **Unterklasse muss überall** dort einsetzbar sein, wo die Oberklasse erwartet wird. |
| **I** | Interface Segregation | Lieber **mehrere kleine**, passende Interfaces als ein grosses Universal-Interface. |
| **D** | Dependency Inversion | Abhängigkeiten über **Interfaces** entkoppeln, statt direkt von konkreten Klassen abzuhängen. |

Präsentationen der Teams im Unterricht: O = Team A (wir), L = Team B, I = Team C, D = Team D

---

## S – Single Responsibility

**Schlecht:** Eine Klasse liest die Eingabe, zerlegt sie, führt den Befehl aus und schreibt in die Konsole.

**Gut (Command Shell):**

| Klasse | einzige Aufgabe |
|---|---|
| `Parser` | Eingabezeile zerlegen |
| `CommandFactory` | passendes Command erzeugen |
| `DirCommand` | Verzeichnisinhalt aufbereiten |
| `ConsoleService` | mit der Konsole reden |

## O – Open-Closed

Für einen neuen Befehl **schreibt man eine neue Klasse**. Die Shell, der Invoker und die anderen Commands bleiben unverändert.

```csharp
// Schlecht: Für jeden neuen Befehl muss diese Methode geändert werden
switch (name)
{
    case "dir": ShowDir(); break;
    case "cd":  ChangeDir(); break;
    // neuer Befehl → hier ändern
}

// Gut: neuer Befehl = neue Klasse, die von Command erbt
public class VerCommand : Command
{
    public override CommandResult Execute(CommandContext context) { ... }
}
```

Bei der Simple Factory muss man nur eine Zeile ergänzen, die Logik bleibt geschlossen.

## L – Liskov Substitution

Der Invoker ruft `command.Execute(context)` auf und **weiss nicht**, ob es ein `DirCommand` oder ein `CdCommand` ist. Jede Unterklasse muss sich dabei so verhalten, wie `Command` es verspricht.

```csharp
Command command = CommandFactory.Create(name, ...);  // irgendein Command
CommandResult result = command.Execute(context);      // funktioniert immer
```

**Verstoss:** Eine Unterklasse überschreibt `Execute` mit einer anderen Signatur oder wirft eine `NotImplementedException`.

## I – Interface Segregation

```
Schlecht:  IShellService  (ReadLine, WriteLine, DeleteFile, ChangeDirectory, GetOSVersion …)

Gut:       IConsoleService     → ReadLine, WriteLine, Clear, SetColor
           IFileSystemService  → CurrentDirectory, ChangeDirectory, DeleteFile …
```

Ein Command, das nur die Konsole braucht, hängt dann nicht vom Dateisystem ab.

## D – Dependency Inversion

Die Commands hängen von **Interfaces** ab und nicht von `Console` oder `File` direkt.

```
DirCommand ──► IFileSystemService ◄── FileSystemService      (echt)
                                  ◄── FakeFileSystemService  (im Test)
```

**Vorteil:** Im Unit Test setzt man einen Fake ein. Die Tests laufen dann ohne echte Dateien und immer gleich.

Quelle: Unterricht M426, T4 «03 SOLID Prinzipien»
