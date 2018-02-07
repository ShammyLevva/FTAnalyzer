namespace FTAnalyzer
{
    public interface IDisplaySource
    {
        string SourceID { get; }
        string SourceTitle { get; }
        string Publication { get; }
        string Author { get; }
        string SourceText { get; }
        string SourceMedium { get; }
        int FactCount { get; }
    }
}
