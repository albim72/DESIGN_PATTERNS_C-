using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("PROTOTYPE + REFLEKSJA - AUTOMATYCZNA KOPIA GŁĘBOKA");
Console.WriteLine("--------------------------------------------------\n");

DocumentTemplate baseDocument = new DocumentTemplate
{
    Title = "Pismo urzędowe",
    DocumentNumber = "ZUS/BASE/0000",
    DocumentType = "Szablon bazowy",
    Priority = "Normalny",

    Recipient = new Recipient
    {
        FirstName = "Jan",
        LastName = "Kowalski",
        Address = "ul. Przykładowa 1, 00-000 Warszawa"
    },

    Content = new DocumentContent
    {
        Header = "Zakład Ubezpieczeń Społecznych",
        Introduction = "Treść wprowadzająca dokumentu bazowego.",
        MainText = "Treść bazowa dokumentu.",
        Footer = "Dokument wygenerowany automatycznie."
    },

    Sections = new List<DocumentSection>
    {
        new DocumentSection
        {
            Name = "Podstawa prawna",
            Text = "Podstawa prawna dokumentu bazowego."
        },
        new DocumentSection
        {
            Name = "Uzasadnienie",
            Text = "Uzasadnienie dokumentu bazowego."
        }
    },

    Attachments = new List<Attachment>
    {
        new Attachment
        {
            FileName = "informacja_ogolna.pdf",
            Description = "Ogólna informacja dla strony"
        }
    }
};

// Automatyczna kopia głęboka
DocumentTemplate wezwanie = DeepCloneHelper.DeepClone(baseDocument);

wezwanie.Title = "Wezwanie do uzupełnienia dokumentów";
wezwanie.DocumentNumber = "ZUS/W/2026/001";
wezwanie.DocumentType = "Wezwanie";
wezwanie.Priority = "Wysoki";

wezwanie.Recipient.FirstName = "Anna";
wezwanie.Recipient.LastName = "Nowak";
wezwanie.Recipient.Address = "ul. Lipowa 15, 20-001 Lublin";

wezwanie.Content.MainText = "Prosimy o uzupełnienie brakujących dokumentów.";
wezwanie.Sections[0].Text = "Art. 50 Kodeksu postępowania administracyjnego.";

wezwanie.Attachments.Add(new Attachment
{
    FileName = "lista_brakow.pdf",
    Description = "Lista brakujących dokumentów"
});

DocumentTemplate decyzjaPozytywna = DeepCloneHelper.DeepClone(baseDocument);

decyzjaPozytywna.Title = "Decyzja o przyznaniu świadczenia";
decyzjaPozytywna.DocumentNumber = "ZUS/D/2026/002";
decyzjaPozytywna.DocumentType = "Decyzja pozytywna";
decyzjaPozytywna.Priority = "Normalny";

decyzjaPozytywna.Recipient.FirstName = "Piotr";
decyzjaPozytywna.Recipient.LastName = "Zieliński";
decyzjaPozytywna.Recipient.Address = "ul. Ogrodowa 8, 30-002 Kraków";

decyzjaPozytywna.Content.MainText = "Świadczenie zostaje przyznane od dnia 1 stycznia 2026 roku.";
decyzjaPozytywna.Sections[1].Text = "Wnioskodawca spełnił wymagane warunki.";

DocumentTemplate decyzjaOdmowna = DeepCloneHelper.DeepClone(baseDocument);

decyzjaOdmowna.Title = "Decyzja odmowna";
decyzjaOdmowna.DocumentNumber = "ZUS/D/2026/003";
decyzjaOdmowna.DocumentType = "Decyzja odmowna";
decyzjaOdmowna.Priority = "Wysoki";

decyzjaOdmowna.Recipient.FirstName = "Maria";
decyzjaOdmowna.Recipient.LastName = "Wiśniewska";
decyzjaOdmowna.Recipient.Address = "ul. Polna 22, 80-003 Gdańsk";

decyzjaOdmowna.Content.MainText = "Odmawia się przyznania świadczenia.";
decyzjaOdmowna.Sections.Add(new DocumentSection
{
    Name = "Poucznie",
    Text = "Strona może wnieść odwołanie w terminie ustawowym."
});

