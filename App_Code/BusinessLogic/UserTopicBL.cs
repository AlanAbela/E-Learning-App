using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserTopicBL
/// </summary>
public class UserTopicBL
{
    UserTopicDL userTopicDL;

    public UserTopicBL()
    {
        userTopicDL = new UserTopicDL();
    }

    /// <summary>
    /// Inserts a new record.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="topicID"></param>
    public void InsertRecord(int userID, int topicID)
    {
        userTopicDL.InsertRecord(userID, topicID);
    }

    public DataTable GetCompleteTopics(int userID)
    {
        return userTopicDL.GetCompleteTopics(userID);
    }
}