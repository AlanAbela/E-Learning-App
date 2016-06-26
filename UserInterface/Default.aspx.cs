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
    protected void Page_Load(object sender, EventArgs e)
    {

        if(Session["UserID"] == null)
        {
            Response.Redirect("Login.aspx");  
        }
        else
        {
                lblTitle.Text = "Welcome to SQL Learning Platform";                   
        }
    }

      //private void MenuItemSelect(string selection)
    //{
    //    foreach (Control control in navSideMenu.Controls)

    //    {
    //        if (control is HtmlGenericControl)
    //        {
    //            HtmlGenericControl genericControl = (HtmlGenericControl)control;

    //            genericControl.Attributes.Remove("class");

    //            StringWriter stringWriter = new StringWriter();
    //            HtmlTextWriter textWriter = new HtmlTextWriter(stringWriter);
                
    //            genericControl.RenderControl(textWriter);

    //            string value = stringWriter.GetStringBuilder().ToString();

    //            if (value == selection)
    //            {
    //                genericControl.Attributes.Add("class", "active");
    //            }
    //        }
    //    }
    //}

    protected void btnLink_Click (object sender, EventArgs e)
    {

    }
}