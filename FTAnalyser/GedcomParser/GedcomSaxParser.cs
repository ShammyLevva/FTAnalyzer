/**
*  GedcomSaxParser
*  
*  This class is designed to look like a SAX2-compliant XML parser; however,
*  it takes GEDCOM as its input rather than XML.
*  The events sent to the ContentHandler reflect the GEDCOM input "as is";
*  there is no validation or conversion of tags.
*
*  @author mhkay@iclway.co.uk
*  @version 21 January 2001 - revised to conform to SAX2
*/

using Org.System.Xml.Sax.Helpers;
using System.IO;
using System.Net;
using Org.System.Xml.Sax;
using System;
using System.Collections.Generic;
using System.Xml;

namespace FTAnalyser
{

    public class GedcomSaxParser : IXmlReader, ILocator {

        private IContentHandler contentHandler;
        private IErrorHandler errorHandler;
        private AttributesImpl emptyAttList = new AttributesImpl();
        private AttributesImpl attList = new AttributesImpl();

        private string systemId;
        private long lineNr;


        /**
        * Parse input from a supplied BufferedReader
        */

        private void parse(StreamReader reader) {
            string line, currentLine, token1, token2;
            string level;
            int thislevel;
            int prevlevel;
            string iden, tag, xref, valu;
            int cpos1;

            char[] newlineCharArray = new char[1];
            newlineCharArray[0] = '\n';

            lineNr = 0;
            currentLine = "";

            Stack<string> stack = new Stack<string>();
            stack.Push("GED");
            
            prevlevel = -1;
            
            contentHandler.SetDocumentLocator(this);
            contentHandler.StartDocument();
            contentHandler.StartElement("", "GED", "GED", emptyAttList);

            try {
         
              while ( (line=reader.ReadLine() ) != null ) {

                line=line.Trim();

                lineNr++;
                currentLine = line;

                // parse the GEDCOM line into five fields: level, iden, tag, xref, valu

                if (line.Length > 0) {
                    cpos1 = line.IndexOf(' ');
                    if (cpos1<0) throw new SaxException("No space in line");
                    
                    level = firstWord(line);
                    thislevel = Int32.Parse(level);

                    // check the level number

                    if (thislevel>prevlevel && !(thislevel==prevlevel+1))
                        throw new SaxException("Level numbers must increase by 1");
                    if (thislevel<0)
                        throw new SaxException("Level number must not be negative");
                    
                    line = remainder(line);
                    token1 = firstWord(line);
                    line = remainder(line);
     
                    if (token1.StartsWith("@")) {
                        if (token1.Length ==1 || !token1.EndsWith("@"))
                            throw new SaxException("Bad xref_id");
                           
                        iden = token1.Substring(1, token1.Length - 2);
                        tag = firstWord(line);
                        line = remainder(line);
                    } else {
                        iden = "";
                        tag = token1;
                    };

                    xref = "";
                    if ( line.StartsWith("@")) {
                        token2 = firstWord(line);
                        if (token2.Length==1 || !token2.EndsWith("@"))
                            throw new SaxException("Bad pointer value");
                            
                        xref = token2.Substring(1, token2.Length - 2);
                        line = remainder(line);
                    };
     
                    valu = line;
                    
                    // perform validation on the CHAR field (character code)
                    if (tag.Equals("CHAR") &&
                        !(valu.Trim().Equals("ANSEL") || valu.Trim().Equals("ASCII")))
                    {
                        Console.Error.WriteLine("WARNING: Character set is " + valu + ": should be ANSEL or ASCII");
                    }

                    // insert any necessary closing tags
                    while (thislevel <= prevlevel) {
                        string endtag = (string)stack.Pop();
                        contentHandler.EndElement("", endtag, endtag);
                        prevlevel--;
                    }

                    if (!tag.Equals("TRLR")) {
                        attList.Clear();
                        if (!iden.Equals("")) attList.AddAttribute("", "ID", "ID", "ID", iden, true);
                        if (!xref.Equals("")) attList.AddAttribute("", "REF", "REF", "IDREF", xref, true);
                        contentHandler.StartElement("", tag, tag, attList);
    	                stack.Push(tag);
    	                prevlevel = thislevel;
                    }               
                   
                    if (valu.Length>0) {
                        contentHandler.Characters(valu.ToCharArray(), 0, valu.Length);
                    }
	            }
             
              } // end while
     
              contentHandler.EndElement("", "GED", "GED");
              contentHandler.EndDocument();
              //System.err.println("Parsing complete: " + lineNr + " lines");

            } catch (SaxException e1) {
                ParseErrorImpl err = new ParseErrorImpl(e1.Message, e1);
                errorHandler.FatalError(err);
                throw new SaxParseException(err);
            } finally {
                reader.Close();
            }
         
        }
                       

