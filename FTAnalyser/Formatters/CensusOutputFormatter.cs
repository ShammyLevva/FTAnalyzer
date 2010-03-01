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
            List<Individual> members = c.getMembers();
            members.Sort(new CensusAgeComparator());
            foreach (Individual i in members) {
                output.Write("\""); output.Write(c.getFamilyGed()); output.Write("\",");
                output.Write("\""); output.Write(c.getRegistrationLocation()); output.Write("\",");
                output.Write("\""); output.Write(i.getCensusName()); output.Write("\",");
                output.Write("\""); output.Write(i.getAge(c.getRegistrationDate())); output.Write("\",");
                output.Write("\""); output.Write(i.getOccupation()); output.Write("\",");
                output.Write("\""); output.Write(i.getDateOfBirth()); output.Write("\",");
                output.Write("\""); output.Write(i.getBirthLocation()); output.Write("\",");
                output.Write("\""); output.Write(i.getStatus()); output.Write("\"");
                output.WriteLine();
            }
        }
    }
}