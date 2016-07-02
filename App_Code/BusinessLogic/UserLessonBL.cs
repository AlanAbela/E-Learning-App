using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserLesson
/// </summary>
public class UserLessonBL
{

    UserLessonDL userLessonDL;
    public UserLessonBL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Inserts a new record.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="lessonID"></param>
    public void InserNewRecord(int userID, int lessonID)
    {
        userLessonDL = new UserLessonDL();
        userLessonDL.InserRecord(userID, lessonID);
    }

    /// <summary>
    /// Retrieves a record by userID and lessonID.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="lessonID"></param>
    /// <returns></returns>
    public DataTable GetRecord(int userID, int lessonID)
    {
        userLessonDL = new UserLessonDL();
        return userLessonDL.GetRecord(userID, lessonID);
    }
}