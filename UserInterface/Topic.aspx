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

        // If video panel is hidden slide down. If it is visible slide up.
        function showVideo()
        {
  
            if($("#pnlShowVideo").is(":hidden"))
            {
                $("#pnlShowVideo").slideDown("slow");
                $('#btnShowVideo').val("Hide Video Demo");
            }
            else
            {
                $("#pnlShowVideo").slideUp("slow");
                $('#btnShowVideo').val("Show Video Demo");
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
                    <asp:Label ID="lblTopicText" runat="server"></asp:Label>
                </div>
                <div class="table-holder">
                    <asp:GridView ID="gvTableExample" runat="server" CssClass="table table-striped"></asp:GridView>
                    <asp:GridView ID="gvTableExample2" runat="server" CssClass="table table-striped" OnRowDataBound="gvTableExample2_RowDataBound"></asp:GridView>
                    <asp:GridView ID="gvTableExample3" runat="server" CssClass="table table-striped"></asp:GridView>
                </div>
                <div style="text-align: center;">
                    <asp:Button ID="btnShowVideo" ClientIDMode="Static" runat="server" CssClass="btn-default btn-info" Width="200px" Height="50px" Text="Show Video Demo" Font-Bold="true" OnClientClick="showVideo(); return false;" />
                </div>
                <asp:Panel ID="pnlShowVideo" runat="server" ClientIDMode="Static">
                    <div class="video-holder">
                        <div class="spacer"></div>
                        <iframe id="videoSource" runat="server" class="video"></iframe>
                    </div>
                </asp:Panel>
            </div>
            <!-- Modal window code (source w3school.com) -->
            <div class="spacer"></div>
            <div class="topic-info">
                <asp:Label ID="lblTopicText2" runat="server"></asp:Label>
               
            </div>

            <!-- button to open modal window -->
            <div style="text-align: center; text-align:center">
                <button style="width:200px; height:50px;" type="button" class="btn-default btn-info" data-toggle="modal" data-backdrop="static" data-target="#myModal" data-keyboard="false">Try it out</button>
            </div>
            <!-- modal window -->
            <div id="myModal" class="modal fade">
                <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <asp:Button ID="btnCornerClose" runat="server" CssClass="close" BackColor="White" BorderColor="White" Text="&times;" OnClick="btnClose_Click"/>
                <h2 class="modal-title">Try it out</h2>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="upModalText" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txtTryItOut" runat="server" CssClass="form-control" TextMode="MultiLine" AutoPostBack="true"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSubmit"/>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="modal-footer">
                <div style="text-align: left; width: 50%; float: left;">
                    <h3 class="label-no-top-margin">
                        <asp:UpdatePanel ID="upModalLabel" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblResult" runat="server" Visible="false"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </h3>
                </div>
                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-default" Text="close" OnClick="btnClose_Click"></asp:Button>
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Go" ClientIDMode="Static" OnClick="btnSubmit_Click"></asp:Button>
                <h3 style="text-align:left; clear:left;">Result</h3>
                <div class="table-holder" style="width: 100%;">
                    <asp:UpdatePanel ID="upModalTable" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvResultTable" runat="server" CssClass="table table-striped"></asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</div>
        </div>

    </asp:Content>
