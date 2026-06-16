using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("WZORZEC FASADA - GRA O TRON");
Console.WriteLine("============================\n");

// Klient programu korzysta tylko z fasady.
// Nie musi znać klas: ArmyService, SupplyService, RavenService itd.
WarExpeditionFacade facade = new WarExpeditionFacade();

facade.PrepareExpedition(
    "Starkowie",
    "Winterfell",
    "Obrona Północy"
);

facade.PrepareExpedition(
    "Lannisterowie",
    "Królewska Przystań",
    "Zabezpieczenie tronu"
);


// =======================================================
// FASADA
// =======================================================

public class WarExpeditionFacade
{
    private readonly ArmyService _armyService;
    private readonly SupplyService _supplyService;
    private readonly RavenService _ravenService;
    private readonly CastleService _castleService;
    private readonly TreasuryService _treasuryService;

    public WarExpeditionFacade()
    {
        _armyService = new ArmyService();
        _supplyService = new SupplyService();
        _ravenService = new RavenService();
        _castleService = new CastleService();
        _treasuryService = new TreasuryService();
    }

    public void PrepareExpedition(string house, string location, string goal)
    {
        Console.WriteLine("=== PRZYGOTOWANIE WYPRAWY ===");
        Console.WriteLine($"Ród: {house}");
        Console.WriteLine($"Miejsce: {location}");
        Console.WriteLine($"Cel: {goal}");
        Console.WriteLine();

        _treasuryService.CheckTreasury(house);
        _armyService.GatherArmy(house);
        _supplyService.PrepareSupplies(house);
        _ravenService.SendRavens(house, goal);
        _castleService.CheckCastle(location);

        Console.WriteLine();
        Console.WriteLine($"Wyprawa rodu {house} została przygotowana.");
        Console.WriteLine("Niech chorągwie ruszą.");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine();
    }
}


// =======================================================
// PODSYSTEM 1: ARMIA
// =======================================================

public class ArmyService
{
    public void GatherArmy(string house)
    {
        Console.WriteLine($"Armia: zebrano wojowników rodu {house}.");
        Console.WriteLine("Armia: sprawdzono broń, tarcze i gotowość oddziałów.");
    }
}


// =======================================================
// PODSYSTEM 2: ZAPASY
// =======================================================

public class SupplyService
{
    public void PrepareSupplies(string house)
    {
        Console.WriteLine($"Zapasy: przygotowano żywność, wodę, konie i wozy dla rodu {house}.");
    }
}


// =======================================================
// PODSYSTEM 3: KRUKI
// =======================================================

public class RavenService
{
    public void SendRavens(string house, string goal)
    {
        Console.WriteLine($"Kruki: wysłano wiadomości do sojuszników rodu {house}.");
        Console.WriteLine($"Kruki: treść wiadomości dotyczy celu: {goal}.");
    }
}


// =======================================================
// PODSYSTEM 4: ZAMEK
// =======================================================

public class CastleService
{
    public void CheckCastle(string location)
    {
        Console.WriteLine($"Zamek: sprawdzono bramy, mury i straże w miejscu: {location}.");
    }
}


// =======================================================
// PODSYSTEM 5: SKARBIEC
// =======================================================

public class TreasuryService
{
    public void CheckTreasury(string house)
    {
        Console.WriteLine($"Skarbiec: sprawdzono środki rodu {house} na wyprawę.");
    }
}
