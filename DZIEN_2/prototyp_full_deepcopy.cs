using System;
using System.Collections.Generic;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("WZORZEC PROTOTYPE - KOPIA GŁĘBOKA");
Console.WriteLine("----------------------------------\n");

// 1. Tworzymy dokument bazowy - prototyp
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
        Introduction = "Niniejsze pismo zostało wygenerowane na podstawie danych sprawy.",
        MainText = "Treść bazowa dokumentu.",
        Footer = "Dokument wygenerowany automatycznie."
    },

    Sections = new List<DocumentSection>
    {
        new DocumentSection
        {
            Name = "Podstawa prawna",
            Text = "Podstawa prawna zostanie wskazana w zależności od typu sprawy."
        },
        new DocumentSection
        {
            Name = "Uzasadnienie",
            Text = "Uzasadnienie zostanie uzupełnione indywidualnie."
        }
    },

    Attachments = new List<Attachment>
    {
        new Attachment
        {
            FileName = "informacja_ogolna.pdf",
            Description = "Ogólna informacja dla strony"
        }
    },

    Metadata = new DocumentMetadata
    {
        CreatedBy = "System",
        Department = "Departament Obsługi Spraw",
        Tags = new List<string> { "szablon", "pismo", "zus" }
    },

    ApprovalPath = new List<ApprovalStep>
    {
        new ApprovalStep
        {
            Role = "Pracownik merytoryczny",
            Status = "Oczekuje"
        },
        new ApprovalStep
        {
            Role = "Kierownik",
            Status = "Oczekuje"
        }
    }
};

// 2. Przypadek pierwszy: wezwanie do uzupełnienia dokumentów
DocumentTemplate wezwanie = baseDocument.DeepClone();

wezwanie.Title = "Wezwanie do uzupełnienia dokumentów";
wezwanie.DocumentNumber = "ZUS/W/2026/001";
wezwanie.DocumentType = "Wezwanie";
wezwanie.Priority = "Wysoki";

wezwanie.Recipient.FirstName = "Anna";
wezwanie.Recipient.LastName = "Nowak";
wezwanie.Recipient.Address = "ul. Lipowa 15, 20-001 Lublin";

wezwanie.Content.Introduction = "W związku ze złożonym wnioskiem prosimy o uzupełnienie brakujących dokumentów.";
wezwanie.Content.MainText = "Brakuje zaświadczenia o zatrudnieniu oraz potwierdzenia okresów składkowych.";
wezwanie.Content.Footer = "Termin uzupełnienia dokumentów wynosi 14 dni od daty doręczenia pisma.";

wezwanie.Sections[0].Text = "Art. 50 Kodeksu postępowania administracyjnego.";
wezwanie.Sections[1].Text = "Wniosek nie może zostać rozpatrzony bez wymaganych załączników.";

wezwanie.Attachments.Add(new Attachment
{
    FileName = "lista_brakow.pdf",
    Description = "Lista brakujących dokumentów"
});

wezwanie.Metadata.Tags.Add("wezwanie");
wezwanie.Metadata.Tags.Add("braki-formalne");

wezwanie.ApprovalPath[0].Status = "Zaakceptowano";
wezwanie.ApprovalPath[1].Status = "Oczekuje";


// 3. Przypadek drugi: decyzja pozytywna
DocumentTemplate decyzjaPozytywna = baseDocument.DeepClone();

decyzjaPozytywna.Title = "Decyzja o przyznaniu świadczenia";
decyzjaPozytywna.DocumentNumber = "ZUS/D/2026/002";
decyzjaPozytywna.DocumentType = "Decyzja pozytywna";
decyzjaPozytywna.Priority = "Normalny";

decyzjaPozytywna.Recipient.FirstName = "Piotr";
decyzjaPozytywna.Recipient.LastName = "Zieliński";
decyzjaPozytywna.Recipient.Address = "ul. Ogrodowa 8, 30-002 Kraków";

