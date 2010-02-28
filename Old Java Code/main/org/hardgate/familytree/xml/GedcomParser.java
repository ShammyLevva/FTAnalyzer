package org.hardgate.familytree.xml;

import java.io.BufferedReader;
import java.io.IOException;
import java.net.URL;
import java.util.Locale;
import java.util.Stack;

import org.xml.sax.ContentHandler;
import org.xml.sax.DTDHandler;
import org.xml.sax.EntityResolver;
import org.xml.sax.ErrorHandler;
import org.xml.sax.InputSource;
import org.xml.sax.Locator;
import org.xml.sax.SAXException;
import org.xml.sax.SAXNotRecognizedException;
import org.xml.sax.SAXParseException;
import org.xml.sax.XMLReader;
import org.xml.sax.helpers.AttributesImpl;
import org.xml.sax.helpers.DefaultHandler;

/**
*  GedcomParser
*  
*  This class is designed to look like a SAX2-compliant XML parser; however,
*  it takes GEDCOM as its input rather than XML.
*  The events sent to the ContentHandler reflect the GEDCOM input "as is";
*  there is no validation or conversion of tags.
*
*  @author mhkay@iclway.co.uk
*  @version 21 January 2001 - revised to conform to SAX2
*/



public class GedcomParser implements XMLReader, Locator {

    private ContentHandler contentHandler;
    private ErrorHandler errorHandler;
    private AttributesImpl emptyAttList = new AttributesImpl();
    private AttributesImpl attList = new AttributesImpl();

    private String systemId;
    private int lineNr;

    /**
    * Set the ContentHandler
    * @param handler User-supplied content handler
    */

    public void setContentHandler(ContentHandler handler) {
        contentHandler = handler;
    }
    
    /**
    * Get the ContentHandler
    */
    
    public ContentHandler getContentHandler() {
        return contentHandler;
    }

    /**
    * Set the entityResolver.
    * This call has no effect, because entities are not used in GEDCOM files.
    */

    public void setEntityResolver(EntityResolver er) {}

    /**
    * Get the entityResolver
    */
    
    public EntityResolver getEntityResolver() {
        return null;
    }

    /**
    * Set the DTDHandler
    * This call has no effect, because DTDs are not used in GEDCOM files.
    */

    public void setDTDHandler(DTDHandler dh) {}

    /**
    * Get the DTDHandler
    */
    
    public DTDHandler getDTDHandler() {
        return null;
    }

    /**
    * Set the error handler
    * @param eh A user-supplied error handler
    */

    public void setErrorHandler(ErrorHandler eh) {
        errorHandler = eh;
    }

    /**
    * Get the error handler
    */
    
    public ErrorHandler getErrorHandler() {
        return errorHandler;
    }

    /**
    * Set the locale.
    * This call has no effect: locales are not supported.
    */

    public void setLocale(Locale locale) {}

    /**
    * Parse input from the supplied systemId
    */

    public void parse(String systemId) throws SAXException, IOException {
        this.systemId = systemId;
        parse( new BufferedReader (
                    new AnselInputStreamReader (
                        (new URL(systemId)).openStream() ) ) );
    }

    /**
    * Parse input from the supplied InputSource
    */

    public void parse(InputSource source) throws SAXException, IOException {

        if (contentHandler==null) contentHandler = new DefaultHandler();
        if (errorHandler==null) errorHandler = new DefaultHandler();
        systemId = source.getSystemId();
        
        if (source.getCharacterStream()!=null) {
            parse( new BufferedReader(source.getCharacterStream()) );
        } else if (source.getByteStream()!=null) {
            parse( new BufferedReader (
			            new AnselInputStreamReader (
			                source.getByteStream() ) ) );
        } else if (systemId!=null) {
            parse( systemId );
        } else {
            throw new SAXException("No input supplied");
        }
    }

    /**
    * Parse input from a supplied BufferedReader
    */

