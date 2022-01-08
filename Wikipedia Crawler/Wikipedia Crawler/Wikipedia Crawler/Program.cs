using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Wikipedia_Crawler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Crawler crawler = new (0, new HashSet<string>());
            crawler.CrawlParallelAsync("https://en.wikipedia.org/wiki/Toaster", "https://en.wikipedia.org/wiki/Bread");
            while(Globals.isCrawling){}
            Console.WriteLine("Route:");
            foreach(var item in crawler._visited)
                Console.WriteLine(item);
        }
    }
}