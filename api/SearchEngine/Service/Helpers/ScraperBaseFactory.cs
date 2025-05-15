using SearchEngine.Service.Interface;

namespace SearchEngine.Service.Helpers
{
    public class ScraperBaseFactory : IScraperBaseFactory
    {
        private readonly HttpClient _httpClient;

        public ScraperBaseFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public IScraperBase Create(bool usePlaywright)
        {
            if (usePlaywright)
            {
                return new PlaywrightScraper();
            }
            
            // default
            return new MetaRefreshScraper(_httpClient);
        }
    }
}
