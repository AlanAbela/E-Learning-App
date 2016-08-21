using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Represents a database connection
/// </summary>
public class Connection
{

    /// <summary>
    /// Enum used to distinguish between different connection strings.
    /// </summary>
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
   /// 
   /// </summary>
   /// <param name="conType">
   /// Define which connection string is returned.
   /// </param>
   /// <returns></returns>
    public static string ConnectionString(ConType conType )
    {
        // Reference the database connection string, sourced from the web.config file.
        string conn = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;

        if(conType == ConType.Two)
        {
            conn = ConfigurationManager.ConnectionStrings["userDB"].ConnectionString;
        }

        return conn;

    }
}