using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("WZORZEC DEKORATOR - PRZYKŁAD KOMPLETNY");
Console.WriteLine("--------------------------------------\n");

// Obiekt bazowy: faktyczna usługa wysyłki dokumentu.
// Dekoratory będą ją owijały kolejnymi warstwami.
IDocumentSender sender =
    new LoggingDocumentSenderDecorator(
        new TimingDocumentSenderDecorator(
            new ValidationDocumentSenderDecorator(
                new EmailDocumentSender()
            )
        )
    );

Document correctDocument = new Document
{
    Number = "ZUS/DEC/2026/001",
    RecipientEmail = "jan.kowalski@example.com",
    Title = "Decyzja o przyznaniu świadczenia",
    Content = "Po rozpatrzeniu wniosku przyznaje się świadczenie."
};

Document incorrectDocument = new Document
{
    Number = "",
    RecipientEmail = "brak-emaila",
    Title = "Decyzja odmowna",
    Content = ""
};

sender.Send(correctDocument);

Console.WriteLine();

sender.Send(incorrectDocument);


// =======================================================
// MODEL DOKUMENTU
// =======================================================

public class Document
{
    public string Number { get; set; } = string.Empty;

    public string RecipientEmail { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}


// =======================================================
// COMPONENT - WSPÓLNY INTERFEJS
// =======================================================

public interface IDocumentSender
{
    void Send(Document document);
}


// =======================================================
// CONCRETE COMPONENT - OBIEKT BAZOWY
// =======================================================

public class EmailDocumentSender : IDocumentSender
{
    public void Send(Document document)
    {
        Console.WriteLine("WYSYŁKA:");
        Console.WriteLine($"Wysyłam dokument e-mailem do: {document.RecipientEmail}");
        Console.WriteLine($"Numer dokumentu: {document.Number}");
        Console.WriteLine($"Tytuł: {document.Title}");
        Console.WriteLine("Dokument został wysłany.");
    }
}


// =======================================================
// BASE DECORATOR - KLASA BAZOWA DEKORATORA
// =======================================================

public abstract class DocumentSenderDecorator : IDocumentSender
{
    protected readonly IDocumentSender InnerSender;

    protected DocumentSenderDecorator(IDocumentSender innerSender)
    {
        InnerSender = innerSender;
    }

    public abstract void Send(Document document);
}


// =======================================================
// CONCRETE DECORATOR 1 - WALIDACJA
// =======================================================

public class ValidationDocumentSenderDecorator : DocumentSenderDecorator
{
    public ValidationDocumentSenderDecorator(IDocumentSender innerSender)
        : base(innerSender)
    {
    }

    public override void Send(Document document)
    {
        Console.WriteLine("WALIDACJA:");
        Console.WriteLine("Sprawdzam poprawność dokumentu...");

        if (string.IsNullOrWhiteSpace(document.Number))
        {
            Console.WriteLine("BŁĄD: Dokument nie ma numeru.");
            return;
        }

        if (string.IsNullOrWhiteSpace(document.RecipientEmail) ||
            !document.RecipientEmail.Contains("@"))
        {
            Console.WriteLine("BŁĄD: Niepoprawny adres e-mail odbiorcy.");
            return;
        }

        if (string.IsNullOrWhiteSpace(document.Content))
        {
            Console.WriteLine("BŁĄD: Dokument nie ma treści.");
            return;
        }

        Console.WriteLine("Dokument poprawny.");

        InnerSender.Send(document);
    }
}


// =======================================================
// CONCRETE DECORATOR 2 - POMIAR CZASU
// =======================================================

public class TimingDocumentSenderDecorator : DocumentSenderDecorator
{
    public TimingDocumentSenderDecorator(IDocumentSender innerSender)
        : base(innerSender)
    {
    }

    public override void Send(Document document)
    {
        DateTime start = DateTime.Now;

        InnerSender.Send(document);

        DateTime end = DateTime.Now;
        TimeSpan duration = end - start;

        Console.WriteLine($"METRYKA: Operacja trwała {duration.TotalMilliseconds} ms.");
    }
}


// =======================================================
// CONCRETE DECORATOR 3 - LOGOWANIE
// =======================================================

public class LoggingDocumentSenderDecorator : DocumentSenderDecorator
{
    public LoggingDocumentSenderDecorator(IDocumentSender innerSender)
        : base(innerSender)
    {
    }

    public override void Send(Document document)
    {
        Console.WriteLine($"LOG: Rozpoczynam wysyłkę dokumentu: {document.Number}");

        InnerSender.Send(document);

        Console.WriteLine($"LOG: Zakończono próbę wysyłki dokumentu: {document.Number}");
    }
}
