using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.Params["logout"]))
        {
            FormsAuthentication.SignOut();
            Response.Redirect("./");
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if(valForm.IsValid)
        {
                LoginBL loginBL = new LoginBL();

                // Store the user ID in session.
            Context.Session["UserID"] = loginBL.GetUserID(txtUsername.Text, txtPassword.Text);

            FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, true);
         
        }
 
    }

    #region Events
    protected void valForm_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;
        valForm.ErrorMessage = string.Empty;
        CustomValidator validator = (CustomValidator)source;

        if (txtUsername.Text.Length < 5)
        {
            args.IsValid = false;
            valForm.ErrorMessage = "Username must be 5 or more characters long. ";
        }

        if (txtPassword.Text.Length < 8)
        {
            args.IsValid = false;
            valForm.ErrorMessage = valForm.ErrorMessage + "Password must be 8 or more characters long. ";
        }

        if (txtPassword.Text.Contains(" "))
        {
            args.IsValid = false;
            valForm.ErrorMessage = valForm.ErrorMessage + "Password must not contain white spaces. ";
        }

        LoginBL loginBL = new LoginBL();
        if(loginBL.AuthenticateUser(txtUsername.Text, txtPassword.Text) == 0)
        {
            args.IsValid = false;
            valForm.ErrorMessage = valForm.ErrorMessage + "Incorrect user name or password";
        }

    }
    #endregion
}