decyzjaPozytywna.Content.Introduction = "Po rozpatrzeniu wniosku organ przyznaje świadczenie.";
decyzjaPozytywna.Content.MainText = "Świadczenie zostaje przyznane od dnia 1 stycznia 2026 roku.";
decyzjaPozytywna.Content.Footer = "Od niniejszej decyzji przysługuje odwołanie w terminie ustawowym.";

decyzjaPozytywna.Sections[0].Text = "Ustawa o systemie ubezpieczeń społecznych.";
decyzjaPozytywna.Sections[1].Text = "Wnioskodawca spełnił wszystkie wymagane warunki formalne i merytoryczne.";

decyzjaPozytywna.Attachments.Clear();
decyzjaPozytywna.Attachments.Add(new Attachment
{
    FileName = "decyzja_przyznanie.pdf",
    Description = "Decyzja o przyznaniu świadczenia"
});

decyzjaPozytywna.Metadata.Department = "Departament Świadczeń";
decyzjaPozytywna.Metadata.Tags.Add("decyzja");
decyzjaPozytywna.Metadata.Tags.Add("pozytywna");

decyzjaPozytywna.ApprovalPath[0].Status = "Zaakceptowano";
decyzjaPozytywna.ApprovalPath[1].Status = "Zaakceptowano";


// 4. Przypadek trzeci: decyzja odmowna
DocumentTemplate decyzjaOdmowna = baseDocument.DeepClone();

decyzjaOdmowna.Title = "Decyzja odmowna";
decyzjaOdmowna.DocumentNumber = "ZUS/D/2026/003";
decyzjaOdmowna.DocumentType = "Decyzja odmowna";
decyzjaOdmowna.Priority = "Wysoki";

decyzjaOdmowna.Recipient.FirstName = "Maria";
decyzjaOdmowna.Recipient.LastName = "Wiśniewska";
decyzjaOdmowna.Recipient.Address = "ul. Polna 22, 80-003 Gdańsk";

decyzjaOdmowna.Content.Introduction = "Po przeprowadzeniu analizy dokumentacji organ odmawia przyznania świadczenia.";
decyzjaOdmowna.Content.MainText = "Wnioskodawca nie spełnił wymaganego warunku okresu składkowego.";
decyzjaOdmowna.Content.Footer = "Od decyzji przysługuje prawo wniesienia odwołania.";

decyzjaOdmowna.Sections[0].Text = "Podstawą odmowy jest niespełnienie warunków określonych w ustawie.";
decyzjaOdmowna.Sections[1].Text = "Analiza akt sprawy wykazała brak wymaganego okresu ubezpieczenia.";

decyzjaOdmowna.Sections.Add(new DocumentSection
{
    Name = "Poucznie",
    Text = "Strona może złożyć odwołanie za pośrednictwem organu, który wydał decyzję."
});

decyzjaOdmowna.Attachments.Add(new Attachment
{
    FileName = "uzasadnienie_odmowy.pdf",
    Description = "Szczegółowe uzasadnienie decyzji odmownej"
});

decyzjaOdmowna.Metadata.Department = "Departament Kontroli Uprawnień";
decyzjaOdmowna.Metadata.Tags.Add("decyzja");
decyzjaOdmowna.Metadata.Tags.Add("odmowa");
decyzjaOdmowna.Metadata.Tags.Add("odwolanie");

decyzjaOdmowna.ApprovalPath[0].Status = "Zaakceptowano";
decyzjaOdmowna.ApprovalPath[1].Status = "Wymaga dodatkowej analizy";


// 5. Wypisujemy wszystkie dokumenty
Console.WriteLine("=== DOKUMENT BAZOWY - PROTOTYP ===");
baseDocument.Print();

Console.WriteLine("=== PRZYPADEK 1: WEZWANIE ===");
wezwanie.Print();

Console.WriteLine("=== PRZYPADEK 2: DECYZJA POZYTYWNA ===");
decyzjaPozytywna.Print();

