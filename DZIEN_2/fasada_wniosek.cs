using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("WZORZEC FASADA - PRZYKŁAD KOMPLETNY");
Console.WriteLine("-----------------------------------\n");

// Tworzymy usługi podsystemu.
WniosekValidator validator = new WniosekValidator();
PersonRegistryService registryService = new PersonRegistryService();
CaseNumberGenerator caseNumberGenerator = new CaseNumberGenerator();
WniosekRepository repository = new WniosekRepository();
DecisionService decisionService = new DecisionService();
NotificationService notificationService = new NotificationService();
ArchiveService archiveService = new ArchiveService();

// Fasada ukrywa cały proces obsługi wniosku.
ObslugaWnioskuFacade facade = new ObslugaWnioskuFacade(
    validator,
    registryService,
    caseNumberGenerator,
    repository,
    decisionService,
    notificationService,
    archiveService
);

Wniosek poprawnyWniosek = new Wniosek
{
    ImieNazwisko = "Jan Kowalski",
    Pesel = "44051401359",
    TypSprawy = "Świadczenie emerytalne"
};

Wniosek blednyWniosek = new Wniosek
{
    ImieNazwisko = "",
    Pesel = "123",
    TypSprawy = "Świadczenie rentowe"
};

facade.PrzyjmijWniosek(poprawnyWniosek);

Console.WriteLine();

facade.PrzyjmijWniosek(blednyWniosek);


// =======================================================
// MODEL DANYCH
// =======================================================

public class Wniosek
{
    public string ImieNazwisko { get; set; } = string.Empty;

    public string Pesel { get; set; } = string.Empty;

    public string TypSprawy { get; set; } = string.Empty;

    public string NumerSprawy { get; set; } = string.Empty;
}

public class Decyzja
{
    public string NumerSprawy { get; set; } = string.Empty;

    public string Tresc { get; set; } = string.Empty;
}


// =======================================================
// FASADA
// =======================================================

public class ObslugaWnioskuFacade
{
    private readonly WniosekValidator _validator;
    private readonly PersonRegistryService _registryService;
    private readonly CaseNumberGenerator _caseNumberGenerator;
    private readonly WniosekRepository _repository;
    private readonly DecisionService _decisionService;
    private readonly NotificationService _notificationService;
    private readonly ArchiveService _archiveService;

    public ObslugaWnioskuFacade(
        WniosekValidator validator,
        PersonRegistryService registryService,
        CaseNumberGenerator caseNumberGenerator,
        WniosekRepository repository,
        DecisionService decisionService,
        NotificationService notificationService,
        ArchiveService archiveService)
    {
        _validator = validator;
        _registryService = registryService;
        _caseNumberGenerator = caseNumberGenerator;
        _repository = repository;
        _decisionService = decisionService;
        _notificationService = notificationService;
        _archiveService = archiveService;
    }

    public void PrzyjmijWniosek(Wniosek wniosek)
    {
        Console.WriteLine("FASADA: Rozpoczynam obsługę wniosku.");

        if (!_validator.IsValid(wniosek))
        {
            Console.WriteLine("FASADA: Wniosek jest niepoprawny. Proces przerwany.");
            return;
        }

        if (!_registryService.PersonExists(wniosek.Pesel))
        {
            Console.WriteLine("FASADA: Osoba nie istnieje w rejestrze. Proces przerwany.");
            return;
        }

        wniosek.NumerSprawy = _caseNumberGenerator.Generate();

        _repository.Save(wniosek);

        Decyzja decyzja = _decisionService.GenerateDecision(wniosek);

        _notificationService.SendConfirmation(wniosek, decyzja);

        _archiveService.Archive(wniosek, decyzja);

        Console.WriteLine("FASADA: Zakończono obsługę wniosku.");
    }
}


// =======================================================
// PODSYSTEM 1 - WALIDACJA
// =======================================================

public class WniosekValidator
{
    public bool IsValid(Wniosek wniosek)
    {
        Console.WriteLine("Walidacja: sprawdzam dane wniosku.");

        if (string.IsNullOrWhiteSpace(wniosek.ImieNazwisko))
        {
            Console.WriteLine("Walidacja: brak imienia i nazwiska.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(wniosek.Pesel) || wniosek.Pesel.Length != 11)
        {
            Console.WriteLine("Walidacja: niepoprawny PESEL.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(wniosek.TypSprawy))
        {
            Console.WriteLine("Walidacja: brak typu sprawy.");
            return false;
        }

        Console.WriteLine("Walidacja: wniosek poprawny.");
        return true;
    }
}


// =======================================================
// PODSYSTEM 2 - REJESTR OSÓB
// =======================================================

public class PersonRegistryService
{
    public bool PersonExists(string pesel)
    {
        Console.WriteLine("Rejestr osób: sprawdzam osobę po numerze PESEL.");

        // Symulacja sprawdzenia w rejestrze.
        return pesel == "44051401359";
    }
}


// =======================================================
// PODSYSTEM 3 - GENERATOR NUMERU SPRAWY
// =======================================================

public class CaseNumberGenerator
{
    public string Generate()
    {
        Console.WriteLine("Generator numeru: nadaję numer sprawy.");

        return $"ZUS/{DateTime.Now:yyyyMMdd}/001";
    }
}


// =======================================================
// PODSYSTEM 4 - REPOZYTORIUM
// =======================================================

public class WniosekRepository
{
    public void Save(Wniosek wniosek)
    {
        Console.WriteLine($"Repozytorium: zapisuję wniosek {wniosek.NumerSprawy} do bazy.");
    }
}


// =======================================================
// PODSYSTEM 5 - DECYZJA
// =======================================================

public class DecisionService
{
    public Decyzja GenerateDecision(Wniosek wniosek)
    {
        Console.WriteLine("Decyzja: generuję decyzję dla wniosku.");

        return new Decyzja
        {
            NumerSprawy = wniosek.NumerSprawy,
            Tresc = $"Przyjęto wniosek typu: {wniosek.TypSprawy}."
        };
    }
}


// =======================================================
// PODSYSTEM 6 - POWIADOMIENIA
// =======================================================

public class NotificationService
{
    public void SendConfirmation(Wniosek wniosek, Decyzja decyzja)
    {
        Console.WriteLine($"Powiadomienie: wysyłam potwierdzenie dla {wniosek.ImieNazwisko}.");
        Console.WriteLine($"Powiadomienie: treść decyzji: {decyzja.Tresc}");
    }
}


// =======================================================
// PODSYSTEM 7 - ARCHIWUM
// =======================================================

public class ArchiveService
{
    public void Archive(Wniosek wniosek, Decyzja decyzja)
    {
        Console.WriteLine($"Archiwum: archiwizuję sprawę {wniosek.NumerSprawy}.");
    }
}
