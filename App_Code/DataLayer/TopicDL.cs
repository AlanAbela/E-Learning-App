using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Topic
/// </summary>
public class TopicDL
{
    #region Global variables
    string connectionString1 = Connection.ConnectionString(Connection.ConType.One);
    DataTable table;
    #endregion

    public TopicDL()
    {
        
    }

    /// <summary>
    /// Gets all topics of the defined lesson.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public DataTable GetAllTopicsByLessonID(int lessonID)
    {
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            SqlCommand command = new SqlCommand("GetAllTopicsByLessonID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@lessonID", lessonID);

            connection.Open();

          SqlDataReader reader =  command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);

            return table;
        }
    }

    /// <summary>
    /// Retrieves a record from the topic table by its ID.
    /// </summary>
    /// <param name="topicID"></param>
    /// <returns></returns>
    public DataTable GetTopicByID(int topicID)
    {
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            SqlCommand command= new SqlCommand("GetTopicByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@topicID", topicID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);
            
            return table;
        }
    }

    /// <summary>
    /// Retrieves records according to the query stored in table topic.
    /// </summary>
    /// <returns></returns>
    public DataTable GetExampleTable(int topicID)
    {
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            // Get the example query
            SqlCommand command = new SqlCommand("GetExampleQuery", connection);
            command.Parameters.AddWithValue("@topicID", topicID);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);

            // Get the result table of the example query.
            string query = table.Rows[0].Field<string>("query");
            command = new SqlCommand(query, connection);
            reader = command.ExecuteReader();
            table.Reset();
            table.Load(reader);

            // If example is an UPDATE or INSERT
            if(table.Rows.Count == 0)
            {
                command = new SqlCommand("GetAllEmployee", connection);
                reader = command.ExecuteReader();
                table.Load(reader);
            }

            // Reset the table in case the query changes the fields of the table
            command = new SqlCommand("CreateTableEmp", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();

            return table;
        }
    }

    /// <summary>
    /// Retrieves records according to the query stored in table topic.
    /// </summary>
    /// <param name="topicID"></param>
    /// <returns></returns>
    public DataTable GetExampleTable2(int topicID)
    {
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            // Get second example query
            SqlCommand command = new SqlCommand("GetExampleQuery2", connection);
            command.Parameters.AddWithValue("@topicID", topicID);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);

            // Get the result table of the example query.
            string query = table.Rows[0].Field<string>("query");
            command = new SqlCommand(query, connection);
            reader = command.ExecuteReader();
            table.Reset();
            table.Load(reader);

            return table;
        }
    }

    /// <summary>
    /// Retrieves records according to the query stored in table topic.
    /// </summary>
    /// <param name="topicID"></param>
    /// <returns></returns>
    public DataTable GetExampleTable3(int topicID)
    {
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            // Get second example query
            SqlCommand command = new SqlCommand("GetExampleQuery3", connection);
            command.Parameters.AddWithValue("@topicID", topicID);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);

            // Get the result table of the example query.
            string query = table.Rows[0].Field<string>("query");
            command = new SqlCommand(query, connection);
            reader = command.ExecuteReader();
            table.Reset();
            table.Load(reader);

            return table;
        }
    }

    /// <summary>
    /// Retrives all records by lesson ID including users that have completed the topic.
    /// </summary>
    /// <param name="lessonID"></param>
    /// <returns></returns>
    public DataTable GetTopicsAndUserID(int lessonID)
    {
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            SqlCommand command = new SqlCommand("GetTopicsRelatedToLessonAndUser", connection);
            command.Parameters.AddWithValue("@lessonID", lessonID);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);

            return table;
        }
    }

    /// <summary>
    /// Retrieves the count of all topics by lesson id.
    /// </summary>
    /// <param name="lessonID"></param>
    /// <returns></returns>
    public int GetCountTopicsByLessonID(int lessonID)
    {
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            SqlCommand command = new SqlCommand("CountTopicsByLessonID", connection);
            command.Parameters.AddWithValue("@lessonID", lessonID);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            return Convert.ToInt32(command.ExecuteScalar());
        }
    }

    /// <summary>
    /// Retrieves the number of topics that are complete by for a defined lesson by a defined user.
    /// </summary>
    /// <param name="lessonID"></param>
    /// <returns></returns>
    public int GetCountCompletedTopics(int lessonID, int userID)
    {
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            SqlCommand command = new SqlCommand("CountCompleteTopics", connection);
            command.Parameters.AddWithValue("@lessonID", lessonID);
            command.Parameters.AddWithValue("@userID", userID);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}