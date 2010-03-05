using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace FTAnalyzer
{
    public class BirthOutputFormatter : BaseOutputFormatter {

        public override void printHeader(TextWriter output)
        {
            output.Write("Surname,Forenames,Age,Occupation,Residence,");
            output.Write("DateOfBirth,PlaceOfBirth,BestLocation,");
            output.Write("FathersName,FathersOccupation,FatherDeceased,");
            output.Write("MothersName,MothersOccupation,MotherDeceased,");
            output.Write("Gender,ParentsMarriageDate,ParentsMarriagePlace");
            output.WriteLine();
        }

        public override void printItem(Registration reg, TextWriter output)
        {
            BirthRegistration b = (BirthRegistration) reg;
            printFamilyGroup(b.RegistrationDate, b.FamilyGroup, output);
            output.Write("\""); output.Write(b.Gender); output.Write("\",");
            output.Write("\""); output.Write(b.ParentsMarriageDate); output.Write("\",");
            output.Write("\""); output.Write(b.ParentsMarriageLocation); output.WriteLine("\"");
        }
    }
}