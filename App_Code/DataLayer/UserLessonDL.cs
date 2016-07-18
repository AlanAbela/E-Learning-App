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
            SqlCommand command = new SqlCommand("InsertUserLesson", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@lessonID", lessonID);

            connection.Open();

            int result = CheckRecord(connection, userID, lessonID);

            if (result == 0)
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Inserts completion date.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="lessonID"></param>
    public void InsertDateCompleted(int userID, int lessonID)
    {
        using (connection)
        {
            SqlCommand command = new SqlCommand("InsertUserLessonCompleteDate", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@lessonID", lessonID);
            command.Parameters.AddWithValue("@date", DateTime.Now);

            connection.Open();

            command.ExecuteNonQuery();
        }
    }

/// <summary>
/// Insert Mark.
/// </summary>
/// <param name="userID"></param>
/// <param name="lessonID"></param>
/// <param name="mark"></param>
    public void InsertMark(int userID, int lessonID, int correct, int incorrect)
    {
        using (connection)
        {
            SqlCommand command = new SqlCommand("UpdateUserLessonMark", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@lessonID", lessonID);
            command.Parameters.AddWithValue("@correct", correct);
            command.Parameters.AddWithValue("@incorrect", incorrect);

            connection.Open();

            command.ExecuteNonQuery();
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

    /// <summary>
    /// Inserts time taken to complete quiz.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="lessonID"></param>
    /// <param name="dateTime"></param>
    public void InsertQuizTime(int userID, int lessonID, TimeSpan time)
    {
        using (connection)
        {
            SqlCommand command = new SqlCommand("InsertUserLessonQuizTime", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@quizTime", time);
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@lessonID", lessonID);

            connection.Open();

            command.ExecuteNonQuery();
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