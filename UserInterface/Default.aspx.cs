﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class UserInterface_Default : System.Web.UI.Page
{

    #region Properties
    int UserID { get; set; }
    #endregion

    static List<int> LessonIDs { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if(Session["UserID"] == null)
        {
            Response.Redirect("Login.aspx");  
        }
        else
        {
                lblTitle.Text = "Welcome to SQL Learning Platform";
                UserID = Convert.ToInt32(Session["UserID"]);

              BindSideMenu();

            if (!IsPostBack)
            {
                CalculateScore(UserID);
            }     
        }
    }


    private void BindSideMenu()
    {
        try
        {
            LessonBL lesson = new LessonBL();

            // Get all lessons available in table "Lesson"
            DataTable table = lesson.GetLessons();

            UserLessonBL userLesson = new UserLessonBL();
            DataTable userLessonTable = null;

            // If records are present in table Lesson.
            if (table.Rows.Count > 0)
            {
                // For every record.
                foreach (DataRow dr in table.Rows)
                {
                    // Create a list item.
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                 
                    // Get the lesson ID from the record.
                    int lessonID = Convert.ToInt32(dr["ID"]);

                    LinkButton linkB = new LinkButton();

                    // Check if a record is present in table User_lesson.
                    userLessonTable = userLesson.GetRecord(UserID, lessonID);

                    // If a record is present in User_lesson table mark the lesson as complete.
                    if (userLessonTable.Rows.Count == 0)
                    {
                        linkB.Text = "<img src=http://localhost:3787/image/c1.jpg> " + dr[1].ToString();
                    }
                     else if (userLessonTable.Rows[0].Field<DateTime?>("DateCompleted") == null)
                    {
                        linkB.Text = "<img src=http://localhost:3787/image/c1.jpg> " + dr[1].ToString();
                    }
                    else
                    {
                        linkB.Text = "<img src=http://localhost:3787/image/c2.jpg> " + dr[1].ToString();
                    }

                    // Add the list item to the unsorted list.
                    linkB.Attributes.Add("runat", "server");
                    linkB.CommandArgument = lessonID.ToString();
                    //  linkB.Attributes.Add("onclick", "lessonRedirect("+ lessonID + ")");

                    linkB.Click += new EventHandler(Redirect);
                    listItem.Controls.Add(linkB);

                    // Add the list item to the unsorted list.
                    navSideMenu.Controls.Add(listItem);
                }
            }

        }
        catch (SqlException ex)
        {
            Response.Redirect("ErrorPage.aspx?Error ="+ex.Message);
        }
    }

    protected void Redirect(object sender, EventArgs e)
    {
        Response.Redirect("lesson.aspx?ID=" + ((LinkButton)(sender)).CommandArgument);
    }

    /// <summary>
    /// Calculates the score obtained in the final exam.
    /// </summary>
    /// <param name="userID"></param>
    private void CalculateScore(int userID)
    {
        try
        {
            UserBL userBL = new UserBL();
            DataTable user = userBL.GetUser(userID);

            // If user has taken the exam there will be a number of correct answers.
            if (user.Rows[0].Field<int?>("Correct_Answers") != null)
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
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx?Error =" + ex.Message);
        }
        }
    }
