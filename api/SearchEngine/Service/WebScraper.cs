using Microsoft.Extensions.Options;
using SearchEngine.Common.Model;
using SearchEngine.Service.Helpers;
using SearchEngine.Service.Interface;

namespace SearchEngine.Service
{
    /// <summary>
    /// Decision making class to determine which web scraping helper class to instantiate
    /// </summary>
    public class WebScraper : BaseLogger<WebScraper>, IWebScraper
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        private readonly IScraperFactory _scraperFactory;

        public WebScraper(ILogger<WebScraper> logger, HttpClient httpClient, IOptions<AppSettings> appSettings, IScraperFactory scraperFactory) : base(logger)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
            _scraperFactory = scraperFactory;
        }

        public async Task<IEnumerable<SearchEngineResultBase>> FetchByUrlTerms(string url, string searchTerm, bool usePlaywright)
        {
            var scraper = _scraperFactory.Create(url);

            // Check the terms and conditions of the search engine but override by config setting
            if (!_appSettings.OverrideTerms && !scraper.IsAllowed())
            {
                var errMsg = $"{url} is not allowed to be scraped";
                Logger.LogInformation(errMsg);
                return new List<SearchEngineResultBase>();
            }

            return await scraper.Scrape(url, searchTerm, _appSettings.UrlSearchId, usePlaywright);
        }
    }
}
