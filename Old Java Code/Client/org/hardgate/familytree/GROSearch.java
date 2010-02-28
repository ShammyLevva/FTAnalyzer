/*
 * Created on 29-Dec-2004
 *
 */
package org.hardgate.familytree;

import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.List;

import org.hardgate.familytree.comparators.DateComparator;
import org.hardgate.familytree.comparators.LocationComparator;
import org.hardgate.familytree.comparators.MultiComparator;
import org.hardgate.familytree.comparators.NameComparator;
import org.hardgate.familytree.core.Client;
import org.hardgate.familytree.core.FactDate;
import org.hardgate.familytree.core.Individual;
import org.hardgate.familytree.core.Location;
import org.hardgate.familytree.filters.AllFilter;
import org.hardgate.familytree.filters.AndFilter;
import org.hardgate.familytree.filters.BirthFilter;
import org.hardgate.familytree.filters.DateFilter;
import org.hardgate.familytree.filters.IncompleteDataFilter;
import org.hardgate.familytree.filters.OrFilter;
import org.hardgate.familytree.filters.RegistrationFilter;
import org.hardgate.familytree.filters.RelationFilter;
import org.hardgate.familytree.formatters.BaseOutputFormatter;
import org.hardgate.familytree.formatters.BirthOutputFormatter;
import org.hardgate.familytree.formatters.CensusOutputFormatter;
import org.hardgate.familytree.formatters.DeathOutputFormatter;
import org.hardgate.familytree.formatters.MarriageOutputFormatter;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author A-Bisset
 *
 */
public class GROSearch {

    public static void main (String[] args) throws IOException {
        
        Client client = Client.getInstance();
        if(args.length == 0) {
            System.out.println("No member ID given. Format is GROSearch memberID");
            System.exit(1);
        }
        int memberID = Integer.parseInt(args[0]);

	    //client.setRelations(memberID,"I0001");
	    //client.printRelationCount(new PrintWriter(System.out), memberID);
	    //client.setParishes(memberID);
        
        System.out.println("GRO Search generation started.");

        MultiComparator<Registration> byLocation = 
        		new MultiComparator<Registration>();
        MultiComparator<Registration> byCensusLocation = 
    		new MultiComparator<Registration>();
        MultiComparator<Registration> byName = 
    		new MultiComparator<Registration>();
        byLocation.addComparator(new LocationComparator(Location.PARISH));
        byLocation.addComparator(new DateComparator());
        byLocation.addComparator(new NameComparator());
        byCensusLocation.addComparator(new LocationComparator(Location.PARISH));
        byCensusLocation.addComparator(new DateComparator());
        byName.addComparator(new NameComparator());
        byName.addComparator(new DateComparator());
        
        RegistrationFilter missingData = IncompleteDataFilter.MISSING_DATA_FILTER;
        // partial filter has data but only up to parish level ie: no address
        
        RegistrationFilter partialData = new IncompleteDataFilter(Location.PARISH);
        
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
                new AllFilter(), byCensusLocation);
                
        System.out.println("GRO extraction started.");
        List<Registration> births = client.getAllBirthRegistrations(memberID);
        BirthOutputFormatter birthFormatter = new BirthOutputFormatter();
        //process("OPR_births", oprRP, births, birthFormatter);
        process("Online_births", onlineBirthsRP, births, birthFormatter);
        births = null;
        System.out.println("GRO births files created.");
        
        List<Registration> deaths = client.getAllDeathRegistrations(memberID);
        DeathOutputFormatter deathFormatter = new DeathOutputFormatter();
        //process("OPR_deaths", oprRP, deaths, deathFormatter);
        process("Online_deaths", onlineDeathsRP, deaths, deathFormatter);
        deaths = null;
        System.out.println("GRO deaths files created.");
        
        List<Registration> marriages = client.getAllMarriageRegistrations(memberID);
        MarriageOutputFormatter marriageFormatter = new MarriageOutputFormatter();
        //process("OPR_marriages", oprRP, marriages, marriageFormatter);
        process("Online_marriages", onlineMarriagesRP, marriages, marriageFormatter);
        marriages = null;
        System.out.println("GRO marriages files created");

        List<Registration> census;
        CensusOutputFormatter censusFormatter = new CensusOutputFormatter();
          
        census = client.getAllCensusRegistrations(memberID,FactDate.CENSUS1841);
        process("1841_census", censusRP, census, censusFormatter);
        System.out.println("1841 Census Details written.");
    
        census = client.getAllCensusRegistrations(memberID,FactDate.CENSUS1851);
        process("1851_census", censusRP, census, censusFormatter);
        System.out.println("1851 Census Details written.");

        census = client.getAllCensusRegistrations(memberID,FactDate.CENSUS1861);
        process("1861_census", censusRP, census, censusFormatter);
        System.out.println("1861 Census Details written.");

        census = client.getAllCensusRegistrations(memberID,FactDate.CENSUS1871);
        process("1871_census", censusRP, census, censusFormatter);
        System.out.println("1871 Census Details written.");

        census = client.getAllCensusRegistrations(memberID,FactDate.CENSUS1881);
        process("1881_census", censusRP, census, censusFormatter);
        System.out.println("1881 Census Details written.");
        
        census = client.getAllCensusRegistrations(memberID,FactDate.CENSUS1891);
        process("1891_census", censusRP, census, censusFormatter);
        System.out.println("1891 Census Details written.");

        census = client.getAllCensusRegistrations(memberID,FactDate.CENSUS1901);
        process("1901_census", censusRP, census, censusFormatter);
        System.out.println("1901 Census Details written.");

        System.out.println("GRO Search completed.");
    }    
    
    private static void process(String filename, RegistrationsProcessor rp, 
            						  List<Registration> sourceRegs, BaseOutputFormatter formatter) 
    						throws IOException {
        PrintWriter output = new PrintWriter(new FileWriter("C:/temp/GRO/" + filename + ".csv"));
        formatter.printHeader(output);
        List<Registration> regs = rp.processRegistrations(sourceRegs);
        for (Registration r : regs) {
            formatter.printItem(r, output);
        }
        System.out.println("written " + regs.size() + " records to " + filename);
        output.close();
    }
}