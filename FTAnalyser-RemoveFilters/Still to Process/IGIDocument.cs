using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

namespace FTAnalyzer
{
    public class FamilySearchDocument {
        
        // private XmlDocument doc;
        
        public FamilySearchDocument (HttpWebRequest url, string proxy, string port)
	    {
		    if (proxy != null) {
                int portNum = Int32.Parse(port);
			    Properties.NetworkSettings.Default.ProxyServer = proxy;
			    Properties.NetworkSettings.Default.ProxyPort = portNum;
		    }
//            SAXReader reader = new SAXReader(false);
//            doc = reader.read(url);
	    }

        public FamilySearchDocument(HttpWebRequest url) 
            : this(url, null, null)
        { }

        
        public FamilySearchDocument(string strUrl) 
            : this(createURL(strUrl), null, null)
        { }

        public FamilySearchDocument(string strUrl, string proxy, string port)
    		: this(createURL(strUrl), proxy, port)
        { }

        private static HttpWebRequest createURL(string strUrl) {
            return (HttpWebRequest) WebRequest.Create(strUrl);
        }
    }
}