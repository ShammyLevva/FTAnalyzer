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
  
using System;
using System.IO;
using Org.System.Xml.Sax;
using System.Text;
using Org.System.Xml.Sax.Helpers;
namespace FTAnalyser
{
    
    public class GedcomOutputter : DefaultHandler
    {
        int level = 0;
        bool acceptCharacters = true;
        GedcomLine line = new GedcomLine();
        StreamWriter writer;
     
       /**
        * Start of the document. 
        */
        public void startDocument ()
        {
            // Create/open the output file
            try {
                writer = new AnselOutputStreamWriter(Console.OpenStandardOutput());
            } catch (Exception err) {
                throw new SaxException("Failed to create output stream", err);
            }
        }

        /**
        * End of the document.
        */

        public void endDocument ()
        {
            try {
                if (line.level>=0) flushLine();
                writer.Write("0 TRLR\n");
                writer.Close();
            } catch (IOException err) {
                throw new SaxException(err.Message);
            }
        }

        /**
        * Start of an element.
        */
        
        public void startElement (string pref, string ns, string name, IAttributes attributes)
        {
            if (name.Equals("GED")) return;
            
            if (line.level>=0) flushLine();
            
            line.level = level;
            line.id = attributes.GetValue("", "ID");       
            line.tag = name;
            line.reference = attributes.GetValue("", "REF");
            line.text.Length = 0;

            acceptCharacters = true;
            level++;
            
        }

        /**
        * End of an element.
        */

        public void endElement (string prefix, string ns, string name)
        {
            level--;
            if (line.level>=0) flushLine();
            acceptCharacters = false;
        }

        /**
        * Character data.
        */
        
        public void characters (char[] ch, int start, int length)
        {
            if (!acceptCharacters) {
                for (int i=start; i<start+length; i++) {
                    if (!Char.IsWhiteSpace(ch[i])) {
                        throw new SaxException("Character data not allowed after end tag");
                    }
                }
            } else {
                line.text.Append(ch, start, length);
            }
            
        }

        /**
        * Ignore ignorable whitespace.
        */

        public void ignorableWhitespace (char[] ch, int start, int length)
        {}


        /**
        * Handle a processing instruction.
        */
        
        public void processingInstruction (string target, string data)
        {}

        /**
        * Flush the accumulated output line
        */
        
        private void flushLine() 
        {
            string text = line.text.ToString().Trim();
            int baseLevel = line.level;
            // if the line contains a newline char, add a CONT element
            while (true) {
                int nl = text.IndexOf('\n');
                string textline = text;
                if (nl>=0) {
                    line.text.Length = nl;
                    textline = text.Substring(0,nl);
                } else {
                    textline = text;
                }
                
                // if the line is longer than the GEDCOM limit, add CONC elements
                while (true) {
                    int split = -1;
                    int len = line.tag.Length + 4 +
                                (line.id==null ? 0 : line.id.Length+3) +
                                (line.reference==null ? 0 : line.reference.Length+3) +
                                textline.Length;
                    if (len > 255) {                    // exceeds the limit
                        // split the line if possible between two non-space characters
                        split = 80;
                        while (split>0 &&
                                 (Char.IsWhiteSpace(text[split]) ||
                                  Char.IsWhiteSpace(text[split+1]))) {
                            split--;
                        }
                        if (split==0) split = 80;       // failed: split at column 80
                        line.text.Length = split;
                        line.write(writer);
                        
                        line.level = baseLevel+1;
                        line.id = null;
                        line.tag = "CONC";
                        line.reference = null;
                        line.text.Length = 0;
                        textline = textline.Substring(split);
                        line.text.Append(textline);
                    }
                                
                    line.write(writer);
                    if (split<0) break;
                }
                
                // prepare next line of output
                if (nl > 0 && nl+1 < text.Length) {                   
                    line.level = baseLevel+1;
                    line.id = null;
                    line.tag = "CONT";
                    line.reference = null;
                    line.text.Length = 0;
                    text = text.Substring(nl+1);
                    line.text.Append(text);
                } else {
                    break;
                }
            }
            line.text.Length = 0;
            line.level = -1;
        }

        /**
        * Inner class representing a line of GEDCOM output
        */

        private class GedcomLine {

            public int level = -1;
            public string id;
            public string tag;
            public string reference;
            public StringBuilder text = new StringBuilder();

            /**
            * Write the GEDCOM line using the current writer
            */

            public void write(StreamWriter writer) {
                try {
                    writer.Write(level + " ");
                    if (id!=null) {
                        writer.Write('@' + id + "@ ");
                    }
                    writer.Write(tag + ' ');
                    if (reference!=null) {
                        writer.Write('@' + reference + "@ ");
                    }
                    writer.Write(text.ToString() + '\n');
                } catch (IOException err) {
                    throw new SaxException(err.Message);
                }
            }
        }

    }
}