using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AnswerBL
/// </summary>
public class AnswerBL
{

    #region Properties
    public AnswerDL answerDL {get; set;}
    #endregion

    #region Constructors
    public AnswerBL()
    {
        answerDL = new AnswerDL();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Get all options related to the specified question.
    /// </summary>
    /// <param name="questionID"></param>
    /// <returns></returns>
    public DataTable GetAnswersByQuestionID(int questionID)
    {
        return answerDL.GetAnswersByQuestionID(questionID);
    }
    #endregion
}