using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Connection
/// </summary>
public class Connection
{
    public Connection()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Get the connection string from the config file.
    /// </summary>
    /// <returns></returns>
    public static string ConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["localDB"].ConnectionString; ;

    }
}