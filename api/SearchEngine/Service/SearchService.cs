using SearchEngine.Common.Model;
using SearchEngine.Common.Model.Response;
using SearchEngine.Repository.Interface;
using SearchEngine.Service.Interface;

namespace SearchEngine.Service
{
    /// <summary>
    /// Service class to process search requests and direct the processing
    /// </summary>
    public class SearchService : BaseLogger<SearchService>, ISearchService
    {
        private readonly IWebScraper _webScraper;
        private readonly ISearchRepository _searchRepository;
        
        public SearchService(ILogger<SearchService> logger, IWebScraper webScraper, ISearchRepository searchRepository) : base(logger)
        {
            _webScraper = webScraper;
            _searchRepository = searchRepository;
        }

        /// <summary>
        /// Method to fetch results from the search engine
        /// </summary>
        /// <param name="url"></param>
        /// <param name="searchTerm"></param>
        /// <param name="usePlaywright"></param>
        /// <returns></returns>
        public async Task<SearchEngineResultResponse> FetchByUrlTerms(string url, string searchTerm, bool usePlaywright) 
        {
            var results = new List<SearchEngineResult>();
            var msg = string.Empty;
            var newEngineId = 0;
            try
            {
                var engineId = 0;
                // find the engine type from existing list
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
                    newEngineId = engineId = engineResults.Data.Any() ? engineResults.Data.Max(s => s.EngineId) + 1 : 1;
                    _searchRepository.InsertSearchEngineType(engineId, url);
                }

                // fetch new results from scraping
                var scrapeResults = await _webScraper.FetchByUrlTerms(url, searchTerm, usePlaywright);
                var scrapeResultsList = scrapeResults.ToList();
                if (!scrapeResultsList.Any())
                {
                    // if nothing - zero is a valid result
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
                // only remove new search engines which have failed for some reason
                if (newEngineId > 0)
                {
                    _searchRepository.DeleteSearchEngineType(newEngineId);
                }

                msg = ex.Message;
                Logger.LogError($"Unhandled error: {nameof(FetchByUrlTerms)}: {msg}");
            }

            return new SearchEngineResultResponse { Data = results, Message = msg };
        }

        /// <summary>
        /// Method to return list of previously used search engine websites
        /// </summary>
        /// <returns></returns>
        public async Task<SearchEngineTypeResponse> GetSearchEngineType()
        {
            return await  _searchRepository.GetSearchEngineType();
        }
    }
}
