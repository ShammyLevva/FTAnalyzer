/*
 * Created on 04-Jan-2005
 *
 */
package org.hardgate.familytree.formatters;

import java.io.PrintWriter;

import org.hardgate.familytree.core.FactDate;
import org.hardgate.familytree.core.Individual;
import org.hardgate.familytree.core.ParentalGroup;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author db
 *
 */
public abstract class BaseOutputFormatter {

    public abstract void printHeader (PrintWriter output);
    public abstract void printItem (Registration reg, PrintWriter output);

    protected void printFamilyGroup (FactDate fd, ParentalGroup f, PrintWriter output) {
        if (f != null) {
            Individual ind = f.getIndividual();
	        output.print("\""); output.print(ind.getSurname()); output.print("\",");
	        output.print("\""); output.print(ind.getForenames()); output.print("\",");
	        output.print("\""); output.print(ind.getAge(fd)); output.print("\",");
	        output.print("\""); output.print(ind.getOccupation()); output.print("\",");
	        output.print("\""); output.print(f.getResidence()); output.print("\",");
	        output.print("\""); output.print(ind.getDateOfBirth()); output.print("\",");
	        output.print("\""); output.print(ind.getBirthLocation()); output.print("\",");
	        output.print("\""); output.print(ind.getBestLocation()); output.print("\",");
	        output.print("\""); output.print(f.getFathersName()); output.print("\",");
	        output.print("\""); output.print(f.getFathersOccupation()); output.print("\",");
	        output.print("\""); output.print(f.getFatherDeceased(fd)); output.print("\",");
	        output.print("\""); output.print(f.getMothersName()); output.print("\",");
	        output.print("\""); output.print(f.getMothersOccupation()); output.print("\",");
	        output.print("\""); output.print(f.getMotherDeceased(fd)); output.print("\",");
        } else {
            output.print(",,,,,,,,,,,,,,");
        }
    }
}
