using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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

			Queue<CrawlerPage> queue = new();
			queue.Enqueue(sourcePage);

			while (queue.Count > 0)
			{
				_currDepth++;
				var currPage = queue.Dequeue();

				if (currPage.mainLink == destinationPage.mainLink)
				{
					visited.Add(currPage.mainLink);
					break;
				}

				if (visited.Contains(currPage.mainLink))
					continue;

				visited.Add(currPage.mainLink);

				foreach (var neighbor in await currPage.GetPages())
				{
					if (!visited.Contains(neighbor.mainLink))
					{
						queue.Enqueue(neighbor);
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
