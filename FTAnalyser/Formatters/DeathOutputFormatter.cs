using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace FTAnalyser
{
    public class DeathOutputFormatter : BaseOutputFormatter {

        public override void printHeader(TextWriter output)
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

        public override void printItem(Registration reg, TextWriter output)
        {
            DeathRegistration d = (DeathRegistration) reg;
            printFamilyGroup(d.RegistrationDate, d.FamilyGroup, output);
            output.Write("\""); output.Write(d.SpousesName); output.Write("\",");
            output.Write("\""); output.Write(d.SpousesOccupation); output.Write("\",");
            output.Write("\""); output.Write(d.SpouseDeceased); output.Write("\",");
            output.Write("\""); output.Write(d.DateOfDeath); output.Write("\",");
            output.Write("\""); output.Write(d.PlaceOfDeath); output.Write("\",");
            output.Write("\""); output.Write(d.MaritalStatus); output.Write("\",");
            output.Write("\""); output.Write(d.Gender); output.Write("\",");
            output.Write("\""); output.Write(d.CauseOfDeath); output.Write("\"");
            output.WriteLine();
        }
    }
}