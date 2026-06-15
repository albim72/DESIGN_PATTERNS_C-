using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Singleton.DocumentGenerator;

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
            AppConfiguration configuration = AppConfiguration.Instance;
            // Logika generowania decyzji na podstawie numeru sprawy
            Console.WriteLine($"Generowanie decyzji administracyjnej dla sprawy: {caseNumber}");
            Console.WriteLine($"Używana aplikacja: {configuration.ApplicationName}");
            Console.WriteLine($"Używany format dokumentu: {configuration.DefaultDocumentFormat}");
            Console.WriteLine($"Środowisko: {configuration.EnvironmentName}");
        }
    }

    internal class Program
    {
  
        static void Main(string[] args)
        {
            Console.WriteLine("Tworzenie instancji DocumentGenerator...WZORZEC SINGLETON");
            Console.WriteLine("________________________________________________");
            AppConfiguration config1 = AppConfiguration.Instance;
            AppConfiguration config2 = AppConfiguration.Instance;

            Console.WriteLine($"Czy config1 i config2 to ta sama instancja? {ReferenceEquals(config1, config2)}");

            config1.PrintConfiguration();
            Console.WriteLine("_________________________________");
            config2.PrintConfiguration();


            Console.WriteLine("Zmiana środowiska na 'PROD' za pomocą config1...");
            config1.ChangeEnvironment("PROD");

            config2.PrintConfiguration();

            //generowanie dokumentu decyzji administracyjnej
            var generator = new DocumentGenerator();
            generator.GenerateDecision("ZUS/12345/2024");

            Console.WriteLine(generator.GetType().Name);

            Console.WriteLine("Singleton Test");

            SingletonTest singletonTest1 = SingletonTest.Instance;
            SingletonTest singletonTest2 = SingletonTest.Instance;

            Console.WriteLine($"Czy singletonTest1 i singletonTest2 to ta sama instancja? {ReferenceEquals(singletonTest1, singletonTest2)}");


        }
    }
}
