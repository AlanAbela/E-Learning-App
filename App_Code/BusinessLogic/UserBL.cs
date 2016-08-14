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
    public UserDL UserDL {get; set;}

public UserBL()
    {
        UserDL = new UserDL();
    }

    #region Public methods
    /// <summary>
    /// Retrives a user record.
    /// </summary>
    /// <param name="userID"></param>
    /// <returns>A userBL object or null</returns>
    public DataTable GetUser(int userID)
    {
        return UserDL.GetUserByID(userID);

    }


    /// <summary>
    /// Insert final test's number of correct answers.
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="correctAnswers"></param>
    public void UpdateCorrectAnswers(int ID, int? correctAnswers)
    {
        UserDL.UpdateCorrectAnswers(ID, correctAnswers);
    }

    /// <summary>
    /// Insert time taken to complete test.
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="time"></param>
    public void InsertTimeTaken(int ID, TimeSpan? time)
    {
        UserDL.InsertTimeTaken(ID, time);
    }

    /// <summary>
    /// Insert time when test is completed.
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="time"></param>
    public void InsertTestCompleteDate(int ID)
    {
        UserDL.InsertTestCompleteDate(ID);
    }

        #endregion

    }