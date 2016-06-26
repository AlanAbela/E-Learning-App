using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserInterface_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// If username and password are valid add record in table User.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if(valForm.IsValid)
        { 
            try
            {
                RegisterBL register = new RegisterBL();
                register.RegisterUser(txtUsername.Text, txtPassword.Text);
          
            }
                catch(SqlException ex)
            {

            }
        }
    }
    /// <summary>
    /// Check if the username is already registered in the database.
    /// Check if username has five or more characters.
    /// Check if username is left open or not.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void valForm_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;
        valForm.ErrorMessage = string.Empty;
        CustomValidator validator = (CustomValidator)source;
        
        if (RegisterBL.DuplicateUser(txtUsername.Text) > 0)
        {
            args.IsValid = false;
            valForm.ErrorMessage = "Username already Taken! ";
        }
        
        if(txtUsername.Text.Length < 5)
        {
            args.IsValid = false;
            valForm.ErrorMessage = "Username must be 5 or more characters long. ";
        }

        if(txtPassword.Text.Length < 8)
        {
            args.IsValid = false;
            valForm.ErrorMessage = valForm.ErrorMessage + "Password must be 8 or more characters long. ";
        }

        if(txtPassword.Text.Contains(" "))
        {
            args.IsValid = false;
            valForm.ErrorMessage = valForm.ErrorMessage + "Password must not contain white spaces. ";
        }

        if(args.IsValid && !txtPassword.Text.Equals(txtConfirmPassword.Text))
        {
            args.IsValid = false;
            valForm.ErrorMessage = valForm.ErrorMessage + "Passwords do not match. ";
        }

        if(args.IsValid)
        {
            Response.Redirect("Login.aspx");
        }
 
    }

    /// <summary>
    /// Redirects to Login page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGoToLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}