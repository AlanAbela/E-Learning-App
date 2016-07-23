using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;

/// <summary>
/// Summary description for User
/// </summary>
public class UserDL
{
    #region Properties
    public DateTime RegisteredDate { get; set; }
    public string Username { get; set;}
    public string Password { get; set;}
    public int ID {get; set;}
    #endregion

    #region Constructors
    public UserDL()
    {
        
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Insert a new record.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public void RegisterUser()
    {
        string hashedPassword = EncyptPassword(Password);

        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {

            SqlCommand command = new SqlCommand("AddRecord", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", Username);
            command.Parameters.AddWithValue("@password", hashedPassword);
            command.Parameters.AddWithValue("@date", DateTime.Now);

            conn.Open();

            command.ExecuteNonQuery();
            conn.Dispose();
        }
    }

    /// <summary>
    /// Gets the deatails of the user.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public DataTable GetUserDetails(string username, string password)
    {

        string hashedPassword = EncyptPassword(password);
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {

            SqlCommand command = new SqlCommand("GetUser", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", hashedPassword);

            conn.Open();

            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            conn.Dispose();
            return table;

           
        }
    }
    #endregion


    #region Static methods
    /// <summary>
    /// Check if a user already exists.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>1 if present, 0 if missing.</returns>
    public static int DuplicateUser(string username)
    {

        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {

            SqlCommand command = new SqlCommand("GetUserCount", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);

            conn.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            conn.Dispose();
            // If username already exists will return a value bigger than 0.
            return result;

        }
    }

    /// <summary>
    /// Checks if a user record with the defined password and username exists.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>1 if present, 0 if missing.</returns>
    public static int AuthenticateUser(string username, string password)
    {
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {

            SqlCommand command = new SqlCommand("GetUser", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);
            string hashedPassword = UserDL.EncyptPassword(password);
            command.Parameters.AddWithValue("@password", hashedPassword);

            conn.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            conn.Dispose();
            // If username already exists will return a value bigger than 0.
            return result;
        }
    }


    #endregion

    /// <summary>
    /// Retrieves a record by user ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public DataTable GetUserByID(int ID)
    {
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {
            SqlCommand command = new SqlCommand("GetUserByID", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userID", ID);

            conn.Open();

            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            conn.Dispose();
            return table;
        }

    }

    /// <summary>
    /// Insert final test number of correct answers.
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="correctAnswers"></param>
    public void InsertCorrectAnswers(int ID, int? correctAnswers)
    {
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {
            SqlCommand command = new SqlCommand("InsertUserCorrectAnswers", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userID", ID);
            command.Parameters.AddWithValue("@correctAnswers", correctAnswers);

            conn.Open();

            command.ExecuteNonQuery();

            conn.Dispose();
        }
    }

    /// <summary>
    /// Insert time taken to complete test.
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="time"></param>
    public void InsertTimeTaken (int ID, TimeSpan? time)
    {
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {
            SqlCommand command = new SqlCommand("InsertUserTestTime", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userID", ID);
            command.Parameters.AddWithValue("@time", time);

            conn.Open();

            command.ExecuteNonQuery();

            conn.Dispose();
        }
    }

    /// <summary>
    /// Insert time when test is completed.
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="time"></param>
    public void InsertTestCompleteDate(int ID)
    {
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {
            SqlCommand command = new SqlCommand("InsertUserFinalTestCompleteDate", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userID", ID);
            command.Parameters.AddWithValue("@dateCompleted", DateTime.Now);

            conn.Open();

            command.ExecuteNonQuery();

            conn.Dispose();
        }
    }

    /// <summary>
    /// Encripts a string type with SHA1 enchription.
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    private static string EncyptPassword(string password)
    {
        string hashedPassword = Crypto.SHA1(password);
        return hashedPassword;
    }
}

