package org.hardgate.utilities;

import java.util.Properties;

import javax.ejb.EJBHome;
import javax.naming.InitialContext;
import javax.naming.NamingException;
import javax.rmi.PortableRemoteObject;

import org.hardgate.exceptions.DelegateException;


/**
 * A locator of EJBHome interfaces. Caches the initial context to save
 * lookup overhead.
 */
public class ServiceLocator
{
    /** The singleton instance */
    private static ServiceLocator uniqueInstance = new ServiceLocator();
    /** The cached initial context */
    private InitialContext initialContext;

    /**
     * An exception indicating that the requested service was not found.
     */
    public class LocatorException extends DelegateException {
    	private static final long serialVersionUID = 0;
    	public LocatorException(String message, Throwable cause) {
            super(message, cause);
        }
    }

    // Make the constructor private since this is a Singleton.
    private ServiceLocator() throws LocatorException {

/*				Properties props = PropertyLoader.loadProperties("org.hardgate.familytree.jndi");
		IndividualManagement indivMgmt = IndividualManagementUtil
				.getHome(props).create();
*/
        Properties properties = 
        	PropertyLoader.loadProperties("org.hardgate.utilities.jndi.properties");
        try {
	        // Note that the initialContext is now cached, saving
	        // the lookup overhead on subsequent calls.
//        	initialContext = new InitialContext(properties);
            initialContext = new InitialContext();
        } catch (NamingException e) {
        	throw new LocatorException("Error creating inital Context",e);
        }
    }

    /**
     * Returns the ServiceLocator singleton instance.
     * @return the singleton instance
     */
    public static ServiceLocator getInstance() {
        return uniqueInstance;
    }

    /**
     * Returns the EJBHome for the provided service name. Throws a
     * <code>ServiceLocator.LocatorException</code> if the lookup fails.
     *
     * @param serviceName the name of the Session Bean to look up
     * @param type the class of the Session Bean's home interface
     * @return the home interface for the given service name
     */
    public EJBHome lookup(String serviceName, Class type) throws LocatorException {
        try {
            Object objref = initialContext.lookup(serviceName);
            return (EJBHome) PortableRemoteObject.narrow(objref, type);
        }
        catch (NamingException e) {
            throw new LocatorException(
                "Lookup failed for service named " + serviceName, e);
        }
    }
}
