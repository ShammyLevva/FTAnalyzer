namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVFamily : VirtualDataGridView<IDisplayFamily>
    {
        protected override object GetValueFor(IDisplayFamily fam, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IDisplayFamily.FamilyID):
                    return fam.FamilyID;
                case nameof(IDisplayFamily.HusbandID):
                    return fam.HusbandID;
                case nameof(IDisplayFamily.Husband):
                    return fam.Husband;
                case nameof(IDisplayFamily.WifeID):
                    return fam.WifeID;
                case nameof(IDisplayFamily.Wife):
                    return fam.Wife;
                case nameof(IDisplayFamily.Marriage):
                    return fam.Marriage;
                case nameof(IDisplayFamily.Location):
                    return fam.Location;
                case nameof(IDisplayFamily.Children):
                    return fam.Children;
                case nameof(IDisplayFamily.FamilySize):
                    return fam.FamilySize;
                case nameof(IDisplayFamily.HusbandSurname):
                    return fam.HusbandSurname;
                case nameof(IDisplayFamily.HusbandForenames):
                    return fam.HusbandForenames;
                case nameof(IDisplayFamily.WifeSurname):
                    return fam.WifeSurname;
                case nameof(IDisplayFamily.WifeForenames):
                    return fam.WifeForenames;
                case nameof(IDisplayFamily.MaritalStatus):
                    return fam.MaritalStatus;
            }
            return null;
        }
    }
}
