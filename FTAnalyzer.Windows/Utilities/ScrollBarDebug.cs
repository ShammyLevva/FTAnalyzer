namespace FTAnalyzer.Utilities
{
    static class ScrollBarDebug
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ScrollBarDebug));

        static public void LogScreenData(Form mainform, DataGridView gridView, string routine)
        {
            log.Debug($"In routine {routine}");
            log.Debug($"Form Height: {mainform.Height}, Width {mainform.Width}");
            log.Debug($"Grid {gridView.Name} Height: {gridView.Height}, Width {gridView.Width}");
            log.Debug($"Grid {gridView.Parent.Name} Height: {gridView.Parent.Height}, Width {gridView.Parent.Width}");
        }
    }
}
