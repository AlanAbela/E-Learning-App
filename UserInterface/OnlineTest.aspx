<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/MasterPage.master" AutoEventWireup="true" CodeFile="OnlineTest.aspx.cs" Inherits="UserInterface_OnlineTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id ="page-test">
        <!--Title -->
        <div class="page-header" style="width:100%; text-align: center";>
            <h1>
                <asp:Label ID="lblTestTitle" runat="server" Text="Course test"></asp:Label>
            </h1>
        </div>

        <!-- Body -->
    <div id="test-body">
    <asp:UpdatePanel ID="upTime" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="position: relative; left: 0px; width: 200px; text-align: left;">
                <asp:Label ID="lblTime" runat="server"></asp:Label>
            </div>
            <div style="position: relative; left: 0px; width: 100px; text-align: left; float: left;">
                <asp:Label ID="lblScore" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="timer" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Timer ID="timer" runat="server" Interval="1000" OnTick="Timer_Tick" Enabled=" true"></asp:Timer>


    <div>
    <asp:UpdatePanel ID="upPan" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="question-container">
            <asp:MultiView ID="mvQuestions" runat="server">
               
            </asp:MultiView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
        <asp:Button ID="btnSubmit" runat="server" OnClick="btn_Click" Text="Submit" CssClass="btn btn-default btn-submit" />
    </div>

        </div>
        </div>
</asp:Content>

