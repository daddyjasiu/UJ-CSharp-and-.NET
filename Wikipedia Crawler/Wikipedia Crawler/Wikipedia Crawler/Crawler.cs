using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikipedia_Crawler
{
    internal class Crawler
    {
        private int _currDepth;
        public HashSet<string> _visited;

        public Crawler(int currDepth, HashSet<string> visited)
        {
            _currDepth = currDepth;
            _visited = visited;
        }

        public Task CrawlParallelAsync(string sourceLink, string destinationLink)
        {
            Task task = Task.Run(() =>
            {
                if (Globals.isCrawling)
                {
                    var currPage = new CrawlerPage(sourceLink);
                    var destinationPage = new CrawlerPage(destinationLink); ;

                    var currPageSubpages = currPage.GetPages();

                    if (String.Equals(currPage.mainLink, destinationPage.mainLink))
                    {
                        Console.Write("Link found at depth: ");
                        Console.WriteLine(_currDepth);
                        _visited.Add(currPage.mainLink);
                        Globals.isCrawling = false;
                    }

                    if (_visited.Contains(currPage.mainLink))
                        return;

                    _visited.Add(currPage.mainLink);

                    List<Task> taskList = new();
                    taskList.AsParallel();

                    if (Globals.isCrawling)
                    {
                        foreach (var page in currPageSubpages)
                        {
                            if (!_visited.Contains(page.mainLink))
                            {
                                Crawler newCrawler = new(_currDepth + 1, _visited);
                                taskList.Add(newCrawler.CrawlParallelAsync(page.mainLink, destinationLink));
                            }
                        }
                    }
                }
            });
            
            return task;
        }
    }
}