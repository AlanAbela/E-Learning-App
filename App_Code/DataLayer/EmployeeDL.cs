using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeDL
/// </summary>
public class EmployeeDL
{
    #region Global variables
   private static string connectionString1 = Connection.ConnectionString(Connection.ConType.One);
    DataTable table = new DataTable();
    #endregion

    #region Constructors
    public EmployeeDL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    /// <summary>
    /// Checks if the table employee is present
    /// </summary>
    /// <returns>true if present else returns false</returns>
    public int IsEmployee ()
    {
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            
            SqlCommand command = new SqlCommand("CheckEmployee", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());

            return result;
        }
    }

    /// <summary>
    /// Recreated the table Employee.
    /// </summary>
    public void RefreshTable()
    {
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            SqlCommand command = new SqlCommand("CreateTableEmp", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            command.ExecuteReader();
        }
    }

    /// <summary>
    /// Retrieves the records present in the table Employee.
    /// </summary>
    /// <returns></returns>
    public DataTable GetEmployee()
    {
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            SqlCommand command = new SqlCommand("GetAllEmployee", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);
            return table;
        }
    }
}