using System;

namespace FactoryMethodDemo
{
    public interface IDokument
    {
        void Generuj();
    }

    public class PdfDokument : IDokument
    {
        public void Generuj()
        {
            Console.WriteLine("Generuję decyzję administracyjną w formacie PDF.");
        }
    }

    public class HtmlDokument : IDokument
    {
        public void Generuj()
        {
            Console.WriteLine("Generuję decyzję administracyjną w formacie HTML.");
        }
    }

    public class TxtDokument : IDokument
    {
        public void Generuj()
        {
            Console.WriteLine("Generuję decyzję administracyjną w formacie TXT.");
        }
    }

    public abstract class EksporterDokumentu
    {
        public void Eksportuj(string numerSprawy)
        {
            Console.WriteLine("====================================");
            Console.WriteLine($"Rozpoczynam eksport sprawy: {numerSprawy}");

            IDokument dokument = UtworzDokument();

            dokument.Generuj();

            Console.WriteLine("Eksport zakończony.");
            Console.WriteLine("====================================");
            Console.WriteLine();
        }

        protected abstract IDokument UtworzDokument();
    }

    public class PdfEksporter : EksporterDokumentu
    {
        protected override IDokument UtworzDokument()
        {
            return new PdfDokument();
        }
    }

    public class HtmlEksporter : EksporterDokumentu
    {
        protected override IDokument UtworzDokument()
        {
            return new HtmlDokument();
        }
    }

    public class TxtEksporter : EksporterDokumentu
    {
        protected override IDokument UtworzDokument()
        {
            return new TxtDokument();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Factory Method w C# ===");
            Console.WriteLine();

            EksporterDokumentu pdfEksporter = new PdfEksporter();
            pdfEksporter.Eksportuj("ZUS/2026/001");

            EksporterDokumentu htmlEksporter = new HtmlEksporter();
            htmlEksporter.Eksportuj("ZUS/2026/002");

            EksporterDokumentu txtEksporter = new TxtEksporter();
            txtEksporter.Eksportuj("ZUS/2026/003");

            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć.");
            Console.ReadKey();
        }
    }
}
