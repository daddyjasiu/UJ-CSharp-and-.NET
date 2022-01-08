using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikipedia_Crawler
{
	internal class CrawlerPage
	{
		public string mainLink;
		private List<CrawlerPage> _pages = new();
		public CrawlerPage(string mainLink)
		{
			this.mainLink = mainLink;
		}

		public async Task<List<CrawlerPage>> GetPages()
		{
			var pagesLinks = await GetPages(this);
			
			foreach(var page in pagesLinks)
			{
				_pages.Add(new CrawlerPage(page));
			}

			return _pages;
		}

		private async Task<HashSet<string>> GetPages(CrawlerPage page)
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
			var htmlContent = await client.GetStringAsync(page.mainLink);

			HtmlDocument htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(htmlContent);
			var programmerLinks = htmlDoc.DocumentNode
				.Descendants("li")
				.Where(node => !node.GetAttributeValue("class", "").Contains("tocsection")).ToList();

			HashSet<string> wikiLinks = new();

			foreach (var link in programmerLinks)
			{
				if (link.FirstChild.Attributes.Count > 0)
					wikiLinks.Add("https://en.wikipedia.org/" + link.FirstChild.Attributes[0].Value);
			}

			return wikiLinks;
		}
	}
}
