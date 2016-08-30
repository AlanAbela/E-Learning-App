using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoginBL
/// </summary>
public class LoginBL
{
    #region Properties
    public UserDL userDL { get; set;}
    #endregion

    #region Constructors
    public LoginBL()
    {
        userDL = new UserDL();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Checks if a record exists with matching user name and password.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public int AuthenticateUser(string username, string password)
    {
        return UserDL.AuthenticateUser(username, password);
    }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public int GetUserID(string username, string password)
    {
        DataTable table = userDL.GetUserDetails(username, password);      
        return table.Rows[0].Field<int>("ID");
    }
    #endregion
}