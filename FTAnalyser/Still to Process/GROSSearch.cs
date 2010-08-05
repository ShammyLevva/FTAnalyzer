using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class GROSSearch {

        public static void main (String[] args) {

            FamilyTree ft = FamilyTree.Instance;
            Console.WriteLine("GROS Search generation started.");

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

            Filter<Registration> missingScottishData = new AndFilter<Registration>(
                    IncompleteDataFilter.MISSING_DATA_FILTER,
                    LocationFilter<Registration>.SCOTLAND);
            // partial filter has data but only up to parish level ie: no address
            Filter<Registration> partialScottishData = new AndFilter<Registration>(
                    new IncompleteDataFilter(FactLocation.PARISH),
                    LocationFilter<Registration>.SCOTLAND);
            Filter<Registration> directOrBlood = new OrFilter<Registration>(
                    new OrFilter<Registration>(new RelationFilter<Registration>(Individual.DIRECT), 
        					     new RelationFilter<Registration>(Individual.BLOOD)),
        		    new RelationFilter<Registration>(Individual.MARRIEDTODB));
            Filter<Registration> unknownOrMarriage = new OrFilter<Registration>(
                    new RelationFilter<Registration>(Individual.UNKNOWN), 
                    new RelationFilter<Registration>(Individual.MARRIAGE));
          
            RegistrationsProcessor onlineBirthsRP = new RegistrationsProcessor(
                    new AndFilter<Registration>(DateFilter<Registration>.ONLINE_BIRTH_FILTER,
                            new AndFilter<Registration>(directOrBlood, partialScottishData)),
                    byName);
            RegistrationsProcessor onlineDeathsRP = new RegistrationsProcessor(
                    new AndFilter<Registration>(BirthFilter<Registration>.ONLINE_DEATH_FILTER,
                            new AndFilter<Registration>(directOrBlood, partialScottishData)),
                    byName);
            RegistrationsProcessor onlineMarriagesRP = new RegistrationsProcessor(
                    new AndFilter<Registration>(BirthFilter<Registration>.ONLINE_MARRIAGE_FILTER,
                            new AndFilter<Registration>(directOrBlood, partialScottishData)),
                    byName);
            RegistrationsProcessor grosBirthsRP = new RegistrationsProcessor(
                    new AndFilter<Registration>(DateFilter<Registration>.GROS_BIRTH_FILTER,
                            new AndFilter<Registration>(directOrBlood, partialScottishData)),
                    byName);
            RegistrationsProcessor grosDeathsRP = new RegistrationsProcessor(
                    new AndFilter<Registration>(BirthFilter<Registration>.GROS_DEATH_FILTER,
                            new AndFilter<Registration>(directOrBlood, partialScottishData)),
                    byName);
            RegistrationsProcessor grosMarriagesRP = new RegistrationsProcessor(
                    new AndFilter<Registration>(BirthFilter<Registration>.GROS_MARRIAGE_FILTER,
                            new AndFilter<Registration>(directOrBlood, partialScottishData)),
                    byName);
            RegistrationsProcessor oprRP = new RegistrationsProcessor(
                    new AndFilter<Registration>(DateFilter<Registration>.PRE_1855_FILTER,
                            new AndFilter<Registration>(directOrBlood, partialScottishData)),
                    byLocation);
            RegistrationsProcessor censusRP = new RegistrationsProcessor(
                    new AndFilter<Registration>(directOrBlood, LocationFilter<Registration>.SCOTLAND),
                    byCensusLocation);
            RegistrationsProcessor bissetRP = new RegistrationsProcessor(
                    new AndFilter<Registration>(new SurnameFilter<Registration>("Bisset"),
                		          LocationFilter<Registration>.SCOTLAND),
                    byCensusLocation);
                    

            Console.WriteLine("GROS extraction started.");
            List<Registration> births = ft.getAllBirthRegistrations();
            BirthOutputFormatter birthFormatter = new BirthOutputFormatter();
            process("OPR_births", oprRP, births, birthFormatter);
            process("Online_births", onlineBirthsRP, births, birthFormatter);
            process("GROS_births", grosBirthsRP, births, birthFormatter);
            births = null;
            Console.WriteLine("GROS births files created.");
            
            List<Registration> deaths = ft.getAllDeathRegistrations();
            DeathOutputFormatter deathFormatter = new DeathOutputFormatter();
            process("OPR_deaths", oprRP, deaths, deathFormatter);
            process("Online_deaths", onlineDeathsRP, deaths, deathFormatter);
            process("GROS_deaths", grosDeathsRP, deaths, deathFormatter);
            deaths = null;
            Console.WriteLine("GROS deaths files created.");
            
            List<Registration> marriages = ft.getAllMarriageRegistrations();
            MarriageOutputFormatter marriageFormatter = new MarriageOutputFormatter();
            process("OPR_marriages", oprRP, marriages, marriageFormatter);
            process("Online_marriages", onlineMarriagesRP, marriages, marriageFormatter);
            process("GROS_marriages", grosMarriagesRP, marriages, marriageFormatter);
            marriages = null;
            Console.WriteLine("GROS marriages files created");
 /*
            List<Registration> census;
            CensusOutputFormatter censusFormatter = new CensusOutputFormatter();
             
            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1841, false);
            process("bisset_1841_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1841 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1851, false);
            process("bisset_1851_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1851 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1861, false);
            process("bisset_1861_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1861 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1871, false);
            process("bisset_1871_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1871 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1881, false);
            process("bisset_1881_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1881 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1891, false);
            process("bisset_1891_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1891 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1901, false);
            process("bisset_1901_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1901 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1841, false);
            process("1841_census", censusRP, census, censusFormatter);
            Console.WriteLine("1841 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1851, false);
            process("1851_census", censusRP, census, censusFormatter);
            Console.WriteLine("1851 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1861, false);
            process("1861_census", censusRP, census, censusFormatter);
            Console.WriteLine("1861 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1871, false);
            process("1871_census", censusRP, census, censusFormatter);
            Console.WriteLine("1871 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1881, false);
            process("1881_census", censusRP, census, censusFormatter);
            Console.WriteLine("1881 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1891, false);
            process("1891_census", censusRP, census, censusFormatter);
            Console.WriteLine("1891 Census Details written.");

            census = ft.getAllCensusRegistrations(CensusDate.UKCENSUS1901, false);
            process("1901_census", censusRP, census, censusFormatter);
            Console.WriteLine("1901 Census Details written.");
*/
            Console.WriteLine("GROS Search completed.");
        }    
        
        private static void process(String filename, RegistrationsProcessor rp, 
            						List<Registration> sourceRegs, BaseOutputFormatter formatter) 
    	{
            TextWriter output = new StreamWriter("C:/temp/GROS/" + filename + ".csv");
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