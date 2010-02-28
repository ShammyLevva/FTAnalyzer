/*
 * Created on 04-Jan-2005
 *
 */
package org.hardgate.familytree.formatters;

import java.io.PrintWriter;

import org.hardgate.familytree.registrations.DeathRegistration;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author db
 *
 */
public class DeathOutputFormatter extends BaseOutputFormatter {

    /* (non-Javadoc)
     * @see org.hardgate.familytree.formatters.BaseOutputFormatter#printHeader(java.io.PrintWriter)
     */
    public void printHeader (PrintWriter output) {
        output.print("Surname,Forenames,Age,Occupation,Residence,");
        output.print("BirthDate,BirthLocation,BestLocation,");
        output.print("FathersName,FathersOccupation,FatherDeceased,");
        output.print("MothersName,MothersOccupation,MotherDeceased,");
        output.print("SpousesName,SpousesOccupation,SpouseDeceased,");
        output.print("DateOfDeath,PlaceOfDeath,MaritalStatus,Gender,");
        output.print("CauseOfDeath");
        output.println();
    }

    /* (non-Javadoc)
     * @see org.hardgate.familytree.formatters.BaseOutputFormatter#printItem(org.hardgate.familytree.Registration, java.io.PrintWriter)
     */
    public void printItem (Registration reg, PrintWriter output) {
        DeathRegistration d = (DeathRegistration) reg;
        printFamilyGroup(d.getRegistrationDate(), d.getFamilyGroup(), output);
        output.print("\""); output.print(d.getSpousesName()); output.print("\",");
        output.print("\""); output.print(d.getSpousesOccupation()); output.print("\",");
        output.print("\""); output.print(d.getSpouseDeceased()); output.print("\",");
        output.print("\""); output.print(d.getDateOfDeath()); output.print("\",");
        output.print("\""); output.print(d.getPlaceOfDeath()); output.print("\",");
        output.print("\""); output.print(d.getMaritalStatus()); output.print("\",");
        output.print("\""); output.print(d.getGender()); output.print("\",");
        output.print("\""); output.print(d.getCauseOfDeath()); output.print("\"");
        output.println();
    }
}
