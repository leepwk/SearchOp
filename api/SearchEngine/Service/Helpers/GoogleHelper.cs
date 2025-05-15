using SearchEngine.Common.Model;
using SearchEngine.Service.Interface;

namespace SearchEngine.Service.Helpers
{
    public class GoogleHelper : IScraper
    {
        private readonly HttpClient _httpClient;
        public GoogleHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // required to bypass disclaimer
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
        }

        public bool IsAllowed()
        {
            return true;
        }

        public async Task<IEnumerable<SearchEngineResultBase>> Scrape(string url, string searchTerm, string urlSearchId, bool usePlaywright = false)
        {
            IScraperBase scraper = null;
            if (usePlaywright)
            {
                scraper = new PlaywrightScraper();
            }
            else
            {
                scraper = new MetaRefreshScraper(_httpClient);
            }

            return await scraper.Scrape(url, searchTerm, urlSearchId);
        }

    }
}
