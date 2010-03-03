using System;

namespace HTML
{
    /// <summary>
    /// Summary description for ParseHTML.
    /// </summary>

    public class ParseHTML : Parse
    {
        public AttributeList GetTag()
        {
            AttributeList tag = new AttributeList();
            tag.Name = m_tag;

            foreach (Attribute x in List)
            {
                tag.Add((Attribute)x.Clone());
            }

            return tag;
        }

        public String BuildTag()
        {
            String buffer = "<";
            buffer += m_tag;
            int i = 0;
            while (this[i] != null)
            {// has attributes
                buffer += " ";
                if (this[i].Value == null)
                {
                    if (this[i].Delim != 0)
                        buffer += this[i].Delim;
                    buffer += this[i].Name;
                    if (this[i].Delim != 0)
                        buffer += this[i].Delim;
                }
                else
                {
                    buffer += this[i].Name;
                    if (this[i].Value != null)
                    {
                        buffer += "=";
                        if (this[i].Delim != 0)
                            buffer += this[i].Delim;
                        buffer += this[i].Value;
                        if (this[i].Delim != 0)
                            buffer += this[i].Delim;
                    }
                }
                i++;
            }
            buffer += ">";
            return buffer;
        }

        protected void ParseTag()
        {
            m_tag = "";
            Clear();

            // Is it a comment?
            if ((GetCurrentChar() == '!') &&
              (GetCurrentChar(1) == '-') &&
              (GetCurrentChar(2) == '-'))
            {
                while (!Eof())
                {
                    if ((GetCurrentChar() == '-') &&
                      (GetCurrentChar(1) == '-') &&
                      (GetCurrentChar(2) == '>'))
                        break;
                    if (GetCurrentChar() != '\r')
                        m_tag += GetCurrentChar();
                    Advance();
                }
                m_tag += "--";
                Advance();
                Advance();
                Advance();
                ParseDelim = (char)0;
                return;
            }

            // Find the tag name
            while (!Eof())
            {
                if (IsWhiteSpace(GetCurrentChar()) ||
                                 (GetCurrentChar() == '>'))
                    break;
                m_tag += GetCurrentChar();
                Advance();
            }

            EatWhiteSpace();

            // Get the attributes
            while (GetCurrentChar() != '>')
            {
                ParseName = "";
                ParseValue = "";
                ParseDelim = (char)0;

                ParseAttributeName();

                if (GetCurrentChar() == '>')
                {
                    AddAttribute();
                    break;
                }

                // Get the value(if any)
                ParseAttributeValue();
                AddAttribute();
            }
            Advance();
        }


        public char Parse()
        {
            if (GetCurrentChar() == '<')
            {
                Advance();

                char ch = char.ToUpper(GetCurrentChar());
                if ((ch >= 'A') && (ch <= 'Z') || (ch == '!') || (ch == '/'))
                {
                    ParseTag();
                    return (char)0;
                }

                else return (AdvanceCurrentChar());
            }
            else return (AdvanceCurrentChar());
        }
    }
}
