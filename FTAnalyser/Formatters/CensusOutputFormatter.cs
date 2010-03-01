using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class CensusOutputFormatter : BaseOutputFormatter {

        public void printHeader(PrintWriter output) {
            output.print("FamilyGed,BestLocation,CensusName,Age,");
            output.print("Occupation,DateOfBirth,BirthLocation,Status");
            output.println();
        }

        public void printItem(Registration reg, PrintWriter output) {
            CensusRegistration c = (CensusRegistration) reg;
            List<Individual> members = c.getMembers();
            Collections.sort(members, new CensusAgeComparator());
            foreach (Individual i in members) {
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
}