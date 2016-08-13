﻿using BusinessLogic;
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

        // If current user is already logged in redirect to Default page.
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("Default.aspx");
        }
    

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

                RegisterBL register = new RegisterBL();
                register.RegisterUser(txtUsername.Text, txtPassword.Text);
                
                    Response.Redirect("Login.aspx", false);
                
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
        
        // Reference the custom validator.
        valForm.ErrorMessage = string.Empty;


        // Compares username to database records for duplicates.
        if (RegisterBL.DuplicateUser(txtUsername.Text) > 0)
        {
            args.IsValid = false;
            valForm.ErrorMessage = "Username already Taken! ";
        }
        
        // Checks user name length.
        if(txtUsername.Text.Length < 5)
        {
            args.IsValid = false;
            valForm.ErrorMessage = "Username must be 5 or more characters long. ";
        }

        // Checks password length.
        if(txtPassword.Text.Length < 8)
        {
            args.IsValid = false;
            valForm.ErrorMessage = valForm.ErrorMessage + "Password must be 8 or more characters long. ";
        }

        // Checks if password contains white spaces.
        if(txtPassword.Text.Contains(" "))
        {
            args.IsValid = false;
            valForm.ErrorMessage = valForm.ErrorMessage + "Password must not contain white spaces. ";
        }

        // Checks that confirmation password matched the password.
        if(args.IsValid && !txtPassword.Text.Equals(txtConfirmPassword.Text))
        {
            args.IsValid = false;
            valForm.ErrorMessage = valForm.ErrorMessage + "Passwords do not match. ";
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