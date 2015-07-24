package org.hardgate.exceptions;

/**
 *  
 */
public class DelegateException extends RuntimeException {
	private static final long serialVersionUID = 0;
	public DelegateException(String message) {
		this(message, null);
	}

	public DelegateException(String message, Throwable cause) {
		super(message, cause);
	}
}