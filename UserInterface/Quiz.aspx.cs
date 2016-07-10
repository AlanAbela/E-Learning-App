using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserInterface_Quiz : System.Web.UI.Page
{
    public int LessonID { get; set; }

    public int UserID { get; set; }

    public DataTable questions { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if user has a session.
        if (Session["UserID"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        
        try
        {
            if (!IsPostBack)
            {
                // Validate Query string value.
                Validation();

                LessonID = Convert.ToInt32(Request.QueryString["ID"]);
                ViewState["lessonID"] = LessonID;
                UserID = Convert.ToInt32(Session["UserID"]);

                // Get the questions realted to this lesson.
                QuestionBL questionBL = new QuestionBL();
                
                // Get the list of questions.               
                questions = questionBL.GetQuestionsByLessonID(LessonID);

                // Bind questions to views.
                BindQuestions();
            }
            else
            {
                // Store selections made.
                StoreSelections();
            }

        }
        catch(Exception ex)
        {

        }
    }

    /// <summary>
    /// Called on Submit button click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        reqField.IsValid = false;
            // If viewing the pre last question.
            if (mvQuestions.ActiveViewIndex == mvQuestions.Views.Count - 2)
            {
                // Move to last view.
                mvQuestions.ActiveViewIndex = mvQuestions.ActiveViewIndex + 1;
                reqField.IsValid = true;
                btnSubmit.Visible = false;

            // Declare a datatable to bind to the result grid view.
            DataTable table = new DataTable();
            
            table.Columns.Add("Question", typeof(string));
            table.Columns.Add("Your Answer", typeof(string));
            table.Columns.Add("Correct?", typeof(string));
            table.Columns.Add("QuestionID", typeof(int));
            table.Columns.Add("link");
            
           

            string value = string.Empty;

                for (int i = 0; i < mvQuestions.Views.Count-1; i++)
                {
                    value = "chkQuizList" + i + "Value";

                    // Translate boolean to values.
                    if (((string)(ViewState[value])).Equals("True"))
                    {
                        ViewState[value] = "Yes";
                    }
                    else
                    {
                        ViewState[value] = "No";
                    }

               // Get the question
               Label label = (Label)(mvQuestions.Views[i].FindControl("lblViewQ"+i));

                    table.Rows.Add(label.Text, ViewState["chkQuizList" + i + "Text"].ToString(), ViewState["chkQuizList" + i + "Value"].ToString(), ViewState["Q"+i].ToString());
            
            }

                gvResult.DataSource = table;
            
            //gvResult.Columns[4].Visible = false;
                gvResult.DataBind();
            int k = gvResult.Columns.Count;

        }

            if (mvQuestions.ActiveViewIndex < 4)
            {

                mvQuestions.ActiveViewIndex = mvQuestions.ActiveViewIndex + 1;

                if (reqField.IsValid == false)
                {

                    if (mvQuestions.ActiveViewIndex == 1)
                    {
                        reqField.ControlToValidate = "chkQuizList1";
                        reqField.IsValid = true;
                    }

                    if (mvQuestions.ActiveViewIndex == 2)
                    {
                        reqField.ControlToValidate = "chkQuizList2";
                        reqField.IsValid = true;
                    }

                    if (mvQuestions.ActiveViewIndex == 3)
                    {
                        reqField.ControlToValidate = "chkQuizList3";
                        reqField.IsValid = true;
                    }

                    if (mvQuestions.ActiveViewIndex == 4)
                    {
                        reqField.ControlToValidate = "chkQuizList4";
                        reqField.IsValid = true;
                    }

                }

            }
        
    }

    #region Privare methods
    /// <summary>
    /// Validates the lesson ID.
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

    /// <summary>
    /// Store user selections in viewstates
    /// </summary>
    private void StoreSelections()
    {   

        if(chkQuizList0.SelectedItem != null)
        {
            ViewState["chkQuizList0Text"] = chkQuizList0.SelectedItem.Text;
            ViewState["chkQuizList0Value"] = chkQuizList0.SelectedItem.Value;
            
        }

        if (chkQuizList1.SelectedItem != null)
        {
            ViewState["chkQuizList1Text"] = chkQuizList1.SelectedItem.Text;
            ViewState["chkQuizList1Value"] = chkQuizList1.SelectedItem.Value;
         
        }

        if (chkQuizList2.SelectedItem != null)
        {
            ViewState["chkQuizList2Text"] = chkQuizList2.SelectedItem.Text;
            ViewState["chkQuizList2Value"] = chkQuizList2.SelectedItem.Value;
          
        }

        if (chkQuizList3.SelectedItem != null)
        {
            ViewState["chkQuizList3Text"] = chkQuizList3.SelectedItem.Text;
            ViewState["chkQuizList3Value"] = chkQuizList3.SelectedItem.Value;
       
        }

        if (chkQuizList4.SelectedItem != null)
        {
            ViewState["chkQuizList4Text"] = chkQuizList4.SelectedItem.Text;
            ViewState["chkQuizList4Value"] = chkQuizList4.SelectedItem.Value;

        }
    }

    /// <summary>
    /// Binds data to the page.
    /// </summary>
    /// <param name="topicID"></param>
    private void BindQuestions()
    {
        try
        {
            LessonBL lessonBL = new LessonBL();
            DataTable table = lessonBL.GetLesson(LessonID);

            lblLessonTitle.Text = table.Rows[0].Field<string>("Title");

         //   QuestionBL questionBL = new QuestionBL();
            AnswerBL answerBL = new AnswerBL();

            // Get the questions realted to this lesson.
         //   DataTable questions = questionBL.GetQuestionsByLessonID(LessonID);
            

            // Check that the number of questions are 5 before binding to labels.
            if (questions.Rows.Count == 5)
            {

                lblViewQ0.Text = questions.Rows[0].Field<string>("Question");
                ViewState["Q0"] = questions.Rows[0].Field<int>("TopicID");
                // Get the question answer and distractors.
                chkQuizList0.DataSource = answerBL.GetAnswersByQuestionID(questions.Rows[0].Field<int>("ID"));
                chkQuizList0.DataTextField = "Text";
                chkQuizList0.DataValueField = "Correct";
                chkQuizList0.DataBind();

                lblViewQ1.Text = questions.Rows[1].Field<string>("Question");
                ViewState["Q1"] = questions.Rows[1].Field<int>("TopicID");
                chkQuizList1.DataSource = answerBL.GetAnswersByQuestionID(questions.Rows[1].Field<int>("ID"));
                chkQuizList1.DataTextField = "Text";
                chkQuizList1.DataValueField = "Correct";
                chkQuizList1.DataBind();

                lblViewQ2.Text = questions.Rows[2].Field<string>("Question");
                ViewState["Q2"] = questions.Rows[2].Field<int>("TopicID");
                chkQuizList2.DataSource = answerBL.GetAnswersByQuestionID(questions.Rows[2].Field<int>("ID"));
                chkQuizList2.DataTextField = "Text";
                chkQuizList2.DataValueField = "Correct";
                chkQuizList2.DataBind();

                lblViewQ3.Text = questions.Rows[3].Field<string>("Question");
                ViewState["Q3"] = questions.Rows[3].Field<int>("TopicID");
                chkQuizList3.DataSource = answerBL.GetAnswersByQuestionID(questions.Rows[3].Field<int>("ID"));
                chkQuizList3.DataTextField = "Text";
                chkQuizList3.DataValueField = "Correct";
                chkQuizList3.DataBind();

                lblViewQ4.Text = questions.Rows[4].Field<string>("Question");
                ViewState["Q4"] = questions.Rows[4].Field<int>("TopicID");
                chkQuizList4.DataSource = answerBL.GetAnswersByQuestionID(questions.Rows[4].Field<int>("ID"));
                chkQuizList4.DataTextField = "Text";
                chkQuizList4.DataValueField = "Correct";
                chkQuizList4.DataBind();
            }
        }
        catch(SqlException ex)
        {

        }

    }

    class Selection
    {
        private string Result { get; set; }
        private string Answer { get; set; }
        private string Question { get; set; }
    }

    #endregion

    protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[3].Visible = false;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[2].Text.Equals("No"))
           { 
            HyperLink the_url = new HyperLink();
            the_url.NavigateUrl = "Topic.aspx?ID=" + e.Row.Cells[3].Text + "&lessonid=" + ViewState["lessonID"].ToString() ;
                TopicBL topicBL = new TopicBL();
               DataTable topic = topicBL.GetTopicByID(Convert.ToInt32(e.Row.Cells[3].Text));
                if (topic.Rows.Count > 0)
                {
                    the_url.Text = topic.Rows[0].Field<string>("Title");
                }

            e.Row.Cells[4].Controls.Add(the_url);
           }
        }

        //if(e.Row.RowType == DataControlRowType.Header)
        //{
        //    Label label = new Label();
        //    label.Text = "Recommended Review";
        //    e.Row.Cells[3].Controls.Add(label);
        //}
    }
}