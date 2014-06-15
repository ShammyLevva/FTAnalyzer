using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayChildrenStatus
    {
        string FamilyID { get; }
        string HusbandID { get; }
        CensusIndividual Husband { get; }
        string WifeID { get; }
        CensusIndividual Wife { get; }
        int ChildrenTotal { get; }
        int ChildrenAlive { get; }
        int ChildrenDead { get; }
        int ExpectedTotal { get; }
        int ExpectedAlive { get; }
        int ExpectedDead { get; }
    }
}
