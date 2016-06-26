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
    SqlConnection connection = new SqlConnection(Connection.ConnectionString());
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
    public DataTable GetTopicDLsByLessonID(int ID)
    {
        using (connection)
        {
            SqlCommand command = new SqlCommand("GetAllTopicsByLessonID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@lessonID", ID);

            connection.Open();

          SqlDataReader reader =  command.ExecuteReader();
            table.Load(reader);

            return table;
        }
    }
}