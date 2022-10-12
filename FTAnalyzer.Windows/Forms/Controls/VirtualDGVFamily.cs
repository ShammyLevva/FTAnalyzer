namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVFamily : VirtualDataGridView<IDisplayFamily>
    {
        protected override object GetValueFor(IDisplayFamily fam, string propertyName)
        {
            return propertyName switch
            {
                nameof(IDisplayFamily.FamilyID) => fam.FamilyID,
                nameof(IDisplayFamily.HusbandID) => fam.HusbandID,
                nameof(IDisplayFamily.Husband) => fam.Husband,
                nameof(IDisplayFamily.WifeID) => fam.WifeID,
                nameof(IDisplayFamily.Wife) => fam.Wife,
                nameof(IDisplayFamily.Marriage) => fam.Marriage,
                nameof(IDisplayFamily.Location) => fam.Location,
                nameof(IDisplayFamily.Children) => fam.Children,
                nameof(IDisplayFamily.FamilySize) => fam.FamilySize,
                nameof(IDisplayFamily.HusbandSurname) => fam.HusbandSurname,
                nameof(IDisplayFamily.HusbandForenames) => fam.HusbandForenames,
                nameof(IDisplayFamily.WifeSurname) => fam.WifeSurname,
                nameof(IDisplayFamily.WifeForenames) => fam.WifeForenames,
                nameof(IDisplayFamily.MaritalStatus) => fam.MaritalStatus,
                _ => null,
            };
        }
    }
}
