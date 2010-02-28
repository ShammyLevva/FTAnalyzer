/*
 * Created on 04-Jan-2005
 *
 */
package org.hardgate.familytree.formatters;

import java.io.PrintWriter;

import org.hardgate.familytree.registrations.BirthRegistration;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author db
 *
 */
public class BirthOutputFormatter extends BaseOutputFormatter {

    /* (non-Javadoc)
     * @see org.hardgate.familytree.formatters.BaseOutputFormatter#printHeader(java.io.PrintWriter)
     */
    public void printHeader (PrintWriter output) {
        output.print("Surname,Forenames,Age,Occupation,Residence,");
        output.print("DateOfBirth,PlaceOfBirth,BestLocation,");
        output.print("FathersName,FathersOccupation,FatherDeceased,");
        output.print("MothersName,MothersOccupation,MotherDeceased,");
        output.print("Gender,ParentsMarriageDate,ParentsMarriagePlace");
        output.println();
    }

    /* (non-Javadoc)
     * @see org.hardgate.familytree.formatters.BaseOutputFormatter#printItem(org.hardgate.familytree.Registration, java.io.PrintWriter)
     */
    public void printItem (Registration reg, PrintWriter output) {
        BirthRegistration b = (BirthRegistration) reg;
        printFamilyGroup(b.getRegistrationDate(), b.getFamilyGroup(), output);
        output.print("\""); output.print(b.getGender()); output.print("\",");
        output.print("\""); output.print(b.getParentsMarriageDate()); output.print("\",");
        output.print("\""); output.print(b.getParentsMarriageLocation()); output.println("\"");
    }
}