    private void parse(BufferedReader reader) throws SAXException, IOException {
        
        String line, currentLine, token1, token2;
        String level;
        int thislevel;
        int prevlevel;
        String iden, tag, xref, valu, type;
        int cpos1;
        int cpos2;
        int i;

        char[] newlineCharArray = new char[1];
        newlineCharArray[0] = '\n';

        lineNr = 0;
        currentLine = "";

        Stack<String> stack = new Stack<String>();
        stack.push("GED");
        
        prevlevel = -1;
        
        contentHandler.setDocumentLocator(this);
        contentHandler.startDocument();
        contentHandler.startElement("", "GED", "GED", emptyAttList);

        try {
     
          while ( (line=reader.readLine() ) != null ) {

            line=line.trim();

            lineNr++;
            currentLine = line;

            // parse the GEDCOM line into five fields: level, iden, tag, xref, valu

            if (line.length() > 0) {
                cpos1 = line.indexOf(' ');
                if (cpos1<0) throw new SAXException("No space in line");
                
                level = firstWord(line);
                try {
                    thislevel = Integer.parseInt(level);
                } catch (NumberFormatException err) {
                    throw new SAXException("Level number is not an integer");
                }

                // check the level number

                if (thislevel>prevlevel && !(thislevel==prevlevel+1))
                    throw new SAXException("Level numbers must increase by 1");
                if (thislevel<0)
                    throw new SAXException("Level number must not be negative");
                
                line = remainder(line);
                token1 = firstWord(line);
                line = remainder(line);
 
                if (token1.startsWith("@")) {
                    if (token1.length()==1 || !token1.endsWith("@"))
                        throw new SAXException("Bad xref_id");
                       
                    iden = token1.substring(1, token1.length() - 1);
                    tag = firstWord(line);
                    line = remainder(line);
                } else {
                    iden = "";
                    tag = token1;
                };

                xref = "";
                if ( line.startsWith("@")) {
                    token2 = firstWord(line);
                    if (token2.length()==1 || !token2.endsWith("@"))
                        throw new SAXException("Bad pointer value");
                        
                    xref = token2.substring(1, token2.length() - 1);
                    line = remainder(line);
                };
 
                valu = line;
                
                // perform validation on the CHAR field (character code)
                if (tag.equals("CHAR") &&
                    !(valu.trim().equals("ANSEL") || valu.trim().equals("ASCII")))
                {
                    System.err.println("WARNING: Character set is " + valu + ": should be ANSEL or ASCII");
                }

                // insert any necessary closing tags
                while (thislevel <= prevlevel) {
                    String endtag = (String)stack.pop();
                    contentHandler.endElement("", endtag, endtag);
                    prevlevel--;
                }

                if (!tag.equals("TRLR")) {
                    attList.clear();
                    if (!iden.equals("")) attList.addAttribute("", "ID", "ID", "ID", iden);
                    if (!xref.equals("")) attList.addAttribute("", "REF", "REF", "IDREF", xref);
                    contentHandler.startElement("", tag, tag, attList);
    	            stack.push(tag);
    	            prevlevel = thislevel;
                }               
               
                if (valu.length()>0) {
                    contentHandler.characters(valu.toCharArray(), 0, valu.length());
                }
	        }
         
          } // end while
 
          contentHandler.endElement("", "GED", "GED");
          contentHandler.endDocument();
          //System.err.println("Parsing complete: " + lineNr + " lines");

        } catch (SAXException e1) {
            SAXParseException err = new SAXParseException(e1.getMessage(), this);
            errorHandler.fatalError(err);
            throw err;
        } finally {
            reader.close();
        }
     
    };
    
    /**
    * Set a feature
    */
    
    public void setFeature(String s, boolean b) throws SAXNotRecognizedException {
        if (s.equals("http://xml.org/sax/features/namespaces") && b) return;
        if (s.equals("http://xml.org/sax/features/namespace-prefixes") && !b) return;        
        throw new SAXNotRecognizedException("Gedcom Parser does not recognize any features");
    }
    
    /**
    * Get a feature
    */
    
    public boolean getFeature(String s) throws SAXNotRecognizedException {
        if (s.equals("http://xml.org/sax/features/namespaces")) return true;
        if (s.equals("http://xml.org/sax/features/namespace-prefixes")) return false;     
        throw new SAXNotRecognizedException("Gedcom Parser does not recognize any features");
    }

    /**
    * Set a property
    */
    
    public void setProperty(String s, Object b) throws SAXNotRecognizedException {
        throw new SAXNotRecognizedException("Gedcom Parser does not recognize any properties");
    }
    
    /**
    * Get a property
    */
    
    public Object getProperty(String s) throws SAXNotRecognizedException {
        throw new SAXNotRecognizedException("Gedcom Parser does not recognize any properties");
    }    
    

  /**
    * Procedure to return the first word in a string
    */
    private static String firstWord(String inp)
    {
        int i;
        i = inp.indexOf(' ');
        if (i==0) return firstWord(inp.trim());
        if (i<0) return inp;
        return inp.substring(0,i).trim();
    };

  /**
    * Procedure to return the text after the first word in a string
    */
    
    private static String remainder(String inp)
    {
        int i;
        i = inp.indexOf(' ');
        if (i==0) return remainder(inp.trim());
        if (i<0) return new String("");
        return inp.substring(i+1,inp.length()).trim();
    };

    /**
    * Get the publicId: always null
    */

    public String getPublicId() {
        return null;
    }

    /**
    * Get the system ID
    */

    public String getSystemId() {
        return systemId;
    }

    /**
    * Get the line number
    */

    public int getLineNumber() {
        return lineNr;
    }

    /**
    * Get the column number: always -1
    */

    public int getColumnNumber() {
        return -1;
    }

};


