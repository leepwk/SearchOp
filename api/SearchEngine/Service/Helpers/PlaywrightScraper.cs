using Microsoft.Playwright;
using SearchEngine.Common.Model;
using SearchEngine.Service.Interface;

namespace SearchEngine.Service.Helpers
{
    /// <summary>
    /// Specific class for scraping a Google web search using a minimal browser
    /// Partially works but requires manual intervention to solve the CAPTCHA
    /// </summary>
    public class PlaywrightScraper : IScraperBase
    {
        public async Task<IEnumerable<SearchEngineResultBase>> Scrape(string url, string searchTerm, string urlSearchId)
        {
            // launch playwright browser
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync(url);

            // Click the agree button if it appears
            var agreeButton = await page.QuerySelectorAsync("button:has-text('Accept All')")
                              ?? await page.QuerySelectorAsync("div[role='none'] button");
            if (agreeButton != null)
            {
                await agreeButton.ClickAsync();
            }

            await page.GotoAsync($"{url}/search?num=100&q={Uri.EscapeDataString(searchTerm)}");

            // Proceed when CAPTCHA is resolved and page is ready
            // most reliable selector to find the links
            await page.WaitForSelectorAsync("cite:has-text('https:')");

            // Extract result links
            var urls = await page.EvalOnSelectorAllAsync<string[]>(
                "cite:has-text('https:')",
                @"els => els.map(el => { try { return el.innerText; } catch (e) { return null; }})");

            var resultLinks = new List<SearchEngineResultBase>();

            for (int i = 0; i < urls.Length; i++)
            {
                var href = urls[i];
                if (!string.IsNullOrEmpty(href) && href.Contains(urlSearchId))
                {
                    resultLinks.Add(new SearchEngineResultBase { Rank = i + 1, Url = href });
                }
            }
            
            return resultLinks.Distinct();
        }
    }
}
