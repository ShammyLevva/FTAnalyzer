using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FTAnalyser
{
    public class IGISearch {

        public static void main (String[] args) {
            // URL of CGI-Bin script.
            HttpWebRequest url = (HttpWebRequest)WebRequest.Create("http://www.familysearch.org/Eng/Search/customsearchresults.asp");
/* TODO: URL Stuff            
            // URL connection channel.
            URLConnection urlConn = url.openConnection();
            urlConn.setDoInput(true);
            urlConn.setDoOutput(true);
            urlConn.setUseCaches(false);
            urlConn.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
            
            // Send POST output.
            DataOutputStream printout = new DataOutputStream(urlConn.getOutputStream());
           
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
            BufferedReader input = new BufferedReader(new InputStreamReader(urlConn.getInputStream()));
            TextWriter output = new StreamWriter("results.html");
            String str;
            while (null != ((str = input.readLine()))) {
                output.WriteLine(str);
            }
            input.Close();
            output.Close();
 */
            Console.WriteLine("Results File written:\n");
        }
    }
}