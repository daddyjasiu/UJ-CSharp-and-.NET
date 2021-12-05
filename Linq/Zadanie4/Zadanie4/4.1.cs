using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;

namespace Zadanie4
{
    public class Zadanie4_1 {

        public Zadanie4_1()
        {
            int N = Convert.ToInt32(Console.ReadLine());
                                
            List<int> numbers = Enumerable.Range(1, N).ToList();
            var numQuery = 
                from num in numbers
                where (num != 5 && num != 9 && (num % 2 != 0 || num % 7 == 0))
                select num*num;
            
            Console.WriteLine("Suma wszystkich elementow: {0}", numQuery.Sum());
            Console.WriteLine("Liczba wszystkich elementow: {0}",numQuery.Count());
            Console.WriteLine("Pierwszy element: {0}",numQuery.First());
            Console.WriteLine("Ostatni element: {0}",numQuery.Last());
            Console.WriteLine("Trzeci element: {0}",numQuery.ElementAt(2));
        }
    }
}