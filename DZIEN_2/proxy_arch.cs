using System;
using System.Collections.Generic;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("WZORZEC PROXY - PRZYKŁAD ADMINISTRACYJNY");
Console.WriteLine("----------------------------------------\n");

User clerk = new User
{
    Name = "Anna Urzędnik",
    Role = "Clerk"
};

User guest = new User
{
    Name = "Jan Gość",
    Role = "Guest"
};

IDocumentService documentServiceForClerk =
    new SecureDocumentProxy(clerk);

IDocumentService documentServiceForGuest =
    new SecureDocumentProxy(guest);

Console.WriteLine("PRÓBA 1: użytkownik z uprawnieniami");
documentServiceForClerk.DisplayDocument("ZUS/DEC/2026/001");

Console.WriteLine();

Console.WriteLine("PRÓBA 2: ten sam dokument drugi raz, powinien zadziałać cache");
documentServiceForClerk.DisplayDocument("ZUS/DEC/2026/001");

Console.WriteLine();

Console.WriteLine("PRÓBA 3: użytkownik bez uprawnień");
documentServiceForGuest.DisplayDocument("ZUS/DEC/2026/002");


// =======================================================
// MODEL UŻYTKOWNIKA
// =======================================================

public class User
{
    public string Name { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}


// =======================================================
// MODEL DOKUMENTU
// =======================================================

public class AdministrativeDocument
{
    public string Number { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}


// =======================================================
// SUBJECT - WSPÓLNY INTERFEJS
// =======================================================

public interface IDocumentService
{
    void DisplayDocument(string documentNumber);
}


// =======================================================
// REAL SUBJECT - PRAWDZIWY SERWIS
// =======================================================

public class ArchiveDocumentService : IDocumentService
{
    public void DisplayDocument(string documentNumber)
    {
        AdministrativeDocument document = LoadDocumentFromArchive(documentNumber);

        Console.WriteLine("DOKUMENT:");
        Console.WriteLine($"Numer: {document.Number}");
        Console.WriteLine($"Tytuł: {document.Title}");
        Console.WriteLine($"Treść: {document.Content}");
    }

    public AdministrativeDocument LoadDocumentFromArchive(string documentNumber)
    {
        Console.WriteLine("ARCHIWUM: Pobieram dokument z archiwum...");
        Console.WriteLine("ARCHIWUM: To jest kosztowna operacja.");

        return new AdministrativeDocument
        {
            Number = documentNumber,
            Title = "Decyzja administracyjna",
            Content = "Treść decyzji pobrana z archiwum elektronicznego."
        };
    }
}


// =======================================================
// PROXY - KONTROLOWANY DOSTĘP DO SERWISU
// =======================================================

public class SecureDocumentProxy : IDocumentService
{
    private readonly User _user;

    private readonly ArchiveDocumentService _realService;

    private readonly Dictionary<string, AdministrativeDocument> _cache;

    public SecureDocumentProxy(User user)
    {
        _user = user;
        _realService = new ArchiveDocumentService();
        _cache = new Dictionary<string, AdministrativeDocument>();
    }

    public void DisplayDocument(string documentNumber)
    {
        Console.WriteLine($"PROXY: Użytkownik: {_user.Name}");
        Console.WriteLine($"PROXY: Rola: {_user.Role}");
        Console.WriteLine($"PROXY: Żądany dokument: {documentNumber}");

        if (!HasAccess())
        {
            Console.WriteLine("PROXY: Brak uprawnień do odczytu dokumentu.");
            return;
        }

        AdministrativeDocument document;

        if (_cache.ContainsKey(documentNumber))
        {
            Console.WriteLine("PROXY: Dokument znaleziony w cache.");
            document = _cache[documentNumber];
        }
        else
        {
            Console.WriteLine("PROXY: Dokumentu nie ma w cache.");
            document = _realService.LoadDocumentFromArchive(documentNumber);
            _cache[documentNumber] = document;
        }

        Console.WriteLine("PROXY: Dostęp przyznany. Wyświetlam dokument.");
        Console.WriteLine();

        Console.WriteLine("DOKUMENT:");
        Console.WriteLine($"Numer: {document.Number}");
        Console.WriteLine($"Tytuł: {document.Title}");
        Console.WriteLine($"Treść: {document.Content}");
    }

    private bool HasAccess()
    {
        return _user.Role == "Clerk" || _user.Role == "Manager";
    }
}
