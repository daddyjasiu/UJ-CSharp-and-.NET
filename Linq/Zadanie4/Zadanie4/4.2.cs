using System;
using System.Collections.Generic;
using System.Linq;

namespace Zadanie4
{
    public class Zadanie4_2 {

        public Zadanie4_2()
        {
            int N = Convert.ToInt32(Console.ReadLine());
            int M = Convert.ToInt32(Console.ReadLine());
            
            Random random = new Random();

            var matrix = Enumerable
                .Range(0, N)
                .Select(i => Enumerable
                    .Repeat(0, M)
                    .Select(j => random.Next(0, 10))
                    .ToList())
                .ToList();

            var sum = matrix.SelectMany(x => x).Sum();
            Console.WriteLine("Suma: {0}", sum);
        }
    }
}