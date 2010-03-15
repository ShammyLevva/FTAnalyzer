using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer
{
    public class IGIResultWriter
    {
        private TextWriter resultFile;

        public IGIResultWriter(TextWriter resultFile)
        {
            this.resultFile = resultFile;
        }

        private void WriteHeader()
        {
            resultFile.WriteLine("<?xml version='1.0' encoding='UTF-8' standalone='yes' ?>");
            resultFile.WriteLine("<!DOCTYPE IGIResults [");
            resultFile.WriteLine("<!ELEMENT IGIResults (Individual*)>");
            resultFile.WriteLine("<!ELEMENT Individual (Name,Gender,Birth,Christening,Death,Burial,Father,Mother,Spouse,Marriage)>");
            resultFile.WriteLine("<!ELEMENT Name        (#PCDATA)>");
            resultFile.WriteLine("<!ELEMENT Gender      (#PCDATA)>");
            resultFile.WriteLine("<!ELEMENT Birth       (#PCDATA)>");
            resultFile.WriteLine("<!ELEMENT Christening (#PCDATA)>");
            resultFile.WriteLine("<!ELEMENT Death       (#PCDATA)>");
            resultFile.WriteLine("<!ELEMENT Burial      (#PCDATA)>");
            resultFile.WriteLine("<!ELEMENT Father      (#PCDATA)>");
            resultFile.WriteLine("<!ELEMENT Mother      (#PCDATA)>");
            resultFile.WriteLine("<!ELEMENT Spouse      (#PCDATA)>");
            resultFile.WriteLine("<!ELEMENT Marriage    (#PCDATA)>");
            resultFile.WriteLine("]>");

            resultFile.WriteLine("<IGIResults>");
        }

        private void writeResultFooter()
        {
            resultFile.Write("</IGIResults>");
            resultFile.Close();
        }

        public void writeResult(IGIResult result)
        {
            resultFile.WriteLine("<Individual>");
            resultFile.Write("<SearchType>");
            resultFile.Write(result.SearchType);
            resultFile.WriteLine("</SearchType>");
            resultFile.Write("<Batch>");
            resultFile.Write(result.Batch);
            resultFile.WriteLine("</Batch>");
            resultFile.Write("<Parish>");
            resultFile.Write(result.Parish);
            resultFile.WriteLine("</Parish>");
            resultFile.Write("<Name>");
            resultFile.Write(result.Person);
            resultFile.WriteLine("</Name>");
            resultFile.Write("<Gender>");
            resultFile.Write(result.Gender);
            resultFile.WriteLine("</Gender>");
            resultFile.Write("<Birth>");
            resultFile.Write(result.Birth);
            resultFile.WriteLine("</Birth>");
            resultFile.Write("<Christening>");
            resultFile.Write(result.Christening);
            resultFile.WriteLine("</Christening>");
            resultFile.Write("<Death>");
            resultFile.Write(result.Death);
            resultFile.WriteLine("</Death>");
            resultFile.Write("<Burial>");
            resultFile.Write(result.Burial);
            resultFile.WriteLine("</Burial>");
            resultFile.Write("<Father>");
            resultFile.Write(result.Father);
            resultFile.WriteLine("</Father>");
            resultFile.Write("<Mother>");
            resultFile.Write(result.Mother);
            resultFile.WriteLine("</Mother>");
            resultFile.Write("<Spouse>");
            resultFile.Write(result.Spouse);
            resultFile.WriteLine("</Spouse>");
            resultFile.Write("<Marriage>");
            resultFile.Write(result.Marriage);
            resultFile.WriteLine("</Marriage>");
            resultFile.WriteLine("</Individual>");
        }
    }
}