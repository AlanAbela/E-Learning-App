<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="UserInterface_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

        function lessonRedirect(id)
        {
            window.open("lesson.aspx?ID=" + id);
        }

    </script>
     
  <div id="defaultContent">
    <div class ="page-header" style="width:100%; text-align:center;">
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
          <asp:Label ID="lblDesc" runat="server" Text="Welcome to the my SQL learning platfrom."></asp:Label>
          </div>
  </div>

</asp:Content>

