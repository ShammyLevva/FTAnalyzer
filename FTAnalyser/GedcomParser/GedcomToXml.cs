using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace FTAnalyzer
{
    class GedcomToXml
    {
        private static readonly Encoding isoWesternEuropean = Encoding.GetEncoding(28591);

        public static XmlDocument Load(string path) { return Load(path, isoWesternEuropean); }
        public static XmlDocument Load(string path, Encoding encoding)
        {
            //StreamReader reader = new AnselInputStreamReader(checkInvalidCR(path));
            //StreamReader reader = new AnselInputStreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
            StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read), encoding);
            return Parse(reader);
        }

        private static MemoryStream CheckInvalidCR(string path)
        {
            FileStream infs = new FileStream(path, FileMode.Open, FileAccess.Read);
            MemoryStream outfs = new MemoryStream();
            byte b = (byte) infs.ReadByte();
            while (infs.Position < infs.Length)
            {
                if (b == 0x0d)
                {
                    b = (byte) infs.ReadByte();
                    if (b == 0x0a)
                    { // we have 0x0d 0x0a so write the 0x0d so that normal write works.
                        outfs.WriteByte(0x0d);
                    }
                }
                outfs.WriteByte(b);
                b = (byte) infs.ReadByte();
            }
            outfs.Position = 0;
            return outfs;
        }

        private static XmlDocument Parse(StreamReader reader)
        {
            long lineNr = 0;
            int badLineCount = 0;

            string line, nextline, token1, token2;
            string level;
            int thislevel;
            int prevlevel;
            string iden, tag, xref, value;
            int cpos1;

            char[] newlineCharArray = new char[1];
            newlineCharArray[0] = '\r';

            lineNr = 0;

            Stack<string> stack = new Stack<string>();
            stack.Push("GED");

            prevlevel = -1;

            XmlDocument document = new XmlDocument();
            XmlNode node = document.CreateElement("GED");
            document.AppendChild(node);

            try
            {
                line = reader.ReadLine();
                while (line != null)
                {
                    lineNr++;
                    nextline = reader.ReadLine();
                    //need to check if nextline is valid if not line=line+nextline and nextline=reader.ReadLine();

                    // parse the GEDCOM line into five fields: level, iden, tag, xref, valu
                    line = line.Trim();
                    if (line.Length > 0)
                    {
                        try
                        {
                            line = line.Replace('–', '-').Replace('—', '-');
                            cpos1 = line.IndexOf(' ');
                            if (cpos1 < 0) throw new Exception("No space in line");

                            level = firstWord(line);
                            thislevel = Int32.Parse(level);

                            // check the level number

                            if (thislevel > prevlevel && !(thislevel == prevlevel + 1))
                                throw new Exception("Level numbers must increase by 1");
                            if (thislevel < 0)
                                throw new Exception("Level number must not be negative");

                            line = remainder(line);
                            token1 = firstWord(line);
                            line = remainder(line);

                            if (token1.StartsWith("@"))
                            {
                                if (token1.Length == 1 || !token1.EndsWith("@"))
                                    throw new Exception("Bad xref_id");

                                iden = token1.Substring(1, token1.Length - 2);
                                tag = firstWord(line);
                                line = remainder(line);
                            }
                            else
                            {
                                iden = "";
                                tag = token1;
                            };

                            xref = "";
                            if (line.StartsWith("@"))
                            {
                                token2 = firstWord(line);
                                if (token2.Length == 1 || !token2.EndsWith("@"))
                                    throw new Exception("Bad pointer value");

                                xref = token2.Substring(1, token2.Length - 2);
                                line = remainder(line);
                            };

                            value = line;

                            // perform validation on the CHAR field (character code)
                            if (tag.Equals("CHAR") &&
                                !(value.Trim().Equals("ANSEL") || value.Trim().Equals("ASCII") || value.Trim().Equals("ANSI") || value.Trim().Equals("UTF-8")))
                            {
                                FamilyTree.Instance.XmlErrorBox.AppendText("WARNING: Character set is " + value + ": should be ANSEL, ANSI or ASCII\n");
                            }

                            // insert any necessary closing tags
                            while (thislevel <= prevlevel)
                            {
                                stack.Pop();
                                node = node.ParentNode;
                                prevlevel--;
                            }

                            if (!tag.Equals("TRLR"))
                            {
                                XmlNode newNode = document.CreateElement(tag);
                                node.AppendChild(newNode);
                                node = newNode;

                                if (!iden.Equals(""))
                                {
                                    XmlAttribute attr = document.CreateAttribute("ID");
                                    attr.Value = iden;
                                    node.Attributes.Append(attr);
                                }
                                if (!xref.Equals(""))
                                {
                                    XmlAttribute attr = document.CreateAttribute("REF");
                                    attr.Value = xref;
                                    node.Attributes.Append(attr);
                                }
                                stack.Push(tag);
                                prevlevel = thislevel;
                            }

                            if (value.Length > 0)
                            {
                                XmlText text = document.CreateTextNode(value);
                                node.AppendChild(text);
                            }
                        }
                        catch (Exception e)
                        {
                            FamilyTree.Instance.XmlErrorBox.AppendText("Found bad line " + lineNr + ": '" + line + "'. " +
                                "Error was : " + e.Message + "\n");
                            badLineCount++;
                        }
                    }
                    line = nextline;
                    System.Windows.Forms.Application.DoEvents();
                    if (badLineCount > 20)
                    {
                        DialogResult result = MessageBox.Show("Found more than 20 errors in the GEDCOM file.\nContinue Loading?",
                                                         "Continue Loading?", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                            badLineCount = 0;
                        else
                        {
                            document = null;
                            break;
                        }
                    }
                        
                } // end while

            }
            finally
            {
                reader.Close();
            }
            return document;
        }

        /**
         * Procedure to return the first word in a string
         */
        private static string firstWord(string inp)
        {
            int i;
            i = inp.IndexOf(' ');
            if (i == 0) return firstWord(inp.Trim());
            if (i < 0) return inp;
            return inp.Substring(0, i).Trim();
        }

        /**
          * Procedure to return the text after the first word in a string
          */

        private static string remainder(string inp)
        {
            int i;
            i = inp.IndexOf(' ');
            if (i == 0) return remainder(inp.Trim());
            if (i < 0) return "";
            return inp.Substring(i + 1).Trim();
        }
    }
}
