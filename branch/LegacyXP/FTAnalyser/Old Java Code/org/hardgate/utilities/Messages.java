/*
 * Created on 28-Oct-2004
 *
 */
package org.hardgate.utilities;

import java.util.MissingResourceException;
import java.util.ResourceBundle;

/**
 * @author A-Bisset
 *
 */
public class Messages {
    private static final String BUNDLE_NAME = "org.hardgate.utilities.messages";//$NON-NLS-1$

    private static final ResourceBundle RESOURCE_BUNDLE = 
    	ResourceBundle.getBundle(BUNDLE_NAME);

    private Messages() {
    }

    public static String getString(String key) {
        try {
            return RESOURCE_BUNDLE.getString(key);
        } catch (MissingResourceException e) {
            return '!' + key + '!';
        }
    }
}