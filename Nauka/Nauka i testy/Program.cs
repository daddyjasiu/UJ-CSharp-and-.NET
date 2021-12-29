using System;
using System.Collections.Generic;

namespace Nauka_i_testy
{
    public class Program
    {
    
    public static List<long> minimmCost(List<int> red, List<int> blue, int blueCost)
        {
            if(red.Count != blue.Count)
                return null;
            
            var result = new List<long>();
            bool isBlueLineActive = false;
        
            result.Add(0);
        
            for(int i=1; i<red.Count+1; i++)
            { 
                //if not on blue line, then pick between red and blue with cost
                if(!isBlueLineActive)
                {
                    //if the cost is the same, then pick whatever
                    if(red[i-1] == blue[i-1]+blueCost)
                    {
                        result.Add(result[i-1] + red[i-1]);
                        continue;
                    }
                    result.Add(result[i-1] + Math.Min(red[i-1], blue[i-1]+blueCost)); 
                }
                //if on blue line, then go with red or continue on blue without cost
                else
                {
                    //if the cost is the same, then pick whatever
                    if(red[i-1] == blue[i-1])
                    {
                        result.Add(result[i-1] + red[i-1]);
                        continue;
                    }
                    result.Add(result[i-1] + Math.Min(red[i-1], blue[i-1]));  
                }

                //check whether on blue or red line
                if(result[i] == blue[i-1]+blueCost)
                {
                    isBlueLineActive = true;
                }
                else if(result[i] == red[i-1])
                {
                    isBlueLineActive = false;
                }
            }
            
            return result;
        }

        public static void Main(string[] args)
        {
            var red = new List<int> {2, 3, 4};
            var blue = new List<int> {3, 1, 1};
            var cost = 2;

            var result = minimmCost(red, blue, cost);

            foreach (var i in result)
            {
                Console.Write(i + " ");
            }
        }
    }
}