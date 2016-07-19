<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="UserInterface_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

        function lessonRedirect(id)
        {
            window.opener = self;
            window.close();
            window.open("lesson.aspx?ID=" + id);
        }

    </script>
     
  <div id="defaultContent">
    <div class ="page-header" style="width:100%; text-align:center; margin-right:auto; margin-left:auto;">
     <h1><asp:Label ID="lblTitle" runat="server"></asp:Label></h1>
    </div>
      
               <!-- Side menu -->
            <div id="masterSideMenu">
                <h3 style="margin:0px; height:50px;">
                   <asp:Label ID="lblSideMenuTitle" runat="server" CssClass="page-header no-margin"  Text="SQL Lessons"></asp:Label>
                </h3>
                <ul class="nav nav-pills nav-stacked" id="navSideMenu" runat="server" style="width: 100%">
                   
                </ul>               
            </div>
          <!-- Description -->
          <div id="page-default">
              <div>
          <asp:Label ID="lblDesc" runat="server" ><b>Welcome to The SQL learning platform.</b> </br></br> With the help of this 
              web site you will gain SQL knowledge for the most used SQL statements. </br></br> In the video tutorials presented in this web site,
              I am using Microsoft SQL server and Microsoft SQL Server Management Studio. You are not required to use the same or similar tools for your learning
              practices, because an SQL Editor is included for your practices. However to practice more advanced SQL queries that are not listed in this website, 
              you will need to install a Database server as well as an SQL management tool that supports the selected database server.
          </asp:Label>
                  </div>
          </div>
  </div>

</asp:Content>

