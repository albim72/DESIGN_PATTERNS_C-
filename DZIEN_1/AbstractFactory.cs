using System;

namespace AbstractFactoryDemo
{
    public interface IWalidator
    {
        void Waliduj(string numerSprawy);
    }

    public interface INadawca
    {
        void Wyslij(string numerSprawy);
    }

    public interface IArchiwizator
    {
        void Archiwizuj(string numerSprawy);
    }

    public interface IKanalKomunikacjiFactory
    {
        IWalidator UtworzWalidator();
        INadawca UtworzNadawce();
        IArchiwizator UtworzArchiwizator();
    }

    public class EpuapWalidator : IWalidator
    {
        public void Waliduj(string numerSprawy)
        {
            Console.WriteLine($"[ePUAP] Waliduję dokument dla sprawy {numerSprawy}.");
            Console.WriteLine("[ePUAP] Sprawdzam profil zaufany i format dokumentu.");
        }
    }

    public class EpuapNadawca : INadawca
    {
        public void Wyslij(string numerSprawy)
        {
            Console.WriteLine($"[ePUAP] Wysyłam dokument dla sprawy {numerSprawy} przez ePUAP.");
        }
    }

    public class EpuapArchiwizator : IArchiwizator
    {
        public void Archiwizuj(string numerSprawy)
        {
            Console.WriteLine($"[ePUAP] Archiwizuję urzędowe poświadczenie przedłożenia dla sprawy {numerSprawy}.");
        }
    }

    public class EmailWalidator : IWalidator
    {
        public void Waliduj(string numerSprawy)
        {
            Console.WriteLine($"[EMAIL] Waliduję dokument dla sprawy {numerSprawy}.");
            Console.WriteLine("[EMAIL] Sprawdzam adres e-mail i rozmiar załącznika.");
        }
    }

    public class EmailNadawca : INadawca
    {
        public void Wyslij(string numerSprawy)
        {
            Console.WriteLine($"[EMAIL] Wysyłam dokument dla sprawy {numerSprawy} e-mailem.");
        }
    }

    public class EmailArchiwizator : IArchiwizator
    {
        public void Archiwizuj(string numerSprawy)
        {
            Console.WriteLine($"[EMAIL] Archiwizuję kopię wiadomości e-mail dla sprawy {numerSprawy}.");
        }
    }

    public class PapierWalidator : IWalidator
    {
        public void Waliduj(string numerSprawy)
        {
            Console.WriteLine($"[PAPIER] Waliduję dokument dla sprawy {numerSprawy}.");
            Console.WriteLine("[PAPIER] Sprawdzam kompletność danych adresowych.");
        }
    }

    public class PapierNadawca : INadawca
    {
        public void Wyslij(string numerSprawy)
        {
            Console.WriteLine($"[PAPIER] Kieruję dokument dla sprawy {numerSprawy} do wydruku i wysyłki pocztowej.");
        }
    }

    public class PapierArchiwizator : IArchiwizator
    {
        public void Archiwizuj(string numerSprawy)
        {
            Console.WriteLine($"[PAPIER] Archiwizuję potwierdzenie przekazania do wysyłki dla sprawy {numerSprawy}.");
        }
    }

    public class EpuapFactory : IKanalKomunikacjiFactory
    {
        public IWalidator UtworzWalidator()
        {
            return new EpuapWalidator();
        }

        public INadawca UtworzNadawce()
        {
            return new EpuapNadawca();
        }

        public IArchiwizator UtworzArchiwizator()
        {
            return new EpuapArchiwizator();
        }
    }

    public class EmailFactory : IKanalKomunikacjiFactory
    {
        public IWalidator UtworzWalidator()
        {
            return new EmailWalidator();
        }

        public INadawca UtworzNadawce()
        {
            return new EmailNadawca();
        }

        public IArchiwizator UtworzArchiwizator()
        {
            return new EmailArchiwizator();
        }
    }

    public class PapierFactory : IKanalKomunikacjiFactory
    {
        public IWalidator UtworzWalidator()
        {
            return new PapierWalidator();
        }

        public INadawca UtworzNadawce()
        {
            return new PapierNadawca();
        }

        public IArchiwizator UtworzArchiwizator()
        {
            return new PapierArchiwizator();
        }
    }

    public class ProcesWysylki
    {
        private readonly IWalidator _walidator;
        private readonly INadawca _nadawca;
        private readonly IArchiwizator _archiwizator;

        public ProcesWysylki(IKanalKomunikacjiFactory factory)
        {
            _walidator = factory.UtworzWalidator();
            _nadawca = factory.UtworzNadawce();
            _archiwizator = factory.UtworzArchiwizator();
        }

        public void Obsluz(string numerSprawy)
        {
            Console.WriteLine("====================================");
            Console.WriteLine($"Rozpoczynam obsługę wysyłki sprawy: {numerSprawy}");

            _walidator.Waliduj(numerSprawy);
            _nadawca.Wyslij(numerSprawy);
            _archiwizator.Archiwizuj(numerSprawy);

            Console.WriteLine("Proces wysyłki zakończony.");
            Console.WriteLine("====================================");
            Console.WriteLine();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Abstract Factory w C# ===");
            Console.WriteLine();

            IKanalKomunikacjiFactory epuapFactory = new EpuapFactory();
            var procesEpuap = new ProcesWysylki(epuapFactory);
            procesEpuap.Obsluz("ZUS/2026/001");

            IKanalKomunikacjiFactory emailFactory = new EmailFactory();
            var procesEmail = new ProcesWysylki(emailFactory);
            procesEmail.Obsluz("ZUS/2026/002");

            IKanalKomunikacjiFactory papierFactory = new PapierFactory();
            var procesPapier = new ProcesWysylki(papierFactory);
            procesPapier.Obsluz("ZUS/2026/003");

            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć.");
            Console.ReadKey();
        }
    }
}
