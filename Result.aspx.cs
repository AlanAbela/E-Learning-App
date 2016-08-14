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
                // Reference the user ID.
                int userID = (int)Context.Session["UserID"];

               // Get user record from the database.
                UserBL userBL = new UserBL();
                DataTable user = userBL.GetUser(userID);

                // Check that required data is present.
                if (user.Rows[0].Field<bool>("Course_Complete") && Session["time"] != null && Session["views"] != null)
                {
                    // Reference the views.
                    List<View> views = (List<View>)Session["views"];

                    // Get the test time.
                    int[] testTime = (int[])Session["time"];

                    // Declare an integer to represent controls' ID
                    int controlsID = 1;
                    
                    // Declare a nullable integer to hold the number of correct answers.
                    int? correctAnswers = 0;

                    // Declare a datatable to bind to the result grid view.
                    DataTable table = new DataTable();
                    table.Columns.Add("Question", typeof(string));
                    table.Columns.Add("Your Answer", typeof(string));
                    table.Columns.Add("Correct?", typeof(string));
                    table.Columns.Add("LessonID", typeof(string));
                    table.Columns.Add("TopicID", typeof(string));
                    table.Columns.Add("Recomended topic review");

                    // Iterate the views and get their contents.
                    foreach (View view in views)
                    {
                    // Reference the radio button list control, that is present in the view.
                        RadioButtonList buttonList = (RadioButtonList)(view.FindControl("radioButtonList" + controlsID));

                        string correctValue = "No";
                        string answer = "no answer was selected";

                        // Get the question.
                        string question = ((Label)(view.FindControl("lblQuestion" + controlsID))).Text;
                        // Get the lessonID.
                        string lessonID = ((HiddenField)(view.FindControl("hiddenField" + controlsID))).Value;
                        // Get the topic ID.
                        string topicID = ((HiddenField)(view.FindControl("hiddenFieldTID" + controlsID))).Value;

                        if (buttonList.SelectedItem != null)
                        {
                            // Get the answer
                            answer = buttonList.SelectedItem.Text;
                            
                           // Get question result.
                            bool isCorrect = Convert.ToBoolean(buttonList.SelectedItem.Value);

                            if (isCorrect)
                            {
                                // Update correct value and increment correct answer count.
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

                        // Add values to the table.
                        table.Rows.Add(question, answer, correctValue, lessonID, topicID);

                        // Increment the ID.
                        controlsID++;
                    }

                    // Bind the table to the grid view.
                    gvResult.DataSource = table;
                    gvResult.DataBind();

                    // Get the number of questions present in the database table.
                    QuestionBL questionBL = new QuestionBL();
                    int totalQuestions = questionBL.GetAllQuestions().Rows.Count;

                    // Create a time span from the time int array.
                    TimeSpan time = new TimeSpan(testTime[0], testTime[1], testTime[2]);

                    // If the number of correct answered questions is greater than the previos best result.
                    if (user.Rows[0].Field<int?>("Correct_Answers") < correctAnswers)
                    {
                    // Update the user record with new results.
                        userBL.UpdateCorrectAnswers(userID, correctAnswers);
                        userBL.InsertTestCompleteDate(userID);
                        userBL.InsertTimeTaken(userID, time);
                   // Create message for feedback.
                        GetInfo(user.Rows[0].Field<string>("Username"), totalQuestions, (int)correctAnswers, true, time);
                    }
                    // If taking the test for the first time.
                    else if (user.Rows[0].Field<int?>("Correct_Answers") == null)
                    {
                  // Update the user record.
                        userBL.UpdateCorrectAnswers(userID, correctAnswers);
                        userBL.InsertTestCompleteDate(userID);
                        userBL.InsertTimeTaken(userID, time);
                        GetInfo(user.Rows[0].Field<string>("Username"), totalQuestions, (int)correctAnswers, false, time);
                    }
                    else
                    // Show feedback only.
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
               // If the answer of this row is wrong provide link to topic.
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

        // If score was better than previous time.
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