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
    #region Properties
    UserLessonDL userLessonDL { get; set;}
    #endregion

    #region Constructors
    public UserLessonBL()
    {
        userLessonDL = new UserLessonDL();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Inserts a new record.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="lessonID"></param>
    public void InserNewRecord(int userID, int lessonID)
    {
        userLessonDL.InserRecord(userID, lessonID);
    }

    /// <summary>
    /// Updates record's completion date.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="lessonID"></param>
    public void SetCompletionDate(int userID, int lessonID)
    {
        userLessonDL.InsertDateCompleted(userID, lessonID);
    }

    /// <summary>
    /// Retrieves a record by userID and lessonID.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="lessonID"></param>
    /// <returns></returns>
    public DataTable GetRecord(int userID, int lessonID)
    {
        return userLessonDL.GetRecord(userID, lessonID);
    }

    /// <summary>
    /// Inserts user mark.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="lessonID"></param>
    /// <param name="mark"></param>
    public void InsertMark(int userID, int lessonID, int correct, int incorrect)
    {
        userLessonDL.InsertMark(userID, lessonID, correct, incorrect);
    }

    /// <summary>
    /// Insert Quiz time.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="lessonID"></param>
    /// <param name="time"></param>
    public void InsertQuizTime(int userID, int lessonID, TimeSpan time)
    {
        userLessonDL.InsertQuizTime(userID, lessonID, time);
    }
    #endregion
}