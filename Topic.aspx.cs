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

    public UserTopicBL userTopicBL {get; set;}
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
       
            // Reference USer ID
                UserID = Convert.ToInt32(Context.Session["UserID"]);

                // Validate Query strings.
                Validation();

                // Reference values stored in query strings.
                TopicID = Convert.ToInt32(Request.QueryString["ID"]);
                LessonID = Convert.ToInt32(Request.QueryString["lessonid"]);

                // Create example tables according to the topic ID.
                BindExampleTable();

               // Create an instance of the Topic business logic
                TopicBL topicBL = new TopicBL();

              // Get the topic record by its ID from the database.
                DataTable dataTable = topicBL.GetTopicByID(TopicID);

              // Pass the video link to the Iframe instance.
                videoSource.Src = dataTable.Rows[0].Field<string>("VideoLink");
            
              // Get text content to populate the Topic page.
                BindContent(TopicID);

                if(!IsPostBack)
                {
                    // Insert record if it doesn't exist in the linking table between user and topic.
                    userTopicBL = new UserTopicBL();
                    userTopicBL.InsertRecord(UserID, TopicID);
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
            // Declare variables.
            gvResultTable.DataSource = null;
            gvResultTable.DataBind();
            lblResult.Visible = false;
            QueryBL queryBL = new QueryBL();
            EmployeeBL employeeBL = new EmployeeBL();
            bool correct = true;

            // Retrieve the correct query, execute it against the database, and return a datatable representation.
            DataTable compareTable = queryBL.GetQueryResult(TopicID);

            // If the query is an UPDATE, INSERT or DELETE query. Get the altered result.
            if(compareTable.Rows.Count == 0)
            {
                compareTable = new DataTable();
                compareTable = employeeBL.GetAllEmployee();
            }

            // Reference the user query.
            string query = txtTryItOut.Text;

            // Check if user text field contains any characters.
            if (!string.IsNullOrEmpty(txtTryItOut.Text))
            {
                query = txtTryItOut.Text.Trim();
            }
            // Inform user to type text.
            else
            {
                lblResult.Attributes.Add("class", "label label-warning");
                lblResult.Visible = true;
                // Retrieve error from XML file.
                lblResult.Text = ErrorMessage.GetErrorDesc(9).Replace("|","<br/>");
                return;
            }

            // Recreate the table before running the user query.
            employeeBL.RecreateTable();

            // Process the user input on the table.
            DataTable resultTable = queryBL.ProcessQuery(query);

            // If the query is an UPDATE, INSERT or DELETE, retrieve the altered table.
            if (resultTable == null || resultTable.Rows.Count == 0)
            {
                resultTable = new DataTable();
                resultTable = employeeBL.GetAllEmployee();
            }   

            // If table was not dropped by user query.
            if (employeeBL.IsEmployee() == 1)
            {
                // Bind to grid view.
                gvResultTable.DataSource = resultTable;
                gvResultTable.DataBind();

                // Compare no of rows and columns of database's query table to user's query table..                
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
                // If table is dropped set correct to false, bind an empty data table to the 1st grid view.
                correct = false;
                gvResultTable.DataSource = new DataTable();
                gvResultTable.DataBind();

                // Retrieve error message from XML file.
                errorMessage = ErrorMessage.GetErrorDesc(8).Replace("|", "<br/>"); ;         
            }

            if(correct)
            {
                errorMessage = ErrorMessage.GetErrorDesc(5);
                lblResult.Visible = true;
                lblResult.Attributes.Add("class", "label label-success");
                lblResult.Text = errorMessage;

                userTopicBL = new UserTopicBL();

                // Set Topic completion date.
                userTopicBL.SetCompleteDate(UserID, TopicID);

                // Check if lesson is complete, by comparing complete topics with the number of topics under current lesson.
                TopicBL topicBL = new TopicBL();
                int countTopicsUnderLesson = topicBL.GetCountTopicsByLessonID(LessonID);
                int countTopicsCompletedByUser = topicBL.GetCountCompletedTopics(LessonID, UserID);

                // If all lessons are complete add the lesson completion date.
                if(countTopicsCompletedByUser == countTopicsUnderLesson)
                {
                    UserLessonBL userLessonBL = new UserLessonBL();
                    userLessonBL.SetCompletionDate(UserID, LessonID);
                }
            }
            // If not correct, show the error message to the user.
            else
            {
                lblResult.Attributes.Add("class", "label label-warning");
                lblResult.Visible = true;
                lblResult.Text = errorMessage;
            }

            // Drop table and recreate it.
            employeeBL.RecreateTable();

        }

        // This is an attempt to show different messages to the users if the submitted query gives an SQL exception.
        catch (SqlException ex)
        {
            // Error number 102 refers to bad syntax.
           if(ex.Number == 102)
            {
                errorMessage = ErrorMessage.GetErrorDesc(3).Replace("|","<br/>");
            }
           // User tries to create a table (currently not implemeted). Since user has no permission to create en exception is thrown.
           else if(ex.Number == 262)
            {
                errorMessage = ErrorMessage.GetErrorDesc(7).Replace("|", "<br/>");
            }
           // Invalid field name.
           else if(ex.Number == 207)
            {
                errorMessage = ErrorMessage.GetErrorDesc(10).Replace("|", "<br/>");
            }
           else
           // Currently other types of exceptions show a general error.
            {
                errorMessage = ErrorMessage.GetErrorDesc(3).Replace("|", "</br>");
            }
      
            lblResult.Attributes.Add("class", "label label-warning");
            lblResult.Visible = true;
            
            lblResult.Text = errorMessage;
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
            throw new Exception(ErrorMessage.GetErrorDesc(2));
        }
    }

    /// <summary>
    /// Binds topic title and topic description.
    /// </summary>
    /// <param name="topicID"></param>
    private void BindContent(int topicID)
    {
     
            TopicBL topicBL = new TopicBL();
            DataTable table = topicBL.GetTopicByID(topicID);

            lblLessonTitle.Text = table.Rows[0].Field<string>("Title");
            lblTopicText.Text = table.Rows[0].Field<string>("Text");
            lblTopicText2.Text = table.Rows[0].Field<string>("Text2");
        
      
    }

    /// <summary>
    /// Bind example tables to gridviews.
    /// </summary>
    private void BindExampleTable()
    {
        // Create instance of Topic business logic.
        TopicBL topicBL = new TopicBL();

        // Get the first example table
        DataTable table = topicBL.GetExampleTable(TopicID);
        gvTableExample.DataSource = table;
        gvTableExample.DataBind();

        DataTable topic = topicBL.GetTopicByID(TopicID);
        // If the topic has a second example query, populate the second example table.
        if(topic.Rows[0].Field<string>("ExampleQuery2") != null)
        {
            DataTable table2 = topicBL.GetExampleTable2(TopicID);
            gvTableExample2.DataSource = table2;
            gvTableExample2.DataBind();
        }

        // If the topic has a third example query, populate the third example table.
        if (topic.Rows[0].Field<string>("ExampleQuery3") != null)
        {
            DataTable table3 = topicBL.GetExampleTable3(TopicID);
            gvTableExample3.DataSource = table3;
            gvTableExample3.DataBind();
        }

    }

    #endregion


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("lesson.aspx?ID=" + LessonID.ToString());
    }
}