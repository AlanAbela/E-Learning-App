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
    /// Udates record's completion date.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="lessonID"></param>
    public void SetCompletionDate(int userID, int lessonID)
    {
        userLessonDL = new UserLessonDL();
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
        userLessonDL = new UserLessonDL();
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
        userLessonDL = new UserLessonDL();
        userLessonDL.InsertMark(userID, lessonID, correct, incorrect);
    }

    public void InsertQuizTime(int userID, int lessonID, TimeSpan time)
    {
        userLessonDL = new UserLessonDL();
        userLessonDL.InsertQuizTime(userID, lessonID, time);
    }

    }