using System;
using System.Collections.Generic;

namespace WesterosBuilderExercise
{
    // PRODUKT: gotowa misja, którą budujemy krok po kroku.
    public class Mission
    {
        // TODO 1: Uzupełnij właściwości misji.
        // Wskazówka: użyj typu string dla większości pól.
        public string HouseOrGroup { get; set; } = "";
        public string Commander { get; set; } = "";
        public string Goal { get; set; } = "";
        public string Place { get; set; } = "";
        public List<string> Resources { get; set; } = new List<string>();
        public string Ally { get; set; } = "";
        public string RiskLevel { get; set; } = "";

        public override string ToString()
        {
            // TODO 2: Zbuduj czytelny opis misji.
            // Wskazówka: połącz elementy tekstu oraz użyj string.Join dla zasobów.
            // Przykład składni: string.Join(", ", Resources)
            return "TODO: tutaj powinien pojawić się opis misji";
        }
    }

    // BUILDER: obiekt odpowiedzialny za etapowe składanie misji.
    public class MissionBuilder
    {
        private Mission _mission = new Mission();

        public MissionBuilder SetHouseOrGroup(string houseOrGroup)
        {
            // TODO 3: Przypisz houseOrGroup do odpowiedniej właściwości obiektu _mission.
            return this;
        }

        public MissionBuilder SetCommander(string commander)
        {
            // TODO 4: Przypisz commander do odpowiedniej właściwości obiektu _mission.
            return this;
        }

        public MissionBuilder SetGoal(string goal)
        {
            // TODO 5: Przypisz goal do odpowiedniej właściwości obiektu _mission.
            return this;
        }

        public MissionBuilder SetPlace(string place)
        {
            // TODO 6: Przypisz place do odpowiedniej właściwości obiektu _mission.
            return this;
        }

        public MissionBuilder AddResource(string resource)
        {
            // TODO 7: Dodaj pojedynczy zasób do listy Resources.
            return this;
        }

        public MissionBuilder SetAlly(string ally)
        {
            // TODO 8: Przypisz ally do odpowiedniej właściwości obiektu _mission.
            return this;
        }

        public MissionBuilder SetRiskLevel(string riskLevel)
        {
            // TODO 9: Przypisz riskLevel do odpowiedniej właściwości obiektu _mission.
            return this;
        }

        public Mission Build()
        {
            // TODO 10: Zwróć gotową misję.
            // Dodatkowe wyzwanie: po zwróceniu obiektu możesz zresetować builder,
            // aby dało się go bezpiecznie wykorzystać ponownie.
            throw new NotImplementedException();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // TODO 11: Utwórz pierwszą misję przy pomocy MissionBuilder.
            // Przykład tematu: Starkowie, Obrona Północy, Winterfell.
            // Uwaga: nie używaj dużego konstruktora z wieloma parametrami.

            Mission mission1 = null;

            // TODO 12: Utwórz drugą misję przy pomocy MissionBuilder.
            // Przykład tematu: Nocna Straż, Zwiad za Murem, za Murem.

            Mission mission2 = null;

            // TODO 13: Wypisz obie misje w konsoli.
            Console.WriteLine("=== MISJE WESTEROS ===");
            Console.WriteLine(mission1);
            Console.WriteLine();
            Console.WriteLine(mission2);

            // TODO 14: Odpowiedz ustnie lub w komentarzu:
            // Dlaczego wzorzec Builder pasuje do budowania misji?
        }
    }
}
