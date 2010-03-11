using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class GROSearch {

        public static void main (String[] args) {
            
            FamilyTree ft = FamilyTree.Instance;
            Console.WriteLine("GRO Search generation started.");

            MultiComparator<Registration> byLocation = new MultiComparator<Registration>();
            MultiComparator<Registration> byCensusLocation = new MultiComparator<Registration>();
            MultiComparator<Registration> byName = new MultiComparator<Registration>();
            byLocation.addComparator(new LocationComparator(FactLocation.PARISH));
            byLocation.addComparator(new DateComparator());
            byLocation.addComparator(new NameComparator());
            byCensusLocation.addComparator(new LocationComparator(FactLocation.PARISH));
            byCensusLocation.addComparator(new DateComparator());
            byName.addComparator(new NameComparator());
            byName.addComparator(new DateComparator());
            
            RegistrationFilter missingData = IncompleteDataFilter.MISSING_DATA_FILTER;
            // partial filter has data but only up to parish level ie: no address
            
            RegistrationFilter partialData = new IncompleteDataFilter(FactLocation.PARISH);
            
            RegistrationFilter directOrBlood = new OrFilter(
                    new RelationFilter(Individual.DIRECT), 
                    new RelationFilter(Individual.BLOOD));
            
            RegistrationFilter unknownOrMarriage = new OrFilter(
                    new RelationFilter(Individual.UNKNOWN), 
                    new RelationFilter(Individual.MARRIAGE));
          
            RegistrationsProcessor onlineBirthsRP = new RegistrationsProcessor(
                    new AndFilter(DateFilter.POST_1837_FILTER, 
                            partialData),
                    byName);
            RegistrationsProcessor onlineDeathsRP = new RegistrationsProcessor(
                    new AndFilter(BirthFilter.POST_1837_FILTER, 
                            partialData),
                    byName);
            RegistrationsProcessor onlineMarriagesRP = new RegistrationsProcessor(
                    new AndFilter(BirthFilter.POST_1837_FILTER, 
                            partialData),
                    byName);

            RegistrationsProcessor oprRP = new RegistrationsProcessor(
                    new AndFilter(DateFilter.PRE_1837_FILTER, 
                            partialData),
                    byLocation);
            RegistrationsProcessor censusRP = new RegistrationsProcessor(
                    new TrueFilter(), byCensusLocation);
                    
            Console.WriteLine("GRO extraction started.");
            List<Registration> births = ft.getAllBirthRegistrations();
            BirthOutputFormatter birthFormatter = new BirthOutputFormatter();
            //process("OPR_births", oprRP, births, birthFormatter);
            process("Online_births", onlineBirthsRP, births, birthFormatter);
            births = null;
            Console.WriteLine("GRO births files created.");
            
            List<Registration> deaths = ft.getAllDeathRegistrations();
            DeathOutputFormatter deathFormatter = new DeathOutputFormatter();
            //process("OPR_deaths", oprRP, deaths, deathFormatter);
            process("Online_deaths", onlineDeathsRP, deaths, deathFormatter);
            deaths = null;
            Console.WriteLine("GRO deaths files created.");
            
            List<Registration> marriages = ft.getAllMarriageRegistrations();
            MarriageOutputFormatter marriageFormatter = new MarriageOutputFormatter();
            //process("OPR_marriages", oprRP, marriages, marriageFormatter);
            process("Online_marriages", onlineMarriagesRP, marriages, marriageFormatter);
            marriages = null;
            Console.WriteLine("GRO marriages files created");

            List<Registration> census;
            CensusOutputFormatter censusFormatter = new CensusOutputFormatter();
              
            census = ft.getAllCensusRegistrations(FactDate.CENSUS1841, false);
            process("1841_census", censusRP, census, censusFormatter);
            Console.WriteLine("1841 Census Details written.");

            census = ft.getAllCensusRegistrations(FactDate.CENSUS1851, false);
            process("1851_census", censusRP, census, censusFormatter);
            Console.WriteLine("1851 Census Details written.");

            census = ft.getAllCensusRegistrations(FactDate.CENSUS1861, false);
            process("1861_census", censusRP, census, censusFormatter);
            Console.WriteLine("1861 Census Details written.");

            census = ft.getAllCensusRegistrations(FactDate.CENSUS1871, false);
            process("1871_census", censusRP, census, censusFormatter);
            Console.WriteLine("1871 Census Details written.");

            census = ft.getAllCensusRegistrations(FactDate.CENSUS1881, false);
            process("1881_census", censusRP, census, censusFormatter);
            Console.WriteLine("1881 Census Details written.");

            census = ft.getAllCensusRegistrations(FactDate.CENSUS1891, false);
            process("1891_census", censusRP, census, censusFormatter);
            Console.WriteLine("1891 Census Details written.");

            census = ft.getAllCensusRegistrations(FactDate.CENSUS1901, false);
            process("1901_census", censusRP, census, censusFormatter);
            Console.WriteLine("1901 Census Details written.");

            Console.WriteLine("GRO Search completed.");
        }    
        
        private static void process(String filename, RegistrationsProcessor rp, 
            						List<Registration> sourceRegs, BaseOutputFormatter formatter) 
        {
            TextWriter output = new StreamWriter("C:/temp/GRO/" + filename + ".csv");
            formatter.printHeader(output);
            List<Registration> regs = rp.processRegistrations(sourceRegs);
            foreach (Registration r in regs) {
                formatter.printItem(r, output);
            }
            Console.WriteLine("written " + regs.Count + " records to " + filename);
            output.Close();
        }
    }
}