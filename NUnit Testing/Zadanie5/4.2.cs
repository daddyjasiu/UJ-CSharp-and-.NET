using System;

namespace Zadanie5
{
    public class BetterStringAdder
    {
        public string Add(string s1, string s2)
        {
            if (s1 == null || s2 == null)
                throw new ArgumentNullException();

            return s1+s2;
        }
    }
}