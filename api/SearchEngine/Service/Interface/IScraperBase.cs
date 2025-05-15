using SearchEngine.Common.Model;

namespace SearchEngine.Service.Interface
{
    public interface IScraperBase
    {
        Task<IEnumerable<SearchEngineResultBase>> Scrape(string url, string searchTerm, string urlSearchId);
    }
}
