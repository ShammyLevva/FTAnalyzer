using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using FTAnalyzer.Utilities;

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
            StreamReader reader;
            if (Properties.FileHandling.Default.LoadWithFilters)
                reader = new StreamReader(CheckSpuriousOD(path), encoding);
            else
                reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read), encoding);
            return Parse(reader);
        }

        private static MemoryStream CheckInvalidCR(string path)
        {
            FileStream infs = new FileStream(path, FileMode.Open, FileAccess.Read);
            MemoryStream outfs = new MemoryStream();
            byte b = (byte)infs.ReadByte();
            while (infs.Position < infs.Length)
            {
                if (b == 0x0d)
                {
                    b = (byte)infs.ReadByte();
                    if (b == 0x0a)
                    { // we have 0x0d 0x0a so write the 0x0d so that normal write works.
                        outfs.WriteByte(0x0d);
                    }
                }
                outfs.WriteByte(b);
                b = (byte)infs.ReadByte();
            }
            outfs.Position = 0;
            return outfs;
        }

        private static MemoryStream CheckSpuriousOD(string path)
        {
            FileStream infs = new FileStream(path, FileMode.Open, FileAccess.Read);
            MemoryStream outfs = new MemoryStream();
            byte b = (byte)infs.ReadByte();
            while (infs.Position < infs.Length)
            {
                while (b == 0x0d && infs.Position < infs.Length)
                {
                    b = (byte)infs.ReadByte();
                    if (b == 0x0a)
                    { // we have 0x0d 0x0a so write out the 0x0d and the 0x0a will follow in the normal write.
                        outfs.WriteByte(0x0d);
                    } // otherwise we drop though and have ignored the 0x0d on its own
                }
                outfs.WriteByte(b);
                b = (byte)infs.ReadByte();
            }
            outfs.Position = 0;
            return outfs;
        }

        private static XmlDocument Parse(StreamReader reader)
        {
            long lineNr = 0;
            int badLineCount = 0;
            int badLineMax = 30;

            string line, nextline, token1, token2;
            string level;
            int thislevel;
            int prevlevel = -1;
            string iden, tag, xref, value;
            int cpos1;
            Stack<string> stack = new Stack<string>();
            stack.Push("GED");
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
                            line = line.Replace('–', '-').Replace('—', '-').Replace("***Data is already there***", ""); // "data is already there" is some Ancestry anomaly
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
                            }

                            xref = "";
                            if (line.StartsWith("@"))
                            {
                                if (!token1.Equals("CONT") && !token1.Equals("CONC"))
                                {
                                    token2 = firstWord(line);
                                    if (token2.Length == 1 || (!token2.EndsWith("@") && !token2.EndsWith("@,")))
                                        throw new Exception("Bad pointer value");
                                    if (token2.EndsWith("@,"))
                                        xref = token2.Substring(1, token2.Length - 3);
                                    else
                                        xref = token2.Substring(1, token2.Length - 2);
                                    line = remainder(line);
                                }
                            }
                            if (token1.Equals("CONT") || token1.Equals("CONC"))
                            {
                                // check if nextline does not start with a number ie: could be a wrapped line, if so then concatenate
                                while (nextline != null && !nextline.StartsWithNumeric())
                                {
                                    line = line + "\n" + nextline.Trim();
                                    nextline = reader.ReadLine();
                                }
                            }

                            value = line;

                            // perform validation on the CHAR field (character code)
                            string valtrim = value.Trim();
                            if (tag.Equals("CHAR") &&
                                !(valtrim.Equals("ANSEL") || valtrim.Equals("ASCII") || valtrim.Equals("ANSI") || valtrim.Equals("UTF-8") || valtrim.Equals("UNICODE")))
                            {
                                FamilyTree.Instance.XmlErrorBox.AppendText("WARNING: Character set is " + value + ": should be ANSEL, ANSI, ASCII, UTF-8 or UNICODE\n");
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
                    if (badLineCount > badLineMax)
                    {
                        string message = "Found more than " + badLineMax + " consecutive errors in the GEDCOM file.";
                        if (!Properties.FileHandling.Default.LoadWithFilters)
                            message += "\n\nNB. You might get less errors if you turn on the option to 'Use Special Character Filters When Loading' from the Tools Options menu.";
                        message += "\n\nContinue Loading?";
                        DialogResult result = MessageBox.Show(message, "Continue Loading?", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            badLineCount = 0;
                            badLineMax *= 2; // double count of errors before next act
                        }
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
