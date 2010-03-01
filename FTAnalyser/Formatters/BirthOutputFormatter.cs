using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace FTAnalyser
{
    public class BirthOutputFormatter : BaseOutputFormatter {

        public override void printHeader(StreamWriter output)
        {
            output.Write("Surname,Forenames,Age,Occupation,Residence,");
            output.Write("DateOfBirth,PlaceOfBirth,BestLocation,");
            output.Write("FathersName,FathersOccupation,FatherDeceased,");
            output.Write("MothersName,MothersOccupation,MotherDeceased,");
            output.Write("Gender,ParentsMarriageDate,ParentsMarriagePlace");
            output.WriteLine();
        }

        public override void printItem(Registration reg, StreamWriter output)
        {
            BirthRegistration b = (BirthRegistration) reg;
            printFamilyGroup(b.getRegistrationDate(), b.getFamilyGroup(), output);
            output.Write("\""); output.Write(b.getGender()); output.Write("\",");
            output.Write("\""); output.Write(b.getParentsMarriageDate()); output.Write("\",");
            output.Write("\""); output.Write(b.getParentsMarriageLocation()); output.WriteLine("\"");
        }
    }
}