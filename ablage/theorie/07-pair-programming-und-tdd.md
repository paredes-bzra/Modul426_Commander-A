# 07 – Pair Programming und Test Driven Development

## Pair Programming

```
      ┌──────────┐
      │ Monitor  │
      └──────────┘
   Pilot      Navigator
 (schreibt)  (prüft, denkt mit)
     ⇄ alle 10–15 Min. tauschen
```

- **Pilot:** schreibt den Code
- **Navigator:** prüft, ob der Code und der Lösungsansatz korrekt sind, und denkt über Verbesserungen am Design nach
- Beide sind **gleichberechtigt** und tauschen alle 10–15 Minuten die Rollen. So lernt jeder alle Teile des Projekts kennen.

**Einstellung:** kompromissbereit und kritikfähig sein, keine Scheu haben (niemand ist perfekt), dazulernen.

**Vorteile:**
- bessere Qualität
- Fehler werden früh erkannt, deshalb wenig Korrekturaufwand
- das Wissen verbreitet sich im Team
- bessere Kommunikation
- mehr Spass

## Test Driven Development (TDD)

**Idee:** **Zuerst den Test** schreiben, **dann** den Code, der den Test erfüllt. So ist sichergestellt, dass der Code getestet ist.

```
┌─► 1. Unit Test schreiben
│        │
│   2. Test ausführen ──── erfolgreich? ──► zurück zu 1
│        │ fehlerhaft (rot)
│   3. Code schreiben / anpassen (Refactoring)
│        │
│   4. Test ausführen ──── fehlerhaft? ──► zurück zu 3
│        │ erfolgreich (grün)
└────────┘ wiederholen
```

Kurz gesagt: **Rot → Grün → Refactor**

## Unit Tests schreiben

### AAA-Methode

| Schritt | Was | Beispiel (xUnit, C#) |
|---|---|---|
| **Arrange** | Objekte erzeugen, Werte setzen | `var fs = new FakeFileSystemService();` |
| **Act** | zu testende Methode aufrufen | `var result = command.Execute(context);` |
| **Assert** | Ergebnis prüfen | `Assert.True(result.Success);` |

```csharp
[Fact]
public void MotorSize500_CalculateTax_ExpectTaxEqual69()
{
    // Arrange
    double motorSize = 500;
    double expected = 69;

    // Act
    double actual = VehicleTaxCalculator.GetTaxForMotorSize(motorSize);

    // Assert
    Assert.Equal(expected, actual);
}
```

### Testnamen nach Given – When – Then

| | Beispiel |
|---|---|
| Given | Motor mit 500 ccm |
| When | die Verkehrssteuer wird berechnet |
| Then | das Resultat ist CHF 69.– |

Schema: `Given_When_Then()`
- lang: `Given_MotorSize500_When_CalculateTaxByMotorSize_Then_ExpectTaxEqual_69()`
- kurz: `MotorSize500_CalculateTaxByMotorSize_ExpectTaxEqual_69()`

Für unser Projekt z. B.: `CdIntoMissingFolder_Execute_ReturnsError()`

### JUnit (Java) und xUnit (C#)

| | JUnit 5 (Unterricht, IntelliJ) | xUnit (unser Projekt, Rider) |
|---|---|---|
| Testmethode | `@Test` | `[Fact]` |
| mit Parametern | `@ParameterizedTest` | `[Theory]` + `[InlineData(...)]` |
| Vergleich | `assertEquals(exp, act)` | `Assert.Equal(exp, act)` |
| Wahr / Falsch | `assertTrue(...)` | `Assert.True(...)` |

## Übung Ü6 – Verkehrssteuer Kanton Zürich

Ziel der Übung: TDD und Pair Programming ausprobieren. Zuerst ein Test für die kleinste Kategorie, dann die Logik, dann die nächste Kategorie usw.

- Hubraum: bis 1'200 cm³ → CHF 69.–, 1'201–1'400 → CHF 88.–, … bis 10'001–11'000 → CHF 2'758.–, danach + CHF 300.– pro weitere angefangene 1'000 cm³
- Gewicht: bis 1'200 kg → CHF 50.–, … bis 3'201–3'500 kg → CHF 930.–, danach + CHF 260.– pro weitere angefangene 500 kg
- Refactoring-Teil: Die Berechnung soll **ohne** `if-else-if` / `switch` auskommen, z. B. mit einer Tabelle oder einem Dictionary.

Quelle: Unterricht M426, Folien 12 «Pair Programming», 13 «TDD», Übung M426-EX06
