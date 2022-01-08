using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
			var pagesLinks = await Task.Run(() => GetPages(this));
			
			foreach(var page in pagesLinks)
			{
				_pages.Add(new CrawlerPage(page));
			}

			return _pages;
		}

		private HashSet<string> GetPages(CrawlerPage page)
		{
			string result = "";

			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage response = client.GetAsync(page.mainLink).Result)
				{
					using (HttpContent content = response.Content)
					{
						result = content.ReadAsStringAsync().Result;
					}
				}
			}

			var wikiLinksList = ParseLinks(result)
				.Where(x => x.Contains("/wiki/") && !x.Contains("https://") && !x.Contains(".jpg") &&
				            !x.Contains(".png"))
				.AsParallel()
				.ToList();

			var wikiLinksHashSet = new HashSet<string>();
			foreach(var wikiLink in wikiLinksList)
			{
				wikiLinksHashSet.Add("https://en.wikipedia.org" + wikiLink);
			}

			HashSet<string> ParseLinks(string html)
			{
				var doc = new HtmlDocument();
				doc.LoadHtml(html);
				var nodes = doc.DocumentNode.SelectNodes("//a[@href]");
				return nodes == null ? new HashSet<string>() : nodes.AsParallel().ToList().ConvertAll(
					   r => r.Attributes.AsParallel().ToList().ConvertAll(
					   i => i.Value)).SelectMany(j => j).AsParallel().ToHashSet();
			}

			return wikiLinksHashSet;
		}
	}
}
