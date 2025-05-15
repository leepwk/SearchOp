using SearchEngine.Service.Interface;

namespace SearchEngine.Service.Helpers
{
    public class ScraperFactory : IScraperFactory
    {
        private readonly HttpClient _httpClient;
        private readonly IScraperBaseFactory _scraperBaseFactory;
        
        public ScraperFactory(HttpClient httpClient, IScraperBaseFactory scraperBaseFactory)
        {
            _httpClient = httpClient;
            _scraperBaseFactory = scraperBaseFactory;
        }
        
        public IScraper Create(string url)
        {
            if (url.Contains(SearchHelper.GoogleStr))
            {
                return new GoogleHelper(_httpClient, _scraperBaseFactory);
            }

            if (url.Contains(SearchHelper.BingStr))
            {
                return new BingHelper(_httpClient);
            }
            
            // default
            return new GenericHelper(_httpClient);
        }
    }
}
