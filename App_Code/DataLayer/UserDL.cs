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

        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString()))
        {

            SqlCommand command = new SqlCommand("AddRecord", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", Username);
            command.Parameters.AddWithValue("@password", hashedPassword);
            command.Parameters.AddWithValue("@date", DateTime.Now);

            conn.Open();

            command.ExecuteNonQuery();

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
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString()))
        {

            SqlCommand command = new SqlCommand("GetUser", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", hashedPassword);

            conn.Open();

            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);

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

        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString()))
        {

            SqlCommand command = new SqlCommand("GetUserCount", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);

            conn.Open();

            // If username already exists will return a value bigger than 0.
            return Convert.ToInt32(command.ExecuteScalar());
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
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString()))
        {

            SqlCommand command = new SqlCommand("GetUser", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);
            string hashedPassword = UserDL.EncyptPassword(password);
            command.Parameters.AddWithValue("@password", hashedPassword);

            conn.Open();

            // If username already exists will return a value bigger than 0.
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }


    #endregion

    /// <summary>
    /// Retrieves a record by user ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public DataTable GetUSerByID(int ID)
    {
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString()))
        {
            SqlCommand command = new SqlCommand("GetUserByID", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userID", ID);

            conn.Open();

            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return table;
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

