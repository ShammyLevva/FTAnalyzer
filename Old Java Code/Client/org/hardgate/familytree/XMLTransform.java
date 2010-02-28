/*
 * Created on 29-Dec-2004
 *
 */
package org.hardgate.familytree;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;

import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.stream.StreamSource;

/**
 * @author A-Bisset
 *
 */
public class XMLTransform {

    public static void main (String[] args) throws IOException {
        TransformerFactory tFactory =
            TransformerFactory.newInstance();
      String path = "c:/Personal/Family History/IGI OPR results/";
      String xmlFile = "Barbour-Scotland-Ayrshire-all-OPR-Results";
//      String path = "g:/my documents/genealogy";
//      String xmlFile = "Bisset-Scotland-Aberdeenshire-all-OPR-Results";
      String stylesheet = "docroot/WEB-INF/xml/IGIOPR.xsl";
      String sourceId = path + xmlFile + ".xml";
      File htmlFile = new File(path + xmlFile + ".html");
//      String sourceId = path + "/search results/" + xmlFile + ".xml";
//     File htmlFile = new File(path + "/search results/" + xmlFile + ".html");
      try {
          FileOutputStream os = new FileOutputStream(htmlFile);
          Transformer transformer = 
              tFactory.newTransformer(new StreamSource(stylesheet));
          transformer.transform(
              new StreamSource(sourceId), new StreamResult(os));
       } catch (FileNotFoundException e) {
          // really bad if we have written file and its not now found
          System.out.println(e.getMessage());
      } catch (TransformerException e2) {
         System.out.println(e2.getMessage());            
      }
      System.out.println("Done");
   }
}