Console.WriteLine("=== DOKUMENT BAZOWY ===");
baseDocument.Print();

Console.WriteLine("=== WEZWANIE ===");
wezwanie.Print();

Console.WriteLine("=== DECYZJA POZYTYWNA ===");
decyzjaPozytywna.Print();

Console.WriteLine("=== DECYZJA ODMOWNA ===");
decyzjaOdmowna.Print();


// =======================================================
// AUTOMATYCZNY MECHANIZM KOPII GŁĘBOKIEJ
// =======================================================

public static class DeepCloneHelper
{
    public static T DeepClone<T>(T source)
    {
        return (T)DeepCloneObject(source)!;
    }

    private static object? DeepCloneObject(object? source)
    {
        if (source == null)
        {
            return null;
        }

        Type type = source.GetType();

        if (IsSimpleType(type))
        {
            return source;
        }

        if (typeof(IList).IsAssignableFrom(type))
        {
            IList sourceList = (IList)source;
            IList clonedList = (IList)Activator.CreateInstance(type)!;

            foreach (object? item in sourceList)
            {
                clonedList.Add(DeepCloneObject(item));
            }

            return clonedList;
        }

        object clone = Activator.CreateInstance(type)!;

        foreach (PropertyInfo property in type.GetProperties(
            BindingFlags.Public | BindingFlags.Instance))
        {
            if (!property.CanRead || !property.CanWrite)
            {
                continue;
            }

            object? value = property.GetValue(source);
            object? clonedValue = DeepCloneObject(value);

            property.SetValue(clone, clonedValue);
        }

        return clone;
    }

    private static bool IsSimpleType(Type type)
    {
        return type.IsPrimitive
            || type.IsEnum
            || type == typeof(string)
            || type == typeof(decimal)
            || type == typeof(DateTime)
            || type == typeof(Guid);
    }
}


// =======================================================
// KLASY MODELU
// =======================================================

public class DocumentTemplate
{
    public string Title { get; set; } = string.Empty;

    public string DocumentNumber { get; set; } = string.Empty;

    public string DocumentType { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public Recipient Recipient { get; set; } = new Recipient();

    public DocumentContent Content { get; set; } = new DocumentContent();

    public List<DocumentSection> Sections { get; set; } = new List<DocumentSection>();

    public List<Attachment> Attachments { get; set; } = new List<Attachment>();

    public void Print()
    {
        Console.WriteLine($"Tytuł: {Title}");
        Console.WriteLine($"Numer: {DocumentNumber}");
        Console.WriteLine($"Typ: {DocumentType}");
        Console.WriteLine($"Priorytet: {Priority}");

        Console.WriteLine();
        Console.WriteLine("Adresat:");
        Console.WriteLine($"  {Recipient.FirstName} {Recipient.LastName}");
        Console.WriteLine($"  {Recipient.Address}");

        Console.WriteLine();
        Console.WriteLine("Treść:");
        Console.WriteLine($"  Nagłówek: {Content.Header}");
        Console.WriteLine($"  Wprowadzenie: {Content.Introduction}");
        Console.WriteLine($"  Tekst główny: {Content.MainText}");
        Console.WriteLine($"  Stopka: {Content.Footer}");

        Console.WriteLine();
        Console.WriteLine("Sekcje:");
        foreach (DocumentSection section in Sections)
        {
            Console.WriteLine($"  - {section.Name}: {section.Text}");
        }

        Console.WriteLine();
        Console.WriteLine("Załączniki:");
        foreach (Attachment attachment in Attachments)
        {
            Console.WriteLine($"  - {attachment.FileName}: {attachment.Description}");
        }

        Console.WriteLine();
        Console.WriteLine("----------------------------------");
        Console.WriteLine();
    }
}

public class Recipient
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
}

public class DocumentContent
{
    public string Header { get; set; } = string.Empty;

    public string Introduction { get; set; } = string.Empty;

    public string MainText { get; set; } = string.Empty;

    public string Footer { get; set; } = string.Empty;
}

public class DocumentSection
{
    public string Name { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
}

public class Attachment
{
    public string FileName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
