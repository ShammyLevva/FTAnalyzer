namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVSurnames : VirtualDataGridView<IDisplaySurnames>
    {
        protected override object GetValueFor(IDisplaySurnames occ, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IDisplaySurnames.Surname):
                    return occ.Surname;
                case nameof(IDisplaySurnames.Individuals):
                    return occ.Individuals;
                case nameof(IDisplaySurnames.Families):
                    return occ.Families;
                case nameof(IDisplaySurnames.Marriages):
                    return occ.Marriages;
            }
            return null;
        }
    }
}
