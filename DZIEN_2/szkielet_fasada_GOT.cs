using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("ZADANIE: WZORZEC FASADA - GRA O TRON");
Console.WriteLine("------------------------------------\n");

// TODO:
// Utwórz obiekt fasady.

// TODO:
// Wywołaj metodę PrepareExpedition.


// =======================================================
// FASADA
// =======================================================

public class WarExpeditionFacade
{
    // TODO:
    // Dodaj prywatne pola:
    // ArmyService
    // SupplyService
    // RavenService
    // CastleService


    public WarExpeditionFacade()
    {
        // TODO:
        // Utwórz obiekty klas podsystemu.
    }


    public void PrepareExpedition(string house, string location, string goal)
    {
        // TODO:
        // Wypisz dane wyprawy:
        // ród, miejsce, cel.

        // TODO:
        // Wywołaj metody klas podsystemu:
        // GatherArmy
        // PrepareSupplies
        // SendRavens
        // CheckCastle

        // TODO:
        // Wypisz komunikat końcowy.
    }
}


// =======================================================
// PODSYSTEM: ARMIA
// =======================================================

public class ArmyService
{
    public void GatherArmy(string house)
    {
        // TODO:
        // Wypisz komunikat o zebraniu wojowników.
    }
}


// =======================================================
// PODSYSTEM: ZAPASY
// =======================================================

public class SupplyService
{
    public void PrepareSupplies(string house)
    {
        // TODO:
        // Wypisz komunikat o przygotowaniu zapasów.
    }
}


// =======================================================
// PODSYSTEM: KRUKI
// =======================================================

public class RavenService
{
    public void SendRavens(string house, string goal)
    {
        // TODO:
        // Wypisz komunikat o wysłaniu kruków.
    }
}


// =======================================================
// PODSYSTEM: ZAMEK
// =======================================================

public class CastleService
{
    public void CheckCastle(string location)
    {
        // TODO:
        // Wypisz komunikat o sprawdzeniu gotowości zamku.
    }
}
