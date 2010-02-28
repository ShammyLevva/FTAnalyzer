using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace FTAnalyser
{
    class GedcomToXml
    {

        public static XmlDocument Load(String path)
        {
            StreamReader reader = new AnselInputStreamReader(new FileStream(path, FileMode.Open));
            return parse(reader);
        }

        private static XmlDocument parse(StreamReader reader)
        {
            long lineNr = 0;

            string line, token1, token2;
            string level;
            int thislevel;
            int prevlevel;
            string iden, tag, xref, value;
            int cpos1;

            char[] newlineCharArray = new char[1];
            newlineCharArray[0] = '\n';

            lineNr = 0;

            Stack<string> stack = new Stack<string>();
            stack.Push("GED");

            prevlevel = -1;

            XmlDocument document = new XmlDocument();
            XmlNode node = document.CreateElement("GED");
            document.AppendChild(node);

            try
            {

                while ((line = reader.ReadLine()) != null)
                {

                    line = line.Trim();

                    lineNr++;

                    // parse the GEDCOM line into five fields: level, iden, tag, xref, valu

                    if (line.Length > 0)
                    {
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
                            !(value.Trim().Equals("ANSEL") || value.Trim().Equals("ASCII")))
                        {
                            Console.Error.WriteLine("WARNING: Character set is " + value + ": should be ANSEL or ASCII");
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
