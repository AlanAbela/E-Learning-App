﻿<%@ Page Language="C#" MasterPageFile="~/UserInterface/MasterPage.master" AutoEventWireup="true" CodeFile="Lesson.aspx.cs" Inherits="UserInterface_Lesson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

        function topicRedirect(id)
        {
            window.opener = self;
            window.close();
            window.open("topic.aspx?ID="+id+"&lessonid="+ <%= LessonID.ToString() %>);
        }

        function backClick()
        {
            window.opener = self;
            window.close();
            window.open("Default.aspx");
        }


        $(document).ready(function () {
            //$("ul li").click(function (e) {
            //    window.location.replace("topic.aspx?ID=" + $(this).attr("id"), "_self");
            //    return false;
            //});
        });

    </script>

        <div id="page-lesson">
            <!-- Page Header -->
            <div style="float:left;"><asp:Button ID="btnBack" CssClass="btn btn-default" runat="server" Text="Back" OnClientClick="backClick()"/></div>
            <div class="page-header" style="width: 50%; margin-left:auto; margin-right:auto; text-align: center;">
                <h1>
                    <asp:Label ID="lblLessonTitle" runat="server"></asp:Label>
                </h1>
            </div>
            <!-- Page navigation -->
            <div id="lesson-nav">
                <h3 style="margin: 0px; height: 50px;">
                    <asp:Label ID="lblSideMenuTitle" runat="server" CssClass="page-header no-margin" Text="Topics"></asp:Label>
                </h3>
                <ul class="nav nav-pills nav-stacked" id="navSideMenu" runat="server" style="width: 100%">
                </ul>
            </div>
            <!-- Lesson Content -->
            <div id="lesson-content">
                <asp:Label ID="lblLessonContent" runat="server" Text=" testing"></asp:Label>
                <div class="error-message">
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </div>
            </div>
        </div>
 </asp:Content>
