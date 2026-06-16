using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("WZORZEC PRIVATE CLASS DATA - PRZYKŁAD ADMINISTRACYJNY");
Console.WriteLine("-----------------------------------------------------\n");

// Tworzymy prywatny pakiet danych sprawy.
// Te dane opisują tożsamość sprawy i nie powinny być przypadkowo zmieniane.
CaseData caseData = new CaseData(
    "ZUS/2026/001",
    "Jan Kowalski",
    "44051401359",
    new DateTime(2026, 6, 16),
    "Wniosek o świadczenie emerytalne",
    "Oddział ZUS Lublin"
);

// Klasa główna otrzymuje dane i trzyma je jako prywatny obiekt.
AdministrativeCase administrativeCase = new AdministrativeCase(caseData);

administrativeCase.PrintDetails();

administrativeCase.StartProcessing();
administrativeCase.PrintDetails();

administrativeCase.Approve("Wniosek spełnia wymagania formalne i merytoryczne.");
administrativeCase.PrintDetails();


// =======================================================
// PRIVATE CLASS DATA
// Dane inicjalizacyjne przeniesione do osobnego obiektu.
// Record dobrze nadaje się do przechowywania danych,
// które po utworzeniu nie powinny być swobodnie zmieniane.
// =======================================================

public record CaseData(
    string CaseNumber,
    string CitizenName,
    string Pesel,
    DateTime SubmissionDate,
    string CaseType,
    string Department
);


// =======================================================
// KLASA GŁÓWNA
// =======================================================

public class AdministrativeCase
{
    private readonly CaseData _caseData;

    private string _status;

    private string _decisionNote;

    public AdministrativeCase(CaseData caseData)
    {
        _caseData = caseData;
        _status = "Nowa";
        _decisionNote = "Brak decyzji.";
    }

    public void StartProcessing()
    {
        if (_status != "Nowa")
        {
            Console.WriteLine("Sprawa nie może zostać ponownie rozpoczęta.");
            return;
        }

        _status = "W trakcie obsługi";

        Console.WriteLine($"Sprawa {_caseData.CaseNumber} została przekazana do obsługi.");
        Console.WriteLine();
    }

    public void Approve(string decisionNote)
    {
        if (_status != "W trakcie obsługi")
        {
            Console.WriteLine("Sprawa musi być najpierw w trakcie obsługi.");
            return;
        }

        _status = "Zatwierdzona";
        _decisionNote = decisionNote;

        Console.WriteLine($"Sprawa {_caseData.CaseNumber} została zatwierdzona.");
        Console.WriteLine();
    }

    public void Reject(string decisionNote)
    {
        if (_status != "W trakcie obsługi")
        {
            Console.WriteLine("Sprawa musi być najpierw w trakcie obsługi.");
            return;
        }

        _status = "Odrzucona";
        _decisionNote = decisionNote;

        Console.WriteLine($"Sprawa {_caseData.CaseNumber} została odrzucona.");
        Console.WriteLine();
    }

    public void PrintDetails()
    {
        Console.WriteLine("SPRAWA ADMINISTRACYJNA");
        Console.WriteLine($"Numer sprawy: {_caseData.CaseNumber}");
        Console.WriteLine($"Obywatel: {_caseData.CitizenName}");
        Console.WriteLine($"PESEL: {_caseData.Pesel}");
        Console.WriteLine($"Data wpływu: {_caseData.SubmissionDate:yyyy-MM-dd}");
        Console.WriteLine($"Typ sprawy: {_caseData.CaseType}");
        Console.WriteLine($"Jednostka: {_caseData.Department}");
        Console.WriteLine($"Status: {_status}");
        Console.WriteLine($"Notatka decyzyjna: {_decisionNote}");
        Console.WriteLine("-----------------------------------------------------");
        Console.WriteLine();
    }
}
