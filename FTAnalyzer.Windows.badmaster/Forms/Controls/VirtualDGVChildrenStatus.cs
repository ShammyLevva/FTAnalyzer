namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVChildrenStatus : VirtualDataGridView<IDisplayChildrenStatus>
    {
        protected override object GetValueFor(IDisplayChildrenStatus fam, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IDisplayChildrenStatus.FamilyID):
                    return fam.FamilyID;
                case nameof(IDisplayChildrenStatus.Surname):
                    return fam.Surname;
                case nameof(IDisplayChildrenStatus.HusbandID):
                    return fam.HusbandID;
                case nameof(IDisplayChildrenStatus.Husband):
                    return fam.Husband;
                case nameof(IDisplayChildrenStatus.WifeID):
                    return fam.WifeID;
                case nameof(IDisplayChildrenStatus.Wife):
                    return fam.Wife;
                case nameof(IDisplayChildrenStatus.ChildrenTotal):
                    return fam.ChildrenTotal;
                case nameof(IDisplayChildrenStatus.ChildrenAlive):
                    return fam.ChildrenAlive;
                case nameof(IDisplayChildrenStatus.ChildrenDead):
                    return fam.ChildrenDead;
                case nameof(IDisplayChildrenStatus.ExpectedTotal):
                    return fam.ExpectedTotal;
                case nameof(IDisplayChildrenStatus.ExpectedAlive):
                    return fam.ExpectedAlive;
                case nameof(IDisplayChildrenStatus.ExpectedDead):
                    return fam.ExpectedDead;
                default:
                    break;
            }
            return null;
        }
    }
}
