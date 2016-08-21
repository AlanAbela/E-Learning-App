using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Represents Employee.
/// </summary>
public class EmployeeDL
{
    #region Global variables
    // Reference connection string.
    string connectionString = Connection.ConnectionString(Connection.ConType.One);
    // Declare a datatable variable.
    DataTable table;
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
    /// Checks if the table employee is present.
    /// </summary>
    /// <returns>
    /// Retruns 1 if present 0 if not.
    /// </returns>
    public int IsEmployee ()
    {
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // SQL command to represent an SQL statement or stored procedure.
            SqlCommand command = new SqlCommand("CheckEmployee", connection);
            // Specify that a stored procedure is used.
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            // Execute query and reference the first value.
            int result = Convert.ToInt32(command.ExecuteScalar());

            return result;
        }
    }

    /// <summary>
    /// Recreated the table Employee.
    /// </summary>
    public void RefreshTable()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // SQL command to represent an SQL statement or stored procedure
            SqlCommand command = new SqlCommand("CreateTableEmp", connection);
            // Specify that a stored procedure is used.
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            // Execute the query.
            command.ExecuteReader();
        }
    }

    /// <summary>
    /// Retrieves the records present in the table Employee.
    /// </summary>
    /// <returns></returns>
    public DataTable GetEmployee()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // SQL command to represent an SQL statement or stored procedure
            SqlCommand command = new SqlCommand("GetAllEmployee", connection);

            // Specify that a stored procedure is used.
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            // Retrieve data.
            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);
            return table;
        }
    }
}