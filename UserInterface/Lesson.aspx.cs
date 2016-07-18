using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class UserInterface_Lesson : System.Web.UI.Page
{

    #region Properties
    public int LessonID { get; set; }
    #endregion
    public int UserID { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                UserID = Convert.ToInt32(Session["UserID"]);
              
                    Validation();

                    LessonID = Convert.ToInt32(Request.QueryString["ID"]);         
                    
                    BindNavMenu(LessonID, UserID);

                    LessonBL lessonBL = new LessonBL();

                    DataTable table = lessonBL.GetLesson(LessonID);

                    if (table.Rows.Count > 0)
                    {
                        lblLessonTitle.Text = table.Rows[0].Field<string>("Title");
                        lblLessonContent.Text = table.Rows[0].Field<string>("Description");
                    }
                
               
                CalculateScore();
           
            }
        }
        catch(Exception ex)
        {
            lblError.Text = ex.Message;
        }
        
    }

    /// <summary>
    /// Binds values to Topic navigation bar.
    /// </summary>
    public void BindNavMenu(int lessonID, int userID)
    {
        try
        {
            TopicBL topics = new TopicBL();
           
            // Get all topics of this lesson.
            DataTable lessonTopics = topics.GetTopicsByLessonID(lessonID);

            // Filter for completed topics by the user.
            UserTopicBL userTopicBL = new UserTopicBL();


            DataTable userTopicRecord = null;

            foreach (DataRow row in lessonTopics.Rows)
            {
                HtmlGenericControl listItemTitle = new HtmlGenericControl("li");

                int topicID = row.Field<int>("ID");
                userTopicRecord = userTopicBL.GetRecord(UserID, topicID);

                // If record not present insert a new one.
                if(userTopicRecord.Rows.Count < 1)
                {
                    userTopicBL.InsertRecord(UserID, topicID);
                    userTopicRecord = userTopicBL.GetRecord(UserID, topicID);
                }

                LinkButton linkButton = new LinkButton();

                // Get completion date value.
                DateTime? date = userTopicRecord.Rows[0].Field<DateTime?>("DateCompleted");

                // If topic was not complete show white very good sign.
                if (date == null)
                {
                    linkButton.Text = "<img src=http://localhost:3787/image/c1.jpg> " + row.Field<string>("Title").ToString();
                }
                // else show green very good sign.
                else
                {
                    linkButton.Text = "<img src=http://localhost:3787/image/c2.jpg> " + row.Field<string>("Title").ToString();
                }
                linkButton.Attributes.Add("runat", "server");
                linkButton.Attributes.Add("onclick", "topicRedirect("+ row.Field<int>("ID").ToString() + ")");
                listItemTitle.Controls.Add(linkButton);

                navSideMenu.Controls.Add(listItemTitle);
            }


        }
        catch(SqlException ex)
        {

        }
    }

    /// <summary>
    /// Validate that a lesson ID was passed.
    /// </summary>
    private void Validation()
    {
        bool valid = true;
        int output = 0;

        if(Request.QueryString["ID"] == null)
        {
            valid = false;
        }

        if(!int.TryParse(Request.QueryString["ID"], out output))
        {
            valid = false;
        }

        if(!valid)
        {
            throw new CustomException(ErrorMessage.GetErrorDesc(1));
        }
    }

    private void CalculateScore()
    {
        UserLessonBL userLessonBL = new UserLessonBL();
        DataTable record = userLessonBL.GetRecord(UserID, LessonID);
        int? correctAnswer = record.Rows[0].Field<int?>("Correct_Answer");
        int? incorrectAnswer = record.Rows[0].Field<int?>("Incorrect_Answer");


        if (correctAnswer != null && incorrectAnswer != null)
        {
            int totalQuestions = (int)(correctAnswer) + (int)(incorrectAnswer);
            int correctAnswerToInt = (int)(correctAnswer);

            int percentComplete = (int)Math.Round((double)(100 * correctAnswerToInt) / totalQuestions);
            TimeSpan timeTaken = record.Rows[0].Field<TimeSpan>("Quiz_Time");
            int hr = timeTaken.Hours;
            int min = timeTaken.Minutes;
            int sec = timeTaken.Seconds;

           string MarkColor = "color:blue;";

            if(percentComplete < 50)
            {
                MarkColor = "color:red;";
            }
            

             lblMark.Text = "Best Score was: <span style = "+ MarkColor+">" +  percentComplete.ToString() + "%" +"</span>" + "</br>Time taken: "+hr + ":" + min + ":" + sec;
        }
    }

    protected void btnQuiz_Click(object sender, EventArgs e)
    {
        Response.Redirect("Quiz.aspx?ID="+LessonID);
    }
}