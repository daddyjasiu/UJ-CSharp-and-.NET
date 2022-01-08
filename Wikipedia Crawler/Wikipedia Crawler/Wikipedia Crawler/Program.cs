namespace Wikipedia_Crawler // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Crawler crawler = new (10);
            //await crawler.CrawlParallelAsync("https://en.wikipedia.org/wiki/World_War_II", "https://en.wikipedia.org/wiki/Axis_powers");

            CrawlerPage page = new
                ("https://en.wikipedia.org/wiki/Axis_powers");
            var pages = await page.GetPages();

            foreach(var link in pages)
			{
                Console.WriteLine(link.mainLink);
			}
        
        }
    }
}