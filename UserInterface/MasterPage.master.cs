using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class UserInterface_MasterPage : System.Web.UI.MasterPage
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    { 

        if(Session["UserID"] != null)
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            UserBL userBL = new UserBL();
            DataTable table = userBL.GetUserBL(userID);

            if(table.Rows.Count > 0)
            {
                lblLogged.Text = "Loggen in as " + table.Rows[0].Field<string>("Username");
            }

            BindSideMenu();
        }
    }

    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        Session["UserID"] = null;
        Response.Redirect("Login.aspx");
    }

    #endregion

    #region Private methods

    private void BindSideMenu()
    {
        try
        {
            LessonBL lesson = new LessonBL();
            DataTable table = lesson.GetLessons();

            if (table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("id", dr["ID"].ToString());

                    LinkButton linkB = new LinkButton();
                    linkB.Text = dr[1].ToString();
                    linkB.Attributes.Add("runat", "server");
                    listItem.Controls.Add(linkB);

                    navSideMenu.Controls.Add(listItem);
                }
            }

        }
        catch (SqlException ex)
        {

        }
    }

    #endregion


}
