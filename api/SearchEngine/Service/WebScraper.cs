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

        public WebScraper(ILogger<WebScraper> logger, HttpClient httpClient, IOptions<AppSettings> appSettings) : base(logger)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public async Task<IEnumerable<SearchEngineResultBase>> FetchByUrlTerms(string url, string searchTerm, bool usePlaywright)
        {
            IScraper scraper = null;

            if (url.Contains(SearchHelper.GoogleStr))
            {
                scraper = new GoogleHelper(_httpClient);
            }
            else if (url.Contains(SearchHelper.BingStr))
            {
                scraper = new BingHelper(_httpClient);
            }
            else
            {
                // default
                scraper = new GenericHelper(_httpClient);
            }

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
