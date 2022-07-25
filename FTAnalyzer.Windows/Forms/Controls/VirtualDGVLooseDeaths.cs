namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVLooseDeaths : VirtualDataGridView<IDisplayLooseDeath>
    {
        protected override object GetValueFor(IDisplayLooseDeath ind, string propertyName)
        {
            switch (propertyName)
            {  
                case nameof(IDisplayLooseDeath.IndividualID):
                    return ind.IndividualID;
                case nameof(IDisplayLooseDeath.Forenames):
                    return ind.Forenames;
                case nameof(IDisplayLooseDeath.Surname):
                    return ind.Surname;
                case nameof(IDisplayLooseDeath.BirthDate):
                    return ind.BirthDate;
                case nameof(IDisplayLooseDeath.BirthLocation):
                    return ind.BirthLocation;
                case nameof(IDisplayLooseDeath.DeathDate):
                    return ind.DeathDate;
                case nameof(IDisplayLooseDeath.DeathLocation):
                    return ind.DeathLocation;
                case nameof(IDisplayLooseDeath.LooseDeath):
                    return ind.LooseDeath;
            }
            return null;
        }
    }
}
