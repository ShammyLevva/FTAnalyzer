/*
 * Created on 29-Dec-2004
 *
 */
package org.hardgate.familytree;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.URL;
import java.net.URLConnection;

/**
 * @author A-Bisset
 *  
 */
public class IGISearch {

    public static void main (String[] args) throws IOException {
        // URL of CGI-Bin script.
        URL url = new URL("http://www.familysearch.org/Eng/Search/customsearchresults.asp");
        
        // URL connection channel.
        URLConnection urlConn = url.openConnection();
        urlConn.setDoInput(true);
        urlConn.setDoOutput(true);
        urlConn.setUseCaches(false);
        urlConn.setRequestProperty("Content-Type",
        		"application/x-www-form-urlencoded");
        
        // Send POST output.
        DataOutputStream printout = new DataOutputStream(
                urlConn.getOutputStream());
        
        IGISearchForm form = new IGISearchForm();
        form.setParameter("fathers_last_name", "moffat");
        form.setParameter("fathers_first_name", "john");
        form.setParameter("mothers_last_name", "allen");
        form.setParameter("mothers_first_name", "mary");
        String content = form.getEncodedParameters();
        printout.writeBytes(content);
        printout.flush();
        printout.close();
        
        // Get response data.
        BufferedReader input = new BufferedReader(
                new InputStreamReader(urlConn.getInputStream()));
        PrintWriter output = new PrintWriter(new FileWriter("results.html"));
        String str;
        while (null != ((str = input.readLine()))) {
            output.println(str);
        }
        input.close();
        output.close();
        System.out.println("Results File written:\n");
    }
}