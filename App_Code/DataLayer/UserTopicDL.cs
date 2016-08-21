using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserTopicDL
/// </summary>
public class UserTopicDL
{
    #region Global variables
    // Reference connection string.
    string connectionString = Connection.ConnectionString(Connection.ConType.One);
    // Declare a datatable variable.
    DataTable table;
    #endregion

    public UserTopicDL()
    {
      
    }


    #region public Methods
    /// <summary>
    /// Inserts a record if it does not exist.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="TopicID"></param>
    public void InsertRecord(int userID, int topicID)
    {
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("InsertUserTopic", connection);

            // Declare that a store procedure is used.
            command.CommandType = CommandType.StoredProcedure;

            // Add values to the stored procedure.
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@topicID", topicID);
            
            connection.Open();

            // Reference int value of the method call CheckRecord
            int result = CheckRecord(connection, userID, topicID);

            // If the result is 0 add a new record.
                if(result == 0)
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Inserts the completion date.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="topicID"></param>
    public void SetCompleteDate(int userID, int topicID)
    {
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("InsertUserTopicCompleteDate", connection);

            // Add values to the stored procedure.
            command.CommandType = CommandType.StoredProcedure;

            // Add values to the stored procedure.
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@topicID", topicID);
            command.Parameters.AddWithValue("@date", DateTime.Now);
           

            connection.Open();

            command.ExecuteNonQuery();
        }

    }


/// <summary>
/// Get records by user ID.
/// </summary>
/// <param name="userID"></param>
/// <returns></returns>
public DataTable GetCompleteTopics(int userID)
    {
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("GetTopicsByUserID", connection);

            // Add values to the stored procedure.
            command.CommandType = CommandType.StoredProcedure;

            // Add values to the stored procedure.
            command.Parameters.AddWithValue("@userID", userID);
            
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);

            return table;
        }
    }

    /// <summary>
    /// Retrieves a record by user ID and Topic ID.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="topicID"></param>
    /// <returns></returns>
    public DataTable GetRecord(int userID, int topicID)
    {
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("GetUserTopic", connection);

            // Add values to the stored procedure.
            command.CommandType = CommandType.StoredProcedure;

            // Add values to the stored procedure.
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@topicID", topicID);
            
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);

            return table;
        }
    }
    #endregion


    #region Private Methods
    /// <summary>
    /// Checks if a record with the defined user id and topic id exists.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="TopicID"></param>
    /// <returns>1 if exists, 0 if not</returns>
    private int CheckRecord(SqlConnection connection, int userID, int topicID)
    {
      
            SqlCommand command = new SqlCommand("CheckUserTopic", connection);
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@topicID", topicID);
            command.CommandType = CommandType.StoredProcedure;

            return Convert.ToInt32(command.ExecuteScalar());
    }
    #endregion

 
}

