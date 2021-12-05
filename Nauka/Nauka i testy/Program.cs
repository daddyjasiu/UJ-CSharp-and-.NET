using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace Nauka_i_testy
{
    public class Program
    {
        public static bool IsAnagram(string s, string t)
        {

            if (s.Length != t.Length)
                return false;
            
            Dictionary<char, int> dictionary = new();
        
            foreach(var c in s)
            {
                if(!dictionary.ContainsKey(c))
                    dictionary.Add(c, 1);
                else
                    dictionary[c]++;
            }
            foreach(var c in t)
            {
                if(dictionary.ContainsKey(c))
                    dictionary[c]--;
            }
            foreach(var pair in dictionary)
            {
                if(pair.Value != 0)
                    return false;
            }
            return true;
        }
        
        public static void Main(string[] args)
        {
            Console.WriteLine(IsAnagram("a", "ab"));
        }
    }
}