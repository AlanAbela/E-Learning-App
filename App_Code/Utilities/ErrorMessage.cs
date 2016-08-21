using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for ErrorMessage
/// </summary>
public class ErrorMessage
{
    #region Global variables
    // Reference XML file from web.config file.
    private static string XMLPath = ConfigurationManager.AppSettings["XMLPath"].ToString();

    // Reference Log file from web.config file.
    private static string LogPath = ConfigurationManager.AppSettings["LogPath"].ToString();

    // Declare an instance of XmlDocument.
    private static XmlDocument xml = new XmlDocument();
    #endregion
    public ErrorMessage()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Retrives the error description by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public static string GetErrorDesc(int ID)
    {
        // Load XML file from specified path.
        xml.Load(XMLPath);

        // A variable to hold retrieved value.
        string value = string.Empty;

        // Return an XmlNodelist and reference it.
        XmlNodeList list = xml.DocumentElement.GetElementsByTagName("Error");

        // Loop trough the list and if list item's ID matches ID reference the value.
        foreach (XmlNode node in list)
        {
            if (node is XmlElement)
            {
                string id = (node as XmlElement).GetAttribute("id");

                if (Convert.ToInt32(id) == ID)
                {
                    value = (node as XmlElement).ChildNodes[0].Value;
                }
            }

        }
        return value;
    }

    /// <summary>
    /// Log errors in the log file.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="message"></param>
    /// <param name="stackTrace"></param>
    /// <param name="query"></param>
    public static void LogExceptions(string page, string message, string stackTrace, string query)
    {

        using (StreamWriter writer = new StreamWriter(LogPath, true))
        {
            writer.WriteLine("------------*------------");
            writer.WriteLine(DateTime.Now.ToString());
            writer.WriteLine("Page: " + page);
            writer.WriteLine("Message: " + message);
            writer.WriteLine("StackTrace: " + stackTrace);
            writer.WriteLine("Query: " + query);
            writer.Close();

        }
    }
}