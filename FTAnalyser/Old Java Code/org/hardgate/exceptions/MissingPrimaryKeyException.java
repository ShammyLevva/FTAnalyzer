/*
 * MissingPrimaryKeyException.java
 *
 * Created on 20 October 2002, 21:10
 */

package org.hardgate.exceptions;

public class MissingPrimaryKeyException extends java.lang.Exception {
    
	private static final long serialVersionUID = 0;
	
	public MissingPrimaryKeyException() {
    }
    
    public MissingPrimaryKeyException(String msg) {
        super(msg);
    }
}
