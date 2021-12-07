using System;
using System.Collections.Generic;

namespace Nauka_i_testy
{
    public class Program
    {

        public static void Main(string[] args)
        {
            int[][] arr = {new []{1, 2}, new []{3, 4}, new []{5, 6}};

            foreach (var i in arr)
            {
                Console.Write(i);
            }
        }
    }
}