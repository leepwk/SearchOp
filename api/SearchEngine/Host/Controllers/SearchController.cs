using Microsoft.AspNetCore.Mvc;
using SearchEngine.Common.Model;
using SearchEngine.Common.Model.Response;
using SearchEngine.Service.Interface;

namespace SearchEngine.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : BaseController<SearchController>
    {
        private readonly ISearchService _searchService;
        public SearchController(ILogger<SearchController> logger, ISearchService searchService) : base(logger)
        {
            _searchService = searchService;
        }

        [HttpGet("Rankings", Name = "Rankings")]
        public async Task<SearchEngineResultResponse> Get([FromQuery]string url = "https://www.bing.com",[FromQuery]string searchTerm = "land registry search", [FromQuery]bool usePlaywright = false)
        {
            return await _searchService.FetchByUrlTerms(url, searchTerm, usePlaywright);
        }

        [HttpGet("EngineType", Name = "EngineType")]
        public async Task<SearchEngineTypeResponse> GetEngineType()
        {
            return await _searchService.GetSearchEngineType();
        }
    }
}
