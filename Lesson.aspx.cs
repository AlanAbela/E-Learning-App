using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Lesson : System.Web.UI.Page
{

    #region Properties
    public int LessonID { get; set; }
    #endregion
    public int UserID { get; set; }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        // If the user is not logged in redirect to login page.
            if (!Context.User.Identity.IsAuthenticated && Context.Session["UserID"] != null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                UserID = Convert.ToInt32(Context.Session["UserID"]);
              
            // Validate query strings.
                    Validation();
            // Reference Lesson ID.
                    LessonID = Convert.ToInt32(Request.QueryString["ID"]);

            if (!IsPostBack)
            {
                // Insert record if it doesn't exist.
                UserLessonBL userLessonBL  = new UserLessonBL();
                userLessonBL.InserNewRecord(UserID, LessonID);
            }

            // Create navigation menu.
            BindNavMenu(LessonID, UserID);                

            // Get lesson data.
                DataTable lesson = GetLesson(LessonID);

            // If lesson data is not equal to null.
                    if (lesson.Rows.Count > 0)
                    {
                        lblLessonTitle.Text = lesson.Rows[0].Field<string>("Title");
                        lblLessonContent.Text = lesson.Rows[0].Field<string>("Description");
                    }
                
               // If final test was completed at least once, retrieve and calculate final test score. 
                CalculateScore();   
        }
    }

    /// <summary>
    /// Called on quiz button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuiz_Click(object sender, EventArgs e)
    {
        Response.Redirect("Quiz.aspx?ID=" + LessonID);
    }

    /// <summary>
    /// Redirects to selected topic.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TopicRedirect(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)(sender);
        string topicID = linkButton.CommandArgument;

        Response.Redirect("Topic.aspx?ID=" + topicID + "&lessonid= " + LessonID);
    }

    /// <summary>
    /// Called on back button click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    #endregion

    #region Public methods
    /// <summary>
    /// Binds values to Topic navigation bar.
    /// </summary>
    public void BindNavMenu(int lessonID, int userID)
    {
            TopicBL topics = new TopicBL();
           
            // Get all topics of this lesson.
            DataTable lessonTopics = topics.GetTopicsByLessonID(lessonID);

            // Filter for completed topics by the user.
            UserTopicBL userTopicBL = new UserTopicBL();


            DataTable userTopicRecord = null;

            foreach (DataRow row in lessonTopics.Rows)
            {
            // Create a list item.
                HtmlGenericControl listItemTitle = new HtmlGenericControl("li");

            // Retrieve UserTopic record by user ID
                int topicID = row.Field<int>("ID");
                userTopicRecord = userTopicBL.GetRecord(UserID, topicID);

                // If record not present insert a new one, linking user with topic.
                if(userTopicRecord.Rows.Count < 1)
                {
                    userTopicBL.InsertRecord(UserID, topicID);
                    userTopicRecord = userTopicBL.GetRecord(UserID, topicID);
                }

                // Declare a link button.
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
                linkButton.CommandArgument = topicID.ToString();
                linkButton.Click += new EventHandler(TopicRedirect);

            // Add link button to list item's controls.
                listItemTitle.Controls.Add(linkButton);

            // Add list item to unsorlted list controls.
                navSideMenu.Controls.Add(listItemTitle);
            }
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Validate that a lesson ID was passed via a query string.
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
            Response.Redirect("Error.aspx");
        }
    }

    /// <summary>
    /// Calculate last best quiz score.
    /// </summary>
    private void CalculateScore()
    {
            // Declare a UserLessonBL object.
            UserLessonBL userLessonBL = new UserLessonBL();
           // Get UserLesson data.
            DataTable record = userLessonBL.GetRecord(UserID, LessonID);
          // Reference number of correct and incorrect answers.
            int? correctAnswer = record.Rows[0].Field<int?>("Correct_Answer");
            int? incorrectAnswer = record.Rows[0].Field<int?>("Incorrect_Answer");

            if (correctAnswer != null && incorrectAnswer != null)
            {
                // Reference total questions.
                int totalQuestions = (int)(correctAnswer) + (int)(incorrectAnswer);
                // Down cast correct answer.
                int correctAnswerToInt = (int)(correctAnswer);
                // Calculate percentage of correct answers.
                int percentComplete = (int)Math.Round((double)(100 * correctAnswerToInt) / totalQuestions);
                // Reference time take of the last best result quiz.
                TimeSpan timeTaken = record.Rows[0].Field<TimeSpan>("Quiz_Time");
                // Reference time units.
                int hr = timeTaken.Hours;
                int min = timeTaken.Minutes;
                int sec = timeTaken.Seconds;

                string MarkColor = "color:blue;";

                if (percentComplete < 50)
                {
                    MarkColor = "color:red;";
                }

                lblMark.Text = "Best Score was: <span style = " + MarkColor + ">" + percentComplete.ToString() + "%" + "</span>" + "</br>Time taken: " + hr + ":" + min + ":" + sec;
            }
      
    }

    /// <summary>
    /// Retrieve Lesson data by lesson ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    private DataTable GetLesson(int ID)
    {
            LessonBL lessonBL = new LessonBL();
            DataTable table = lessonBL.GetLesson(LessonID);
            return table;  
    }
    #endregion

  

   


}