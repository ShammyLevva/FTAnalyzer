/*
 * Created on 29-Dec-2004
 *
 */
package org.hardgate.familytree;

import java.io.IOException;
import java.io.PrintWriter;

import org.hardgate.familytree.core.Client;

/**
 * @author A-Bisset
 *
 */
public class TestRelation {

    public static void main (String[] args) throws IOException {
        
        Client client = Client.getInstance();
        int memberID = 14636;

        PrintWriter out = new PrintWriter(System.out);
        
        client.setRelations(memberID, "I0001");
        client.printRelationCount(out,memberID);
    }
}