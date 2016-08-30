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
    #region Properties
    public LessonDL lessonDL { get; set;}
    #endregion

    #region Constructors
    public LessonBL()
    {
        lessonDL = new LessonDL();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Retrieves all records from table Lesson.
    /// </summary>
    /// <returns></returns>
    public DataTable GetLessons()
    {
        return lessonDL.GetAllLessons();
    }

    /// <summary>
    /// Retrieves a lesson by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public DataTable GetLesson(int ID)
    {
        return lessonDL.GetLessonByID(ID);
    }
    #endregion
}