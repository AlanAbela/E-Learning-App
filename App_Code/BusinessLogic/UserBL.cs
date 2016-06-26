using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserBL
/// </summary>
public class UserBL
{

    public UserBL()
    {
       
    }

    #region Public methods
    /// <summary>
    /// Retrives a user record.
    /// </summary>
    /// <param name="userID"></param>
    /// <returns>A userBL object or null</returns>
    public DataTable GetUserBL(int userID)
    {
        UserDL userDL = new UserDL();
        return userDL.GetUSerByID(userID);

    }
    #endregion

}