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

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if user has a session.
        if (Session["UserID"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        
        try
        {

            UserID = Convert.ToInt32(Session["UserID"]);
            
            // Validate Query string value.
            Validation();
            StoreSelections();
            LessonID = Convert.ToInt32(Request.QueryString["ID"]);
            
            BindData();
            ListItem l = chkQuizList0.SelectedItem;
            
           
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
        ListItem l = chkQuizList0.SelectedItem;
        if (mvQuestions.ActiveViewIndex == 4)
        {
            mvQuestions.ActiveViewIndex = mvQuestions.ActiveViewIndex + 1;
            reqField0.IsValid = true;
            btnSubmit.Visible = false;

            string holder = string.Empty;
            for(int i = 0; i<5; i++)
            {
                holder = "chkQuizList" + i + "Value";
                if (((string)(ViewState[holder])).Equals("True"))
                {
                    ViewState[holder] = "Yes";
                }
                else
                {
                    ViewState[holder] = "No";
                }
            }

            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem((string)ViewState["chkQuizList0Text"], (string)ViewState["chkQuizList0Value"]));
            list.Add(new ListItem((string)ViewState["chkQuizList1Text"], (string)ViewState["chkQuizList1Value"]));
            list.Add(new ListItem((string)ViewState["chkQuizList2Text"], (string)ViewState["chkQuizList2Value"]));
            list.Add(new ListItem((string)ViewState["chkQuizList3Text"], (string)ViewState["chkQuizList3Value"]));
            list.Add(new ListItem((string)ViewState["chkQuizList4Text"], (string)ViewState["chkQuizList4Value"]));

            gvResult.DataSource = list;
            gvResult.DataBind();

        }
        if (mvQuestions.ActiveViewIndex < 4)
        {   
            if(mvQuestions.ActiveViewIndex == 0)
            {
              //  ResultList.Add(chkQuizList0.Items[chkQuizList0.SelectedIndex]);
            }         

                mvQuestions.ActiveViewIndex = mvQuestions.ActiveViewIndex + 1;
           
            if (reqField0.IsValid == false)
            {              

                if (mvQuestions.ActiveViewIndex == 1)
                {
                    reqField0.ControlToValidate = "chkQuizList1";
                    reqField0.IsValid = true;
                  //  ResultList.Add(chkQuizList1.Items[chkQuizList1.SelectedIndex]);
                }

             if(mvQuestions.ActiveViewIndex == 2)
                {
                    reqField0.ControlToValidate = "chkQuizList2";
                    reqField0.IsValid = true;
                 //   ResultList.Add(chkQuizList2.SelectedItem);
                }

                if (mvQuestions.ActiveViewIndex == 3)
                {
                    reqField0.ControlToValidate = "chkQuizList3";
                    reqField0.IsValid = true;
                 //   ResultList.Add(chkQuizList3.SelectedItem);
                }

                if (mvQuestions.ActiveViewIndex == 4)
                {
                    reqField0.ControlToValidate = "chkQuizList4";
                    reqField0.IsValid = true;
                 //   ResultList.Add(chkQuizList4.SelectedItem);
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
    private void BindData()
    {
        try
        {
            LessonBL lessonBL = new LessonBL();
            DataTable table = lessonBL.GetLesson(LessonID);

            lblLessonTitle.Text = table.Rows[0].Field<string>("Title");

            QuestionBL questionBL = new QuestionBL();
            AnswerBL answerBL = new AnswerBL();

            DataTable questions = questionBL.GetQuestionsByLessonID(LessonID);
            

            // Check that the number of questions are 5 before binding to labels.
            if (questions.Rows.Count == 5)
            {
                lblViewQ0.Text = questions.Rows[0].Field<string>("Question");
                chkQuizList0.DataSource = answerBL.GetAnswersByQuestionID(questions.Rows[0].Field<int>("ID"));
                chkQuizList0.DataTextField = "Text";
                chkQuizList0.DataValueField = "Correct";
                chkQuizList0.DataBind();
                lblViewQ1.Text = questions.Rows[1].Field<string>("Question");
                chkQuizList1.DataSource = answerBL.GetAnswersByQuestionID(questions.Rows[1].Field<int>("ID"));
                chkQuizList1.DataTextField = "Text";
                chkQuizList1.DataValueField = "Correct";
                chkQuizList1.DataBind();
                lblViewQ2.Text = questions.Rows[2].Field<string>("Question");
                chkQuizList2.DataSource = answerBL.GetAnswersByQuestionID(questions.Rows[2].Field<int>("ID"));
                chkQuizList2.DataTextField = "Text";
                chkQuizList2.DataValueField = "Correct";
                chkQuizList2.DataBind();
                lblViewQ3.Text = questions.Rows[3].Field<string>("Question");
                chkQuizList3.DataSource = answerBL.GetAnswersByQuestionID(questions.Rows[3].Field<int>("ID"));
                chkQuizList3.DataTextField = "Text";
                chkQuizList3.DataValueField = "Correct";
                chkQuizList3.DataBind();
                lblViewQ4.Text = questions.Rows[4].Field<string>("Question");
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

    #endregion
  
}