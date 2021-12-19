using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Threads_and_HTTP_Requests
{

    class Program
    {
        private static List<Tuple<string, string>> _namesAndLinks = new();
        private static TimeSpan _sequentialTime;
        private static TimeSpan _pLinqTime;
        private static TimeSpan _tasksTime;
        
        class FileReader
        {
            public FileReader(string fileName)
            {
                var lines = System.IO.File.ReadAllLines(fileName);

                for(int i=1; i<lines.Length; i++)
                {
                    var nameLinkPair = lines[i].Split(';');
                    _namesAndLinks.Add(new Tuple<string, string>(nameLinkPair[0], nameLinkPair[1]));
                }
            }
        }
        static bool PingHost(string nameOrAddress)
        {
            var pinger = new Ping();
            PingReply reply = pinger.Send(nameOrAddress);
            var pingable = reply?.Status == IPStatus.Success;

            return pingable;
        }

        public static void PingParallelSequential()
        {
            int threadAmount = 4;
            
            static void PingLink(List<Tuple<string, string>> links)
            {
                foreach (var link in links)
                {
                    if (PingHost(link.Item2))
                        Console.WriteLine("OK!");
                    else
                        Console.WriteLine("NOT OK!");
                }
            }
            
            var links = _namesAndLinks
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / (_namesAndLinks.Count/threadAmount + 1))
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();

            Stopwatch stopwatch = new();
            stopwatch.Start();
            List<Thread> threads = new();
            for (int i = 0; i < threadAmount; i++)
            {
                var i1 = i;
                Thread t = new Thread(() => PingLink(links[i1]));
                threads.Add(t);
                t.Start();
            }

            foreach (var thread in threads)
                thread.Join();
            
            stopwatch.Stop();
            
            _sequentialTime = stopwatch.Elapsed;
        }
        
        public static void PingPLinq()
        {
            static void PingLink(string link)
            {
                if (PingHost(link))
                    Console.WriteLine("OK!");
                else
                    Console.WriteLine("NOT OK!");
            }

            Stopwatch stopwatch = new();
            stopwatch.Start();
            
            _namesAndLinks
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .ForAll(x => PingLink(x.Item2));
            stopwatch.Stop();

            _pLinqTime = stopwatch.Elapsed;
        }
        
        public static void PingTasks()
        {
            static Task PingLink(string link)
            {
                return new Task(() =>
                {
                    if (PingHost(link))
                        Console.WriteLine("OK!");
                    else 
                        Console.WriteLine("NOT OK!");
                });
            }

            Stopwatch stopwatch = new();
            stopwatch.Start();
            foreach (var link in _namesAndLinks)
            {
                PingLink(link.Item2).RunSynchronously();
            }

            stopwatch.Stop();
            _tasksTime = stopwatch.Elapsed;
        }
        
        static void Main(string[] args)
        {
            FileReader fileReader = new FileReader("ping.txt");

            //PingParallelSequential();
            //Console.WriteLine(_sequentialTime);
            //PingPLinq();
            //Console.WriteLine(_pLinqTime);
            //PingTasks();
            //Console.WriteLine(_tasksTime);
        }
    }
}