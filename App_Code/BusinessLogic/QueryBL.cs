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
    public QueryBL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Get the query associated with the defined topic.
    /// </summary>
    /// <param name="topicID"></param>
    /// <returns></returns>
    public DataTable GetQueryResult(int topicID)
    {
        QueryDL queryDL = new QueryDL();
        return queryDL.GetResultByQuery(topicID);
    }

    /// <summary>
    /// Processes the defined query.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public void ProcessQuery(string query)
    {
        QueryDL queryDL = new QueryDL();
        queryDL.ProcessQuery(query);
    }
}