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

    #region Events
    protected void Page_Init(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            // Build the question pages.
            CreateViews();

            PopulateMultiview();
                
            // set view on first question.
            mvQuestions.ActiveViewIndex = 0;
        }

        PopulateMultiview();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if user has authorization.
        if (!Context.User.Identity.IsAuthenticated && Context.Session["UserID"] != null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            StopWatch = new Stopwatch();
            StopWatch.Start();
        }
    }

    /// <summary>
    /// Called on Submit button click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Click(object sender, EventArgs e)
    {
        // Reference the total number of questions.
        int totalQuestions = mvQuestions.Views.Count;

        // If there the index is less than total questions less 1, move to next question.
        if(mvQuestions.ActiveViewIndex < totalQuestions - 1)
        {
            mvQuestions.ActiveViewIndex = mvQuestions.ActiveViewIndex + 1;
        }
        // If at last question.
        else if(mvQuestions.ActiveViewIndex == totalQuestions - 1)
        {
            // Stop time and record values
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
    #endregion

    #region Private methods
    /// <summary>
    /// Retrieves all questions.
    /// </summary>
    /// <returns></returns>
    private DataTable GetQuestions()
    {
      
        QuestionBL questionBL = new QuestionBL();
        return questionBL.GetAllQuestions();       
   
    }

    /// <summary>
    /// Retrieves the answers of a question.
    /// </summary>
    /// <param name="questionID"></param>
    /// <returns></returns>
    private DataTable GetAnswers(int questionID)
    {

            AnswerBL answerBL = new AnswerBL();
            return answerBL.GetAnswersByQuestionID(questionID);

    }


    /// <summary>
    /// Creates the views.
    /// </summary>
    private void CreateViews()
    {
            // Retrieve questions from datatbase.
            DataTable questions = GetQuestions();

            // Decalre a list of type View.
            List<View> views = new List<View>();
            // Create a question number holder/counter.
            int totalQuestions = 0;

            foreach (DataRow row in questions.Rows)
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

                // Create a View and store  controls in its control list.
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

                // Add view in list of views.
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
    #endregion

}