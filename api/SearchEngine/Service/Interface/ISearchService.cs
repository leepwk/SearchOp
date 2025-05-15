using SearchEngine.Common.Model.Response;

namespace SearchEngine.Service.Interface
{
    public interface ISearchService
    {
        Task<SearchEngineResultResponse> FetchByUrlTerms(string url, string searchTerm, bool usePlaywright);
        Task<SearchEngineTypeResponse> GetSearchEngineType();
    }
}
