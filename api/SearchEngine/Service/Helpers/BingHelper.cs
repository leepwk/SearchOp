using Microsoft.Playwright;
using SearchEngine.Common.Model;
using SearchEngine.Service.Interface;
using System.Web;

namespace SearchEngine.Service.Helpers
{
    public class BingHelper : IScraper
    {
        private readonly HttpClient _httpClient;
        private const int pageLength = 10;
        public BingHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
        }

        public bool IsAllowed()
        {
            return true;
        }

        public async Task<IEnumerable<SearchEngineResultBase>> Scrape(string url, string searchTerm, string urlSearchId, bool usePlaywright = false)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            var searchUrlTemplate = $"{url}/search?q={HttpUtility.UrlEncode(searchTerm)}&count={pageLength}&first=";

            int pageNumber = 0;
            bool hasMoreResults = true;

            var resultLinks = new List<SearchEngineResultBase>();

            while (hasMoreResults)
            {
                int first = pageNumber * pageLength + 1; // Bing uses For the first page, first = 1, for second page, first = pageLength + 1, etc.
                var searchUrl = $"{searchUrlTemplate}{first}&t={Guid.NewGuid()}";

                await page.GotoAsync(searchUrl);

                await page.WaitForSelectorAsync("cite:has-text('https:')");

                // Extract result links
                var urls = await page.EvalOnSelectorAllAsync<string[]>(
                    "cite:has-text('https:')",
                    @"els => els.map(el => { try { return el.innerText; } catch (e) { return null; }})");

                for (int i = 0; i < urls.Length; i++)
                {
                    var href = urls[i];
                    if (!string.IsNullOrEmpty(href) && href.Contains(urlSearchId))
                    {
                        resultLinks.Add(new SearchEngineResultBase { Rank = first + i, Url = href });
                    }
                }

                if (pageNumber * pageLength >= 100)
                {
                    hasMoreResults = false;  // stop pagination
                }

                pageNumber++;
            }

            return resultLinks.Distinct();
        }
    }
}
