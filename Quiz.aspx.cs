﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserInterface_Quiz : System.Web.UI.Page
{
    public int LessonID { get; set; }

    public int UserID { get; set; }

    public DataTable Questions { get; set; }

    public static Stopwatch StopWatch { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if user has authorization.
        if (!Context.User.Identity.IsAuthenticated && Context.Session["UserID"] != null)
        {
            Response.Redirect("Login.aspx");
        }
        
            if (!IsPostBack)
            {

                // Validate Query string value.
                Validation();

                LessonID = Convert.ToInt32(Request.QueryString["ID"]);
                ViewState["lessonID"] = LessonID;

                UserID = Convert.ToInt32(Context.Session["UserID"]);

                StopWatch = new Stopwatch();
                StopWatch.Start();

                // Get the questions related to this lesson.
                QuestionBL questionBL = new QuestionBL();
                
                // Get the list of questions.               
                Questions = questionBL.GetQuestionsByLessonID(LessonID);

                // Bind questions to views.
                BindQuestions();    

            }
            else
            {

                LessonID = Convert.ToInt32(ViewState["lessonID"]);
                UserID = Convert.ToInt32(Context.Session["UserID"]);

                // Store selections made.
                StoreSelections();

            }

    }

    /// <summary>
    /// Called on Submit button click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
            reqField.IsValid = false;

            // If viewing the pre last question.
            if (mvQuestions.ActiveViewIndex == mvQuestions.Views.Count - 2)
            {
                // Move to last view.
                mvQuestions.ActiveViewIndex = mvQuestions.ActiveViewIndex + 1;
                reqField.IsValid = true;
                btnSubmit.Visible = false;

                // Declare a datatable to bind to the result grid view.
                DataTable table = new DataTable();

                table.Columns.Add("Question", typeof(string));
                table.Columns.Add("Your Answer", typeof(string));
                table.Columns.Add("Correct?", typeof(string));
                table.Columns.Add("TopicID", typeof(int));
                table.Columns.Add("Recomended topic review");

                // Stop time.
                StopWatch.Stop();

                string value = string.Empty;
                int correct = 0;
                int incorrect = 0;

                // Translate boolean to values.
                for (int i = 0; i < mvQuestions.Views.Count - 1; i++)
                {
                    value = "chkQuizList" + i + "Value";
                    if (((string)(ViewState[value])).Equals("True"))
                    {
                        ViewState[value] = "Yes";
                        correct = correct + 1;
                    }
                    else
                    {
                        ViewState[value] = "No";
                        incorrect = incorrect + 1;
                    }

                    // Get the question.
                    Label label = (Label)(mvQuestions.Views[i].FindControl("lblViewQ" + i));

                    // If question text contains these html characters, remove them
                    if (label.Text.Contains("</br></br>"))
                    {
                        label.Text = label.Text.Replace("</br></br>", " ");
                    }

                    if (label.Text.Contains("</br>"))
                    {
                        label.Text = label.Text.Replace("</br>", " ");
                    }

                    table.Rows.Add(label.Text, ViewState["chkQuizList" + i + "Text"].ToString(), ViewState["chkQuizList" + i + "Value"].ToString(), ViewState["Q" + i].ToString());
                }

                // Bind to gridview.
                gvResult.DataSource = table;
                gvResult.DataBind();


                UserLessonBL userLessonBL = new UserLessonBL();
                DataTable record = userLessonBL.GetRecord(UserID, LessonID);
                bool betterScore = false;

                // Store number of correct and incorrect questions.
                int? previousCorrect = record.Rows[0].Field<int?>("Correct_answer");
                if (previousCorrect != null && previousCorrect < correct)
                {
                    lblNotification.Text += "Congratulations you obtained a better score";
                    userLessonBL.InsertMark(UserID, LessonID, correct, incorrect);
                    betterScore = true;
                }
                else if (previousCorrect == null)
                {
                    userLessonBL.InsertMark(UserID, LessonID, correct, incorrect);
                    betterScore = true;
                }

                // Show score.
                int percentCorrect = (int)Math.Round((double)(100 * correct) / (correct + incorrect));
                lblScore.Text = percentCorrect + "% Correct";

                // If time is null or better and score is better, record it.
                TimeSpan? quizTime = record.Rows[0].Field<TimeSpan?>("Quiz_Time");
                if (quizTime != null)
                {
                    int hr = ((TimeSpan)(quizTime)).Hours;
                    int min = ((TimeSpan)(quizTime)).Minutes;
                    int sec = ((TimeSpan)(quizTime)).Seconds;

                    bool isBetterTime = false;

                    if (StopWatch.Elapsed.Hours < hr)
                    {
                        if (betterScore)
                        {
                            userLessonBL.InsertQuizTime(UserID, LessonID, StopWatch.Elapsed);
                        }
                        isBetterTime = true;
                    }
                    else if (StopWatch.Elapsed.Hours == hr && StopWatch.Elapsed.Minutes < min)
                    {
                        if (betterScore)
                        {
                            userLessonBL.InsertQuizTime(UserID, LessonID, StopWatch.Elapsed);
                        }
                        isBetterTime = true;
                    }
                    else if (StopWatch.Elapsed.Hours == hr && StopWatch.Elapsed.Minutes == min && StopWatch.Elapsed.Seconds < sec)
                    {
                        if (betterScore)
                        {
                            userLessonBL.InsertQuizTime(UserID, LessonID, StopWatch.Elapsed);
                        }
                        isBetterTime = true;
                    }

                    if (isBetterTime)
                    {
                        if (lblNotification.Text == "")
                        {
                            lblNotification.Text += "You achieved a better time.";
                        }
                        else
                        {
                            lblNotification.Text += ". and achieved a better time.";
                        }
                    }
                }
                // If null time is present, record the time.
                else
                {
                    userLessonBL.InsertQuizTime(UserID, LessonID, StopWatch.Elapsed);
                }
            }

            // If not pre-least question.
            if (mvQuestions.ActiveViewIndex < 4)
            {
                // Move to next question.
                mvQuestions.ActiveViewIndex = mvQuestions.ActiveViewIndex + 1;

                // If an option selection was made, set control to validate.
                if (reqField.IsValid == false)
                {
                    if (mvQuestions.ActiveViewIndex == 1)
                    {
                        reqField.ControlToValidate = "chkQuizList1";
                        reqField.IsValid = true;
                    }

                    if (mvQuestions.ActiveViewIndex == 2)
                    {
                        reqField.ControlToValidate = "chkQuizList2";
                        reqField.IsValid = true;
                    }

                    if (mvQuestions.ActiveViewIndex == 3)
                    {
                        reqField.ControlToValidate = "chkQuizList3";
                        reqField.IsValid = true;
                    }

                    if (mvQuestions.ActiveViewIndex == 4)
                    {
                        reqField.ControlToValidate = "chkQuizList4";
                        reqField.IsValid = true;
                    }
                }
            }
    }

    #region Privare methods
    /// <summary>
    /// Validates the lesson ID.
    /// </summary>
    private void Validation()
    {
        bool valid = true;
        int output = 0;

        if (Request.QueryString["ID"] == null)
        {
            valid = false;
        }

        if (!int.TryParse(Request.QueryString["ID"], out output))
        {
            valid = false;
        }

        if (!valid)
        {
            throw new Exception(ErrorMessage.GetErrorDesc(2));
        }
    }

    /// <summary>
    /// Store user selections in viewstates.
    /// </summary>
    private void StoreSelections()
    {   

        if(chkQuizList0.SelectedItem != null)
        {
            ViewState["chkQuizList0Text"] = chkQuizList0.SelectedItem.Text;
            ViewState["chkQuizList0Value"] = chkQuizList0.SelectedItem.Value;
            
        }

        if (chkQuizList1.SelectedItem != null)
        {
            ViewState["chkQuizList1Text"] = chkQuizList1.SelectedItem.Text;
            ViewState["chkQuizList1Value"] = chkQuizList1.SelectedItem.Value;
         
        }

        if (chkQuizList2.SelectedItem != null)
        {
            ViewState["chkQuizList2Text"] = chkQuizList2.SelectedItem.Text;
            ViewState["chkQuizList2Value"] = chkQuizList2.SelectedItem.Value;
          
        }

        if (chkQuizList3.SelectedItem != null)
        {
            ViewState["chkQuizList3Text"] = chkQuizList3.SelectedItem.Text;
            ViewState["chkQuizList3Value"] = chkQuizList3.SelectedItem.Value;
       
        }

        if (chkQuizList4.SelectedItem != null)
        {
            ViewState["chkQuizList4Text"] = chkQuizList4.SelectedItem.Text;
            ViewState["chkQuizList4Value"] = chkQuizList4.SelectedItem.Value;

        }
    }

    /// <summary>
    /// Binds data to the page.
    /// </summary>
    /// <param name="topicID"></param>
    private void BindQuestions()
    {
            LessonBL lessonBL = new LessonBL();
            DataTable table = lessonBL.GetLesson(LessonID);

            lblLessonTitle.Text = table.Rows[0].Field<string>("Title");


            AnswerBL answerBL = new AnswerBL();
            

            // Check that the number of questions are 5 before binding to labels.
            if (Questions.Rows.Count == 5)
            {

                lblViewQ0.Text = Questions.Rows[0].Field<string>("Question");
                ViewState["Q0"] = Questions.Rows[0].Field<int>("TopicID");

                // Get the question answer and distractors.
                chkQuizList0.DataSource = answerBL.GetAnswersByQuestionID(Questions.Rows[0].Field<int>("QuestionID"));
                chkQuizList0.DataTextField = "Text";
                chkQuizList0.DataValueField = "Correct";
                chkQuizList0.DataBind();

                lblViewQ1.Text = Questions.Rows[1].Field<string>("Question");
                ViewState["Q1"] = Questions.Rows[1].Field<int>("TopicID");
                chkQuizList1.DataSource = answerBL.GetAnswersByQuestionID(Questions.Rows[1].Field<int>("QuestionID"));
                chkQuizList1.DataTextField = "Text";
                chkQuizList1.DataValueField = "Correct";
                chkQuizList1.DataBind();

                lblViewQ2.Text = Questions.Rows[2].Field<string>("Question");
                ViewState["Q2"] = Questions.Rows[2].Field<int>("TopicID");
                chkQuizList2.DataSource = answerBL.GetAnswersByQuestionID(Questions.Rows[2].Field<int>("QuestionID"));
                chkQuizList2.DataTextField = "Text";
                chkQuizList2.DataValueField = "Correct";
                chkQuizList2.DataBind();

                lblViewQ3.Text = Questions.Rows[3].Field<string>("Question");
                ViewState["Q3"] = Questions.Rows[3].Field<int>("TopicID");
                chkQuizList3.DataSource = answerBL.GetAnswersByQuestionID(Questions.Rows[3].Field<int>("QuestionID"));
                chkQuizList3.DataTextField = "Text";
                chkQuizList3.DataValueField = "Correct";
                chkQuizList3.DataBind();

                lblViewQ4.Text = Questions.Rows[4].Field<string>("Question");
                ViewState["Q4"] = Questions.Rows[4].Field<int>("TopicID");
                chkQuizList4.DataSource = answerBL.GetAnswersByQuestionID(Questions.Rows[4].Field<int>("QuestionID"));
                chkQuizList4.DataTextField = "Text";
                chkQuizList4.DataValueField = "Correct";
                chkQuizList4.DataBind();
            }    

    }

    #endregion

    protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
            e.Row.Cells[3].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               // If option selected is the wrong answer.
                if (e.Row.Cells[2].Text.Equals("No"))
                {
                // Declare a Hyper link 
                    HyperLink the_url = new HyperLink();
                // Pass the related topic url to the hyperlink.
                    the_url.NavigateUrl = "Topic.aspx?ID=" + e.Row.Cells[3].Text + "&lessonid=" + ViewState["lessonID"].ToString();

                    TopicBL topicBL = new TopicBL();
                // Retrieve topic data.
                    DataTable topic = topicBL.GetTopicByID(Convert.ToInt32(e.Row.Cells[3].Text));
                // Pass topic title to URL.
                    if (topic.Rows.Count > 0)
                    {
                        the_url.Text = topic.Rows[0].Field<string>("Title");
                    }
                    // Add URL to grid view.
                    e.Row.Cells[4].Controls.Add(the_url);
                }
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

            if(hour < 10)
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

}