using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace FTAnalyser
{
    public class DeathOutputFormatter : BaseOutputFormatter {

        public override void printHeader(StreamWriter output)
        {
            output.Write("Surname,Forenames,Age,Occupation,Residence,");
            output.Write("BirthDate,BirthLocation,BestLocation,");
            output.Write("FathersName,FathersOccupation,FatherDeceased,");
            output.Write("MothersName,MothersOccupation,MotherDeceased,");
            output.Write("SpousesName,SpousesOccupation,SpouseDeceased,");
            output.Write("DateOfDeath,PlaceOfDeath,MaritalStatus,Gender,");
            output.Write("CauseOfDeath");
            output.WriteLine();
        }

        public override void printItem(Registration reg, StreamWriter output)
        {
            DeathRegistration d = (DeathRegistration) reg;
            printFamilyGroup(d.getRegistrationDate(), d.getFamilyGroup(), output);
            output.Write("\""); output.Write(d.getSpousesName()); output.Write("\",");
            output.Write("\""); output.Write(d.getSpousesOccupation()); output.Write("\",");
            output.Write("\""); output.Write(d.getSpouseDeceased()); output.Write("\",");
            output.Write("\""); output.Write(d.getDateOfDeath()); output.Write("\",");
            output.Write("\""); output.Write(d.getPlaceOfDeath()); output.Write("\",");
            output.Write("\""); output.Write(d.getMaritalStatus()); output.Write("\",");
            output.Write("\""); output.Write(d.getGender()); output.Write("\",");
            output.Write("\""); output.Write(d.getCauseOfDeath()); output.Write("\"");
            output.WriteLine();
        }
    }
}