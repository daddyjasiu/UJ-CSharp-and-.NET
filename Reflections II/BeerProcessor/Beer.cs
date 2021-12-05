using Common;
using System;

namespace BeerProcessor
{
    public class Beer : IZlecenie
    {
        public string Tytul { get; set; }
        public void Process()
        {
            Console.WriteLine("\nPrzepis na: " + Tytul);
            Console.WriteLine("Wez szklanke");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Otworz butelke piwa West Coast IPA");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Nalej piwa do szklanki");
            Console.WriteLine("Gotowe");
        }
    }
}