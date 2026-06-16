using System;
using System.Collections.Generic;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("WZORZEC FLYWEIGHT - SYSTEM ADMINISTRACYJNY");
Console.WriteLine("------------------------------------------\n");

// Fabryka pilnuje, żeby statusy nie były tworzone wielokrotnie.
StatusInfoFactory statusFactory = new StatusInfoFactory();

List<AdministrativeCase> cases = new List<AdministrativeCase>
{
    new AdministrativeCase(
        "ZUS/2026/001",
        "Jan Kowalski",
        new DateTime(2026, 6, 1),
        statusFactory.GetStatus("NOWA")
    ),

    new AdministrativeCase(
        "ZUS/2026/002",
        "Anna Nowak",
        new DateTime(2026, 6, 2),
        statusFactory.GetStatus("NOWA")
    ),

    new AdministrativeCase(
        "ZUS/2026/003",
        "Piotr Zieliński",
        new DateTime(2026, 6, 3),
        statusFactory.GetStatus("W_TRAKCIE")
    ),

    new AdministrativeCase(
        "ZUS/2026/004",
        "Maria Wiśniewska",
        new DateTime(2026, 6, 4),
        statusFactory.GetStatus("W_TRAKCIE")
    ),

    new AdministrativeCase(
        "ZUS/2026/005",
        "Tomasz Wójcik",
        new DateTime(2026, 6, 5),
        statusFactory.GetStatus("ZAKONCZONA")
    ),

    new AdministrativeCase(
        "ZUS/2026/006",
        "Ewa Kamińska",
        new DateTime(2026, 6, 6),
        statusFactory.GetStatus("ODRZUCONA")
    )
};

foreach (AdministrativeCase administrativeCase in cases)
{
    administrativeCase.Print();
}

Console.WriteLine();
Console.WriteLine($"Liczba spraw w systemie: {cases.Count}");
Console.WriteLine($"Liczba utworzonych obiektów statusu: {statusFactory.CreatedStatusesCount}");


// =======================================================
// FLYWEIGHT - WSPÓŁDZIELONY STATUS SPRAWY
// =======================================================

public class StatusInfo
{
    public string Code { get; }

    public string Description { get; }

    public string Color { get; }

    public string UserMessage { get; }

    public int MaxProcessingDays { get; }

    public StatusInfo(
        string code,
        string description,
        string color,
        string userMessage,
        int maxProcessingDays)
    {
        Code = code;
        Description = description;
        Color = color;
        UserMessage = userMessage;
        MaxProcessingDays = maxProcessingDays;
    }

    public void PrintStatusInfo()
    {
        Console.WriteLine($"Status: {Code}");
        Console.WriteLine($"Opis: {Description}");
        Console.WriteLine($"Kolor w systemie: {Color}");
        Console.WriteLine($"Komunikat: {UserMessage}");
        Console.WriteLine($"Maksymalny czas obsługi: {MaxProcessingDays} dni");
    }
}


// =======================================================
// OBIEKT KONTEKSTOWY - KONKRETNA SPRAWA
// =======================================================

public class AdministrativeCase
{
    public string CaseNumber { get; }

    public string CitizenName { get; }

    public DateTime SubmissionDate { get; }

    public StatusInfo Status { get; }

    public AdministrativeCase(
        string caseNumber,
        string citizenName,
        DateTime submissionDate,
        StatusInfo status)
    {
        CaseNumber = caseNumber;
        CitizenName = citizenName;
        SubmissionDate = submissionDate;
        Status = status;
    }

    public void Print()
    {
        Console.WriteLine("SPRAWA ADMINISTRACYJNA");
        Console.WriteLine($"Numer sprawy: {CaseNumber}");
        Console.WriteLine($"Osoba: {CitizenName}");
        Console.WriteLine($"Data wpływu: {SubmissionDate:yyyy-MM-dd}");

        Console.WriteLine();
        Status.PrintStatusInfo();

        Console.WriteLine("------------------------------------------");
    }
}


// =======================================================
// FLYWEIGHT FACTORY - FABRYKA STATUSÓW
// =======================================================

public class StatusInfoFactory
{
    private readonly Dictionary<string, StatusInfo> _statuses =
        new Dictionary<string, StatusInfo>();

    public int CreatedStatusesCount => _statuses.Count;

    public StatusInfo GetStatus(string code)
    {
        if (_statuses.ContainsKey(code))
        {
            Console.WriteLine($"FABRYKA: używam istniejącego statusu: {code}");
            return _statuses[code];
        }

        Console.WriteLine($"FABRYKA: tworzę nowy status: {code}");

        StatusInfo status = CreateStatus(code);

        _statuses[code] = status;

        return status;
    }

    private StatusInfo CreateStatus(string code)
    {
        return code switch
        {
            "NOWA" => new StatusInfo(
                "NOWA",
                "Sprawa została zarejestrowana.",
                "biały",
                "Wniosek został przyjęty i oczekuje na rozpoczęcie obsługi.",
                30
            ),

            "W_TRAKCIE" => new StatusInfo(
                "W_TRAKCIE",
                "Sprawa jest aktualnie obsługiwana.",
                "żółty",
                "Wniosek jest analizowany przez pracownika merytorycznego.",
                60
            ),

            "ZAKONCZONA" => new StatusInfo(
                "ZAKONCZONA",
                "Sprawa została zakończona.",
                "zielony",
                "Obsługa sprawy została zakończona.",
                0
            ),

            "ODRZUCONA" => new StatusInfo(
                "ODRZUCONA",
                "Sprawa została odrzucona.",
                "czerwony",
                "Wniosek został odrzucony z powodów formalnych lub merytorycznych.",
                0
            ),

            _ => new StatusInfo(
                "NIEZNANY",
                "Nieznany status sprawy.",
                "szary",
                "Status sprawy nie został rozpoznany.",
                0
            )
        };
    }
}
