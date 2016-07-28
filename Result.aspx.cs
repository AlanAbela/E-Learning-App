using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserInterface_Result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Context.Session["UserID"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else if(Context.Session["UserID"] != null)
        {

                int userID = (int)Context.Session["UserID"];
                UserBL userBL = new UserBL();
                DataTable user = userBL.GetUser(userID);

                // Check is sessons are not null.
                if (user.Rows[0].Field<bool>("Course_Complete") && Session["time"] != null && Session["views"] != null)
                {
                    // Get the views.
                    List<View> views = (List<View>)Session["views"];

                    // Get the test time.
                    int[] testTime = (int[])Session["time"];

                    int count = 1;
                    int? correctAnswers = 0;

                    // Declare a datatable to bind to the result grid view.
                    DataTable table = new DataTable();
                    table.Columns.Add("Question", typeof(string));
                    table.Columns.Add("Your Answer", typeof(string));
                    table.Columns.Add("Correct?", typeof(string));
                    table.Columns.Add("LessonID", typeof(string));
                    table.Columns.Add("TopicID", typeof(string));
                    table.Columns.Add("Recomended topic review");

                    foreach (View view in views)
                    {
                        RadioButtonList buttonList = (RadioButtonList)(view.FindControl("radioButtonList" + count));
                        string correctValue = "No";
                        string answer = "no answer was selected";
                        // Get the question.
                        string question = ((Label)(view.FindControl("lblQuestion" + count))).Text;
                        // Get the topicID.
                        string lessonID = ((HiddenField)(view.FindControl("hiddenField" + count))).Value;
                        // Get the topic ID.
                        string topicID = ((HiddenField)(view.FindControl("hiddenFieldTID" + count))).Value;


                        if (buttonList.SelectedItem != null)
                        {
                            // Get the answer
                            answer = buttonList.SelectedItem.Text;

                            // Get the number of correct answers.
                            bool isCorrect = Convert.ToBoolean(buttonList.SelectedItem.Value);
                            if (isCorrect)
                            {
                                // Update correct value if answer is correct.
                                correctValue = "yes";
                                correctAnswers++;
                            }
                        }

                        // Remove HTML break line syntax.
                        if (question.Contains("</br></br>"))
                        {
                            question = question.Replace("</br></br>", " ");
                        }
                        if (question.Contains("</br>"))
                        {
                            question = question.Replace("</br>", " ");
                        }

                        table.Rows.Add(question, answer, correctValue, lessonID, topicID);

                        count++;
                    }
                    gvResult.DataSource = table;
                    gvResult.DataBind();

                    QuestionBL questionBL = new QuestionBL();
                    int totalQuestions = questionBL.GetAllQuestions().Rows.Count;

                    // If the score is better store the details in the database.
                    TimeSpan time = new TimeSpan(testTime[0], testTime[1], testTime[2]);
                    if (user.Rows[0].Field<int?>("Correct_Answers") < correctAnswers)
                    {
                        userBL.InsertCorrectAnswers(userID, correctAnswers);
                        userBL.InsertTestCompleteDate(userID);
                        userBL.InsertTimeTaken(userID, time);
                        GetInfo(user.Rows[0].Field<string>("Username"), totalQuestions, (int)correctAnswers, true, time);
                    }
                    else if (user.Rows[0].Field<int?>("Correct_Answers") == null)
                    {
                        userBL.InsertCorrectAnswers(userID, correctAnswers);
                        userBL.InsertTestCompleteDate(userID);
                        userBL.InsertTimeTaken(userID, time);
                        GetInfo(user.Rows[0].Field<string>("Username"), totalQuestions, (int)correctAnswers, false, time);
                    }
                    else
                    {
                        GetInfo(user.Rows[0].Field<string>("Username"), totalQuestions, (int)correctAnswers, false, time);
                    }

                    // Clear sessions
                    Session["time"] = null;
                    Session["views"] = null;
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
        }

    }

    /// <summary>
    /// Creates links in grid view.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {

            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.Cells[2].Text.Equals("No"))
                {
                    HyperLink the_url = new HyperLink();
                    the_url.NavigateUrl = "Topic.aspx?ID=" + e.Row.Cells[4].Text + "&lessonid=" + e.Row.Cells[3].Text;
                    TopicBL topicBL = new TopicBL();
                    DataTable topic = topicBL.GetTopicByID(Convert.ToInt32(e.Row.Cells[4].Text));
                    if (topic.Rows.Count > 0)
                    {
                        the_url.Text = topic.Rows[0].Field<string>("Title");
                    }

                    e.Row.Cells[5].Controls.Add(the_url);
                }
            }
  
    }

    /// <summary>
    /// Summerizes exam information.
    /// </summary>
    /// <param name="userID"></param>
    private void GetInfo(string userName, int totalQuestion, int correctQuestion, bool surpass, TimeSpan time)
    {
        StringBuilder resultTxt = new StringBuilder(@"@user you answered @correctQuestions correctly out of @totalQuestions
                                                     in @hr hours @min mins @sec secs.");
        if(surpass)
        {
            resultTxt.Append(" Congratulations you surpassed yourself.");
        }

        resultTxt.Replace("@user", userName);
        if(correctQuestion > 1 || correctQuestion < 1)
        {
            resultTxt.Replace("@correctQuestions", correctQuestion.ToString() + " questions");
        }
        else
        {
            resultTxt.Replace("@correctQuestions", correctQuestion.ToString() + " question");
        }
        resultTxt.Replace("@totalQuestions", totalQuestion.ToString());
        resultTxt.Replace("@hr", time.Hours.ToString());
        resultTxt.Replace("@min", time.Minutes.ToString());
        resultTxt.Replace("@sec", time.Seconds.ToString());

        lblResult.Text = resultTxt.ToString();

    }
}