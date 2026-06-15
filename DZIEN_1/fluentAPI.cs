using System;
using System.Collections.Generic;

namespace FluentApiDemo
{
    public class Wniosek
    {
        public string NumerSprawy { get; set; }
        public string Pesel { get; set; }
        public string TypSprawy { get; set; }
        public int LiczbaZalacznikow { get; set; }

        public Wniosek(
            string numerSprawy,
            string pesel,
            string typSprawy,
            int liczbaZalacznikow)
        {
            NumerSprawy = numerSprawy;
            Pesel = pesel;
            TypSprawy = typSprawy;
            LiczbaZalacznikow = liczbaZalacznikow;
        }
    }

    public class ValidationResult
    {
        private readonly List<string> _errors = new();

        public IReadOnlyList<string> Errors => _errors.AsReadOnly();

        public bool IsValid => _errors.Count == 0;

        public void AddError(string error)
        {
            _errors.Add(error);
        }

        public void Print()
        {
            if (IsValid)
            {
                Console.WriteLine("Wniosek jest poprawny.");
                return;
            }

            Console.WriteLine("Wniosek zawiera błędy:");

            foreach (var error in Errors)
            {
                Console.WriteLine($"- {error}");
            }
        }
    }

    public class WniosekValidator
    {
        private readonly List<Func<Wniosek, string?>> _rules = new();

        public WniosekValidator WymagajNumeruSprawy()
        {
            _rules.Add(wniosek =>
            {
                if (string.IsNullOrWhiteSpace(wniosek.NumerSprawy))
                {
                    return "Numer sprawy jest wymagany.";
                }

                return null;
            });

            return this;
        }

        public WniosekValidator WymagajPeselu()
        {
            _rules.Add(wniosek =>
            {
                if (string.IsNullOrWhiteSpace(wniosek.Pesel))
                {
                    return "PESEL jest wymagany.";
                }

                return null;
            });

            return this;
        }

        public WniosekValidator PeselMusiMiec11Znakow()
        {
            _rules.Add(wniosek =>
            {
                if (wniosek.Pesel.Length != 11)
                {
                    return "PESEL musi mieć dokładnie 11 znaków.";
                }

                return null;
            });

            return this;
        }

        public WniosekValidator PeselMusiSkladacSieZTylkoZCyfr()
        {
            _rules.Add(wniosek =>
            {
                foreach (var znak in wniosek.Pesel)
                {
                    if (!char.IsDigit(znak))
                    {
                        return "PESEL może zawierać tylko cyfry.";
                    }
                }

                return null;
            });

            return this;
        }

        public WniosekValidator WymagajTypuSprawy()
        {
            _rules.Add(wniosek =>
            {
                if (string.IsNullOrWhiteSpace(wniosek.TypSprawy))
                {
                    return "Typ sprawy jest wymagany.";
                }

                return null;
            });

            return this;
        }

        public WniosekValidator WymagajCoNajmniejJednegoZalacznika()
        {
            _rules.Add(wniosek =>
            {
                if (wniosek.LiczbaZalacznikow < 1)
                {
                    return "Wniosek musi mieć co najmniej jeden załącznik.";
                }

                return null;
            });

            return this;
        }

        public ValidationResult Validate(Wniosek wniosek)
        {
            var result = new ValidationResult();

            foreach (var rule in _rules)
            {
                string? error = rule(wniosek);

                if (error != null)
                {
                    result.AddError(error);
                }
            }

            return result;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Fluent API w C# ===");
            Console.WriteLine();

            var wniosek = new Wniosek(
                numerSprawy: "",
                pesel: "12345ABC",
                typSprawy: "Emerytura",
                liczbaZalacznikow: 0
            );

            var validator = new WniosekValidator()
                .WymagajNumeruSprawy()
                .WymagajPeselu()
                .PeselMusiMiec11Znakow()
                .PeselMusiSkladacSieZTylkoZCyfr()
                .WymagajTypuSprawy()
                .WymagajCoNajmniejJednegoZalacznika();

            var result = validator.Validate(wniosek);

            result.Print();

            Console.WriteLine();
            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć.");
            Console.ReadKey();
        }
    }
}
