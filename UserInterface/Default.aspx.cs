using System;
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

        if (!IsPostBack)
            
                BindSideMenu();        
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

                    // Check if a record is present in table User_lesson.
                    userLessonTable = userLesson.GetRecord(UserID, lessonID);

                    LinkButton linkB = new LinkButton();
                    // If a record is present in User_lesson table mark the lesson as complete.
                    if (userLessonTable.Rows.Count == 0)
                    {
                        linkB.Text = "<img src=http://localhost:3787/image/c1.jpg> " + dr[1].ToString();
                    }
                    else
                    {
                        linkB.Text = "<img src=http://localhost:3787/image/c2.jpg> " + dr[1].ToString();
                    }

                    // Add the list item to the unsorlted list.
                    linkB.Attributes.Add("runat", "server");
                    linkB.Attributes.Add("onclick", "lessonRedirect("+ lessonID + ")");
                    listItem.Controls.Add(linkB);

                    // Add the list item to the unsorted list.
                    navSideMenu.Controls.Add(listItem);
                }
            }

        }
        catch (SqlException ex)
        {

        }
    }
}