/*
 * MissingPrimaryKeyException.java
 *
 * Created on 20 October 2002, 21:10
 */

package org.hardgate.exceptions;

public class BadIGIDataException extends java.lang.Exception {
    
	private static final long serialVersionUID = 0;
	
	public BadIGIDataException() {
    }
    
    public BadIGIDataException(String msg) {
        super(msg);
    }
}
