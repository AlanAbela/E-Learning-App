﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for ErrorMessage
/// </summary>
public class ErrorMessage
{
    #region Global variables
    private static string XMLPath = "C:/Users/Alan/Documents/Visual Studio 2015/Projects/E-Learning App/E-Learning App/XML/Errors.xml";
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
}