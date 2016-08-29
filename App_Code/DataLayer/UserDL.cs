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
    public string Username { get; set;}
    public string Password { get; set;}
    #endregion

    #region Global variables
    // Reference connection string.
    string connectionString = Connection.ConnectionString(Connection.ConType.One);
    // Declare a datatable variable.
    DataTable table;
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
        string hashedPassword = EncriptPassword(Password);

        // Using an SQL connection.
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("AddRecord", conn);
            command.CommandType = CommandType.StoredProcedure;

            // Add values to the stored procedure.
            command.Parameters.AddWithValue("@username", Username);
            command.Parameters.AddWithValue("@password", hashedPassword);
            command.Parameters.AddWithValue("@date", DateTime.Now);

            conn.Open();
            // Execute query.
            command.ExecuteNonQuery();

        }
    }

    /// <summary>
    /// Gets the details of the user.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public DataTable GetUserDetails(string username, string password)
    {
        // Encripth and reference the enchripted password.
        string hashedPassword = EncriptPassword(password);

        // Using an SQL connection.
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("GetUser", conn);

            // Define that a store procedure is used.
            command.CommandType = CommandType.StoredProcedure;

            // Pass parameter value to the stored procedure.
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", hashedPassword);

            conn.Open();

            // Fill a datatable with the table records.
            SqlDataReader reader = command.ExecuteReader();
            table = new DataTable();
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
        // Using an SQL connection.
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("GetUserCount", conn);

            // Define that a store procedure is used.
            command.CommandType = CommandType.StoredProcedure;

            // Pass parameter value to the stored procedure.
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
        //Using an SQL connection.
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("GetUser", conn);

            // Declare that a store procedure is used.
            command.CommandType = CommandType.StoredProcedure;
            
            // Encript and reference encripted password.
            string hashedPassword = UserDL.EncriptPassword(password);

            // Pass parameters to stored procedures.
            command.Parameters.AddWithValue("@username", username);
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
        // Using an SQL connection.
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("GetUserByID", conn);

            // Declare that a store procedure is used.
            command.CommandType = CommandType.StoredProcedure;

            // Pass parameters to stored procedures.
            command.Parameters.AddWithValue("@userID", ID);

            conn.Open();

            // execute the query and reference the returned SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // Fill a datatable with the table records.
            table = new DataTable();
            table.Load(reader);
            conn.Dispose();
            return table;
        }
    }

    /// <summary>
    /// Update final test number of correct answers.
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="correctAnswers"></param>
    public void UpdateCorrectAnswers(int ID, int? correctAnswers)
    {
        // Using an SQL connection.
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("UpdateUserCorrectAnswers", conn);

            // Declare that a store procedure is used.
            command.CommandType = CommandType.StoredProcedure;

            // Pass parameters to the stored procedure.
            command.Parameters.AddWithValue("@userID", ID);
            command.Parameters.AddWithValue("@correctAnswers", correctAnswers);

            conn.Open();
            
            command.ExecuteNonQuery();

        }
    }

    /// <summary>
    /// Insert time taken to complete test.
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="time"></param>
    public void InsertTimeTaken (int ID, TimeSpan? time)
    {
        // Using an SQL connection.
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("InsertUserTestTime", conn);

            // Declare that a store procedure is used.
            command.CommandType = CommandType.StoredProcedure;

            // Pass parameters to the stored procedure.
            command.Parameters.AddWithValue("@userID", ID);
            command.Parameters.AddWithValue("@time", time);

            conn.Open();

            command.ExecuteNonQuery();

        }
    }

    /// <summary>
    /// Insert time when test is completed.
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="time"></param>
    public void InsertTestCompleteDate(int ID)
    {
        // Create SQL command, pass connection and name of stored procedure.
        using (SqlConnection conn = new SqlConnection(Connection.ConnectionString(Connection.ConType.One)))
        {
            // Create SQL command, pass connection and name of stored procedure.
            SqlCommand command = new SqlCommand("InsertUserFinalTestCompleteDate", conn);

            // Declare that a store procedure is used.
            command.CommandType = CommandType.StoredProcedure;

            // Pass parameters to the stored procedure.
            command.Parameters.AddWithValue("@userID", ID);
            command.Parameters.AddWithValue("@dateCompleted", DateTime.Now);

            conn.Open();

            command.ExecuteNonQuery();

        }
    }

    /// <summary>
    /// Encripts a string type with SHA1 enchription.
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    private static string EncriptPassword(string password)
    {
        string hashedPassword = Crypto.SHA256(password);
        return hashedPassword;
    }
}

