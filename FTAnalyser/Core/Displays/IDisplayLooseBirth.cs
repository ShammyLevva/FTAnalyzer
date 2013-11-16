using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayLooseBirth
    {
        string IndividualID { get; }
        string Forenames { get; }
        string Surname { get; }
        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }
        string LooseBirth { get; }
    }
}
