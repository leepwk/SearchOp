using SearchEngine.Common.Model;
using SearchEngine.Common.Model.Response;

namespace SearchEngine.Repository.Interface;

public interface ISearchRepository
{
    Task<SearchEngineTypeResponse> GetSearchEngineType();
    void InsertSearchEngineType(int engineId, string engineDesc);
    Task<IEnumerable<SearchEngineResult>> GetSearchEngineResults(int engineId);
    Task<bool> InsertSearchEngineResults(int engineId, string searchTerm, List<SearchEngineResultBase> toInsert);
}