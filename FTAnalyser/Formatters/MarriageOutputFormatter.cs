using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace FTAnalyser
{
    public class MarriageOutputFormatter : BaseOutputFormatter {

        public override void printHeader(StreamWriter output)
        {
            output.Write("HusbandSurname,HusbandForenames,HusbandAge,HusbandOccupation,");
            output.Write("HusbandResidence,HusbandBirthDate,HusbandBirthLocation,HusbandBestLocation,");
            output.Write("HusbandsFathersName,HusbandsFathersOccupation,HusbandsFatherDeceased,");
            output.Write("HusbandsMothersName,HusbandsMothersOccupation,HusbandsMotherDeceased,");
            output.Write("WifeSurname,WifeForenames,WifeAge,WifeOccupation,");
            output.Write("WifeResidence,WifeBirthDate,WifeBirthLocation,WifeBestLocation,");
            output.Write("WifesFathersName,WifesFathersOccupation,WifesFatherDeceased,");
            output.Write("WifesMothersName,WifesMothersOccupation,WifesMotherDeceased,");
            output.Write("Religion,DateOfMarriage,PlaceOfMarriage");
            output.WriteLine();
         }

        public override void printItem(Registration reg, StreamWriter output)
        {
            MarriageRegistration m = (MarriageRegistration) reg;
            if (m.Gender == "M") {
                printFamilyGroup(m.RegistrationDate, m.FamilyGroup, output);
                printFamilyGroup(m.RegistrationDate, m.SpousesFamilyGroup, output);
            } else {
                printFamilyGroup(m.RegistrationDate, m.SpousesFamilyGroup, output);
                printFamilyGroup(m.RegistrationDate, m.FamilyGroup, output);
            }
            output.Write("\""); output.Write(m.Religion); output.Write("\",");
            output.Write("\""); output.Write(m.DateOfMarriage); output.Write("\",");
            output.Write("\""); output.Write(m.PlaceOfMarriage); output.WriteLine("\"");
        }
    }
}