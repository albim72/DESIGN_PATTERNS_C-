using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("WZORZEC ADAPTER - PRZYKŁAD KOMPLETNY");
Console.WriteLine("------------------------------------\n");

// Stary system, którego nie chcemy albo nie możemy zmienić.
LegacyPeselSystem legacyPeselSystem = new LegacyPeselSystem();

// Adapter dopasowuje stary system do nowego interfejsu.
IIdentifierValidator validator = new PeselAdapter(legacyPeselSystem);

// Nowa usługa aplikacyjna korzysta tylko z czystego interfejsu.
// Nie zna systemu legacy.
CitizenRegistrationService registrationService =
    new CitizenRegistrationService(validator);

registrationService.RegisterCitizen("44051401359");
registrationService.RegisterCitizen("123");
registrationService.RegisterCitizen("");
registrationService.RegisterCitizen("abcdefghijk");


// =======================================================
// INTERFEJS OCZEKIWANY PRZEZ NOWĄ APLIKACJĘ
// =======================================================

public interface IIdentifierValidator
{
    bool IsValid(string value);

    string GetValidationMessage(string value);
}


// =======================================================
// STARY SYSTEM LEGACY
// =======================================================

public class LegacyPeselSystem
{
    /*
        Stary system działa, ale ma niewygodny interfejs.

        Zwraca kody liczbowe:
        0 - PESEL poprawny
        1 - wartość pusta
        2 - błędna długość
        3 - zawiera niedozwolone znaki
    */

    public int SprawdzPesel(string pesel)
    {
        if (string.IsNullOrWhiteSpace(pesel))
        {
            return 1;
        }

        if (pesel.Length != 11)
        {
            return 2;
        }

        foreach (char znak in pesel)
        {
            if (!char.IsDigit(znak))
            {
                return 3;
            }
        }

        return 0;
    }
}


// =======================================================
// ADAPTER
// =======================================================

public class PeselAdapter : IIdentifierValidator
{
    private readonly LegacyPeselSystem _legacyPeselSystem;

    public PeselAdapter(LegacyPeselSystem legacyPeselSystem)
    {
        _legacyPeselSystem = legacyPeselSystem;
    }

    public bool IsValid(string value)
    {
        int resultCode = _legacyPeselSystem.SprawdzPesel(value);

        return resultCode == 0;
    }

    public string GetValidationMessage(string value)
    {
        int resultCode = _legacyPeselSystem.SprawdzPesel(value);

        return resultCode switch
        {
            0 => "PESEL poprawny.",
            1 => "PESEL nie może być pusty.",
            2 => "PESEL musi mieć dokładnie 11 znaków.",
            3 => "PESEL może zawierać tylko cyfry.",
            _ => "Nieznany błąd walidacji."
        };
    }
}


// =======================================================
// KLIENT / USŁUGA NOWEJ APLIKACJI
// =======================================================

public class CitizenRegistrationService
{
    private readonly IIdentifierValidator _identifierValidator;

    public CitizenRegistrationService(IIdentifierValidator identifierValidator)
    {
        _identifierValidator = identifierValidator;
    }

    public void RegisterCitizen(string pesel)
    {
        Console.WriteLine($"Próba rejestracji osoby z PESEL: \"{pesel}\"");

        if (!_identifierValidator.IsValid(pesel))
        {
            Console.WriteLine("Rejestracja odrzucona.");
            Console.WriteLine($"Powód: {_identifierValidator.GetValidationMessage(pesel)}");
            Console.WriteLine();
            return;
        }

        Console.WriteLine("Rejestracja przyjęta.");
        Console.WriteLine($"Komunikat: {_identifierValidator.GetValidationMessage(pesel)}");
        Console.WriteLine();
    }
}
