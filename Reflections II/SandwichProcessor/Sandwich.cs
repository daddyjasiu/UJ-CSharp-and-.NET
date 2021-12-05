using System;
using Common;

namespace SandwichProcessor
{
    public class Sandwich : IZlecenie
    {
        public string Tytul { get; set; }
        public void Process()
        {
            Console.WriteLine("\nPrzepis na: " + Tytul);
            Console.WriteLine("Pokroj chleb");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Posmaruj chleb maslem");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Poloz ser na kromke");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Poloz salami na kromke");
            Console.WriteLine("Gotowe");
        }
    }
}