using Microsoft.AspNetCore.Mvc;

namespace SearchEngine.Host.Controllers
{
    public abstract class BaseController<T> : ControllerBase
    {
        protected readonly ILogger<T> Logger;

        protected BaseController(ILogger<T> logger)
        {
            Logger = logger;
        }
    }
}
