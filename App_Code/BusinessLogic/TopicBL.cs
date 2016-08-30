﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TopicBL
/// </summary>
public class TopicBL
{
    #region Properties
    TopicDL topicDL {get; set;}
    #endregion

    #region Constructors
    public TopicBL()
    {
        topicDL = new TopicDL();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Retrieves all topic records that are related to the defined lesson.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public DataTable GetTopicsByLessonID(int ID)
    {
        return  topicDL.GetAllTopicsByLessonID(ID);
    }

    /// <summary>
    /// Retrieves a topic record by ID
    /// </summary>
    /// <param name="topicID"></param>
    /// <returns></returns>
    public DataTable GetTopicByID(int topicID)
    {
        return topicDL.GetTopicByID(topicID);
    }

    /// <summary>
    /// Retrieve all records from the example table.
    /// </summary>
    /// <returns></returns>
    public DataTable GetExampleTable(int topicID)
    {
        return topicDL.GetExampleTable(topicID);
    }

    /// <summary>
    /// Retrieve all records from the 2nd example table.
    /// </summary>
    /// <param name="topicID"></param>
    /// <returns></returns>
    public DataTable GetExampleTable2(int topicID)
    {
        return topicDL.GetExampleTable2(topicID);
    }

    /// <summary>
    /// Retrieve all records from the 3rd example table.
    /// </summary>
    /// <param name="topicID"></param>
    /// <returns></returns>
    public DataTable GetExampleTable3(int topicID)
    {
        return topicDL.GetExampleTable3(topicID);
    }

    /// <summary>
    /// Get topics related to user.
    /// </summary>
    /// <param name="lessonID"></param>
    /// <returns></returns>
    public DataTable GetTopicsAndUserID(int lessonID)
    {
        return topicDL.GetTopicsAndUserID(lessonID);
    }

    /// <summary>
    /// Get the count of all topics under a defined lesson.
    /// </summary>
    /// <param name="lessonID"></param>
    /// <returns></returns>
    public int GetCountTopicsByLessonID(int lessonID)
    {        
            return topicDL.GetCountTopicsByLessonID(lessonID);
    }

    /// <summary>
    /// Get the count of all complete topics under a defined lesson by a defined user.
    /// </summary>
    /// <param name="lessonID"></param>
    /// <param name="userID"></param>
    /// <returns></returns>
    public int GetCountCompletedTopics(int lessonID, int userID)
    {
        return topicDL.GetCountCompletedTopics(lessonID, userID);
    }
    #endregion
}