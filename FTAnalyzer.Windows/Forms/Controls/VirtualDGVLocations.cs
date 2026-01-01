namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVLocations : VirtualDataGridView<IDisplayLocation>
    {
        protected override object GetValueFor(IDisplayLocation loc, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IDisplayLocation.Country):
                    return loc.Country;
                case nameof(IDisplayLocation.Region):
                    return loc.Region;
                case nameof(IDisplayLocation.SubRegion):
                    return loc.SubRegion;
                case nameof(IDisplayLocation.Address):
                    return loc.Address;
                case nameof(IDisplayLocation.Place):
                    return loc.Place;
                case nameof(IDisplayLocation.Latitude):
                    return loc.Latitude;
                case nameof(IDisplayLocation.Longitude):
                    return loc.Longitude;
#if __PC__
                case nameof(IDisplayLocation.Icon):
                    return loc.Icon;
#endif
                case nameof(IDisplayLocation.Geocoded):
                    return loc.Geocoded;
                case nameof(IDisplayLocation.GEDCOMLoaded):
                    return loc.GEDCOMLoaded;
                case nameof(IDisplayLocation.GEDCOMLocation):
                    return loc.GEDCOMLocation;
                default:
                    break;
            }
            return loc.Country;
        }
    }
}
