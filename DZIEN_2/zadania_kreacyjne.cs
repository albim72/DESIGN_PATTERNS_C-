using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("SYSTEM TWORZENIA WYPRAW FANTASY");
Console.WriteLine("--------------------------------\n");

IHouseFactory starkFactory = new StarkFactory();
IHouseFactory lannisterFactory = new LannisterFactory();
IHouseFactory targaryenFactory = new TargaryenFactory();

Mission mission1 = new MissionBuilder()
    .ForHouse(starkFactory)
    .WithGoal("Obrona Północy")
    .AtLocation("Winterfell")
    .WithRiskLevel("wysokie")
    .Build();

Mission mission2 = new MissionBuilder()
    .ForHouse(lannisterFactory)
    .WithGoal("Zdobycie wpływów")
    .AtLocation("Królewska Przystań")
    .WithRiskLevel("średnie")
    .Build();

Mission mission3 = new MissionBuilder()
    .ForHouse(targaryenFactory)
    .WithGoal("Odzyskanie Żelaznego Tronu")
    .AtLocation("Smocza Skała")
    .WithRiskLevel("bardzo wysokie")
    .Build();

mission1.PrintDescription();
mission2.PrintDescription();
mission3.PrintDescription();


// =======================
// PRODUKTY ABSTRAKCYJNE
// =======================

public interface IWarrior
{
    string Name { get; }
}

public interface ICommander
{
    string Name { get; }
}

public interface ISupport
{
    string Name { get; }
}


// =======================
// FABRYKA ABSTRAKCYJNA
// =======================

public interface IHouseFactory
{
    string HouseName { get; }

    IWarrior CreateWarrior();

    ICommander CreateCommander();

    ISupport CreateSupport();
}


// =======================
// PRODUKTY RODU STARKÓW
// =======================

public class StarkWarrior : IWarrior
{
    public string Name => "Strażnik Północy";
}

public class StarkCommander : ICommander
{
    public string Name => "Lord Dowódca";
}

public class StarkSupport : ISupport
{
    public string Name => "Wilkor";
}


// ===========================
// PRODUKTY RODU LANNISTERÓW
// ===========================

public class LannisterWarrior : IWarrior
{
    public string Name => "Rycerz Lannisterów";
}

public class LannisterCommander : ICommander
{
    public string Name => "Strateg Dworu";
}

public class LannisterSupport : ISupport
{
    public string Name => "Złoto";
}


// ===========================
// PRODUKTY RODU TARGARYENÓW
// ===========================

public class TargaryenWarrior : IWarrior
{
    public string Name => "Gwardzista Smoka";
}

public class TargaryenCommander : ICommander
{
    public string Name => "Matka Smoków";
}

public class TargaryenSupport : ISupport
{
    public string Name => "Smok";
}


// =======================
// FABRYKI KONKRETNE
// =======================

public class StarkFactory : IHouseFactory
{
    public string HouseName => "Starkowie";

    public IWarrior CreateWarrior()
    {
        return new StarkWarrior();
    }

    public ICommander CreateCommander()
    {
        return new StarkCommander();
    }

    public ISupport CreateSupport()
    {
        return new StarkSupport();
    }
}

public class LannisterFactory : IHouseFactory
{
    public string HouseName => "Lannisterowie";

    public IWarrior CreateWarrior()
    {
        return new LannisterWarrior();
    }

    public ICommander CreateCommander()
    {
        return new LannisterCommander();
    }

    public ISupport CreateSupport()
    {
        return new LannisterSupport();
    }
}

public class TargaryenFactory : IHouseFactory
{
    public string HouseName => "Targaryenowie";

    public IWarrior CreateWarrior()
    {
        return new TargaryenWarrior();
    }

    public ICommander CreateCommander()
    {
        return new TargaryenCommander();
    }

    public ISupport CreateSupport()
    {
        return new TargaryenSupport();
    }
}


// =======================
// KLASA WYPRAWY
// =======================

public class Mission
{
    public string HouseName { get; }

    public string Goal { get; }

    public string Location { get; }

    public string RiskLevel { get; }

    public IWarrior Warrior { get; }

    public ICommander Commander { get; }

    public ISupport Support { get; }

    public Mission(
        string houseName,
        string goal,
        string location,
        string riskLevel,
        IWarrior warrior,
        ICommander commander,
        ISupport support)
    {
        HouseName = houseName;
        Goal = goal;
        Location = location;
        RiskLevel = riskLevel;
        Warrior = warrior;
        Commander = commander;
        Support = support;
    }

    public void PrintDescription()
    {
        Console.WriteLine($"WYPRAWA: {Goal}");
        Console.WriteLine($"Ród: {HouseName}");
        Console.WriteLine($"Miejsce: {Location}");
        Console.WriteLine($"Wojownik: {Warrior.Name}");
        Console.WriteLine($"Dowódca: {Commander.Name}");
        Console.WriteLine($"Wsparcie: {Support.Name}");
        Console.WriteLine($"Ryzyko: {RiskLevel}");
        Console.WriteLine();
    }
}


// =======================
// FLUENT API / BUILDER
// =======================

public class MissionBuilder
{
    private IHouseFactory? _houseFactory;
    private string? _goal;
    private string? _location;
    private string? _riskLevel;

    public MissionBuilder ForHouse(IHouseFactory houseFactory)
    {
        _houseFactory = houseFactory;
        return this;
    }

    public MissionBuilder WithGoal(string goal)
    {
        _goal = goal;
        return this;
    }

    public MissionBuilder AtLocation(string location)
    {
        _location = location;
        return this;
    }

    public MissionBuilder WithRiskLevel(string riskLevel)
    {
        _riskLevel = riskLevel;
        return this;
    }

    public Mission Build()
    {
        if (_houseFactory == null)
        {
            throw new InvalidOperationException("Nie wybrano rodu dla wyprawy.");
        }

        if (string.IsNullOrWhiteSpace(_goal))
        {
            throw new InvalidOperationException("Nie podano celu wyprawy.");
        }

        if (string.IsNullOrWhiteSpace(_location))
        {
            throw new InvalidOperationException("Nie podano miejsca wyprawy.");
        }

        if (string.IsNullOrWhiteSpace(_riskLevel))
        {
            throw new InvalidOperationException("Nie podano poziomu ryzyka.");
        }

        return new Mission(
            _houseFactory.HouseName,
            _goal,
            _location,
            _riskLevel,
            _houseFactory.CreateWarrior(),
            _houseFactory.CreateCommander(),
            _houseFactory.CreateSupport()
        );
    }
}
