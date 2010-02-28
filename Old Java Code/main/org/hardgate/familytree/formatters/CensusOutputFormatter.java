/*
 * Created on 14-Jan-2005
 *
 */
package org.hardgate.familytree.formatters;

import java.io.PrintWriter;
import java.util.Collections;
import java.util.List;

import org.hardgate.familytree.comparators.CensusAgeComparator;
import org.hardgate.familytree.core.Individual;
import org.hardgate.familytree.registrations.CensusRegistration;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author a-bisset
 *
 */
public class CensusOutputFormatter extends BaseOutputFormatter {

    public void printHeader(PrintWriter output) {
        output.print("FamilyGed,BestLocation,CensusName,Age,");
        output.print("Occupation,DateOfBirth,BirthLocation,Status");
        output.println();
    }

    public void printItem(Registration reg, PrintWriter output) {
        CensusRegistration c = (CensusRegistration) reg;
        List<Individual> members = c.getMembers();
        Collections.sort(members, new CensusAgeComparator());
        for (Individual i : members) {
            output.print("\""); output.print(c.getFamilyGed()); output.print("\",");
            output.print("\""); output.print(c.getRegistrationLocation()); output.print("\",");
            output.print("\""); output.print(i.getCensusName()); output.print("\",");
            output.print("\""); output.print(i.getAge(c.getRegistrationDate())); output.print("\",");
            output.print("\""); output.print(i.getOccupation()); output.print("\",");
            output.print("\""); output.print(i.getDateOfBirth()); output.print("\",");
            output.print("\""); output.print(i.getBirthLocation()); output.print("\",");
            output.print("\""); output.print(i.getStatus()); output.print("\"");
            output.println();
        }
    }
}
