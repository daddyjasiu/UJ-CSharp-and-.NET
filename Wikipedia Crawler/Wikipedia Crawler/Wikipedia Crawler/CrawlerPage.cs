using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public List<CrawlerPage> GetPages()
        {
            WebClient web = new();
            System.IO.Stream stream = web.OpenRead(mainLink);
            using (System.IO.StreamReader reader = new (stream))
            {
                string text = reader.ReadToEnd();
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(text);

                var links = htmlDocument.DocumentNode.SelectNodes("//p/a[@href]");
                if (links != null)
                {
                    List<string> urlList = new();

                    foreach (var link in links)
                    {
                        string hrefValue = link.GetAttributeValue("href", string.Empty);

                        if (hrefValue.StartsWith("/wiki"))
                        {
                            if (!urlList.Contains(hrefValue))
                            {
                                urlList.Add(hrefValue);
                            }
                        }
                    }

                    foreach (var link in urlList)
                    {
                        _pages.Add(new CrawlerPage("https://en.wikipedia.org" + link));
                    }
                }
            }

            return _pages;
        }
    }
}