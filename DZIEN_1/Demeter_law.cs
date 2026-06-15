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
    }

    public class Obywatel
    {
        public string Imie { get; }
        public string Nazwisko { get; }
        public Adres Adres { get; }
    }

    public class Adres
    {
        public string Ulica { get; }
        public Miasto Miasto { get; }

    }

    public class Miasto
    {
        public string Nazwa { get; }


    }
}
