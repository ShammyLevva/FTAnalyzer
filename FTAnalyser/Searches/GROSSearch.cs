using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyser
{
    public class GROSSearch {

        public static void main (String[] args) {

            FamilyTree ft = FamilyTree.Instance;
            Console.WriteLine("GROS Search generation started.");

            MultiComparator<Registration> byLocation = new MultiComparator<Registration>();
            MultiComparator<Registration> byCensusLocation = new MultiComparator<Registration>();
            MultiComparator<Registration> byName = new MultiComparator<Registration>();
            byLocation.addComparator(new LocationComparator(Location.PARISH));
            byLocation.addComparator(new DateComparator());
            byLocation.addComparator(new NameComparator());
            byCensusLocation.addComparator(new LocationComparator(Location.PARISH));
            byCensusLocation.addComparator(new DateComparator());
            byName.addComparator(new NameComparator());
            byName.addComparator(new DateComparator());
            
            RegistrationFilter missingScottishData = new AndFilter(
                    IncompleteDataFilter.MISSING_DATA_FILTER,
                    LocationFilter.SCOTLAND_FILTER);
            // partial filter has data but only up to parish level ie: no address
            RegistrationFilter partialScottishData = new AndFilter(
                    new IncompleteDataFilter(Location.PARISH),
                    LocationFilter.SCOTLAND_FILTER);
            RegistrationFilter directOrBlood = new OrFilter(
        		    new OrFilter(new RelationFilter(Individual.DIRECT), 
        					     new RelationFilter(Individual.BLOOD)),
        		    new RelationFilter(Individual.MARRIAGEDB));
            RegistrationFilter unknownOrMarriage = new OrFilter(
                    new RelationFilter(Individual.UNKNOWN), 
                    new RelationFilter(Individual.MARRIAGE));
          
            RegistrationsProcessor onlineBirthsRP = new RegistrationsProcessor(
                    new AndFilter(DateFilter.ONLINE_BIRTH_FILTER, 
                            new AndFilter(directOrBlood,partialScottishData)),
                    byName);
            RegistrationsProcessor onlineDeathsRP = new RegistrationsProcessor(
                    new AndFilter(BirthFilter.ONLINE_DEATH_FILTER, 
                            new AndFilter(directOrBlood,partialScottishData)),
                    byName);
            RegistrationsProcessor onlineMarriagesRP = new RegistrationsProcessor(
                    new AndFilter(BirthFilter.ONLINE_MARRIAGE_FILTER, 
                            new AndFilter(directOrBlood,partialScottishData)),
                    byName);
            RegistrationsProcessor grosBirthsRP = new RegistrationsProcessor(
                    new AndFilter(DateFilter.GROS_BIRTH_FILTER, 
                            new AndFilter(directOrBlood,partialScottishData)),
                    byName);
            RegistrationsProcessor grosDeathsRP = new RegistrationsProcessor(
                    new AndFilter(BirthFilter.GROS_DEATH_FILTER, 
                            new AndFilter(directOrBlood,partialScottishData)),
                    byName);
            RegistrationsProcessor grosMarriagesRP = new RegistrationsProcessor(
                    new AndFilter(BirthFilter.GROS_MARRIAGE_FILTER, 
                            new AndFilter(directOrBlood,partialScottishData)),
                    byName);
            RegistrationsProcessor oprRP = new RegistrationsProcessor(
                    new AndFilter(DateFilter.PRE_1855_FILTER, 
                            new AndFilter(directOrBlood,partialScottishData)),
                    byLocation);
            RegistrationsProcessor censusRP = new RegistrationsProcessor(
                    new AndFilter(directOrBlood,LocationFilter.SCOTLAND_FILTER),
                    byCensusLocation);
            RegistrationsProcessor bissetRP = new RegistrationsProcessor(
                    new AndFilter(new SurnameFilter("Bisset"),
                		          LocationFilter.SCOTLAND_FILTER),
                    byCensusLocation);
                    

            Console.WriteLine("GROS extraction started.");
            List<Registration> births = client.getAllBirthRegistrations();
            BirthOutputFormatter birthFormatter = new BirthOutputFormatter();
            process("OPR_births", oprRP, births, birthFormatter);
            process("Online_births", onlineBirthsRP, births, birthFormatter);
            process("GROS_births", grosBirthsRP, births, birthFormatter);
            births = null;
            Console.WriteLine("GROS births files created.");
            
            List<Registration> deaths = client.getAllDeathRegistrations();
            DeathOutputFormatter deathFormatter = new DeathOutputFormatter();
            process("OPR_deaths", oprRP, deaths, deathFormatter);
            process("Online_deaths", onlineDeathsRP, deaths, deathFormatter);
            process("GROS_deaths", grosDeathsRP, deaths, deathFormatter);
            deaths = null;
            Console.WriteLine("GROS deaths files created.");
            
            List<Registration> marriages = client.getAllMarriageRegistrations();
            MarriageOutputFormatter marriageFormatter = new MarriageOutputFormatter();
            process("OPR_marriages", oprRP, marriages, marriageFormatter);
            process("Online_marriages", onlineMarriagesRP, marriages, marriageFormatter);
            process("GROS_marriages", grosMarriagesRP, marriages, marriageFormatter);
            marriages = null;
            Console.WriteLine("GROS marriages files created");

            List<Registration> census;
            CensusOutputFormatter censusFormatter = new CensusOutputFormatter();
              
            census = client.getAllCensusRegistrations(FactDate.CENSUS1841);
            process("bisset_1841_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1841 Census Details written.");
        
            census = client.getAllCensusRegistrations(FactDate.CENSUS1851);
            process("bisset_1851_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1851 Census Details written.");

            census = client.getAllCensusRegistrations(FactDate.CENSUS1861);
            process("bisset_1861_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1861 Census Details written.");

            census = client.getAllCensusRegistrations(FactDate.CENSUS1871);
            process("bisset_1871_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1871 Census Details written.");

            census = client.getAllCensusRegistrations(FactDate.CENSUS1881);
            process("bisset_1881_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1881 Census Details written.");
            
            census = client.getAllCensusRegistrations(FactDate.CENSUS1891);
            process("bisset_1891_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1891 Census Details written.");

            census = client.getAllCensusRegistrations(FactDate.CENSUS1901);
            process("bisset_1901_census", bissetRP, census, censusFormatter);
            Console.WriteLine("Bisset 1901 Census Details written.");

            census = client.getAllCensusRegistrations(FactDate.CENSUS1841);
            process("1841_census", censusRP, census, censusFormatter);
            Console.WriteLine("1841 Census Details written.");
        
            census = client.getAllCensusRegistrations(FactDate.CENSUS1851);
            process("1851_census", censusRP, census, censusFormatter);
            Console.WriteLine("1851 Census Details written.");

            census = client.getAllCensusRegistrations(FactDate.CENSUS1861);
            process("1861_census", censusRP, census, censusFormatter);
            Console.WriteLine("1861 Census Details written.");

            census = client.getAllCensusRegistrations(FactDate.CENSUS1871);
            process("1871_census", censusRP, census, censusFormatter);
            Console.WriteLine("1871 Census Details written.");

            census = client.getAllCensusRegistrations(FactDate.CENSUS1881);
            process("1881_census", censusRP, census, censusFormatter);
            Console.WriteLine("1881 Census Details written.");
            
            census = client.getAllCensusRegistrations(FactDate.CENSUS1891);
            process("1891_census", censusRP, census, censusFormatter);
            Console.WriteLine("1891 Census Details written.");

            census = client.getAllCensusRegistrations(FactDate.CENSUS1901);
            process("1901_census", censusRP, census, censusFormatter);
            Console.WriteLine("1901 Census Details written.");

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