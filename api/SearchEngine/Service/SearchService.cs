using SearchEngine.Common.Model;
using SearchEngine.Common.Model.Response;
using SearchEngine.Repository.Interface;
using SearchEngine.Service.Interface;

namespace SearchEngine.Service
{
    public class SearchService : BaseLogger<SearchService>, ISearchService
    {
        private readonly IWebScraper _webScraper;
        private readonly ISearchRepository _searchRepository;
        
        public SearchService(ILogger<SearchService> logger, IWebScraper webScraper, ISearchRepository searchRepository) : base(logger)
        {
            _webScraper = webScraper;
            _searchRepository = searchRepository;
        }

        public async Task<SearchEngineResultResponse> FetchByUrlTerms(string url, string searchTerm, bool usePlaywright) 
        {
            var results = new List<SearchEngineResult>();
            var msg = string.Empty;

            try
            {
                var engineId = 0;
                var engineResults = await GetSearchEngineType();
                var existingEngine = engineResults.Data.FirstOrDefault(s => url.Contains(s.EngineDescription));
                if (existingEngine != null)
                {
                    url = existingEngine.EngineDescription;
                    engineId = existingEngine.EngineId;
                }
                else
                {
                    // new search url - add to lookup table
                    engineId = engineResults.Data.Any() ? engineResults.Data.Max(s => s.EngineId) + 1 : 1;
                    _searchRepository.InsertSearchEngineType(engineId, url);
                }

                // fetch new results
                var scrapeResults = await _webScraper.FetchByUrlTerms(url, searchTerm, usePlaywright);
                var scrapeResultsList = scrapeResults.ToList();
                if (!scrapeResultsList.Any())
                {
                    // if nothing
                    scrapeResultsList.Add(new SearchEngineResultBase { Rank = 0, SearchTerm = searchTerm, Url = ""});
                }

                // prepare to return scraped results in case database has issues ie. don't rely on the database
                var histResults = scrapeResultsList.Select(scrapeResult => new SearchEngineResult
                {
                    EngineId = engineId,
                    EntryDate = DateTime.Today,
                    Rank = scrapeResult.Rank,
                    SearchTerm = scrapeResult.SearchTerm,
                    Url = scrapeResult.Url
                });

                // try and add to history
                if (await _searchRepository.InsertSearchEngineResults(engineId, searchTerm, scrapeResultsList))
                {
                    histResults = await _searchRepository.GetSearchEngineResults(engineId);
                }

                results = histResults.ToList();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.LogError($"Unhandled error: {nameof(FetchByUrlTerms)}: {msg}");
            }

            return new SearchEngineResultResponse { Data = results, Message = msg };
        }

        public async Task<SearchEngineTypeResponse> GetSearchEngineType()
        {
            return await  _searchRepository.GetSearchEngineType();
        }

    }
}
