/*
 * Created on 29-Dec-2004
 *
 */
package org.hardgate.familytree;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.Properties;

import org.hardgate.familytree.core.Client;
import org.hardgate.familytree.core.Family;

/**
 * @author A-Bisset
 *
 */
public class IGIBatchSearch {

    public static void main (String[] args) throws IOException {
        if(args.length == 0) {
            System.out.println("No member ID given. Format is IGIBatchSearch memberID");
            System.exit(1);
        }
        int memberID = Integer.parseInt(args[0]);
        if (args.length == 3) {
			Properties systemProperties = System.getProperties();
			systemProperties.setProperty("http.proxyHost",args[1]);
			systemProperties.setProperty("http.proxyPort",args[2]);
        }

        IGISearchForm form = new IGISearchForm();
        Client client = Client.getInstance();
        System.out.println("IGI Slurp started for " + memberID);
        Family[] families = client.getAllFamilies(memberID);
        for(int i=0; i<families.length; i++) {
            System.out.print(i + "..");
            form.searchIGI(families[i],"c:/temp/IGISearch/children",
                    IGISearchForm.CHILDRENSEARCH);
            form.searchIGI(families[i],"c:/temp/IGISearch/marriages",
                    IGISearchForm.MARRIAGESEARCH);
        }
        System.out.println();
        System.out.println("IGI Slurp finished");
    }
    
    private static void writeResultHeader(PrintWriter resultFile) {
        resultFile.println("<?xml version='1.0' encoding='UTF-8' standalone='yes' ?>");
        resultFile.println("<!DOCTYPE IGIResults [");
        resultFile.println("<!ELEMENT IGIResults (Individual*)>");
        resultFile.println("<!ELEMENT Individual (Name,Gender,Birth,Christening,Death,Burial,Father,Mother,Spouse,Marriage)>");
        resultFile.println("<!ELEMENT Name        (#PCDATA)>");
        resultFile.println("<!ELEMENT Gender      (#PCDATA)>");
        resultFile.println("<!ELEMENT Birth       (#PCDATA)>");
        resultFile.println("<!ELEMENT Christening (#PCDATA)>");
        resultFile.println("<!ELEMENT Death       (#PCDATA)>");
        resultFile.println("<!ELEMENT Burial      (#PCDATA)>");
        resultFile.println("<!ELEMENT Father      (#PCDATA)>");
        resultFile.println("<!ELEMENT Mother      (#PCDATA)>");
        resultFile.println("<!ELEMENT Spouse      (#PCDATA)>");
        resultFile.println("<!ELEMENT Marriage    (#PCDATA)>");
        resultFile.println("]>");
    
        resultFile.println("<IGIResults>");
    }

    private static void writeResultFooter(PrintWriter resultFile) {
        resultFile.write("</IGIResults>");
        resultFile.close();
    }
}