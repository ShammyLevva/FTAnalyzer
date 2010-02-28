/*
 * Created on 29-Dec-2004
 *
 */
package org.hardgate.familytree;

import java.net.MalformedURLException;
import java.net.URL;
import java.util.Properties;

import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.io.SAXReader;

/**
 * @author A-Bisset
 *
 */
public class IGIDocument {
    
    private Document doc;
    
    public IGIDocument (URL url, String proxy, String port)
							throws DocumentException {
		if (proxy != null) {
			Properties systemProperties = System.getProperties();
			systemProperties.setProperty("http.proxyHost",proxy);
			systemProperties.setProperty("http.proxyPort",port);
		}
        SAXReader reader = new SAXReader(false);
        doc = reader.read(url);
	}

    public IGIDocument(URL url) throws DocumentException {
        this(url, null, null);
    }
    
    public IGIDocument(String strUrl) throws DocumentException {
        this(createURL(strUrl), null, null);
    }

    public IGIDocument(String strUrl, String proxy, String port)
    						throws DocumentException {
        this(createURL(strUrl), proxy, port);
    }

    private static URL createURL(String strUrl) throws DocumentException {
        try {
            return new URL(strUrl);
        } catch (MalformedURLException e) {
        	throw new DocumentException(e);
    	}
    }
}
