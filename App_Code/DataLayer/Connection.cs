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

   public enum ConType
    {
        One,
        Two
    };

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
    public static string ConnectionString(ConType conType )
    {

        string conn = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;

        if(conType == ConType.Two)
        {
            conn = ConfigurationManager.ConnectionStrings["userDB"].ConnectionString;
        }

        return conn;

    }
}