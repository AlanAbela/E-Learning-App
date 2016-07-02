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
    #region Properties
    string ConnectionString1 = Connection.ConnectionString(Connection.ConType.One);
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
        using (SqlConnection connection = new SqlConnection(ConnectionString1))
        {
          
            SqlCommand command = new SqlCommand("InsertUserTopic", connection);
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@topicID", topicID);
            command.Parameters.AddWithValue("@date", DateTime.Now);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            int result = CheckRecord(connection, userID, topicID);

                if(result == 0)
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Get records by user ID.
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public DataTable GetCompleteTopics(int userID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString1))
        {
            SqlCommand command = new SqlCommand("GetTopicsByUserID", connection);
            command.Parameters.AddWithValue("@userID", connection);
            command.CommandType = CommandType.StoredProcedure;
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
        using (SqlConnection connection = new SqlConnection(ConnectionString1))
        {
            SqlCommand command = new SqlCommand("GetUserTopic", connection);
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@topicID", topicID);
            command.CommandType = CommandType.StoredProcedure;
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

