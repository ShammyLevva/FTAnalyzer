/*
 * Created on 12-Sep-2004
 *
 */
package org.hardgate.familytree.core;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;

import javax.xml.transform.Source;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.sax.SAXTransformerFactory;
import javax.xml.transform.sax.TransformerHandler;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.stream.StreamSource;

import org.hardgate.familytree.xml.GedcomParser;
import org.hardgate.utilities.Messages;
import org.xml.sax.InputSource;

/**
 * @author A-Bisset
 *
 */
public class ReadGedcom {

	public static void main(String[] args) throws Exception {

 		transformGedcomToXML(
				Messages.getString("SourceGedcomFile"), 
				Messages.getString("InitialXMLFile")); 
		System.out.println("GEDCOM File Transformed to XML"); 

		transformXML(
				Messages.getString("InitialXMLFile"), 
				Messages.getString("ListType"), 
				Messages.getString("FinalXMLFile")); 
		System.out.println("Gedcom XML Transformed to Reporting XML"); 

		transformXML(
				Messages.getString("FinalXMLFile"), 
				Messages.getString("ListReportType"), 
				Messages.getString("OutputHTMLFile")); 
		System.out.println("XML Transformed to HTML"); 

	}

	private static void transformGedcomToXML(
			String sourcefile, String outfile) throws Exception{
		TransformerFactory tFactory =
			  TransformerFactory.newInstance();
		// define input file
		FileInputStream GEDsource = new FileInputStream(sourcefile);
		// define XML transfer file
		File stylesheet = new File("xml/GedcomToXml.xsl"); 
		// define XML output file
		File gedXml = new File(outfile);
		
		// get content handler for parser
		Source xsltSource = new StreamSource(stylesheet);
		SAXTransformerFactory saxTransFact =
				(SAXTransformerFactory) tFactory;
		TransformerHandler transHand = 
				saxTransFact.newTransformerHandler(xsltSource);
		// set output result file of transformer
		transHand.setResult(new StreamResult(gedXml));
	
		// Get a GEDCOM parser and set transformer as content handler 
		GedcomParser gedParser = new GedcomParser();		
		gedParser.setContentHandler(transHand);
		// parse file (writes to result file)
		gedParser.parse(new InputSource(GEDsource));
	}
	
	private static void transformXML(String gedXML, String stylesheet, String outfile) throws Exception {
		TransformerFactory tFactory =
				TransformerFactory.newInstance();
		FileOutputStream fos = new FileOutputStream(new File(outfile));
		Transformer transformer = 
				tFactory.newTransformer(new StreamSource("xml/" + stylesheet));
		transformer.transform(
				new StreamSource(gedXML), new StreamResult(fos));
	}
}
