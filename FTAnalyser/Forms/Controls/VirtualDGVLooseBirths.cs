namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVLooseBirths : VirtualDataGridView<IDisplayLooseBirth>
    {
        protected override object GetValueFor(IDisplayLooseBirth ind, string propertyName)
        {
            switch (propertyName)
            {  
                case nameof(IDisplayLooseBirth.IndividualID):
                    return ind.IndividualID;
                case nameof(IDisplayLooseBirth.Forenames):
                    return ind.Forenames;
                case nameof(IDisplayLooseBirth.Surname):
                    return ind.Surname;
                case nameof(IDisplayLooseBirth.BirthDate):
                    return ind.BirthDate;
                case nameof(IDisplayLooseBirth.BirthLocation):
                    return ind.BirthLocation;
                case nameof(IDisplayLooseBirth.LooseBirth):
                    return ind.LooseBirth;
            }
            return null;
        }
    }
}
