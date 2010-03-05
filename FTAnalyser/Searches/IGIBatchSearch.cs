using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class IGIBatchSearch
    {

        public static void main(String[] args)
        {
            if (args.Length == 3)
            {
//                Properties systemProperties = System.getProperties();
//                systemProperties.setProperty("http.proxyHost", args[1]);
//                systemProperties.setProperty("http.proxyPort", args[2]);
            }

            IGISearchForm form = new IGISearchForm();
            FamilyTree ft = FamilyTree.Instance;
            Console.WriteLine("IGI Slurp started.");
            List<Family> families = ft.AllFamilies;
            int counter = 0;
            foreach (Family f in families)
            {
                Console.Write(counter++ + "..");
                form.SearchIGI(f, "c:/temp/IGISearch/children", (int) IGISearchForm.CHILDRENSEARCH);
                form.SearchIGI(f, "c:/temp/IGISearch/marriages", (int) IGISearchForm.MARRIAGESEARCH);
            }
            Console.WriteLine();
            Console.WriteLine("IGI Slurp finished");
        }

        private static void writeResultHeader(TextWriter resultFile)
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

        private static void writeResultFooter(TextWriter resultFile)
        {
            resultFile.Write("</IGIResults>");
            resultFile.Close();
        }
    }
}