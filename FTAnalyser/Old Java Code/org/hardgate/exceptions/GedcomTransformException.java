package org.hardgate.exceptions;

import java.sql.SQLException;

public class GedcomTransformException extends SQLException {

	private static final long serialVersionUID = 0;
	private Exception exception;

	// constructor with String argument
	public GedcomTransformException(String message) {
		super(message);
		exception = new SQLException(message);
	}

	// constructor with Exception argument
	public GedcomTransformException(Exception exception) {
		exception = this.exception;
	}

	// printStackTrace of exception from constructor
	public void printStackTrace() {
		exception.printStackTrace();
	}
}