﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomException
/// </summary>
public class CustomException : Exception
{
    public CustomException()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public CustomException(string message) : base(message)
    {

    }
}