      /**
        * Procedure to return the first word in a string
        */
        private static string firstWord(string inp)
        {
            int i;
            i = inp.IndexOf(' ');
            if (i==0) return firstWord(inp.Trim());
            if (i<0) return inp;
            return inp.Substring(0,i).Trim();
        }

      /**
        * Procedure to return the text after the first word in a string
        */
        
        private static string remainder(string inp)
        {
            int i;
            i = inp.IndexOf(' ');
            if (i==0) return remainder(inp.Trim());
            if (i<0) return "";
            return inp.Substring(i+1).Trim();
        }

        #region ILocator Members

        public long ColumnNumber
        {
            get { return -1; }
        }

        public string Encoding
        {
            get { throw new NotImplementedException(); }
        }

        public ParsedEntityType EntityType
        {
            get { throw new NotImplementedException(); }
        }

        public long LineNumber
        {
            get { return lineNr; }
        }

        public string PublicId
        {
            get { return null; }
        }

        public string SystemId
        {
            get { return systemId; }
        }

        public string XmlVersion
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IXmlReader Members

        public void Abort()
        {
            throw new NotImplementedException();
        }

        public IContentHandler ContentHandler
        {
            get
            {
                return contentHandler;
            }
            set
            {
                contentHandler = value;
            }
        }

        public IDeclHandler DeclHandler
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDtdHandler DtdHandler
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public IEntityResolver EntityResolver
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public IErrorHandler ErrorHandler
        {
            get
            {
                return errorHandler;
            }
            set
            {
                errorHandler = value;
            }
        }

        public bool GetFeature(string name)
        {
            if (name.Equals("http://xml.org/sax/features/namespaces")) return true;
            if (name.Equals("http://xml.org/sax/features/namespace-prefixes")) return false;
            throw new SaxException("Gedcom Parser does not recognize any features");
        }

        public IProperty<T> GetProperty<T>(string name)
        {
            throw new SaxException("Gedcom Parser does not recognize any properties");
        }

        public ILexicalHandler LexicalHandler
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Parse(string systemId)
        {
            this.systemId = systemId;
            Stream response = WebRequest.Create(systemId).GetResponse().GetResponseStream();
            parse(new AnselInputStreamReader(response));
        }

        public void Parse(InputSource input)
        {
            if (contentHandler == null) contentHandler = new DefaultHandler();
            if (errorHandler == null) errorHandler = new DefaultHandler();
            systemId = input.SystemId;

            if (systemId != null)
            {
                Parse(systemId);
            }
            else
            {
                throw new SaxException("No input supplied");
            }
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void SetFeature(string name, bool value)
        {
            if (name.Equals("http://xml.org/sax/features/namespaces") && value) return;
            if (name.Equals("http://xml.org/sax/features/namespace-prefixes") && !value) return;
            throw new SaxException("Gedcom Parser does not recognize any features");
        }

        public XmlReaderStatus Status
        {
            get { throw new NotImplementedException(); }
        }

        public void Suspend()
        {
            throw new NotImplementedException();
        }

        #endregion
    };
}


