using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FTAnalyzer
{
    public class IGIDocument {
        
//        private Document doc;
        
        public IGIDocument (HttpWebRequest url, String proxy, String port)
	    {
		    if (proxy != null) {
//			    Properties systemProperties = System.getProperties();
//			    systemProperties.setProperty("http.proxyHost",proxy);
//			    systemProperties.setProperty("http.proxyPort",port);
		    }
//            SAXReader reader = new SAXReader(false);
//            doc = reader.read(url);
	    }

        public IGIDocument(HttpWebRequest url) 
            : this(url, null, null)
        { }

        
        public IGIDocument(String strUrl) 
            : this(createURL(strUrl), null, null)
        { }

        public IGIDocument(String strUrl, String proxy, String port)
    		: this(createURL(strUrl), proxy, port)
        { }

        private static HttpWebRequest createURL(String strUrl) {
            return (HttpWebRequest) WebRequest.Create(strUrl);
        }
    }
}