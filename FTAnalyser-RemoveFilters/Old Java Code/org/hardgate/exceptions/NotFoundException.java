package org.hardgate.exceptions;

/**
 * An exception thrown by find() and save() to indicate that the item was
 * not found.
 */
public class NotFoundException extends RuntimeException {
	private static final long serialVersionUID = 0;
	public NotFoundException(String message) {
		this(message, null);
	}

	public NotFoundException(String message, Throwable cause) {
		super(message, cause);
	}
}