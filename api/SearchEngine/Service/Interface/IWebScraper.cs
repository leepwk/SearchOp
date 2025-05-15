using SearchEngine.Common.Model;

namespace SearchEngine.Service.Interface;

public interface IWebScraper
{
    Task<IEnumerable<SearchEngineResultBase>> FetchByUrlTerms(string url, string searchTerm, bool usePlaywright);
}