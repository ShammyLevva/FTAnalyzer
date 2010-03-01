using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace FTAnalyser
{
    public abstract class BaseOutputFormatter
    {
        public abstract void printHeader(StreamWriter output);
        public abstract void printItem(Registration reg, StreamWriter output);

        internal void printFamilyGroup(FactDate fd, ParentalGroup f, StreamWriter output)
        {
            if (f != null)
            {
                Individual ind = f.getIndividual();
                output.Write("\""); output.Write(ind.getSurname()); output.Write("\",");
                output.Write("\""); output.Write(ind.getForenames()); output.Write("\",");
                output.Write("\""); output.Write(ind.getAge(fd)); output.Write("\",");
                output.Write("\""); output.Write(ind.getOccupation()); output.Write("\",");
                output.Write("\""); output.Write(f.getResidence()); output.Write("\",");
                output.Write("\""); output.Write(ind.getDateOfBirth()); output.Write("\",");
                output.Write("\""); output.Write(ind.getBirthLocation()); output.Write("\",");
                output.Write("\""); output.Write(ind.getBestLocation()); output.Write("\",");
                output.Write("\""); output.Write(f.getFathersName()); output.Write("\",");
                output.Write("\""); output.Write(f.getFathersOccupation()); output.Write("\",");
                output.Write("\""); output.Write(f.getFatherDeceased(fd)); output.Write("\",");
                output.Write("\""); output.Write(f.getMothersName()); output.Write("\",");
                output.Write("\""); output.Write(f.getMothersOccupation()); output.Write("\",");
                output.Write("\""); output.Write(f.getMotherDeceased(fd)); output.Write("\",");
            }
            else
            {
                output.Write(",,,,,,,,,,,,,,");
            }
        }
    }
}