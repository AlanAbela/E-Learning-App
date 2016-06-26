<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lesson.aspx.cs" Inherits="UserInterface_Lesson" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("ul li").click(function (e) {
                window.location.replace("topic.aspx?ID=" + $(this).attr("id"), "_self");
                return false;
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page-lesson">
            <!-- Page Header -->
            <div class="page-header" style="width: 100%; text-align: center;">
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
    </form>
</body>
</html>
