namespace SearchEngine.Common.Model
{
    public class SearchEngineResult : SearchEngineResultBase
    {
        public int Id { get; set; }
        public int EngineId { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
