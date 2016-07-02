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


    /// <summary>
    /// Retrieves all records by user ID.
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public DataTable GetCompleteTopics(int userID)
    {
        return userTopicDL.GetCompleteTopics(userID);
    }

    /// <summary>
    /// Retrieves a record by user ID and topic ID.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="topicID"></param>
    /// <returns></returns>
    public DataTable GetRecord(int userID, int topicID)
    {
        return userTopicDL.GetRecord(userID, topicID);
    }
}