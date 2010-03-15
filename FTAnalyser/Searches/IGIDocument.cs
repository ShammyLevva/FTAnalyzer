using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

namespace FTAnalyzer
{
    public class IGIDocument {
        
        private XmlDocument doc;
        
        public IGIDocument (HttpWebRequest url, string proxy, string port)
	    {
		    if (proxy != null) {
                int portNum = Int32.Parse(port);
			    Properties.NetworkSettings.Default.ProxyServer = proxy;
			    Properties.NetworkSettings.Default.ProxyPort = portNum;
		    }
//            SAXReader reader = new SAXReader(false);
//            doc = reader.read(url);
	    }

        public IGIDocument(HttpWebRequest url) 
            : this(url, null, null)
        { }

        
        public IGIDocument(string strUrl) 
            : this(createURL(strUrl), null, null)
        { }

        public IGIDocument(string strUrl, string proxy, string port)
    		: this(createURL(strUrl), proxy, port)
        { }

        private static HttpWebRequest createURL(string strUrl) {
            return (HttpWebRequest) WebRequest.Create(strUrl);
        }
    }
}