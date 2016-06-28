<%@ Page Language="C#" MasterPageFile="~/UserInterface/MasterPage.master" AutoEventWireup="true" CodeFile="Topic.aspx.cs" Inherits="UserInterface_Topic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

        // At load hide video panel.
        $(document).ready(function () {

            $("#pnlShowVideo").hide();

        });

        function backClick(id)
        {
            window.opener = self;
            window.close();
            window.open("lesson.aspx?ID=" + <%= LessonID.ToString() %>);
        }

        function showVideo()
        {
  
            if($("#pnlShowVideo").is(":hidden"))
            {
                $("#pnlShowVideo").slideDown("slow");
            }
            else
            {
                $("#pnlShowVideo").slideUp("slow");
            }

            return false;
        }
    </script>

        <div id="page-topic">
            <!-- Page Header -->
            <div style="float:left;"><asp:Button ID="btnBack" CssClass="btn btn-default" runat="server" Text="Back" OnClientClick="backClick(); return false;"/></div>
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
                <div class="table-holder">
                    <asp:GridView ID="gvTableExample" runat="server" CssClass="table table-striped" OnLoad="gvTableExample_Load"></asp:GridView>
                </div>
              <div style="text-align:center;"><asp:Button ID="btnShowVideo" runat="server" CssClass="btn-default btn-info" Width="200px" Height="50px" Text="Show Video Demo" Font-Bold="true" OnClientClick="showVideo(); return false;" /></div>
                <asp:Panel ID="pnlShowVideo" runat="server" ClientIDMode="Static">
                    <div class="video-holder">
                        <div class="spacer"></div>
                        <iframe id="videoSource" runat="server" class="video"></iframe>
                    </div>
                </asp:Panel>
            </div>
        </div>

    </asp:Content>
