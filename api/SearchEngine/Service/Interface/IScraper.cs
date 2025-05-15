using SearchEngine.Common.Model;

namespace SearchEngine.Service.Interface
{
    public interface IScraper
    {
        Task<IEnumerable<SearchEngineResultBase>> Scrape(string url, string searchTerm, string urlSearchId, bool usePlaywright);

        bool IsAllowed();
    }
}
