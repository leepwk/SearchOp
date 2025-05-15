using SearchEngine.Common.Model;
using SearchEngine.Service.Interface;

namespace SearchEngine.Service.Helpers
{
    /// <summary>
    /// Specific class for scraping a Google web search
    /// Two methods where attempted, before more success was achieved with using Playwright
    /// </summary>
    public class GoogleHelper : IScraper
    {
        private readonly HttpClient _httpClient;
        private readonly IScraperBaseFactory _scraperBaseFactory;

        public GoogleHelper(HttpClient httpClient, IScraperBaseFactory scraperBaseFactory)
        {
            _httpClient = httpClient;
            _scraperBaseFactory = scraperBaseFactory;
            // required to bypass disclaimer
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
        }

        public bool IsAllowed()
        {
            // TODO: check the web site terms and condition
            return true;
        }

        public async Task<IEnumerable<SearchEngineResultBase>> Scrape(string url, string searchTerm, string urlSearchId, bool usePlaywright = false)
        {
            // using Playwright browser will require manually intervention to get through the CAPTCHA pages
            IScraperBase scraper = _scraperBaseFactory.Create(usePlaywright);

            return await scraper.Scrape(url, searchTerm, urlSearchId);
        }

    }
}
