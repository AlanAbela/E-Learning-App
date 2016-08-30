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
    #region Properties
    public UserTopicDL userTopicDL {get; set;}
     #endregion

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
    /// Set date when the topic is completed.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="topicID"></param>
    public void SetCompleteDate(int userID, int topicID)
    {
        userTopicDL.SetCompleteDate(userID, topicID);
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