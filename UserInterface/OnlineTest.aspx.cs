using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserInterface_OnlineTest : System.Web.UI.Page
{

    #region Properties
    public static Stopwatch StopWatch { get; set; }

    public static List<View> Views { get; set; }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            CreateViews();
            PopulateMultiview();
            mvQuestions.ActiveViewIndex = 0;
        }

        PopulateMultiview();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {
            StopWatch = new Stopwatch();
            StopWatch.Start();
//
           //  CreateViews();
            // PopulateMultiview();
        }

     //   PopulateMultiview();
    }

    /// <summary>
    /// Called on button click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Click(object sender, EventArgs e)
    {
        int totalQuestions = mvQuestions.Views.Count;

        if(mvQuestions.ActiveViewIndex < totalQuestions - 1)
        {
            mvQuestions.ActiveViewIndex = mvQuestions.ActiveViewIndex + 1;
        }
        else if(mvQuestions.ActiveViewIndex == totalQuestions - 1)
        {
            StopWatch.Stop();
            int[] time = new int[3];
            time[0] = StopWatch.Elapsed.Hours;
            time[1] = StopWatch.Elapsed.Minutes;
            time[2] = StopWatch.Elapsed.Seconds;

            Session["time"] = time;
            Session["views"] = Views;

            Response.Redirect("Result.aspx");
        }
            
        
    }

    /// <summary>
    /// Called by timer to measure time.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Timer_Tick(object sender, EventArgs e)
    {

        long sec = StopWatch.Elapsed.Seconds;
        long min = StopWatch.Elapsed.Minutes;
        long hour = StopWatch.Elapsed.Hours;

        if (hour < 10)
        {
            lblTime.Text = "Time: 0" + hour;
        }
        else
        {
            lblTime.Text = "Time: " + hour.ToString();
        }
        lblTime.Text += ":";

        if (min < 10)
        {
            lblTime.Text += "0" + min;
        }
        else
        {
            lblTime.Text += min.ToString();
        }

        lblTime.Text += ":";

        if (sec < 10)
        {
            lblTime.Text += "0" + sec;
        }
        else
        {
            lblTime.Text += sec.ToString();
        }

    }

    /// <summary>
    /// Retrieves all questions.
    /// </summary>
    /// <returns></returns>
    private DataTable GetQuestions()
    {
        try
        { 
        QuestionBL questionBL = new QuestionBL();
        return questionBL.GetAllQuestions();
        }
        catch(SqlException ex)
        {
            Response.Redirect("ErrorPage.aspx?error="+ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Retrieves the answers of a question.
    /// </summary>
    /// <param name="questionID"></param>
    /// <returns></returns>
    private DataTable GetAnswers(int questionID)
    {
        try
        {
            AnswerBL answerBL = new AnswerBL();
            return answerBL.GetAnswersByQuestionID(questionID);
        }
        catch(SqlException ex)
        {
            Response.Redirect("ErrorPage.aspx?error=" + ex.Message);
            return null;
        }
    }


    /// <summary>
    /// Creates the views.
    /// </summary>
    private void CreateViews()
    {
        DataTable questions = GetQuestions();
        List<View> views = new List<View>();
        int totalQuestions = 0;

        foreach(DataRow row in questions.Rows )
        {
            totalQuestions++;

            //Create a Hidden Field to store the Lesson ID
            HiddenField hiddenField = new HiddenField();
            hiddenField.Value = row.Field<int>("LessonID").ToString();
            hiddenField.ID = "hiddenField" + totalQuestions;

            //Create a Hidden Field to store the Topic ID
            HiddenField hiddenFieldTID = new HiddenField();
            hiddenFieldTID.Value = row.Field<int>("TopicID").ToString();
            hiddenFieldTID.ID = "hiddenFieldTID" + totalQuestions;

            // Create label for question number reference.
            Label lblQuestionNo = new Label();
            lblQuestionNo.Text = "Question " + totalQuestions + " of " + questions.Rows.Count;
            lblQuestionNo.ID = "lblQuestionNo" + totalQuestions;

            // Create label to store question.
            Label lblQuestion = new Label();
            lblQuestion.Text = row.Field<string>("Question");
            lblQuestion.ID = "lblQuestion" + totalQuestions;

            // Create the list of answers.
            RadioButtonList radioButtonList = new RadioButtonList();
            radioButtonList.ID = "radioButtonList" + totalQuestions;
            radioButtonList.DataSource = GetAnswers(row.Field<int>("ID"));
            radioButtonList.DataTextField = "Text";
            radioButtonList.DataValueField = "Correct";
            radioButtonList.DataBind();

            // Create a View to store the above components.
            View view = new View();
            view.Controls.Add(hiddenField);
            view.Controls.Add(hiddenFieldTID);
            view.Controls.Add(new LiteralControl("<h2>"));
            view.Controls.Add(lblQuestionNo);
            view.Controls.Add(new LiteralControl("</h2>"));
            view.Controls.Add(new LiteralControl("</br>"));

            view.Controls.Add(new LiteralControl("<h3>"));
            view.Controls.Add(lblQuestion);
            view.Controls.Add(new LiteralControl("</h3>"));
            view.Controls.Add(new LiteralControl("</br>"));

            view.Controls.Add(new LiteralControl("<div class='check-list-container'>"));
            view.Controls.Add(radioButtonList);
            view.Controls.Add(new LiteralControl("</div>"));

            // Store view in list of views.
            views.Add(view);
          
        }
        
        Views = views;  

    }

    /// <summary>
    /// Populates the multi view.
    /// </summary>
    private void PopulateMultiview()
    {

        foreach (View view in Views)
        {
            mvQuestions.Views.Add(view);
        }

    }

   
}