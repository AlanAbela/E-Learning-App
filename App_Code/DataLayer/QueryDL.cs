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
    SqlConnection connection = new SqlConnection(Connection.ConnectionString(Connection.ConType.One));
    SqlConnection connection2 = new SqlConnection(Connection.ConnectionString(Connection.ConType.Two));

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
        using (connection)
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
    /// <param name="query"></param>
    /// <returns></returns>
    public DataTable ProcessQuery(string query)
    {
        using (connection2)
        {
            SqlCommand command = new SqlCommand(query, connection2);
            connection2.Open();

            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);
            return table;
        }
    }
}