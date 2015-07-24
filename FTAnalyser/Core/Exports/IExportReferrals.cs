using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IExportReferrals
    {
        string CensusDate { get; }
        string CensusReference { get; }
        string Forenames  { get; }
        string Surname { get; }
        Age Age { get; }
        string RelationType { get; }
        string Census { get; }
    }
}
