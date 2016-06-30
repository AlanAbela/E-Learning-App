using System;
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
    EmployeeDL employeeDL = new EmployeeDL();
    #endregion

    public EmployeeBL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

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

    public DataTable GetAllEmployee()
    {
        return employeeDL.GetEmployee();
    }
}