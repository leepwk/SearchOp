using Microsoft.AspNetCore.Mvc;

namespace SearchEngine.Host.Controllers
{
    /// <summary>
    /// Inherit common properties and methods for Controllers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseController<T> : ControllerBase
    {
        protected readonly ILogger<T> Logger;

        protected BaseController(ILogger<T> logger)
        {
            Logger = logger;
        }
    }
}
