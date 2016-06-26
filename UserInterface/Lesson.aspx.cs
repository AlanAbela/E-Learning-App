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
    private int LessonID { get; set; }
    #endregion

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
              
                    Validation();
                    LessonID = Convert.ToInt32(Request.QueryString["ID"]);
                    BindNavMenu(LessonID);

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
    public void BindNavMenu(int ID)
    {
        try
        {
            TopicBL topics = new TopicBL();
            DataTable lessonTopics = topics.GetTopicsByLessonID(LessonID);

            foreach (DataRow row in lessonTopics.Rows)
            {
                HtmlGenericControl listItemTitle = new HtmlGenericControl("li");
                listItemTitle.Attributes.Add("id",row.Field<int>("ID").ToString());
                
                LinkButton linkButton = new LinkButton();
                linkButton.Text = row.Field<string>("Title").ToString();
                linkButton.Attributes.Add("runat", "server");
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