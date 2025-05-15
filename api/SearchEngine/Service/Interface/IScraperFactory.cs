namespace SearchEngine.Service.Interface
{
    public interface IScraperFactory
    {
        IScraper Create(string url);
    }
}
