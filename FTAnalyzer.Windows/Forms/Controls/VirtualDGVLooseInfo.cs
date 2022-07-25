namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVLooseInfo : VirtualDataGridView<IDisplayLooseInfo>
    {
        protected override object GetValueFor(IDisplayLooseInfo ind, string propertyName)
        {
            switch (propertyName)
            {  
                case nameof(IDisplayLooseInfo.IndividualID):
                    return ind.IndividualID;
                case nameof(IDisplayLooseInfo.Forenames):
                    return ind.Forenames;
                case nameof(IDisplayLooseInfo.Surname):
                    return ind.Surname;
                case nameof(IDisplayLooseInfo.BirthDate):
                    return ind.BirthDate;
                case nameof(IDisplayLooseInfo.BirthLocation):
                    return ind.BirthLocation;
                case nameof(IDisplayLooseInfo.DeathDate):
                    return ind.DeathDate;
                case nameof(IDisplayLooseInfo.DeathLocation):
                    return ind.DeathLocation;
                case nameof(IDisplayLooseInfo.LooseBirth):
                    return ind.LooseBirth;
                case nameof(IDisplayLooseInfo.LooseDeath):
                    return ind.LooseDeath;
            }
            return null;
        }
    }
}
