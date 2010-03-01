using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class MarriageOutputFormatter : BaseOutputFormatter {

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

        public void printItem (Registration reg, PrintWriter output) {
            MarriageRegistration m = (MarriageRegistration) reg;
            if (m.getGender().Equals("M")) {
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
}