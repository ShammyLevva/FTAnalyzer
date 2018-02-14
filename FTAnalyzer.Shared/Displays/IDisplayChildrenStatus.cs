namespace FTAnalyzer
{
    public interface IDisplayChildrenStatus
    {
        string FamilyID { get; }
        string Surname { get; }
        string HusbandID { get; }
        string Husband { get; }
        string WifeID { get; }
        string Wife { get; }
        int ChildrenTotal { get; }
        int ChildrenAlive { get; }
        int ChildrenDead { get; }
        int ExpectedTotal { get; }
        int ExpectedAlive { get; }
        int ExpectedDead { get; }
    }
}
