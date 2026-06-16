using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocTemplate
{
    public class DocumentTemplate
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public string Content { get; set; }

        public DocumentTemplate Clone()
        {
            return new DocumentTemplate
            {
                Title = this.Title,
                Header = this.Header,
                Footer = this.Footer,
                Content = this.Content
            };
        }
        public void Print()
        {
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Header: " + Header);
            Console.WriteLine("Content: " + Content);
            Console.WriteLine("Footer: " + Footer);
            Console.WriteLine();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            var baseTemplate = new DocumentTemplate
            {
                Title = "Decyzja administracyjna",
                Header = "ZUS",
                Footer = "Dokument wygenerowany automatycznie",
                Content = "Treść decyzji bazowej...."
            };

            var decisionForClient = baseTemplate.Clone();

            decisionForClient.Title = "Decyzja administracyjna w sprawie świadczenia";
            decisionForClient.Content = "Treść decyzji dla klienta.... przyznanie świadczenia";

            var rejectionForClient = baseTemplate.Clone();
            rejectionForClient.Title = "Decyzja odmowna";
            rejectionForClient.Content = "Treść decyzji dla klienta.... odmowa świadczenia";

            baseTemplate.Print();
            decisionForClient.Print();
            rejectionForClient.Print();
        }
    }
}
