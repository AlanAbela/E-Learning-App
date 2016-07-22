<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/MasterPage.master" AutoEventWireup="true" CodeFile="OnlineTest.aspx.cs" Inherits="UserInterface_OnlineTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="upPan" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:MultiView ID="mv" runat="server" ActiveViewIndex="0">
                <asp:View ID="view1" runat="server">
                    view 1
                </asp:View>
                <asp:View ID="view2" runat="server">
                    view 2
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn"/>
        </Triggers>
    </asp:UpdatePanel>
    <asp:Button ID="btn" runat="server" OnClick="btn_Click" Text="Button"/>


</asp:Content>

