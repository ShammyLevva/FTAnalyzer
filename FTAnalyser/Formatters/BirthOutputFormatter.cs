using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class BirthOutputFormatter : BaseOutputFormatter {

        public void printHeader (PrintWriter output) {
            output.print("Surname,Forenames,Age,Occupation,Residence,");
            output.print("DateOfBirth,PlaceOfBirth,BestLocation,");
            output.print("FathersName,FathersOccupation,FatherDeceased,");
            output.print("MothersName,MothersOccupation,MotherDeceased,");
            output.print("Gender,ParentsMarriageDate,ParentsMarriagePlace");
            output.println();
        }

        public void printItem (Registration reg, PrintWriter output) {
            BirthRegistration b = (BirthRegistration) reg;
            printFamilyGroup(b.getRegistrationDate(), b.getFamilyGroup(), output);
            output.print("\""); output.print(b.getGender()); output.print("\",");
            output.print("\""); output.print(b.getParentsMarriageDate()); output.print("\",");
            output.print("\""); output.print(b.getParentsMarriageLocation()); output.println("\"");
        }
    }
}