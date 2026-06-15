using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demeter_law
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class  Sprawa
    {
        public string NumerSprawy { get; }
        public Obywatel Obywatel { get; }

        public Sprawa(string numerSprawy, Obywatel obywatel)
        {
            NumerSprawy = numerSprawy;
            Obywatel = obywatel;
        }

        public string PobierzMiastoObywatela()
        {
            return Obywatel.PobierzMiasto();
        }
    }

    public class Obywatel
    {
        public string Imie { get; }
        public string Nazwisko { get; }
        public Adres Adres { get; }

        public Obywatel(string imie, string nazwisko, Adres adres)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Adres = adres;
        }

        public string PobierzMiasto()
        {
            return Adres.PobierzNazweMiasta();
        }
    }

    public class Adres
    {
        public string Ulica { get; }
        public Miasto Miasto { get; }

        public Adres(string ulica, Miasto miasto)
        {
            Ulica = ulica;
            Miasto = miasto;
        }

        public string PobierzNazweMiasta()
        {
            return Miasto.Nazwa;
        }

    }

    public class Miasto
    {
        public string Nazwa { get; }
        public Miasto(string nazwa)
        {
            Nazwa = nazwa;
        }


    }
}
