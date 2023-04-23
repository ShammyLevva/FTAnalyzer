namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVSources : VirtualDataGridView<IDisplaySource>
    {
        protected override object GetValueFor(IDisplaySource src, string propertyName)
        {
            return propertyName switch
            {
                nameof(IDisplaySource.SourceID) => src.SourceID,
                nameof(IDisplaySource.SourceTitle) => src.SourceTitle,
                nameof(IDisplaySource.Publication) => src.Publication,
                nameof(IDisplaySource.Author) => src.Author,
                nameof(IDisplaySource.SourceText) => src.SourceText,
                nameof(IDisplaySource.SourceMedium) => src.SourceMedium,
                nameof(IDisplaySource.FactCount) => src.FactCount,
                _ => null,
            };
        }
    }
}
