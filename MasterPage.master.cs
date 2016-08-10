using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class UserInterface_MasterPage : System.Web.UI.MasterPage
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

            if (Context.Session["UserID"] != null)
            {
                int userID = Convert.ToInt32(Context.Session["UserID"]);
                UserBL userBL = new UserBL();
                DataTable table = userBL.GetUser(userID);

                if (table.Rows.Count > 0)
                {
                    lblLogged.Text = "Logged in as " + table.Rows[0].Field<string>("Username");
                    // If the course is complete enable link to course page.
                    if (table.Rows[0].Field<bool>("Course_Complete"))
                    {
                        btnTest.Visible = true;
                    }
                }
            }
    }

    /// <summary>
    /// Called on button Sign out click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        Context.Session["UserID"] = null;
        FormsAuthentication.SignOut();
        Response.Redirect("Login.aspx");
    }

    /// <summary>
    /// Called on home button click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void homeLink_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    #endregion


    /// <summary>
    /// Called on final exam button click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTest_Click(object sender, EventArgs e)
    {
        Response.Redirect("OnlineTest.aspx");
    }
}
