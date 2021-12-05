using System;
using System.Collections.Generic;
using System.Reflection;

namespace Program
{
    class Program
    {
        public static void VotingStart(string topic)
        {
            Console.WriteLine("Glosowanie nad: " + topic);
        }
        
        public static void VotingEnd(KeyValuePair<int, int> result, int numberOfVoters)
        {
            Console.WriteLine("Glosow za: " + result.Key + "\nGlosow przeciw: " + result.Value);
        }
        
        static void Main(string[] args)
        {
            Parlament parlament = new Parlament();
            parlament.OnVoteBegin += VotingStart;
            parlament.OnVoteEnd += VotingEnd;
            
            string command;
            command = Console.ReadLine();
            
            if(command.Contains("POCZATEK"))
            {
                string title = command.Substring(9);
                parlament.StartVoting(5, title); 
            }

            Console.ReadKey();
        }
    }
}
