using System;
using System.Collections.Generic;
using System.Data;
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

    public TopicBL()
    {
        topicDL = new TopicDL();
    }

    /// <summary>
    /// Retrieves all topic records that are related to the defined lesson.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public DataTable GetTopicsByLessonID(int ID)
    {
        return topicDL.GetTopicsDLsByLessonID(ID);
    }

    /// <summary>
    /// Retrieves a topic record by ID>
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
    public DataTable GetExampleTabel()
    {
        return topicDL.GetExampleTable();
    }
}