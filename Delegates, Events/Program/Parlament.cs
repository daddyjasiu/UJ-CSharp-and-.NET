using System;
using System.Collections.Generic;

namespace Program
{
    public delegate void VoteBegin(string topic);
    public delegate void VoteEnd(KeyValuePair<int, int> result, int numberOfVoters);
    public class Parlament
    {
        private List<Parlamentarzysta> parlamentaristsList = new();
        
        public event VoteBegin OnVoteBegin;
        public event VoteEnd OnVoteEnd;

        public void StartVoting(int numberOfVoters, string topic)
        {
            OnVoteBegin?.Invoke(topic);
            
            KeyValuePair<int, int> result = VotingProccess(numberOfVoters);
            
            OnVoteEnd?.Invoke(result, numberOfVoters);   
        }

        KeyValuePair<int, int> VotingProccess(int numberOfVoters)
        {
            Console.WriteLine("\nGlosowanie...\n");
            int voteTrueCounter = 0;
            int voteFalseCounter = 0;

            FillVoters(numberOfVoters);
            
            string command;
            do
            {
                command = Console.ReadLine();

                if (command.Contains("GLOS"))
                {
                    int voterID = Int32.Parse(command.Substring(5))-1;
                    if (parlamentaristsList.Count > voterID && voterID >= 0)
                    {
                        if (parlamentaristsList[voterID].Vote())
                        {
                            voteTrueCounter++;
                        }
                        else
                        {
                            voteFalseCounter++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nie ma takiego ID glosujacego!");
                    }
                }
                else if(!command.Contains("GLOS") && !command.Contains("KONIEC"))
                {
                    Console.WriteLine("Zla komenda!");
                }
            } while (!command.Equals("KONIEC"));

            KeyValuePair<int, int> result = new(voteTrueCounter, voteFalseCounter);
            return result;
        }

        void FillVoters(int numberOfVoters)
        {
            for (int i = 0; i < numberOfVoters; i++)
            {
                parlamentaristsList.Add(new Parlamentarzysta());
                parlamentaristsList[i].OnGiveVote += RandomVote;
            }
        }
        
        bool RandomVote()
        {
            var randomNumber = new Random();
            bool result = randomNumber.Next(2)==1;
            return result;
        }
        
    }
}