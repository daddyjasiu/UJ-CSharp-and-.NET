using System;
using System.Linq;

namespace Zadanie5
{
    public interface IAnagramChecker
    {
        /*Sprawdza czy jedno slowo jest anagramem drugiego.
        * Wszystkie niealfanumeryczne znaki są ignorowane.
        * Wielkość liter nie ma znaczenia.
        * word1 - dowolny niepusty string różny od null.
        * word2 - dowolny niepusty string różny od null.
        * Zwraca true wtedy i tylko wtedy gdy word1 jest anagramem word2.
        */
        bool IsAnagram(string word1, string word2);
    }
    
    public class AnagramChecker : IAnagramChecker{
        
        public bool IsAnagram(string word1, string word2)
        {
            if (string.IsNullOrEmpty(word1) || string.IsNullOrEmpty(word2))
                throw new ArgumentNullException();

            word1 = word1.ToLower();
            word2 = word2.ToLower();
            
            var word1Set = word1.ToCharArray().ToHashSet();

            for (int i = 0; i < word1.Length; i++)
            {
                if(word2[i] >= 'a' && word2[i] <= 'z')
                    if (!word1Set.Contains(word2[i]))
                        return false;
            }
            
            return true;
        }
    }
}