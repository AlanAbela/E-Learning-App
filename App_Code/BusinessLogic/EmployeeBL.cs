﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeBL
/// </summary>
public class EmployeeBL
{
    #region Properties
    public EmployeeDL employeeDL { get; set; }
    #endregion

    #region Constructors
    public EmployeeBL()
    {
        employeeDL = new EmployeeDL();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Checks if the table employee is present
    /// </summary>
    /// <returns>true if present else returns false</returns>
    public int IsEmployee()
    {
        return employeeDL.IsEmployee();
    }

    /// <summary>
    /// Deletes and recreates table.
    /// </summary>
    public void RecreateTable()
    {
        employeeDL.RefreshTable();
    }

    /// <summary>
    /// Retrieves all records from table Employee.
    /// </summary>
    /// <returns></returns>
    public DataTable GetAllEmployee()
    {
        return employeeDL.GetEmployee();
    }
    #endregion
}