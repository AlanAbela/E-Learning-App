using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Query
/// </summary>
public class QueryBL
{
    #region Properties
    QueryDL queryDL { get; set;}
    #endregion

    public QueryBL()
    {
        queryDL = new QueryDL();
    }

#region Methods
    /// <summary>
    /// Get the query associated with the defined topic.
    /// </summary>
    /// <param name="topicID"></param>
    /// <returns></returns>
    public DataTable GetQueryResult(int topicID)
    {
        return queryDL.GetResultByQuery(topicID);
    }

    /// <summary>
    /// Processes the defined query.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public DataTable ProcessQuery(string query)
    {
       return queryDL.ProcessQuery(query);
    }
    #endregion
}