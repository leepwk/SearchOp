using HtmlAgilityPack;
using SearchEngine.Common.Model;
using SearchEngine.Service.Interface;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace SearchEngine.Service.Helpers
{
    public class MetaRefreshScraper : IScraperBase
    {
        private readonly HttpClient _httpClient;

        public MetaRefreshScraper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SearchEngineResultBase>> Scrape(string url, string searchTerm, string urlSearchId)
        {
            var searchUrl = $"{url}/search?num=100&q={HttpUtility.UrlEncode(searchTerm)}";
            var html = await AllowMetaRefresh(searchUrl);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var resultLinks = new List<SearchEngineResultBase>();

            var nodes = doc.DocumentNode.SelectNodes("//cite[contains(text(), 'https:')]");

            if (nodes != null)
            {
                for (var i = 0; i < nodes.Count; i++)
                {
                    var href = nodes[i].InnerText;
                    if (!string.IsNullOrEmpty(href) && href.Contains(urlSearchId))
                    {
                        resultLinks.Add(new SearchEngineResultBase { Rank = i + 1, Url = href });
                    }
                }
            }

            return resultLinks.Distinct();
        }

        private async Task<string> AllowMetaRefresh(string searchUrl)
        {
            var html = await _httpClient.GetStringAsync(searchUrl);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Look for meta refresh redirect
            var metaRefresh = doc.DocumentNode.SelectSingleNode("//meta[translate(@http-equiv, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='refresh']");

            if (metaRefresh != null)
            {
                var content = metaRefresh.GetAttributeValue("content", "");
                var match = Regex.Match(content, @"url=(.*)", RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    var redirectUrl = WebUtility.HtmlDecode(match.Groups[1].Value);
                    if (!redirectUrl.StartsWith("http"))
                    {
                        var baseUri = new Uri(searchUrl);
                        redirectUrl = new Uri(baseUri, redirectUrl).ToString();
                    }

                    // Follow the redirect
                    html = await _httpClient.GetStringAsync(redirectUrl);
                }
            }

            return html;
        }
    }
}
