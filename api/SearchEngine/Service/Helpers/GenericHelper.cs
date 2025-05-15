using HtmlAgilityPack;
using SearchEngine.Common.Model;
using SearchEngine.Service.Interface;
using System.Web;

namespace SearchEngine.Service.Helpers
{
    /// <summary>
    /// Generic web scraper for other types of search engines, may or may not be successful
    /// </summary>
    public class GenericHelper : IScraper
    {
        private readonly HttpClient _httpClient;
        public GenericHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool IsAllowed()
        {
            return true;
        }

        public async Task<IEnumerable<SearchEngineResultBase>> Scrape(string url, string searchTerm, string urlSearchId, bool usePlaywright = false)
        {
            // best guess as to the search querystring
            var searchUrl = $"{url}/search?q={HttpUtility.UrlEncode(searchTerm)}";

            var html = await _httpClient.GetStringAsync(searchUrl);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var resultLinks = new List<SearchEngineResultBase>();

            // generically search for links
            var nodes = doc.DocumentNode.SelectNodes("//a[starts-with(@href, 'https:')]");

            if (nodes != null)
            {
                for (var i = 0; i < nodes.Count; i++)
                {
                    var href = nodes[i].GetAttributeValue("href", string.Empty);
                    if (!string.IsNullOrEmpty(href) && href.Contains(urlSearchId))
                    {
                        resultLinks.Add(new SearchEngineResultBase{ Rank = i + 1, Url = href });
                    }
                }
            }

            return resultLinks.Distinct();
        }
    }
}
