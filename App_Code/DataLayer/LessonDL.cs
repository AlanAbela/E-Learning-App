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
    SqlConnection connection = new SqlConnection(Connection.ConnectionString());
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
        using (connection)
        {
            SqlCommand command = new SqlCommand("GetAllLessons", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
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
        using (connection)
        {
            SqlCommand command = new SqlCommand("GetLessonByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@lessonID", ID);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);

            return table;
        }
    }
    #endregion
}