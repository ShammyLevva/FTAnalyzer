using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace FTAnalyser
{
    public class CensusOutputFormatter : BaseOutputFormatter {

        public override void printHeader(StreamWriter output)
        {
            output.Write("FamilyGed,BestLocation,CensusName,Age,");
            output.Write("Occupation,DateOfBirth,BirthLocation,Status");
            output.WriteLine();
        }

        public override void printItem(Registration reg, StreamWriter output)
        {
            CensusRegistration c = (CensusRegistration) reg;
            List<Individual> members = c.Members;
            members.Sort(new CensusAgeComparator());
            foreach (Individual i in members) {
                output.Write("\""); output.Write(c.FamilyGed); output.Write("\",");
                output.Write("\""); output.Write(c.RegistrationLocation); output.Write("\",");
                output.Write("\""); output.Write(i.CensusName); output.Write("\",");
                output.Write("\""); output.Write(i.getAge(c.RegistrationDate)); output.Write("\",");
                output.Write("\""); output.Write(i.Occupation); output.Write("\",");
                output.Write("\""); output.Write(i.DateOfBirth); output.Write("\",");
                output.Write("\""); output.Write(i.BirthLocation); output.Write("\",");
                output.Write("\""); output.Write(i.Status); output.Write("\"");
                output.WriteLine();
            }
        }
    }
}