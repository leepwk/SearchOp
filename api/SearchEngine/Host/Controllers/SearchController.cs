using Microsoft.AspNetCore.Mvc;
using SearchEngine.Common.Model;
using SearchEngine.Common.Model.Response;
using SearchEngine.Service.Helpers;
using SearchEngine.Service.Interface;

namespace SearchEngine.Host.Controllers
{
    /// <summary>
    /// Main Search entry point
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SearchController : BaseController<SearchController>
    {
        private readonly ISearchService _searchService;
        public SearchController(ILogger<SearchController> logger, ISearchService searchService) : base(logger)
        {
            _searchService = searchService;
        }

        /// <summary>
        /// Retrieve search engine scraping results if any
        /// </summary>
        /// <param name="url"></param>
        /// <param name="searchTerm"></param>
        /// <param name="usePlaywright"></param>
        /// <returns></returns>
        [HttpGet("Rankings", Name = "Rankings")]
        public async Task<SearchEngineResultResponse> Get([FromQuery]string url = "https://www.bing.com",[FromQuery]string searchTerm = "land registry search", [FromQuery]bool usePlaywright = false)
        {
            if (url.EnsureHttps(out string validUrl))
            {
                return await _searchService.FetchByUrlTerms(validUrl, searchTerm, usePlaywright);
            }
            return new SearchEngineResultResponse { Data = new List<SearchEngineResult>(), Message = "Invalid URL" };
        }

        /// <summary>
        /// Retrieve list of search url previously used
        /// </summary>
        /// <returns></returns>
        [HttpGet("EngineType", Name = "EngineType")]
        public async Task<SearchEngineTypeResponse> GetEngineType()
        {
            return await _searchService.GetSearchEngineType();
        }
    }
}
