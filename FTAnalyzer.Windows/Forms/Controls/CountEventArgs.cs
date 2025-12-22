namespace FTAnalyzer.Forms.Controls
{
    public class CountEventArgs : EventArgs
    {
        public string FilterText { get; set; }

        public CountEventArgs() => FilterText = null;
    }
}
