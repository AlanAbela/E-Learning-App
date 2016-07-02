using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserLessonDL
/// </summary>
public class UserLessonDL
{
    #region Global variables
    SqlConnection connection = new SqlConnection(Connection.ConnectionString(Connection.ConType.One));
    DataTable table;
#endregion

    public UserLessonDL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Public Methods
    /// <summary>
    /// Inserts a new record.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="lessonID"></param>
    public void InserRecord(int userID, int lessonID)
    {
        using (connection)
        {
            SqlCommand command = new SqlCommand("InserUserLesson", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@lessonID", lessonID);
            command.Parameters.AddWithValue("@date", DateTime.Now);

            connection.Open();

            int result = CheckRecord(connection, userID, lessonID);

            if (result == 0)
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Retrieves a record by userID and LessonID
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="lessonID"></param>
    /// <returns></returns>
    public DataTable GetRecord(int userID, int lessonID)
    {
        using (connection)
        {
            SqlCommand command = new SqlCommand("GetUserLesson", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@lessonID", lessonID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();

            table.Load(reader);

            return table;

        }
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Checks if a record with the defined user id and lesson id exists.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="TopicID"></param>
    /// <returns>1 if exists, 0 if not</returns>
    private int CheckRecord(SqlConnection connection, int userID, int lessonID)
    {

        SqlCommand command = new SqlCommand("CheckUserLesson", connection);
        command.Parameters.AddWithValue("@userID", userID);
        command.Parameters.AddWithValue("@lessonID", lessonID);
        command.CommandType = CommandType.StoredProcedure;

        return Convert.ToInt32(command.ExecuteScalar());
    }
    #endregion
}