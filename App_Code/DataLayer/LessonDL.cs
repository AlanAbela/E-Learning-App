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
    #region Properties
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    #endregion

    #region Global Variables
    SqlConnection connection = new SqlConnection(Connection.ConnectionString(Connection.ConType.One));
    DataTable table = new DataTable();
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
        // Using an SQL connection, which is instantiated on instance creation
        using (connection)
        {
            // SQL command to represent an SQL statement or stored procedure
            SqlCommand command = new SqlCommand("GetAllLessons", connection);
            // Specify that a stored procedure is used.
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            // Retrieve data 
            SqlDataReader reader = command.ExecuteReader();
            // Load the data in a Datatable
            table.Load(reader);
            // Release all connection resources.
            connection.Dispose();

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
        using (connection)
        {
            SqlCommand command = new SqlCommand("GetLessonByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@lessonID", ID);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);
            connection.Dispose();
            return table;
        }
    }
    #endregion
}