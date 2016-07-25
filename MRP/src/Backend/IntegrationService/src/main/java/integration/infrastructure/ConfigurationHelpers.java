package integration.infrastructure;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.Properties;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;

/**
 * The ConfigurationHelpers class is responsible for abstracting the logic of getting property types based on property names.
 */
public class ConfigurationHelpers {
    private final static Logger log = LoggerFactory.getLogger(ConfigurationHelpers.class);

    public static String getString(String name) {
        try {
            return s_props.getProperty(name);
        } catch (Exception ex)
        {
            //log.error("An error occurred resolving string " + name + ". Exception : " + ex.toString());
        }
        return "";
    }

    /**
     * return the configuration value in the type of a integer
     * @param name - the name/key of the configuration value.
     * @return the configuration value associated with the name/key provided.
     */
    public static int getInt(String name) {
        try {
            String prop = s_props.getProperty(name);
            return Integer.parseInt(prop);
        } catch (Exception ex) {
            //log.error("An error occurred resolving int " + name + ". Exception : " + ex.toString());
        }
        return 0;
    }

    /**
     * Load the configuration properties for the propFileName
     * @param propFileName - name of the properties file
     * @return a Properties object containing all key value pairs from the properties file.
     * @throws java.io.IOException
     */
    private static Properties getPropValues(String propFileName) throws IOException {

        Properties props = new Properties();
        
        log.info("Checking For System Property File Argument: " + propFileName);        

        if (propFileName != null && new File(propFileName).isFile()) {
            final FileInputStream in = new FileInputStream(propFileName);
            log.info("Loading System Property File: " + propFileName);

            try {
                props.load(in);
            }
            finally {
                in.close();
            }
        }
        else {
            ClassLoader classLoader = ConfigurationHelpers.class.getClassLoader();
            InputStream inputStream = classLoader.getResourceAsStream(propFileName);

            props.load(inputStream);
            if (inputStream == null) {
                throw new FileNotFoundException("property file '" + propFileName + "' not found in the classpath");
            }
        }

        return props;
    }

    private static Properties getProperties() {
        return s_props;
    }

    static {
        try {
            s_props = getPropValues("application.properties");
        } catch (Exception e) {
            s_props = new Properties();
        }
    }

    private static Properties s_props;
}
