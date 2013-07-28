package org.hardgate.exceptions;

/**
 * An exception thrown by insert() to indicate that the item already exists.
 */
public class DuplicateException extends RuntimeException {
	private static final long serialVersionUID = 0;
	public DuplicateException(String message) {
		this(message, null);
	}

	public DuplicateException(String message, Throwable cause) {
		super(message, cause);
	}
}