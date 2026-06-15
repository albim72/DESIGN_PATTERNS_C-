using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Singleton użyty jako jedna istancja klasy konfigurującej system, która jest używana w wielu miejscach w aplikacji. Singleton zapewnia, że istnieje tylko jedna instancja tej klasy i umożliwia globalny dostęp do niej.

namespace Singleton
{

    public class DocumentGenerator
    {

       public sealed class AppConfiguration { 
            private static readonly Lazy<AppConfiguration> _instance =
                new Lazy<AppConfiguration>(() => new AppConfiguration());

            public static AppConfiguration Instance => _instance.Value;

            public string ApplicationName { get;}
            public string EnvironmentName { get; private set; }
            public string DefaultDocumentFormat { get;}
            public DateTime CreatedAt { get;}

            private AppConfiguration()
            {
                Console.WriteLine("Tworzenie instancji AppConfiguration...[obiekt konfiguracji]");
                ApplicationName = "Document Generator ZUS";
                EnvironmentName = "TEST";
                DefaultDocumentFormat = "PDF";
                CreatedAt = DateTime.Now;
            }

            public void ChangeEnvironment(string environmentName)
            {
                if (string.IsNullOrWhiteSpace(environmentName))
                {
                    throw new ArgumentException("Nazwa środowiska nie może być pusta.", nameof(environmentName));
                }
                EnvironmentName = environmentName;
            }

            public void PrintConfiguration()
            {
                Console.WriteLine($"Nazwa aplikacji: {ApplicationName}");
                Console.WriteLine($"Środowisko: {EnvironmentName}");
                Console.WriteLine($"Domyślny format dokumentu: {DefaultDocumentFormat}");
                Console.WriteLine($"Utworzono: {CreatedAt}");
            }
        }
       public void GenerateDecision(string caseNumber)
        {
            // Logika generowania decyzji na podstawie numeru sprawy
            Console.WriteLine($"Generowanie decyzji dla sprawy: {caseNumber}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
