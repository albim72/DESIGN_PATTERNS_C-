using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderDemo
{
    public class DecyzjaAdministracyjna
    {
        public string NumerSprawy { get; }
        public string Obywatel { get; }
        public string TypSprawy { get; }
        public DateTime DataWydania { get; }
        public IReadOnlyList<string> Sekcje { get; }
        public string Pouczenie { get; }
        public string Podpis { get; }

        public DecyzjaAdministracyjna(
            string numerSprawy,
            string obywatel,
            string typSprawy,
            DateTime dataWydania,
            IReadOnlyList<string> sekcje,
            string pouczenie,
            string podpis)
        {
            NumerSprawy = numerSprawy;
            Obywatel = obywatel;
            TypSprawy = typSprawy;
            DataWydania = dataWydania;
            Sekcje = sekcje;
            Pouczenie = pouczenie;
            Podpis = podpis;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine("====================================");
            builder.AppendLine("DECYZJA ADMINISTRACYJNA");
            builder.AppendLine("====================================");
            builder.AppendLine($"Numer sprawy: {NumerSprawy}");
            builder.AppendLine($"Obywatel: {Obywatel}");
            builder.AppendLine($"Typ sprawy: {TypSprawy}");
            builder.AppendLine($"Data wydania: {DataWydania:yyyy-MM-dd}");
            builder.AppendLine();

            builder.AppendLine("Treść decyzji:");
            foreach (var sekcja in Sekcje)
            {
                builder.AppendLine($"- {sekcja}");
            }

            builder.AppendLine();
            builder.AppendLine($"Pouczenie: {Pouczenie}");
            builder.AppendLine($"Podpis: {Podpis}");
            builder.AppendLine("====================================");

            return builder.ToString();
        }
    }

    public class DecyzjaAdministracyjnaBuilder
    {
        private string? _numerSprawy;
        private string? _obywatel;
        private string? _typSprawy;
        private DateTime _dataWydania = DateTime.Now;
        private readonly List<string> _sekcje = new();
        private string _pouczenie = "Brak pouczenia.";
        private string _podpis = "Brak podpisu.";

        public DecyzjaAdministracyjnaBuilder DlaSprawy(string numerSprawy)
        {
            _numerSprawy = numerSprawy;
            return this;
        }

        public DecyzjaAdministracyjnaBuilder DlaObywatela(string obywatel)
        {
            _obywatel = obywatel;
            return this;
        }

        public DecyzjaAdministracyjnaBuilder TypSprawy(string typSprawy)
        {
            _typSprawy = typSprawy;
            return this;
        }

        public DecyzjaAdministracyjnaBuilder ZDataWydania(DateTime dataWydania)
        {
            _dataWydania = dataWydania;
            return this;
        }

        public DecyzjaAdministracyjnaBuilder DodajSekcje(string trescSekcji)
        {
            _sekcje.Add(trescSekcji);
            return this;
        }

        public DecyzjaAdministracyjnaBuilder ZPouczeniem(string pouczenie)
        {
            _pouczenie = pouczenie;
            return this;
        }

        public DecyzjaAdministracyjnaBuilder ZPodpisem(string podpis)
        {
            _podpis = podpis;
            return this;
        }

        public DecyzjaAdministracyjna Build()
        {
            if (string.IsNullOrWhiteSpace(_numerSprawy))
            {
                throw new InvalidOperationException("Numer sprawy jest wymagany.");
            }

            if (string.IsNullOrWhiteSpace(_obywatel))
            {
                throw new InvalidOperationException("Obywatel jest wymagany.");
            }

            if (string.IsNullOrWhiteSpace(_typSprawy))
            {
                throw new InvalidOperationException("Typ sprawy jest wymagany.");
            }

            if (_sekcje.Count == 0)
            {
                throw new InvalidOperationException("Decyzja musi mieć co najmniej jedną sekcję.");
            }

            return new DecyzjaAdministracyjna(
                _numerSprawy,
                _obywatel,
                _typSprawy,
                _dataWydania,
                _sekcje.AsReadOnly(),
                _pouczenie,
                _podpis
            );
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Wzorzec Builder w C# ===");
            Console.WriteLine();

            var decyzja = new DecyzjaAdministracyjnaBuilder()
                .DlaSprawy("ZUS/2026/001")
                .DlaObywatela("Jan Kowalski")
                .TypSprawy("Emerytura")
                .ZDataWydania(new DateTime(2026, 6, 15))
                .DodajSekcje("Po analizie dokumentów przyznano świadczenie.")
                .DodajSekcje("Wysokość świadczenia została obliczona zgodnie z obowiązującymi zasadami.")
                .DodajSekcje("Decyzja została wydana na podstawie dostarczonych dokumentów.")
                .ZPouczeniem("Od decyzji przysługuje odwołanie w terminie 30 dni.")
                .ZPodpisem("Dyrektor Oddziału")
                .Build();

            Console.WriteLine(decyzja);

            Console.WriteLine();
            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć.");
            Console.ReadKey();
        }
    }
}
