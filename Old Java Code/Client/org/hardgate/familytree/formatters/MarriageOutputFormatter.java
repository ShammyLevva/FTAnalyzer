/*
 * Created on 04-Jan-2005
 *
 */
package org.hardgate.familytree.formatters;

import java.io.PrintWriter;

import org.hardgate.familytree.registrations.MarriageRegistration;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author db
 *
 */
public class MarriageOutputFormatter extends BaseOutputFormatter {

    /* (non-Javadoc)
     * @see org.hardgate.familytree.formatters.BaseOutputFormatter#printHeader(java.io.PrintWriter)
     */
    public void printHeader (PrintWriter output) {
        output.print("HusbandSurname,HusbandForenames,HusbandAge,HusbandOccupation,");
        output.print("HusbandResidence,HusbandBirthDate,HusbandBirthLocation,HusbandBestLocation,");
        output.print("HusbandsFathersName,HusbandsFathersOccupation,HusbandsFatherDeceased,");
        output.print("HusbandsMothersName,HusbandsMothersOccupation,HusbandsMotherDeceased,");
        output.print("WifeSurname,WifeForenames,WifeAge,WifeOccupation,");
        output.print("WifeResidence,WifeBirthDate,WifeBirthLocation,WifeBestLocation,");
        output.print("WifesFathersName,WifesFathersOccupation,WifesFatherDeceased,");
        output.print("WifesMothersName,WifesMothersOccupation,WifesMotherDeceased,");
        output.print("Religion,DateOfMarriage,PlaceOfMarriage");
        output.println();
     }

    /* (non-Javadoc)
     * @see org.hardgate.familytree.formatters.BaseOutputFormatter#printItem(org.hardgate.familytree.Registration, java.io.PrintWriter)
     */
    public void printItem (Registration reg, PrintWriter output) {
        MarriageRegistration m = (MarriageRegistration) reg;
        if (m.getGender().equals("M")) {
            printFamilyGroup(m.getRegistrationDate(), m.getFamilyGroup(), output);
            printFamilyGroup(m.getRegistrationDate(), m.getSpousesFamilyGroup(), output);
        } else {
            printFamilyGroup(m.getRegistrationDate(), m.getSpousesFamilyGroup(), output);
            printFamilyGroup(m.getRegistrationDate(), m.getFamilyGroup(), output);
        }
        output.print("\""); output.print(m.getReligion()); output.print("\",");
        output.print("\""); output.print(m.getDateOfMarriage()); output.print("\",");
        output.print("\""); output.print(m.getPlaceOfMarriage()); output.println("\"");
    }
}
