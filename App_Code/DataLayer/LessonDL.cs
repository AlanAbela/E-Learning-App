using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Lesson
/// </summary>
public class LessonDL
{

    #region Global variables
    // Reference connection string.
    string connectionString = Connection.ConnectionString(Connection.ConType.One);
    // Declare a datatable variable.
    DataTable table;
    #endregion

    #region Constructor
    public LessonDL()
    {
       
    }
    #endregion

    #region Methods

    /// <summary>
    /// Retrieves all records from table Lesson.
    /// </summary>
    /// <returns></returns>
    public DataTable GetAllLessons()
    {
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // SQL command to represent an SQL statement or stored procedure.
            SqlCommand command = new SqlCommand("GetAllLessons", connection);
            // Specify that a stored procedure is used.
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            // Retrieve data.
            SqlDataReader reader = command.ExecuteReader();
            // Load the data in a Datatable.
            table = new DataTable();
            table.Load(reader);

            return table;
        }
    }

    /// <summary>
    /// Retrives the records that match the ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public DataTable GetLessonByID(int ID)
    {
        // Using an SQL connection.
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // SQL command to represent an SQL statement or stored procedure.
            SqlCommand command = new SqlCommand("GetLessonByID", connection);
            // Specify that a stored procedure is used.
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@lessonID", ID);

            connection.Open();

            // Retrieve data.
            SqlDataReader reader = command.ExecuteReader();
            // Load the data in a Datatable.
            table = new DataTable();
            table.Load(reader);

            return table;
        }
    }
    #endregion
}