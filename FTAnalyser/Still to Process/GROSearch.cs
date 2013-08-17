using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTAnalyzer.Filters;

namespace FTAnalyzer
{
    public class GROSearch {

//        public static void main (String[] args) {
            
//            FamilyTree ft = FamilyTree.Instance;
//            Console.WriteLine("GRO Search generation started.");

//            MultiComparator<Registration> byLocation = new MultiComparator<Registration>();
//            MultiComparator<Registration> byCensusLocation = new MultiComparator<Registration>();
//            MultiComparator<Registration> byName = new MultiComparator<Registration>();
//            byLocation.addComparator(new LocationComparator(FactLocation.PARISH));
//            byLocation.addComparator(new DateComparator());
//            byLocation.addComparator(new NameComparator());
//            byCensusLocation.addComparator(new LocationComparator(FactLocation.PARISH));
//            byCensusLocation.addComparator(new DateComparator());
//            byName.addComparator(new NameComparator());
//            byName.addComparator(new DateComparator());
            
//            Func<Registration, bool> missingData = FilterUtils.IncompleteDataFilter<Registration>.MISSING_DATA_FILTER;
//            // partial filter has data but only up to parish level ie: no address

//            Func<Registration, bool> partialData = new IncompleteDataFilter<Registration>(FactLocation.PARISH);

//            Func<Registration, bool> directOrBlood = FilterUtils.OrFilter<Registration>(
//                    FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.DIRECT),
//                    FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.BLOOD));

//            Func<Registration, bool> unknownOrMarriage = FilterUtils.OrFilter<Registration>(
//                    FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.UNKNOWN),
//                    FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.MARRIAGE));
          
//            RegistrationsProcessor onlineBirthsRP = new RegistrationsProcessor(
//                    FilterUtils.AndFilter<Registration>(DateFilter<Registration>.POST_1837_FILTER, 
//                            partialData),
//                    byName);
//            RegistrationsProcessor onlineDeathsRP = new RegistrationsProcessor(
//                    FilterUtils.AndFilter<Registration>(BirthFilter<Registration>.POST_1837_FILTER, 
//                            partialData),
//                    byName);
//            RegistrationsProcessor onlineMarriagesRP = new RegistrationsProcessor(
//                    FilterUtils.AndFilter<Registration>(BirthFilter<Registration>.POST_1837_FILTER, 
//                            partialData),
//                    byName);

//            RegistrationsProcessor oprRP = new RegistrationsProcessor(
//                    FilterUtils.AndFilter<Registration>(DateFilter<Registration>.PRE_1837_FILTER, 
//                            partialData),
//                    byLocation);
//            RegistrationsProcessor censusRP = new RegistrationsProcessor(
//                    FilterUtils.TrueFilter<Registration>(), byCensusLocation);
                    
//            Console.WriteLine("GRO extraction started.");
//            List<Registration> births = ft.getAllBirthRegistrations();
//            BirthOutputFormatter birthFormatter = new BirthOutputFormatter();
//            //process("OPR_births", oprRP, births, birthFormatter);
//            process("Online_births", onlineBirthsRP, births, birthFormatter);
//            births = null;
//            Console.WriteLine("GRO births files created.");
            
//            List<Registration> deaths = ft.getAllDeathRegistrations();
//            DeathOutputFormatter deathFormatter = new DeathOutputFormatter();
//            //process("OPR_deaths", oprRP, deaths, deathFormatter);
//            process("Online_deaths", onlineDeathsRP, deaths, deathFormatter);
//            deaths = null;
//            Console.WriteLine("GRO deaths files created.");
            
//            List<Registration> marriages = ft.getAllMarriageRegistrations();
//            MarriageOutputFormatter marriageFormatter = new MarriageOutputFormatter();
//            //process("OPR_marriages", oprRP, marriages, marriageFormatter);
//            process("Online_marriages", onlineMarriagesRP, marriages, marriageFormatter);
//            marriages = null;
//            Console.WriteLine("GRO marriages files created");
///*
//            List<Registration> census;
//            CensusOutputFormatter censusFormatter = new CensusOutputFormatter();
//            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1841, false, false);
//            process("1841_census", censusRP, census, censusFormatter);
//            Console.WriteLine("1841 Census Details written.");

//            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1851, false, false);
//            process("1851_census", censusRP, census, censusFormatter);
//            Console.WriteLine("1851 Census Details written.");

//            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1861, false, false);
//            process("1861_census", censusRP, census, censusFormatter);
//            Console.WriteLine("1861 Census Details written.");

//            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1871, false, false);
//            process("1871_census", censusRP, census, censusFormatter);
//            Console.WriteLine("1871 Census Details written.");

//            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1881, false, false);
//            process("1881_census", censusRP, census, censusFormatter);
//            Console.WriteLine("1881 Census Details written.");

//            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1891, false);
//            process("1891_census", censusRP, census, censusFormatter);
//            Console.WriteLine("1891 Census Details written.");

//            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1901, false);
//            process("1901_census", censusRP, census, censusFormatter);
//            Console.WriteLine("1901 Census Details written.");
//*/
//            Console.WriteLine("GRO Search completed.");
//        }    
        
//        private static void process(String filename, RegistrationsProcessor rp, 
//                                    List<Registration> sourceRegs, BaseOutputFormatter formatter) 
//        {
//            TextWriter output = new StreamWriter("C:/temp/GRO/" + filename + ".csv");
//            formatter.printHeader(output);
//            List<Registration> regs = rp.processRegistrations(sourceRegs);
//            foreach (Registration r in regs) {
//                formatter.printItem(r, output);
//            }
//            Console.WriteLine("written " + regs.Count + " records to " + filename);
//            output.Close();
//        }
    }
}