using Microsoft.Playwright;
using SearchEngine.Common.Model;
using SearchEngine.Service.Interface;

namespace SearchEngine.Service.Helpers
{
    public class PlaywrightScraper : IScraperBase
    {
        public async Task<IEnumerable<SearchEngineResultBase>> Scrape(string url, string searchTerm, string urlSearchId)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync(url);

            // Try to click the agree button if it appears
            var agreeButton = await page.QuerySelectorAsync("button:has-text('Accept All')")
                              ?? await page.QuerySelectorAsync("div[role='none'] button");
            if (agreeButton != null)
            {
                await agreeButton.ClickAsync();
            }

            await page.GotoAsync($"{url}/search?num=100&q={Uri.EscapeDataString(searchTerm)}");

            // Proceed when CAPTCHA is resolved and page is ready
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
