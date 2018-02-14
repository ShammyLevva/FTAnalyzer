using System;
using static FTAnalyzer.ColourValues;

namespace FTAnalyzer
{
    public interface IDisplayColourCensus
    {
        string IndividualID { get; }        
        string Forenames { get; }           
        string Surname { get; }
        string Relation { get; }
        string RelationToRoot { get; }

        CensusColour C1841 { get; }
        CensusColour C1851 { get; }
        CensusColour C1861 { get; }
        CensusColour C1871 { get; }
        CensusColour C1881 { get; }
        CensusColour C1891 { get; }
        CensusColour C1901 { get; }
        CensusColour C1911 { get; }
        CensusColour C1939 { get; }

        CensusColour US1790 { get; }
        CensusColour US1800 { get; }
        CensusColour US1810 { get; }
        CensusColour US1820 { get; }
        CensusColour US1830 { get; }
        CensusColour US1840 { get; }
        CensusColour US1850 { get; }
        CensusColour US1860 { get; }
        CensusColour US1870 { get; }
        CensusColour US1880 { get; }
        CensusColour US1890 { get; }
        CensusColour US1900 { get; }
        CensusColour US1910 { get; }
        CensusColour US1920 { get; }
        CensusColour US1930 { get; }
        CensusColour US1940 { get; }

        CensusColour Ire1901 { get; }
        CensusColour Ire1911 { get; }

        CensusColour Can1851 { get; }
        CensusColour Can1861 { get; }
        CensusColour Can1871 { get; }
        CensusColour Can1881 { get; }
        CensusColour Can1891 { get; }
        CensusColour Can1901 { get; }
        CensusColour Can1906 { get; }
        CensusColour Can1911 { get; }
        CensusColour Can1916 { get; }
        CensusColour Can1921 { get; }

        CensusColour V1865 { get; }
        CensusColour V1875 { get; }
        CensusColour V1885 { get; }
        CensusColour V1895 { get; }
        CensusColour V1905 { get; }
        CensusColour V1915 { get; }
        CensusColour V1920 { get; }
        CensusColour V1925 { get; }

        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }
        FactDate DeathDate { get; }
        FactLocation DeathLocation { get; }
        FactLocation BestLocation(FactDate when);
        Int64 Ahnentafel { get; }
    }
}
