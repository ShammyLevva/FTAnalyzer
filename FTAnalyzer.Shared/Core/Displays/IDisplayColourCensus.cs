using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayColourCensus
    {
        string IndividualID { get; }        
        string Forenames { get; }           
        string Surname { get; }
        string Relation { get; }
        string RelationToRoot { get; }
        
        int C1841 { get; }
        int C1851 { get; }
        int C1861 { get; }
        int C1871 { get; }
        int C1881 { get; }
        int C1891 { get; }
        int C1901 { get; }
        int C1911 { get; }
        int C1939 { get; }

        int US1790 { get; }
        int US1800 { get; }
        int US1810 { get; }
        int US1820 { get; }
        int US1830 { get; }
        int US1840 { get; }
        int US1850 { get; }
        int US1860 { get; }
        int US1870 { get; }
        int US1880 { get; }
        int US1890 { get; }
        int US1900 { get; }
        int US1910 { get; }
        int US1920 { get; }
        int US1930 { get; }
        int US1940 { get; }

        int Ire1901 { get; }
        int Ire1911 { get; }

        int Can1851 { get; }
        int Can1861 { get; }
        int Can1871 { get; }
        int Can1881 { get; }
        int Can1891 { get; }
        int Can1901 { get; }
        int Can1906 { get; }
        int Can1911 { get; }
        int Can1916 { get; }
        int Can1921 { get; }

        int V1865 { get; }
        int V1875 { get; }
        int V1885 { get; }
        int V1895 { get; }
        int V1905 { get; }
        int V1915 { get; }
        int V1920 { get; }
        int V1925 { get; }

        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }
        FactDate DeathDate { get; }
        FactLocation DeathLocation { get; }
        FactLocation BestLocation(FactDate when);
        Int64 Ahnentafel { get; }
    }
}
