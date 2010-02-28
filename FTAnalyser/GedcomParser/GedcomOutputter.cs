/**
*  A SAX ContentHandler that writes the events to standard output
*  in GEDCOM format.
*  <p>This class expects to receive the data in normalised form, that is,
*  each contiguous piece of character data arrives in a single call of
*  the characters() interface.</p>
*
*  @author mhkay@iclway.co.uk
*  @version 21 January 2001: modified to conform to SAX2
*/
  
namespace FTAnalyser
{
/*
    import java.io.Writer;

    import org.xml.sax.Attributes;
    import org.xml.sax.SAXException;
    import org.xml.sax.helpers.DefaultHandler;
*/
    
    public class GedcomOutputter : DefaultHandler
    {
        int level = 0;
        boolean acceptCharacters = true;
        GedcomLine line = new GedcomLine();
        Writer writer;
     
       /**
        * Start of the document. 
        */
        public void startDocument () throws SAXException
        {
            // Create/open the output file
            try {
                writer = new AnselOutputStreamWriter(System.out);
            } catch (Exception err) {
                throw new SAXException("Failed to create output stream", err);
            }
        }

        /**
        * End of the document.
        */

        public void endDocument () throws SAXException
        {
            try {
                if (line.level>=0) flushLine();
                writer.write("0 TRLR\n");
                writer.close();
            } catch (java.io.IOException err) {
                throw new SAXException(err);
            }
        }

        /**
        * Start of an element.
        */
        
        public void startElement (String pref, String ns, String name, Attributes attributes) throws SAXException
        {
            if (name.equals("GED")) return;
            
            if (line.level>=0) flushLine();
            
            line.level = level;
            line.id = attributes.getValue("", "ID");       
            line.tag = name;
            line.ref = attributes.getValue("", "REF");
            line.text.setLength(0);

            acceptCharacters = true;
            level++;
            
        }

        /**
        * End of an element.
        */

        public void endElement (String prefix, String ns, String name) throws SAXException 
        {
            level--;
            if (line.level>=0) flushLine();
            acceptCharacters = false;
        }

        /**
        * Character data.
        */
        
        public void characters (char ch[], int start, int length) throws SAXException
        {
            if (!acceptCharacters) {
                for (int i=start; i<start+length; i++) {
                    if (!Character.isWhitespace(ch[i])) {
                        throw new SAXException("Character data not allowed after end tag");
                    }
                }
            } else {
                line.text.append(ch, start, length);
            }
            
        }

        /**
        * Ignore ignorable whitespace.
        */

        public void ignorableWhitespace (char ch[], int start, int length)
        {}


        /**
        * Handle a processing instruction.
        */
        
        public void processingInstruction (String target, String data)
        {}

        /**
        * Flush the accumulated output line
        */
        
        private void flushLine() throws SAXException {
            String text = line.text.toString().trim();
            int base = line.level;
            int prevnl = 0;
            // if the line contains a newline char, add a CONT element
            while (true) {
                int nl = text.indexOf('\n');
                String textline = text;
                if (nl>=0) {
                    line.text.setLength(nl);
                    textline = text.substring(0,nl);
                } else {
                    textline = text;
                }
                
                // if the line is longer than the GEDCOM limit, add CONC elements
                while (true) {
                    int split = -1;
                    int len = line.tag.length() + 4 +
                                (line.id==null ? 0 : line.id.length()+3) +
                                (line.ref==null ? 0 : line.ref.length()+3) +
                                textline.length();
                    if (len > 255) {                    // exceeds the limit
                        // split the line if possible between two non-space characters
                        split = 80;
                        while (split>0 &&
                                 (Character.isWhitespace(text.charAt(split)) ||
                                  Character.isWhitespace(text.charAt(split+1)))) {
                            split--;
                        }
                        if (split==0) split = 80;       // failed: split at column 80
                        line.text.setLength(split);
                        line.write();
                        
                        line.level = base+1;
                        line.id = null;
                        line.tag = "CONC";
                        line.ref = null;
                        line.text.setLength(0);
                        textline = textline.substring(split);
                        line.text.append(textline);
                    }
                                
                    line.write();
                    if (split<0) break;
                }
                
                // prepare next line of output
                if (nl > 0 && nl+1 < text.length()) {                   
                    line.level = base+1;
                    line.id = null;
                    line.tag = "CONT";
                    line.ref = null;
                    line.text.setLength(0);
                    text = text.substring(nl+1);
                    line.text.append(text);
                } else {
                    break;
                }
            }
            line.text.setLength(0);
            line.level = -1;
        }

        /**
        * Inner class representing a line of GEDCOM output
        */

        private class GedcomLine {
            public int level = -1;
            public String id;
            public String tag;
            public String ref;
            public StringBuffer text = new StringBuffer();

            /**
            * Write the GEDCOM line using the current writer
            */

            public void write() throws SAXException {
                try {
                    writer.write(level + " ");
                    if (id!=null) {
                        writer.write('@' + id + "@ ");
                    }
                    writer.write(tag + ' ');
                    if (ref!=null) {
                        writer.write('@' + ref + "@ ");
                    }
                    writer.write(text.toString() + '\n');
                } catch (java.io.IOException err) {
                    throw new SAXException(err);
                }
            }
        }

    }
}