Console.WriteLine("=== PRZYPADEK 3: DECYZJA ODMOWNA ===");
decyzjaOdmowna.Print();


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

    public DocumentMetadata Metadata { get; set; } = new DocumentMetadata();

    public List<ApprovalStep> ApprovalPath { get; set; } = new List<ApprovalStep>();

    public DocumentTemplate DeepClone()
    {
        DocumentTemplate copy = new DocumentTemplate
        {
            Title = this.Title,
            DocumentNumber = this.DocumentNumber,
            DocumentType = this.DocumentType,
            Priority = this.Priority,

            Recipient = this.Recipient.DeepClone(),
            Content = this.Content.DeepClone(),
            Metadata = this.Metadata.DeepClone(),

            Sections = new List<DocumentSection>(),
            Attachments = new List<Attachment>(),
            ApprovalPath = new List<ApprovalStep>()
        };

        foreach (DocumentSection section in this.Sections)
        {
            copy.Sections.Add(section.DeepClone());
        }

        foreach (Attachment attachment in this.Attachments)
        {
            copy.Attachments.Add(attachment.DeepClone());
        }

        foreach (ApprovalStep step in this.ApprovalPath)
        {
            copy.ApprovalPath.Add(step.DeepClone());
        }

        return copy;
    }

    public void Print()
    {
        Console.WriteLine($"Tytuł: {Title}");
        Console.WriteLine($"Numer dokumentu: {DocumentNumber}");
        Console.WriteLine($"Typ dokumentu: {DocumentType}");
        Console.WriteLine($"Priorytet: {Priority}");

        Console.WriteLine();
        Console.WriteLine("Adresat:");
        Console.WriteLine($"  Imię i nazwisko: {Recipient.FirstName} {Recipient.LastName}");
        Console.WriteLine($"  Adres: {Recipient.Address}");

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
        Console.WriteLine("Metadane:");
        Console.WriteLine($"  Utworzył: {Metadata.CreatedBy}");
        Console.WriteLine($"  Departament: {Metadata.Department}");
        Console.WriteLine($"  Tagi: {string.Join(", ", Metadata.Tags)}");

        Console.WriteLine();
        Console.WriteLine("Ścieżka akceptacji:");
        foreach (ApprovalStep step in ApprovalPath)
        {
            Console.WriteLine($"  - {step.Role}: {step.Status}");
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

    public Recipient DeepClone()
    {
        return new Recipient
        {
            FirstName = this.FirstName,
            LastName = this.LastName,
            Address = this.Address
        };
    }
}

public class DocumentContent
{
    public string Header { get; set; } = string.Empty;

    public string Introduction { get; set; } = string.Empty;

    public string MainText { get; set; } = string.Empty;

    public string Footer { get; set; } = string.Empty;

    public DocumentContent DeepClone()
    {
        return new DocumentContent
        {
            Header = this.Header,
            Introduction = this.Introduction,
            MainText = this.MainText,
            Footer = this.Footer
        };
    }
}

public class DocumentSection
{
    public string Name { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public DocumentSection DeepClone()
    {
        return new DocumentSection
        {
            Name = this.Name,
            Text = this.Text
        };
    }
}

public class Attachment
{
    public string FileName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Attachment DeepClone()
    {
        return new Attachment
        {
            FileName = this.FileName,
            Description = this.Description
        };
    }
}

public class DocumentMetadata
{
    public string CreatedBy { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = new List<string>();

    public DocumentMetadata DeepClone()
    {
        return new DocumentMetadata
        {
            CreatedBy = this.CreatedBy,
            Department = this.Department,
            Tags = new List<string>(this.Tags)
        };
    }
}

public class ApprovalStep
{
    public string Role { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public ApprovalStep DeepClone()
    {
        return new ApprovalStep
        {
            Role = this.Role,
            Status = this.Status
        };
    }
}
