using System;
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

                BindExampleTable();

                videoSource.Src = "http://www.youtube.com/embed/bijF5_18O6I?autoplay=0";

                BindData(TopicID);

            }
            catch(Exception ex)
            {

            }
        }     
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
            gvResultTable.DataSource = null;
            gvResultTable.DataBind();
            lblResult.Visible = false;

            QueryBL queryBL = new QueryBL();
            EmployeeBL employeeBL = new EmployeeBL();

            // Get table to be compared with.
            DataTable compareTable = queryBL.GetQueryResult(TopicID);
            
            // Remove excessive white spaces
            string query = txtTryItOut.Text.Trim();

            // Process the user input on the table.
            queryBL.ProcessQuery(query);
            DataTable resultTable = employeeBL.GetAllEmployee();

            bool correct = true;

            // If table was not deleted.
            if (employeeBL.IsEmployee() == 1)
            {
                // Bind to grid view.
                gvResultTable.DataSource = resultTable;
                gvResultTable.DataBind();

                // Compare rows and columns.                
                if (compareTable.Rows.Count != resultTable.Rows.Count || compareTable.Columns.Count != resultTable.Columns.Count)
                {
                    correct = false;
                }
                // If rows and columns numbers match compare their content.
                else
                {   
                    for(int i = 0; i < compareTable.Rows.Count; i++)
                    {
                        for(int j = 0; j < compareTable.Columns.Count; j++)
                        {
                            if(!(compareTable.Rows[i][j].Equals(resultTable.Rows[i][j])))
                            {
                                correct = false;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                correct = false;
                gvResultTable.DataSource = new DataTable();
                gvResultTable.DataBind();              
            }

            if(correct)
            {
                lblResult.Visible = true;
                lblResult.Attributes.Add("class", "label label-success");
                lblResult.Text = "Correct!";
            }
            else
            {
                lblResult.Attributes.Add("class", "label label-warning");
                lblResult.Visible = true;
                lblResult.Text = ErrorMessage.GetErrorDesc(4);
            }

            // Drop table and recreate it.
            employeeBL.RecreateTable();

        }
        catch (SqlException ex)
        {
            lblResult.Attributes.Add("class", "label label-warning");
            lblResult.Visible = true;
            lblResult.Text = ErrorMessage.GetErrorDesc(3);
        }

        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtTryItOut.Text = string.Empty;
        gvResultTable.DataSource = null;
        gvResultTable.DataBind();

        lblResult.Visible = false;
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

    private void BindExampleTable()
    {
        TopicBL topicBL = new TopicBL();
        DataTable table = topicBL.GetExampleTabel();
        gvTableExample.DataSource = table;
        gvTableExample.DataBind();
    }


    #endregion

}