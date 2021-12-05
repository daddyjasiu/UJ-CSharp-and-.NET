using System;
using System.Collections.Generic;
using System.Linq;

namespace Zadanie4
{
    public class City
    {
        private string _name;
        private char _firstLetter;

        public string Name { get;}
        public char FirstLetter { get;}

        public City(string name, char firstLetter)
        {
            Name = name;
            FirstLetter = firstLetter;
        }
    }
    public class Zadanie4_3 {

        public void PrintCountries(char letter, Dictionary<char, List<City>> cityQuery)
        {
            if (cityQuery.Keys.Contains(letter))
            {
                foreach (var city in cityQuery[letter])
                    Console.Write($"{city.Name} ");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("PUSTE");
            }
        }
        public Zadanie4_3()
        {
            var cityList = new List<City>();
            
            string inputCity;
            do
            {
                inputCity = Console.ReadLine();
                if(inputCity != null && inputCity != "X")
                    cityList.Add(new City(inputCity, inputCity[0]));
                
            } while (!inputCity.Equals("X"));
            
            var cityQuery = cityList
                .OrderBy(x => x.Name)
                .GroupBy(x => x.FirstLetter)
                .ToDictionary(g => g.Key, g => g.ToList());

            string inputLetter;
            do
            {
                inputLetter  = Console.ReadLine();
                if(inputLetter != null && inputLetter != "X")    
                    PrintCountries(inputLetter[0], cityQuery);

            } while (!inputLetter.Equals("X"));
            
        }
    }
}