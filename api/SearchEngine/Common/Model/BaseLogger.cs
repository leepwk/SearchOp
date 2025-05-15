namespace SearchEngine.Common.Model
{
    public abstract class BaseLogger<T>
    {
        protected readonly ILogger<T> Logger;

        protected BaseLogger(ILogger<T> logger)
        {
            Logger = logger;
        }
    }
}
