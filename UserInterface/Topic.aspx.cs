using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserInterface_Topic : System.Web.UI.Page
{

    #region Properties
    private int TopicID { get; set; }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if user has a session.
        if (Session["UserID"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            // Validate Query string value.
            Validation();

             TopicID = Convert.ToInt32(Request.QueryString["ID"]);
        }

        // videoSource.Src = "http://www.youtube.com/embed/bijF5_18O6I?autoplay=0";

    }

    #endregion

    #region Private Methods
    /// <summary>
    /// Validate that a topic ID was passed.
    /// </summary>
    private void Validation()
    {
        bool valid = true;
        int output = 0;

        if (Request.QueryString["ID"] == null)
        {
            valid = false;
        }

        if (!int.TryParse(Request.QueryString["ID"], out output))
        {
            valid = false;
        }

        if (!valid)
        {
            throw new CustomException(ErrorMessage.GetErrorDesc(2));
        }
    }

    #endregion
}