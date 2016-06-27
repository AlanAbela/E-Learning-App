<%@ Page Language="C#" MasterPageFile="~/UserInterface/MasterPage.master" AutoEventWireup="true" CodeFile="Topic.aspx.cs" Inherits="UserInterface_Topic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="page-topic">
            <!-- Page Header -->
            <div class="page-header" style="width: 100%; text-align: center;">
                <h1>
                    <asp:Label ID="lblLessonTitle" runat="server" Text="Header test"></asp:Label>
                </h1>
            </div>
            <!-- Page Body -->
            <div id="topic-body">
                <div class="topic-info">
                    <asp:Label ID="lblTopicText" runat="server" Text="Test Text"></asp:Label>
                </div>
             <div class ="video-holder">
                 <div class="spacer"></div>
                    <iframe id="videoSource" runat="server" class="video"></iframe>
                </div>
            </div>
        </div>

    </asp:Content>
