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
            DataTable table = lesson.GetLessons();

            UserLessonBL userLesson = new UserLessonBL();
            DataTable userLessonTable = null;

            if (table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                 
                    int lessonID = Convert.ToInt32(dr["ID"]);

                    userLessonTable = userLesson.GetRecord(UserID, lessonID);


                    LinkButton linkB = new LinkButton();
                    if (userLessonTable.Rows.Count == 0)
                    {
                        linkB.Text = "<img src=http://localhost:3787/image/c1.jpg> " + dr[1].ToString();
                    }
                    else
                    {
                        linkB.Text = "<img src=http://localhost:3787/image/c2.jpg> " + dr[1].ToString();
                    }
                    linkB.Attributes.Add("runat", "server");
                    linkB.Attributes.Add("onclick", "lessonRedirect("+ lessonID + ")");
                    listItem.Controls.Add(linkB);

                    navSideMenu.Controls.Add(listItem);
                }
            }

        }
        catch (SqlException ex)
        {

        }
    }
}