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
    #region Constructors
    public LoginBL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion
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
        UserDL userDL = new UserDL();

        DataTable table = userDL.GetUserDetails(username, password); 
        
        return table.Rows[0].Field<int>("ID");
    }
}