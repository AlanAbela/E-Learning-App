using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{

    #region Properties
    int UserID { get; set; }
    static List<int> LessonIDs { get; set; }
    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
          // Pass title string to title label.
             lblTitle.Text = "Welcome to SQL Learning Platform";

        // Reference the user ID.
             UserID = Convert.ToInt32(Context.Session["UserID"]);

        // Call method to populate side menu.
              BindSideMenu();

        // If not post back calculate final test score.
            if (!IsPostBack)
            {
                CalculateScore(UserID);
            }     
    }
 
    /// <summary>
    /// Redirect to Lesson.aspx
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Redirect(object sender, EventArgs e)
    {
        Response.Redirect("lesson.aspx?ID=" + ((LinkButton)(sender)).CommandArgument);
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Create side navigation menu.
    /// </summary>
    private void BindSideMenu()
    {
        // Create an instance of lesson business logic.
        LessonBL lesson = new LessonBL();

        // Get all lessons available in table "Lesson"
        DataTable lessons = lesson.GetLessons();

        // Create an instance of UserLesson business logic.
        UserLessonBL userLesson = new UserLessonBL();

        DataTable userLessonTable = null;

        // If records are present in table Lesson.
        if (lessons.Rows.Count > 0)
        {
            // Loop through the lessons rows.
            foreach (DataRow row in lessons.Rows)
            {
                // Create a list item.
                HtmlGenericControl listItem = new HtmlGenericControl("li");

                // Create a link Button.
                LinkButton linkB = new LinkButton();

                // Get the lesson ID from the record.
                int lessonID = Convert.ToInt32(row["ID"]);             

                // Get the record from "User_Lesson" table that contains the user ID and Lesson ID
                userLessonTable = userLesson.GetRecord(UserID, lessonID);

                // If the record is blank, means that the lesson is not complete by the user, so put a white very good image.
                if (userLessonTable.Rows.Count == 0)
                {
                    linkB.Text = "<img src=http://localhost:3787/image/c1.jpg> " + row[1].ToString();
                }
                // If the record does not contain a complete date it also means that the lesson is not complete.
                else if (userLessonTable.Rows[0].Field<DateTime?>("DateCompleted") == null)
                {
                    linkB.Text = "<img src=http://localhost:3787/image/c1.jpg> " + row[1].ToString();
                }
                // If there is the record with a complete date mark the link button with a green very good sign.
                else
                {
                    linkB.Text = "<img src=http://localhost:3787/image/c2.jpg> " + row[1].ToString();
                }

               // Add runat server attribute to the link button.
                linkB.Attributes.Add("runat", "server");
                // Pass the lesson ID to the link button.
                linkB.CommandArgument = lessonID.ToString();
                // Add the Redirect event.
                linkB.Click += new EventHandler(Redirect);

                // Add the link button to the list item.
                listItem.Controls.Add(linkB);

                // Add the list item to the unsorted list.
                navSideMenu.Controls.Add(listItem);
            }
        }
    }

    /// <summary>
    /// Calculates the score obtained in the final exam.
    /// </summary>
    /// <param name="userID"></param>
    private void CalculateScore(int userID)
    {
            UserBL userBL = new UserBL();
            DataTable user = userBL.GetUser(userID);

            // If user has taken the exam there will be a number of correct answers.
            if (user.Rows.Count != 0 && user.Rows[0].Field<int?>("Correct_Answers") != null)
            {
                // Get the number of correct answers for this user.
                int? correctAnswer = user.Rows[0].Field<int?>("Correct_Answers");

                // Get the total number of questions
                QuestionBL questionBL = new QuestionBL();
                int totalQuestions = questionBL.GetAllQuestions().Rows.Count;

                // Calcultate the percentage of correct answers.
                int percentComplete = (int)Math.Round((double)(100 * correctAnswer) / totalQuestions);

                // Get the time taken to complete the exam.
                TimeSpan timeTaken = (TimeSpan)(user.Rows[0].Field<TimeSpan?>("Test_Time"));

                // Get the completion date.
                DateTime date = (DateTime)(user.Rows[0].Field<DateTime?>("Test_Completion_Date"));

                // Update the label with the information
                int hr = timeTaken.Hours;
                int min = timeTaken.Minutes;
                int sec = timeTaken.Seconds;

                string MarkColor = "color:blue;";

                if (percentComplete < 50)
                {
                    MarkColor = "color:red;";
                }

                lblMark.Text = "Best Course Exam Score was: <span style = " + MarkColor + ">" + percentComplete.ToString() + "%" + "</span>"
                                + "</br></br> Completed in: " + hr + " hours " + min + " mins " + sec + "sec." +
                                "</br></br> Completed on: " + date.Date.ToString("D");
            }
        }
    #endregion
}
