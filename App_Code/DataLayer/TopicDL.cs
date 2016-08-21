﻿using System;
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
    // Reference connection string.
    string connectionString = Connection.ConnectionString(Connection.ConType.One);

    // Declare a datatable variable.
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
        // Using an SQL connection, which is instantiated on instance creation.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("GetAllTopicsByLessonID", connection);
            command.CommandType = CommandType.StoredProcedure;
            // Pass parameter value to the stored procedure.
            command.Parameters.AddWithValue("@lessonID", lessonID);

            connection.Open();

            // Get records from table.
          SqlDataReader reader =  command.ExecuteReader();

            // Fill a datatable with the table records.
            table = new DataTable();
            table.Load(reader);

            // Sort topics.
            table.DefaultView.Sort = "OrderNo ASC";
            table = table.DefaultView.ToTable();

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
        // Using an SQL connection, which is instantiated on instance creation.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("GetTopicByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            // Pass parameter value to the stored procedure.
            command.Parameters.AddWithValue("@topicID", topicID);

            connection.Open();

            // Get records from table.
            SqlDataReader reader = command.ExecuteReader();

            // Fill a datatable with the table records.
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
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Get the example query.
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("GetExampleQuery", connection);
            command.Parameters.AddWithValue("@topicID", topicID);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);

            // Get the result table of the example query.
            string query = table.Rows[0].Field<string>("query");

            // Execute the query against the database.
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
        // Using an SQL connection, which is instantiated on instance creation.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Get second example query.
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

            // If example is an UPDATE or INSERT.
            if (table.Rows.Count == 0)
            {
                command = new SqlCommand("GetAllEmployee", connection);
                reader = command.ExecuteReader();
                table.Load(reader);
            }

            // Reset the table in case the query changes the fields of the table.
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
    public DataTable GetExampleTable3(int topicID)
    {
        // Using an SQL connection, which is instantiated on instance creation.
        using (SqlConnection connection = new SqlConnection(connectionString))
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

            // If example is an UPDATE or INSERT.
            if (table.Rows.Count == 0)
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
    /// Retrives all records by lesson ID including users that have completed the topic.
    /// </summary>
    /// <param name="lessonID"></param>
    /// <returns>
    /// </returns>
    public DataTable GetTopicsAndUserID(int lessonID)
    {
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("GetTopicsRelatedToLessonAndUser", connection);
            // Pass parameter value to the stored procedure.
            command.Parameters.AddWithValue("@lessonID", lessonID);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            // Get records from table.
            SqlDataReader reader = command.ExecuteReader();

            // Fill a datatable with the table records.
            table = new DataTable();
            table.Load(reader);

            return table;
        }
    }

    /// <summary>
    /// Retrieves the count of all topics by lesson id.
    /// </summary>
    /// <param name="lessonID"></param>
    /// <returns>
    /// The number of topics of the defined lesson.
    /// </returns>
    public int GetCountTopicsByLessonID(int lessonID)
    {
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Declare an SQL command.
            SqlCommand command = new SqlCommand("CountTopicsByLessonID", connection);
            // Pass values to the SQL command.
            command.Parameters.AddWithValue("@lessonID", lessonID);
            // Declare the the SQL command is of type store procedure.
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            return Convert.ToInt32(command.ExecuteScalar());
        }
    }

    /// <summary>
    /// Retrieves the number of topics that are complete by for a defined lesson by a defined user.
    /// </summary>
    /// <param name="lessonID"></param>
    /// <returns>
    /// the number of complete topics by a user for the defined lesson.
    /// </returns>
    public int GetCountCompletedTopics(int lessonID, int userID)
    {
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Declare an SQL command.
            SqlCommand command = new SqlCommand("CountCompleteTopics", connection);
            // Pass values to the SQL command.
            command.Parameters.AddWithValue("@lessonID", lessonID);
            command.Parameters.AddWithValue("@userID", userID);
            // Declare the the SQL command is of type store procedure.
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}