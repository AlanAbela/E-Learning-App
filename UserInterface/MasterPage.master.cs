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
                lblLogged.Text = "Logged in as " + table.Rows[0].Field<string>("Username");
            }
        }
    }

    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        Session["UserID"] = null;
        Response.Redirect("Login.aspx");
    }

    #endregion



}
