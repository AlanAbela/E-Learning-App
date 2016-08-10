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
        // If current user is already logged in redirect to Default page.
        if(HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("Default.aspx");
        }
    }

    /// <summary>
    /// Called on btn login click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_Click(object sender, EventArgs e)
    {
      // If login is valid
        if (valForm.IsValid)
        {
                LoginBL loginBL = new LoginBL();

            // Retrieve and store the user ID in session.        
            Context.Session["UserID"] = loginBL.GetUserID(txtUsername.Text, txtPassword.Text);
            

            // Redirect to requested page or default page.
            FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, true);
         
        }
 
    }

    #region Events

    /// <summary>
    /// Called by the custom validator server control.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void valForm_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;
        valForm.ErrorMessage = string.Empty;

  
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