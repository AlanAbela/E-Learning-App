﻿<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/MasterPage.master" AutoEventWireup="true" CodeFile="Quiz.aspx.cs" Inherits="UserInterface_Quiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
 <%--       function radioMe(e) {
  if (!e) e = window.event;
  var sender = e.target || e.srcElement;

  if (sender.nodeName != 'INPUT') return;
  var checker = sender;
  var chkBox = document.getElementById('<%= hdnField.Value %>');
  var chks = chkBox.getElementsByTagName('INPUT');
  for (i = 0; i < chks.length; i++) {
      if (chks[i] != checker)
      chks[i].checked = false;
  }--%>

<%--  $(document).ready(function () {
     
      var validatorID = '<%= hdnField.Value %>';
      var validator = document.getElementById('reqField');
      validator.ControlToValidate = validatorID;
      
  });--%>

    </script>

    <div id="page-quiz">
        <asp:HiddenField id="hdnField" runat="server" ClientIDMode="Static"/>
        <!-- Title -->
        <div class="page-header" style="width: 100%; text-align: center;">
            <h1>
                <asp:Label ID="lblLessonTitle" runat="server" Text="Header test"></asp:Label>
            </h1>
        </div>
        <!-- Body -->
        <div id="quiz-body">
            <div id="question-container">
                <asp:MultiView ID="mvQuestions" runat="server" ActiveViewIndex="0">
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
                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="false" ShowHeader="true" CssClass="table table-striped">
                            <Columns>
                                <asp:BoundField DataField="Text" HeaderText="Your Selection" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Value" HeaderText="Correct?" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </asp:GridView>
                    </asp:View>
                </asp:MultiView>
               <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-default btn-submit" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="redirect()"/>
                <br />
                <br />
                <h3><asp:RequiredFieldValidator ID="reqField0" runat="server" ControlToValidate= "chkQuizList0" ErrorMessage="Please make a selection" CssClass="label label-warning" ClientIDMode="Static"></asp:RequiredFieldValidator></h3>
            </div>
        </div>
    </div>
</asp:Content>
