using System;

namespace AbstractFactoryFluentApiSkeleton
{
    // ============================================================
    // 1. PRODUKTY ABSTRAKCYJNE
    //    Każda frakcja/rod będzie tworzyć własne wersje tych obiektów.
    // ============================================================

    public interface IWarrior
    {
        string GetName();
    }

    public interface ICommander
    {
        string GetName();
    }

    public interface ISupport
    {
        string GetName();
    }

    // ============================================================
    // 2. FABRYKA ABSTRAKCYJNA
    //    Definiuje, co każda konkretna fabryka musi potrafić utworzyć.
    // ============================================================

    public interface IHouseFactory
    {
        string GetHouseName();

        IWarrior CreateWarrior();

        ICommander CreateCommander();

        ISupport CreateSupport();
    }

    // ============================================================
    // 3. KONKRETNE PRODUKTY DLA RODU STARKÓW
    // ============================================================

    public class StarkWarrior : IWarrior
    {
        public string GetName()
        {
            // TODO: zwróć nazwę wojownika Starków, np. "Strażnik Północy"
            return "";
        }
    }

    public class StarkCommander : ICommander
    {
        public string GetName()
        {
            // TODO: zwróć nazwę dowódcy Starków
            return "";
        }
    }

    public class StarkSupport : ISupport
    {
        public string GetName()
        {
            // TODO: zwróć nazwę wsparcia Starków
            return "";
        }
    }

    // ============================================================
    // 4. KONKRETNA FABRYKA DLA RODU STARKÓW
    // ============================================================

    public class StarkFactory : IHouseFactory
    {
        public string GetHouseName()
        {
            // TODO: zwróć nazwę rodu
            return "";
        }

        public IWarrior CreateWarrior()
        {
            // TODO: utwórz i zwróć wojownika Starków
            return null;
        }

        public ICommander CreateCommander()
        {
            // TODO: utwórz i zwróć dowódcę Starków
            return null;
        }

        public ISupport CreateSupport()
        {
            // TODO: utwórz i zwróć wsparcie Starków
            return null;
        }
    }

    // ============================================================
    // 5. KONKRETNE PRODUKTY DLA RODU LANNISTERÓW
    // ============================================================

    public class LannisterWarrior : IWarrior
    {
        public string GetName()
        {
            // TODO: zwróć nazwę wojownika Lannisterów
            return "";
        }
    }

    public class LannisterCommander : ICommander
    {
        public string GetName()
        {
            // TODO: zwróć nazwę dowódcy Lannisterów
            return "";
        }
    }

    public class LannisterSupport : ISupport
    {
        public string GetName()
        {
            // TODO: zwróć nazwę wsparcia Lannisterów
            return "";
        }
    }

    // ============================================================
    // 6. KONKRETNA FABRYKA DLA RODU LANNISTERÓW
    // ============================================================

    public class LannisterFactory : IHouseFactory
    {
        public string GetHouseName()
        {
            // TODO: zwróć nazwę rodu
            return "";
        }

        public IWarrior CreateWarrior()
        {
            // TODO: utwórz i zwróć wojownika Lannisterów
            return null;
        }

        public ICommander CreateCommander()
        {
            // TODO: utwórz i zwróć dowódcę Lannisterów
            return null;
        }

        public ISupport CreateSupport()
        {
            // TODO: utwórz i zwróć wsparcie Lannisterów
            return null;
        }
    }

    // ============================================================
    // 7. KLASA REPREZENTUJĄCA GOTOWĄ WYPRAWĘ
    // ============================================================

    public class Mission
    {
        public string HouseName { get; set; }
        public string Goal { get; set; }
        public string Location { get; set; }
        public string RiskLevel { get; set; }

        public IWarrior Warrior { get; set; }
        public ICommander Commander { get; set; }
        public ISupport Support { get; set; }

        public void ShowDescription()
        {
            Console.WriteLine("WYPRAWA: " + Goal);
            Console.WriteLine("Ród / frakcja: " + HouseName);
            Console.WriteLine("Miejsce: " + Location);
            Console.WriteLine("Wojownik: " + Warrior.GetName());
            Console.WriteLine("Dowódca: " + Commander.GetName());
            Console.WriteLine("Wsparcie: " + Support.GetName());
            Console.WriteLine("Ryzyko: " + RiskLevel);
            Console.WriteLine();
        }
    }

    // ============================================================
    // 8. FLUENT API
    //    Ten builder pozwala tworzyć wyprawę metodami łańcuchowymi.
    // ============================================================

    public class MissionBuilder
    {
        private Mission mission = new Mission();

        public MissionBuilder ForHouse(IHouseFactory factory)
        {
            // TODO:
            // 1. Ustaw nazwę rodu/frakcji.
            // 2. Użyj fabryki do utworzenia wojownika.
            // 3. Użyj fabryki do utworzenia dowódcy.
            // 4. Użyj fabryki do utworzenia wsparcia.
            //
            // Podpowiedź:
            // mission.HouseName = ...
            // mission.Warrior = ...
            // mission.Commander = ...
            // mission.Support = ...

            return this;
        }

        public MissionBuilder WithGoal(string goal)
        {
            // TODO: ustaw cel wyprawy
            return this;
        }

        public MissionBuilder AtLocation(string location)
        {
            // TODO: ustaw miejsce wyprawy
            return this;
        }

        public MissionBuilder WithRiskLevel(string riskLevel)
        {
            // TODO: ustaw poziom ryzyka
            return this;
        }

        public Mission Build()
        {
            // TODO: opcjonalnie sprawdź, czy najważniejsze pola są ustawione
            return mission;
        }
    }

    // ============================================================
    // 9. PROGRAM GŁÓWNY
    // ============================================================

    internal class Program
    {
        private static void Main(string[] args)
        {
            // TODO: utwórz fabrykę Starków
            IHouseFactory starkFactory = null;

            // TODO: utwórz fabrykę Lannisterów
            IHouseFactory lannisterFactory = null;

            // TODO: utwórz pierwszą wyprawę przy użyciu Fluent API
            Mission mission1 = new MissionBuilder()
                .ForHouse(starkFactory)
                .WithGoal("TODO: wpisz cel wyprawy")
                .AtLocation("TODO: wpisz miejsce")
                .WithRiskLevel("TODO: wpisz ryzyko")
                .Build();

            // TODO: utwórz drugą wyprawę przy użyciu Fluent API
            Mission mission2 = new MissionBuilder()
                .ForHouse(lannisterFactory)
                .WithGoal("TODO: wpisz cel wyprawy")
                .AtLocation("TODO: wpisz miejsce")
                .WithRiskLevel("TODO: wpisz ryzyko")
                .Build();

            // TODO: wypisz obie wyprawy
            // mission1.ShowDescription();
            // mission2.ShowDescription();

            Console.ReadKey();
        }
    }
}
