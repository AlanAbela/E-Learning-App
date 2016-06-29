﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserInterface_Topic : System.Web.UI.Page
{

    #region Properties
    public int TopicID { get; set; }
    public int LessonID { get; set; }
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
            try
            {
                // Validate Query string value.
                Validation();

                TopicID = Convert.ToInt32(Request.QueryString["ID"]);
                LessonID = Convert.ToInt32(Request.QueryString["lessonid"]);

                videoSource.Src = "http://www.youtube.com/embed/bijF5_18O6I?autoplay=0";

                BindData(TopicID);

            }
            catch(Exception)
            {

            }
        }     
    }


    protected void gvTableExample_Load(object sender, EventArgs e)
    {
        TopicBL topicBL = new TopicBL();
        DataTable table = topicBL.GetExampleTabel();
        gvTableExample.DataSource = table;
        gvTableExample.DataBind();
    }


    /// <summary>
    /// Called on submit button click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            TopicBL topicBL = new TopicBL();
            DataTable topic = topicBL.GetTopicByID(TopicID);
        }
        catch (SqlException ex)
        {

        }
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtTryItOut.Text = string.Empty;
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

        if (Request.QueryString["lessonid"] == null)
        {
            valid = false;
        }

        if(!int.TryParse(Request.QueryString["lessonid"], out output))
        {
            valid = false;
        }

        if (!valid)
        {
            throw new CustomException(ErrorMessage.GetErrorDesc(2));
        }
    }


    /// <summary>
    /// Binds topic title and topic description.
    /// </summary>
    /// <param name="topicID"></param>
    private void BindData(int topicID)
    {
        try
        {
            TopicBL topicBL = new TopicBL();
            DataTable table = topicBL.GetTopicByID(topicID);

            lblLessonTitle.Text = table.Rows[0].Field<string>("Title");
            lblTopicText.Text = table.Rows[0].Field<string>("Text");
        }
        catch(Exception ex)
        {

        }
    }

    #endregion

}