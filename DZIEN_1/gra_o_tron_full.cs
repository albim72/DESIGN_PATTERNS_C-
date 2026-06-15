using System;

namespace BuilderGraOTron
{
    public class Mission
    {
        public string HouseOrGroup { get; set; }
        public string Commander { get; set; }
        public string Goal { get; set; }
        public string Place { get; set; }
        public string Resources { get; set; }
        public string Ally { get; set; }
        public string RiskLevel { get; set; }
        public string MissionType { get; set; }

        public void ShowDescription()
        {
            Console.WriteLine("MISJA: " + Goal);
            Console.WriteLine("Ród / grupa: " + HouseOrGroup);
            Console.WriteLine("Dowódca: " + Commander);
            Console.WriteLine("Miejsce: " + Place);
            Console.WriteLine("Zasoby: " + Resources);
            Console.WriteLine("Sojusznik: " + Ally);
            Console.WriteLine("Ryzyko: " + RiskLevel);
            Console.WriteLine("Charakter misji: " + MissionType);
            Console.WriteLine();
        }
    }

    public class MissionBuilder
    {
        private Mission mission;

        public MissionBuilder()
        {
            mission = new Mission();
        }

        public MissionBuilder SetHouseOrGroup(string houseOrGroup)
        {
            mission.HouseOrGroup = houseOrGroup;
            return this;
        }

        public MissionBuilder SetCommander(string commander)
        {
            mission.Commander = commander;
            return this;
        }

        public MissionBuilder SetGoal(string goal)
        {
            mission.Goal = goal;
            return this;
        }

        public MissionBuilder SetPlace(string place)
        {
            mission.Place = place;
            return this;
        }

        public MissionBuilder SetResources(string resources)
        {
            mission.Resources = resources;
            return this;
        }

        public MissionBuilder SetAlly(string ally)
        {
            mission.Ally = ally;
            return this;
        }

        public MissionBuilder SetRiskLevel(string riskLevel)
        {
            mission.RiskLevel = riskLevel;
            return this;
        }

        public MissionBuilder SetMissionType(string missionType)
        {
            mission.MissionType = missionType;
            return this;
        }

        public Mission Build()
        {
            return mission;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Mission starkMission = new MissionBuilder()
                .SetHouseOrGroup("Starkowie")
                .SetCommander("Jon Snow")
                .SetGoal("Obrona Północy")
                .SetPlace("Winterfell")
                .SetResources("żywność, stal, zwiadowcy")
                .SetAlly("Dzicy")
                .SetRiskLevel("wysokie")
                .SetMissionType("misja obronna")
                .Build();

            Mission nightWatchMission = new MissionBuilder()
                .SetHouseOrGroup("Nocna Straż")
                .SetCommander("Jeor Mormont")
                .SetGoal("Zwiad za Murem")
                .SetPlace("za Murem")
                .SetResources("konie, pochodnie, broń")
                .SetAlly("brak")
                .SetRiskLevel("bardzo wysokie")
                .SetMissionType("misja rozpoznawcza")
                .Build();

            starkMission.ShowDescription();
            nightWatchMission.ShowDescription();

            Console.ReadKey();
        }
    }
}
