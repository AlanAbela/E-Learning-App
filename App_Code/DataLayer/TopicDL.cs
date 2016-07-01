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
    SqlConnection connection = new SqlConnection(Connection.ConnectionString(Connection.ConType.One));
    DataTable table = new DataTable();
    #endregion

    public TopicDL()
    {
        
    }

    /// <summary>
    /// Gets all topics of the defined lesson.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public DataTable GetTopicsDLsByLessonID(int lessonID)
    {
        using (connection)
        {
            SqlCommand command = new SqlCommand("GetAllTopicsByLessonID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@lessonID", lessonID);

            connection.Open();

          SqlDataReader reader =  command.ExecuteReader();
            table.Load(reader);
            connection.Dispose();
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
        using (connection)
        {
            SqlCommand command= new SqlCommand("GetTopicByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@topicID", topicID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);
            connection.Dispose();
            return table;
        }
    }

    /// <summary>
    /// Retrieves all recrods from table Employee.
    /// </summary>
    /// <returns></returns>
    public DataTable GetExampleTable()
    {
        using (connection)
        {
            SqlCommand command = new SqlCommand("SEDB_GetAllEmployees", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);

            return table;
        }
    }

    /// <summary>
    /// Retrieves records 
    /// </summary>
    /// <param name="lessonID"></param>
    /// <returns></returns>
    public DataTable GetUserTopicDesc(int lessonID)
    {
        using (connection)
        {
            SqlCommand command = new SqlCommand("GetUserTopicDesc", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@lessonID", lessonID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);

            return table;

        }
    }
}