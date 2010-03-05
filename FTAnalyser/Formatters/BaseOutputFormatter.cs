using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace FTAnalyzer
{
    public abstract class BaseOutputFormatter
    {
        public abstract void printHeader(TextWriter output);
        public abstract void printItem(Registration reg, TextWriter output);

        internal void printFamilyGroup(FactDate fd, ParentalGroup f, TextWriter output)
        {
            if (f != null)
            {
                Individual ind = f.Individual;
                output.Write("\""); output.Write(ind.Surname); output.Write("\",");
                output.Write("\""); output.Write(ind.Forenames); output.Write("\",");
                output.Write("\""); output.Write(ind.getAge(fd)); output.Write("\",");
                output.Write("\""); output.Write(ind.Occupation); output.Write("\",");
                output.Write("\""); output.Write(f.Residence); output.Write("\",");
                output.Write("\""); output.Write(ind.DateOfBirth); output.Write("\",");
                output.Write("\""); output.Write(ind.BirthLocation); output.Write("\",");
                output.Write("\""); output.Write(ind.BestLocation); output.Write("\",");
                output.Write("\""); output.Write(f.FathersName); output.Write("\",");
                output.Write("\""); output.Write(f.FathersOccupation); output.Write("\",");
                output.Write("\""); output.Write(f.isFatherDeceased(fd)); output.Write("\",");
                output.Write("\""); output.Write(f.MothersName); output.Write("\",");
                output.Write("\""); output.Write(f.MothersOccupation); output.Write("\",");
                output.Write("\""); output.Write(f.isMotherDeceased(fd)); output.Write("\",");
            }
            else
            {
                output.Write(",,,,,,,,,,,,,,");
            }
        }
    }
}