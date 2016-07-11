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

    public int UserID { get; set; }
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
                UserID = Convert.ToInt32(Session["UserID"]);

                // Validate Query strings.
                Validation();

                // Reference values stored in query strings.
                TopicID = Convert.ToInt32(Request.QueryString["ID"]);
                LessonID = Convert.ToInt32(Request.QueryString["lessonid"]);

                // Bind table, table according to topic ID.
                BindExampleTable();

                TopicBL topicBL = new TopicBL();
                DataTable dataTable = topicBL.GetTopicByID(TopicID);

                videoSource.Src = dataTable.Rows[0].Field<string>("VideoLink");
            

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
        string errorMessage = string.Empty;

        try
        {
            gvResultTable.DataSource = null;
            gvResultTable.DataBind();
            lblResult.Visible = false;
        
            QueryBL queryBL = new QueryBL();
            EmployeeBL employeeBL = new EmployeeBL();
            bool correct = true;

            // Get table to be compared with.
            DataTable compareTable = queryBL.GetQueryResult(TopicID);

            string query = txtTryItOut.Text;
            // Remove excessive white spaces
            if (!string.IsNullOrEmpty(txtTryItOut.Text))
            {
                query = txtTryItOut.Text.Trim();
            }
            else
            {
                lblResult.Attributes.Add("class", "label label-warning");
                lblResult.Visible = true;
                lblResult.Text = ErrorMessage.GetErrorDesc(9).Replace("|","<br/>");
                return;
            }
            

            // Process the user input on the table.
            DataTable resultTable = queryBL.ProcessQuery(query);
            // If the query is an UPDATE or DELETE
            if (resultTable == null)
            {
                resultTable = employeeBL.GetAllEmployee();
            }   

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
                    errorMessage = ErrorMessage.GetErrorDesc(4).Replace("|","<br/>");
                }
                // If rows and columns numbers match, compare their content.
                else
                {   
                    for(int i = 0; i < compareTable.Rows.Count; i++)
                    {
                        for(int j = 0; j < compareTable.Columns.Count; j++)
                        {
                            if(!(compareTable.Rows[i][j].Equals(resultTable.Rows[i][j])))
                            {
                                errorMessage = ErrorMessage.GetErrorDesc(6).Replace("|", "<br/>"); ;
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
                // Table was deleted so bind an empty datatable to the gridview.
                gvResultTable.DataSource = new DataTable();
                gvResultTable.DataBind();
                errorMessage = ErrorMessage.GetErrorDesc(8).Replace("|", "<br/>"); ;         
            }

            if(correct)
            {
                errorMessage = ErrorMessage.GetErrorDesc(5);
                lblResult.Visible = true;
                lblResult.Attributes.Add("class", "label label-success");
                lblResult.Text = errorMessage;

                // Add record in table User_Topic
                UserTopicBL userTopicBL = new UserTopicBL();
                userTopicBL.InsertRecord(UserID, TopicID);

                // Check if lesson is complete
                TopicBL topicBL = new TopicBL();
                int countTopicsUnderLesson = topicBL.GetCountTopicsByLessonID(LessonID);
                int countTopicsCompletedByUser = topicBL.GetCountCompletedTopics(LessonID, UserID);

                // If all lessons are complete add the record in table User_Lesson
                if(countTopicsCompletedByUser == countTopicsUnderLesson)
                {
                    UserLessonBL userLessonBL = new UserLessonBL();
                    userLessonBL.InserNewRecord(UserID, LessonID);
                }
            }
            else
            {
                lblResult.Attributes.Add("class", "label label-warning");
                lblResult.Visible = true;
                lblResult.Text = errorMessage;
            }

            // Drop table and recreate it.
            employeeBL.RecreateTable();

        }
        catch (SqlException ex)
        {
           if(ex.Number == 102)
            {
                errorMessage = ErrorMessage.GetErrorDesc(3).Replace("|","<br/>");
            }
           else if(ex.Number == 262)
            {
                errorMessage = ErrorMessage.GetErrorDesc(7).Replace("|", "<br/>");
            }
           else if(ex.Number == 207)
            {
                errorMessage = ErrorMessage.GetErrorDesc(10).Replace("|", "<br/>");
            }

            else
            {
                errorMessage = ex.Message;
            }
            lblResult.Attributes.Add("class", "label label-warning");
            lblResult.Visible = true;
            
            lblResult.Text = errorMessage;
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
     
            TopicBL topicBL = new TopicBL();
            DataTable table = topicBL.GetTopicByID(topicID);

            lblLessonTitle.Text = table.Rows[0].Field<string>("Title");
            lblTopicText.Text = table.Rows[0].Field<string>("Text");
            lblTopicText2.Text = table.Rows[0].Field<string>("Text2");
        
      
    }

    private void BindExampleTable()
    {
        TopicBL topicBL = new TopicBL();
        DataTable table = topicBL.GetExampleTabel(TopicID);
        gvTableExample.DataSource = table;
        gvTableExample.DataBind();
    }


    #endregion

}