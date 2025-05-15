namespace SearchEngine.Service.Interface
{
    public interface IScraperBaseFactory
    {
        IScraperBase Create(bool usePlaywright);
    }
}
