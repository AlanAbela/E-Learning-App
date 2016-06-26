﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TopicBL
/// </summary>
public class TopicBL
{
    public TopicBL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Retrieves all topic records that are related to the defined lesson.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public DataTable GetTopicsByLessonID(int ID)
    {
        TopicDL topicDL = new TopicDL();
        return topicDL.GetTopicDLsByLessonID(ID);
    }
}