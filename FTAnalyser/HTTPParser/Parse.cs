using System;

namespace HTML
{
    /// <summary>
    /// Base class for parsing tag based files, such as HTML,
    /// HTTP headers, or XML.
    ///
    /// This source code may be used freely under the
    /// Limited GNU Public License(LGPL).
    ///
    /// Written by Jeff Heaton (http://www.jeffheaton.com)
    /// </summary>
    public class Parse : AttributeList
    {
        /// <summary>
        /// The source text that is being parsed.
        /// </summary>
        private string m_source;

        /// <summary>
        /// The current position inside of the text that
        /// is being parsed.
        /// </summary>
        private int m_idx;

        /// <summary>
        /// The most recently parsed attribute delimiter.
        /// </summary>
        private char m_parseDelim;

        /// <summary>
        /// This most recently parsed attribute name.
        /// </summary>
        private string m_parseName;

        /// <summary>
        /// The most recently parsed attribute value.
        /// </summary>
        private string m_parseValue;

        /// <summary>
        /// The most recently parsed tag.
        /// </summary>
        public string m_tag;

        /// <summary>
        /// Determine if the specified character is whitespace or not.
        /// </summary>
        /// <param name="ch">A character to check</param>
        /// <returns>true if the character is whitespace</returns>
        public static bool IsWhiteSpace(char ch)
        {
            return ("\t\n\r ".IndexOf(ch) != -1);
        }


        /// <summary>
        /// Advance the index until past any whitespace.
        /// </summary>
        public void EatWhiteSpace()
        {
            while (!Eof())
            {
                if (!IsWhiteSpace(GetCurrentChar()))
                    return;
                m_idx++;
            }
        }

        /// <summary>
        /// Determine if the end of the source text has been reached.
        /// </summary>
        /// <returns>True if the end of the source text has been
        /// reached.</returns>
        public bool Eof()
        {
            return (m_idx >= m_source.Length);
        }

        /// <summary>
        /// Parse the attribute name.
        /// </summary>
        public void ParseAttributeName()
        {
            EatWhiteSpace();
            // get attribute name
            while (!Eof())
            {
                if (IsWhiteSpace(GetCurrentChar()) ||
                  (GetCurrentChar() == '=') ||
                  (GetCurrentChar() == '>'))
                    break;
                m_parseName += GetCurrentChar();
                m_idx++;
            }

            EatWhiteSpace();
        }


        /// <summary>
        /// Parse the attribute value
        /// </summary>
        public void ParseAttributeValue()
        {
            if (m_parseDelim != 0)
                return;

            if (GetCurrentChar() == '=')
            {
                m_idx++;
                EatWhiteSpace();
                if ((GetCurrentChar() == '\'') ||
                  (GetCurrentChar() == '\"'))
                {
                    m_parseDelim = GetCurrentChar();
                    m_idx++;
                    while (GetCurrentChar() != m_parseDelim)
                    {
                        m_parseValue += GetCurrentChar();
                        m_idx++;
                    }
                    m_idx++;
                }
                else
                {
                    while (!Eof() &&
                      !IsWhiteSpace(GetCurrentChar()) &&
                      (GetCurrentChar() != '>'))
                    {
                        m_parseValue += GetCurrentChar();
                        m_idx++;
                    }
                }
                EatWhiteSpace();
            }
        }

        /// <summary>
        /// Add a parsed attribute to the collection.
        /// </summary>
        public void AddAttribute()
        {
            Attribute a = new Attribute(m_parseName,
              m_parseValue, m_parseDelim);
            Add(a);
        }


        /// <summary>
        /// Get the current character that is being parsed.
        /// </summary>
        /// <returns></returns>
        public char GetCurrentChar()
        {

            return GetCurrentChar(0);

        }



        /// <summary>
        /// Get a few characters ahead of the current character.
        /// </summary>
        /// <param name="peek">How many characters to peek ahead
        /// for.</param>
        /// <returns>The character that was retrieved.</returns>
        public char GetCurrentChar(int peek)
        {
            if ((m_idx + peek) < m_source.Length)
                return m_source[m_idx + peek];
            else
                return (char)0;
        }



        /// <summary>
        /// Obtain the next character and advance the index by one.
        /// </summary>
        /// <returns>The next character</returns>
        public char AdvanceCurrentChar()
        {
            return m_source[m_idx++];
        }



        /// <summary>
        /// Move the index forward by one.
        /// </summary>
        public void Advance()
        {
            m_idx++;
        }


        /// <summary>
        /// The last attribute name that was encountered.
        /// <summary>
        public string ParseName
        {
            get
            {
                return m_parseName;
            }

            set
            {
                m_parseName = value;
            }
        }

        /// <summary>
        /// The last attribute value that was encountered.
        /// <summary>
        public string ParseValue
        {
            get
            {
                return m_parseValue;
            }

            set
            {
                m_parseValue = value;
            }
        }

        /// <summary>
        /// The last attribute delimeter that was encountered.
        /// <summary>
        public char ParseDelim
        {
            get
            {
                return m_parseDelim;
            }

            set
            {
                m_parseDelim = value;
            }
        }

        /// <summary>
        /// The text that is to be parsed.
        /// <summary>
        public string Source
        {
            get
            {
                return m_source;
            }

            set
            {
                m_source = value;
            }
        }
    }
}
