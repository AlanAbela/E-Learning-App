using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LessonBL
/// </summary>
public class LessonBL
{
    #region Constructors
    public LessonBL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Methods
    /// <summary>
    /// Retrieves all records from table Lesson.
    /// </summary>
    /// <returns></returns>
    public DataTable GetLessons()
    {
        LessonDL lesson = new LessonDL();
        return lesson.GetAllLessons();
    }

    /// <summary>
    /// Retrieves a lesson by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public DataTable GetLesson(int ID)
    {
        LessonDL lesson = new LessonDL();
        return lesson.GetLessonByID(ID);
    }


    #endregion
}