using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wikipedia_Crawler
{
	internal class Crawler
	{
		private int _maxDepth;
		private int _currDepth;

		public Crawler(int maxDepth)
		{
			_currDepth = 0;
			_maxDepth = maxDepth;
		}
		
		public async Task CrawlParallelAsync(string sourceLink, string destinationLink)
		{
			var sourcePage = new CrawlerPage(sourceLink);
			var destinationPage = new CrawlerPage(destinationLink);
			var visited = new HashSet<string>();
			
			Queue <CrawlerPage> queue = new();
			queue.Enqueue(sourcePage);

			while (queue.Count > 0)
			{
				var currPage = queue.Dequeue();
				Console.WriteLine(currPage.mainLink);
				
				var currPageSubpages = await Task.Run(() => currPage.GetPages());

				if (currPage.mainLink == destinationPage.mainLink || _currDepth == _maxDepth)
				{
					visited.Add(currPage.mainLink);
					break;
				}

				if (visited.Contains(currPage.mainLink))
					continue;

				visited.Add(currPage.mainLink);
				
				foreach (var page in currPageSubpages)
				{
					if (!visited.Contains(page.mainLink))
					{
						queue.Enqueue(page);
					}
				}
			}

			foreach (var visitedPage in visited)
			{
				Console.WriteLine(visitedPage);
			}
		}
	}
}
