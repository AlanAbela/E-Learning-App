using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for QueryDL
/// </summary>
public class QueryDL
{
    #region Global variables
    // Reference connection strings.
    string connectionString = Connection.ConnectionString(Connection.ConType.One);
    string connectionStringUser = Connection.ConnectionString(Connection.ConType.Two);

    // Declare a datatable variable.
    DataTable table;
    #endregion

    public QueryDL()
    {
        
    }

    /// <summary>
    /// Executes the query retrieved from the table Query.
    /// </summary>
    /// <param name="topicID "></param>
    /// <returns></returns>
    public DataTable GetResultByQuery(int topicID)
    {
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("GetQueryByTopicID", connection);
            command.CommandType = CommandType.StoredProcedure;
            // Pass parameter value to the stored procedure.
            command.Parameters.AddWithValue("@topicID", topicID);

            connection.Open();
            // Get records from table.
            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);

            // Record returned contains the SQL query. Execute the query against the database
            // and create a Datatable representation of the query. 
            command = new SqlCommand(table.Rows[0].Field<string>("Query"), connection);
            reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);
            return table;
        }
    }

    /// <summary>
    /// Processes the defined query. 
    /// </summary>
    /// <param name="query">
    /// string representing an SQL query.
    /// </param>
    /// <returns>
    /// Datatable representing the queried table.
    /// </returns>
    public DataTable ProcessQuery(string query)
    {
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionStringUser))
        {
            // Declare a new sqlcommand to represent the query.
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            // Execute query.
            SqlDataReader reader = command.ExecuteReader();

            // Populate a datatable with the returned records.
            table = new DataTable();
            table.Load(reader);

            return table;
        }
    }
}