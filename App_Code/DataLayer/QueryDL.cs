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

    DataTable table = new DataTable();
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
            SqlCommand command = new SqlCommand("GetQueryByTopicID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@topicID", topicID);

            connection.Open();
            // Get records from table.
            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);

            // Use value from table to retrive the records to match with. 
            command = new SqlCommand(table.Rows[0].Field<string>("Query"), connection);
            reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);
            connection.Dispose();
            return table;
        }
    }

    /// <summary>
    /// Processes the defined query. 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public void ProcessQuery(string query)
    {
        using (connection2)
        {
            SqlCommand command = new SqlCommand(query, connection2);
            connection2.Open();

            SqlDataReader reader = command.ExecuteReader();
        }
    }
}