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
    private static string XMLPath = ConfigurationManager.AppSettings["XMLPath"].ToString();
    private static string LogPath = ConfigurationManager.AppSettings["LogPath"].ToString();
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
        
        xml.Load(XMLPath);
        string value = string.Empty;

        XmlNodeList list = xml.DocumentElement.GetElementsByTagName("Error");

        foreach (XmlNode node in list)
        {
            if (node is XmlElement)
            {
                string id = (node as XmlElement).GetAttribute("id");

                if (Convert.ToInt32(id) == ID)
                {
                    //value = (node as XmlElement).GetAttribute("desc");
                    value = (node as XmlElement).ChildNodes[0].Value;
                }
            }

        }
        return value;
    }

    public static void LogExceptions(string page, string message, string stackTrace, string query)
    {
        //    xml.Load(XMLExceptionErrorPath);
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

        //    if(xml.)
    }
}