namespace FTAnalyzer.Forms.Controls
{
    class VirtualDgvDataErrors : VirtualDataGridView<IDisplayDataError>
    {
        protected override object GetValueFor(IDisplayDataError err, string propertyName)
        {
            switch (propertyName)
            {
#if __PC__
                case nameof(IDisplayDataError.Icon):
                    return err.Icon;
#endif
                case nameof(IDisplayDataError.ErrorType):
                    return err.ErrorType;
                case nameof(IDisplayDataError.Reference):
                    return err.Reference;
                case nameof(IDisplayDataError.Name):
                    return err.Name;
                case nameof(IDisplayDataError.Description):
                    return err.Description;
                case nameof(IDisplayDataError.Born):
                    return err.Born;
                case nameof(IDisplayDataError.Died):
                    return err.Died;
                case nameof(IDisplayDataError.Forenames):
                    return err.Forenames;
                case nameof(IDisplayDataError.Surname):
                    return err.Surname;
                default:
                    break;
            }
            return err.Reference;
        }
    }
}
