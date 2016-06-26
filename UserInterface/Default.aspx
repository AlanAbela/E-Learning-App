<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="UserInterface_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:HiddenField ID="hiddenField" runat="server" ClientIDMode="Static"/>
     
  <div id="defaultContent">
    <div class ="page-header" style="width:100%; text-align:center;">
     <h1><asp:Label ID="lblTitle" runat="server"></asp:Label></h1>
    </div>
      <div>
          <asp:Label ID="lblDesc" runat="server" Text="Welcome to the my SQL learning platfrom."></asp:Label>
     </div>
  </div>
</asp:Content>

