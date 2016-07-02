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
            DataTable lessonTopics = topics.GetTopicsAndUserID(lessonID);
            UserTopicBL userTopicBL = new UserTopicBL();
           

            foreach (DataRow row in lessonTopics.Rows)
            {
                HtmlGenericControl listItemTitle = new HtmlGenericControl("li");
                
                LinkButton linkButton = new LinkButton();
                if (row.Field<int?>("UserID") == null)
                {
                    linkButton.Text = "<img src=http://localhost:3787/image/c1.jpg>" + row.Field<string>("Title").ToString();
                }
                else
                {
                    linkButton.Text = "<img src=http://localhost:3787/image/c2.jpg>" + row.Field<string>("Title").ToString();
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
    
}