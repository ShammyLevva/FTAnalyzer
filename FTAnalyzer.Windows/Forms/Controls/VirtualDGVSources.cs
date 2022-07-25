namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVSources : VirtualDataGridView<IDisplaySource>
    {
        protected override object GetValueFor(IDisplaySource src, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IDisplaySource.SourceID):
                    return src.SourceID;
                case nameof(IDisplaySource.SourceTitle):
                    return src.SourceTitle;
                case nameof(IDisplaySource.Publication):
                    return src.Publication;
                case nameof(IDisplaySource.Author):
                    return src.Author;
                case nameof(IDisplaySource.SourceText):
                    return src.SourceText;
                case nameof(IDisplaySource.SourceMedium):
                    return src.SourceMedium;
                case nameof(IDisplaySource.FactCount):
                    return src.FactCount;
            }
            return null;
        }
    }
}
