<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Quiz.aspx.cs" Inherits="UserInterface_Quiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

        // Redirect to topic and replace current window.
         function topicRedirect(id)
        {
            window.opener = self;
            window.close();
            window.open("topic.aspx?ID="+id+"&lessonid="+ <%= LessonID.ToString() %>);
        }

    </script>

    <div id="page-quiz">
        <asp:HiddenField ID="hdnField" runat="server" ClientIDMode="Static" />
        <!-- Title -->
        <div class="page-header" style="width: 100%; text-align: center;">
        
            <h1>
                <asp:Label ID="lblLessonTitle" runat="server" Text="Header test"></asp:Label>
            </h1>
        </div>

        <!-- Body -->
        <div id="quiz-body">
                <asp:UpdatePanel ID="upTimer" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="position: relative; left: 0px; width: 200px; text-align:left;">
                        <asp:Label ID="lblTime" runat="server"></asp:Label>        
                    </div>
                    <div style="position: relative; left: 0px; width: 100px; text-align:left; float:left;">
                        <asp:Label ID="lblScore" runat="server"></asp:Label>
                    </div>            
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="timer" EventName="Tick"/>
                </Triggers>
            </asp:UpdatePanel>
            <asp:Timer ID="timer" runat="server" Interval="1000" OnTick="Timer_Tick" Enabled =" true"></asp:Timer>
            <div id="question-container">
                <asp:MultiView ID="mvQuestions" runat="server" ActiveViewIndex="0" >
                    <asp:View runat="server">
                        <h2>
                            <asp:Label ID="lblView0" runat="server" Text="Question 1 of 5"></asp:Label></h2>
                        <br />
                        <h3>
                            <asp:Label ID="lblViewQ0" runat="server"></asp:Label></h3>
                        <br />
                        <div class="check-list-container">
                            <asp:RadioButtonList ID="chkQuizList0" runat="server">
                            </asp:RadioButtonList>                     
                        </div>
                    </asp:View>
                    <asp:View runat="server">
                        <h2>
                            <asp:Label ID="lblView1" runat="server" Text="Question 2 of 5"></asp:Label></h2>
                        <br />
                        <h3>
                            <asp:Label ID="lblViewQ1" runat="server" Text="Which statement selects all fields from a table?"></asp:Label></h3>
                        <br />
                        <div class="check-list-container">
                            <asp:RadioButtonList ID="chkQuizList1" runat="server">
                            </asp:RadioButtonList>
                        </div>
                    </asp:View>
                    <asp:View runat="server">
                        <h2>
                            <asp:Label ID="lblView2" runat="server" Text="Question 3 of 5"></asp:Label></h2>
                        <br />
                        <h3>
                            <asp:Label ID="lblViewQ2" runat="server" Text="Which statement selects all fields from a table?"></asp:Label></h3>
                        <br />
                        <div class="check-list-container">
                            <asp:RadioButtonList ID="chkQuizList2" runat="server">
                            </asp:RadioButtonList>
                        </div>
                    </asp:View>
                     <asp:View runat="server">
                        <h2>
                            <asp:Label ID="lblView3" runat="server" Text="Question 4 of 5"></asp:Label></h2>
                        <br />
                        <h3>
                            <asp:Label ID="lblViewQ3" runat="server" Text="Which statement selects all fields from a table?"></asp:Label></h3>
                        <br />
                        <div class="check-list-container">
                            <asp:RadioButtonList ID="chkQuizList3" runat="server">
                            </asp:RadioButtonList>
                        </div>
                    </asp:View>
                     <asp:View runat="server">
                        <h2>
                            <asp:Label ID="lblView4" runat="server" Text="Question 5 of 5"></asp:Label></h2>
                        <br />
                        <h3>
                            <asp:Label ID="lblViewQ4" runat="server" Text="Which statement selects all fields from a table?"></asp:Label></h3>
                        <br />
                        <div class="check-list-container">
                            <asp:RadioButtonList ID="chkQuizList4" runat="server">
                            </asp:RadioButtonList>
                        </div>
                     </asp:View>
                    <asp:View runat="server">
                        <div style="position:relative; width:75%; height:75%; margin-left:auto; margin-right:auto; top:50px;">
                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="true" ShowHeader="true" CssClass="table table-striped" OnRowDataBound="gvResult_RowDataBound">
                           <Columns>
                              
                           </Columns>
                        </asp:GridView>
                            </div>
                    </asp:View>
                </asp:MultiView>
               <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-default btn-submit" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="valGroup"/>
                <br />
                <br />
                <h3>
                    <asp:RequiredFieldValidator ID="reqField" runat="server" ControlToValidate="chkQuizList0" ErrorMessage="Please make a selection" CssClass="label label-warning" ValidationGroup="valGroup"></asp:RequiredFieldValidator></h3>
            </div>
            <div style="width:auto;">
                <h3> <asp:Label ID="lblNotification" CssClass="label label-success" runat="server"></asp:Label></h3>
            </div>
        </div>
    </div>
</asp:Content>

